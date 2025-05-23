﻿using GymManagement.Application.Gyms.Commands.AddTrainer;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Commands.DeleteGym;
using GymManagement.Application.Gyms.Queries.GetGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ControllerBase
{
    private readonly ISender _mediator;

    public GymsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGym(CreateGymRequest request, Guid subscriptionId)
    {
        var command = new CreateGymCommand(request.Name, subscriptionId);

        var createGymResult = await _mediator.Send(command);

        return createGymResult.Match(
            gym => CreatedAtAction(
                nameof(GetGym),
                new { subscriptionId, GymId = gym.Id },
                new GymResponse(gym.Id, gym.Name)),
            _ => Problem(createGymResult.FirstError.ToString()));
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid gymId, Guid subscriptionId)
    {
        var command = new GetGymQuery(subscriptionId, gymId);

        var gymResult = await _mediator.Send(command);

        return gymResult.Match(
            gym => Ok(new GymResponse(gym.Id, gym.Name)), 
            _ => Problem());
    }

    [HttpDelete("{gymId:guid}")]
    public async Task<IActionResult> DeleteGym(Guid gymId, Guid subscriptionId)
    {
        var command = new DeleteGymCommand(subscriptionId, gymId);

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            _ => Problem(result.FirstError.ToString()));
    }

    [HttpGet()]
    public async Task<IActionResult> ListGyms(Guid subscriptionId)
    {
        var command = new ListGymsQuery(subscriptionId);

        var gymsResult = await _mediator.Send(command);

        return gymsResult.Match(
            gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
            _ => Problem());
    }

    [HttpPost("{gymId:guid}/trainers")]
    public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid subscriptionId, Guid gymId)
    {
        var command = new AddTrainerCommand(subscriptionId, gymId, request.TrainerId);

        var addTrainerResult = await _mediator.Send(command);

        return addTrainerResult.MatchFirst<IActionResult>(
            success => Ok(),
            error => Problem());
    }
}