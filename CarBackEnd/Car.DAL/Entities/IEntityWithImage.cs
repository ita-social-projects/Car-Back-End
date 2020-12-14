namespace Car.DAL.Entities
{
    public interface IEntityWithImage : IEntity
    {
        public string ImageId { get; set; }
    }
}
