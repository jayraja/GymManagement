namespace GymManagement.Domain.Gyms
{
    public class Gym
    {
        public Guid Id { get; }
        public string Name { get; init; } = null!;
        public Guid SubscriptionId { get; init; }
        
        public Gym(string name, Guid subscriptionId, Guid? id = null)
        {
            Name = name;
            SubscriptionId = subscriptionId;
            Id = id ?? Guid.NewGuid();
        }

        private Gym() { }
    }
}
