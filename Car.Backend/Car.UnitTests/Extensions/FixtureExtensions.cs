using AutoFixture;

namespace Car.UnitTests.Extensions
{
    public static class FixtureExtensions
    {
        public static T CreateInRange<T>(this IFixture fixture, T min, T max)
            where T : struct
        {
            return (fixture.Create<T>() % ((dynamic)max - (dynamic)min + 1)) + min;
        }
    }
}
