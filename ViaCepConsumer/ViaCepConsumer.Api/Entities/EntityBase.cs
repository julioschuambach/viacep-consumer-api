namespace ViaCepConsumer.Api.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; private set; }

        public EntityBase()
            => Id = Guid.NewGuid();
    }
}
