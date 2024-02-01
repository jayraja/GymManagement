using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;

namespace GymManagement.Infrastructure.Gyms.Persistence;

public class GymsRepository : IGymsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public GymsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddGymAsync(Gym gym)
    {
        await _dbContext.AddAsync(gym);
    }
}

