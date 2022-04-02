using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Models.Stops;
using Car.UnitTests.Extensions;

namespace Car.UnitTests.Base.Customizations
{
    public class GenerateValidGeoCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Address>(composer =>
                composer
                .With(a => a.Latitude, fixture.CreateInRange<double>(-90, 90))
                .With(a => a.Longitude, fixture.CreateInRange<double>(-180, 180)));

            fixture.Customize<JourneyPoint>(composer =>
                composer
                .With(jp => jp.Latitude, fixture.CreateInRange<double>(-90, 90))
                .With(jp => jp.Longitude, fixture.CreateInRange<double>(-180, 180)));

            fixture.Customize<AddressModel>(composer =>
                composer
                .With(am => am.Latitude, fixture.CreateInRange<double>(-90, 90))
                .With(am => am.Longitude, fixture.CreateInRange<double>(-180, 180)));
        }
    }
}
