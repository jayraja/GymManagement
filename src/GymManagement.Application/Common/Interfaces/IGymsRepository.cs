using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);
}

