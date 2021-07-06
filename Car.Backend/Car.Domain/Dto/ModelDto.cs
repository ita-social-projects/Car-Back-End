using Car.Data.Entities;

namespace Car.Domain.Dto
{
    public class ModelDto
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public string Name { get; set; } = string.Empty;

        public BrandDto? Brand { get; set; }
    }
}
