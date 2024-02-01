using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GymManagement.Domain.Gyms;

namespace GymManagement.Infrastructure.Gyms.Persistence
{
    public class GymConfigurations : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .ValueGeneratedNever();
            
            builder.Property(g => g.Name);

            builder.Property(g => g.SubscriptionId);
        }
    }
}
