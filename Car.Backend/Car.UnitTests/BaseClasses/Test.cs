using System.Diagnostics.CodeAnalysis;
using AutoFixture;

namespace Car.UnitTests.BaseClasses
{
    [SuppressMessage(
        "ReSharper",
        "SA1401",
        Justification = @"Fixture is Used in Derived classes, so there is no need to encapsulate the field with private modifier")]
    public class Test
    {
        protected readonly Fixture Fixture;

        protected Test()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}