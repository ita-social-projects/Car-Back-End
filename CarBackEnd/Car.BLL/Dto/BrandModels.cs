using System.Collections.Generic;

namespace Car.BLL.Dto
{
    public class BrandModels
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Modell> Models { get; set; }
    }
}
