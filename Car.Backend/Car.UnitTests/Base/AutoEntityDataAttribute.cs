using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Car.UnitTests.Base
{
    #pragma warning disable CS0618
    public class AutoEntityDataAttribute : AutoDataAttribute
    {
        public AutoEntityDataAttribute()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
    #pragma warning restore CS0618
}
