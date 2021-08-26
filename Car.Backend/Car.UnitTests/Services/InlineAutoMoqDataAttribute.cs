using AutoFixture.Xunit2;
using Car.UnitTests.Base;

namespace Car.UnitTests.Services
{
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] objects)
            : base(new AutoEntityDataAttribute(), objects)
        {
        }
    }
}