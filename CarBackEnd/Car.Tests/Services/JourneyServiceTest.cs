using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Moq;
using Xunit;

namespace Car.Tests.Services
{
    public class JourneyServiceTest
    {
        private IJourneyService journeyService;

        private Mock<IUnitOfWork<Journey>> unitOfWork;

        private Mock<IRepository<Journey>> repository;

        public JourneyServiceTest()
        {
            repository = new Mock<IRepository<Journey>>();
            unitOfWork = new Mock<IUnitOfWork<Journey>>();

            journeyService = new JourneyService(unitOfWork.Object);
        }

        public Journey GetTestJourney()
        {
            return new Journey()
            {
                Id = 1,
                Comments = "Some comments",
                CountOfSeats = 5,
                IsFree = true,
            };
        }

        [Fact]
        public void TestGetJourneyById_WhenJourneyExists()
        {
            var journey = GetTestJourney();

            repository.Setup(repo => repo.GetById(journey.Id)).Returns(journey);
            unitOfWork.Setup(unit => unit.GetRepository()).Returns(repository.Object);

            var resultJourney = journeyService.GetJourneyById(journey.Id);

            Assert.Equal(journey, resultJourney);
        }

        [Fact]
        public void TestGetJourneyById_WhenJourneyNotExist()
        {
            var journey = GetTestJourney();

            repository.Setup(repo => repo.GetById(journey.Id)).Returns(journey);
            unitOfWork.Setup(unit => unit.GetRepository()).Returns(repository.Object);

            var resultJourney = journeyService.GetJourneyById(journey.Id + 1);

            Assert.Null(resultJourney);
        }
    }
}
