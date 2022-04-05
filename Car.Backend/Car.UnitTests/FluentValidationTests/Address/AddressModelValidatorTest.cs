using AutoFixture;
using Car.Domain.FluentValidation.Address;
using Car.Domain.Models.Stops;
using Car.UnitTests.Base;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Address
{
    public class AddressModelValidatorTest : TestBase
    {
        private readonly AddressModelValidator validator;

        public AddressModelValidatorTest()
        {
            validator = new AddressModelValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Name_NameIsEmpty_GeneratesValidationError(string name)
        {
            var addressModel = Fixture.Build<AddressModel>()
                .OmitAutoProperties()
                .With(a => a.Name, name)
                .Create();

            validator.ShouldHaveValidationErrorFor(a => a.Name, addressModel);
        }

        [Theory]
        [AutoEntityData]
        public void Name_NameIsNotEmpty_ReturnsSuccessfullResult(string name)
        {
            var addressModel = Fixture.Build<AddressModel>()
                .OmitAutoProperties()
                .With(a => a.Name, name)
                .Create();

            validator.ShouldNotHaveValidationErrorFor(a => a.Name, addressModel);
        }

        [Theory]
        [InlineData(-91)]
        [InlineData(91)]
        public void Latitude_LatitudeIsNotValid_GeneratesValidationError(double latitude)
        {
            var addressModel = Fixture.Build<AddressModel>()
                .OmitAutoProperties()
                .With(a => a.Latitude, latitude)
                .Create();

            validator.ShouldHaveValidationErrorFor(a => a.Latitude, addressModel);
        }

        [Theory]
        [InlineData(-90)]
        [InlineData(90)]
        [InlineData(-80)]
        [InlineData(80)]
        public void Latitude_LatitudeIsValid_ReturnsSuccessfullResult(double latitude)
        {
            var addressModel = Fixture.Build<AddressModel>()
                .OmitAutoProperties()
                .With(a => a.Latitude, latitude)
                .Create();

            validator.ShouldNotHaveValidationErrorFor(a => a.Latitude, addressModel);
        }

        [Theory]
        [InlineData(-181)]
        [InlineData(181)]
        public void Longitude_LongitudeIsNotValid_GeneratesValidationError(double longitude)
        {
            var addressModel = Fixture.Build<AddressModel>()
                .OmitAutoProperties()
                .With(a => a.Longitude, longitude)
                .Create();

            validator.ShouldHaveValidationErrorFor(a => a.Longitude, addressModel);
        }

        [Theory]
        [InlineData(-180)]
        [InlineData(180)]
        [InlineData(-170)]
        [InlineData(170)]
        public void Longitude_LongitudeIsValid_ReturnsSuccessfullResult(double longitude)
        {
            var addressModel = Fixture.Build<AddressModel>()
                .OmitAutoProperties()
                .With(a => a.Longitude, longitude)
                .Create();

            validator.ShouldNotHaveValidationErrorFor(a => a.Longitude, addressModel);
        }
    }
}
