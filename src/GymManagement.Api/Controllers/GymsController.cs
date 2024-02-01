using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Contracts.Gyms;
using GymManagement.Domain.Gyms;
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
            _ => Problem());
    }

    [HttpGet]
    public async Task<ActionResult<Gym>> GetGym(Guid id, Guid subscriptionId)
    {
        throw new NotImplementedException();
    }
}