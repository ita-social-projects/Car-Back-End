using Car.DAL.Context;
using Car.DAL.Interfaces;
using Car.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Car.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private CarContext context;
        private BaseRepository<Address> addressRepository;
        private BaseRepository<Entities.Car> carRepository;
        private BaseRepository<Journey> journeyRepository;
        private BaseRepository<Message> messageRepository;
        private BaseRepository<Notification> notificationRepository;
        private BaseRepository<Schedule> scheduleRepository;
        private BaseRepository<Stop> stopRepository;
        private BaseRepository<User> userRepository;
        private BaseRepository<UserJourney> userJourneyRepository;
        private BaseRepository<UserPreferences> userPreferencesRepository;
        public UnitOfWork(CarContext _context)
        {
            context = _context;
        }
        public DbContext db { get => context; }
        public BaseRepository<Address> AddressRepository
        {
            get
            {
                if (this.addressRepository == null)
                {
                    this.addressRepository = new BaseRepository<Address>(context);
                }
                return addressRepository;
            }
        }
        public BaseRepository<Entities.Car> CarRepository
        {
            get
            {
                if (this.carRepository == null)
                {
                    this.carRepository = new BaseRepository<Entities.Car>(context);
                }
                return carRepository;
            }
        }
        public BaseRepository<Journey> JourneyRepository
        {
            get
            {
                if (this.journeyRepository == null)
                {
                    this.journeyRepository = new BaseRepository<Journey>(context);
                }
                return journeyRepository;
            }
        }
        public BaseRepository<Message> MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                {
                    this.messageRepository = new BaseRepository<Message>(context);
                }
                return messageRepository;
            }
        }
        public BaseRepository<Notification> NotificationRepository
        {
            get
            {
                if (this.notificationRepository == null)
                {
                    this.notificationRepository = new BaseRepository<Notification>(context);
                }
                return notificationRepository;
            }
        }
        public BaseRepository<Schedule> ScheduleRepository
        {
            get
            {
                if (this.scheduleRepository == null)
                {
                    this.scheduleRepository = new BaseRepository<Schedule>(context);
                }
                return scheduleRepository;
            }
        }
        public BaseRepository<Stop> StopRepository
        {
            get
            {
                if (this.stopRepository == null)
                {
                    this.stopRepository = new BaseRepository<Stop>(context);
                }
                return stopRepository;
            }
        }
        public BaseRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new BaseRepository<User>(context);
                }
                return userRepository;
            }
        }
        public BaseRepository<UserJourney> UserJourneyRepository
        {
            get
            {
                if (this.userJourneyRepository == null)
                {
                    this.userJourneyRepository = new BaseRepository<UserJourney>(context);
                }
                return userJourneyRepository;
            }
        }
        public BaseRepository<UserPreferences> UserPreferencesRepository
        {
            get
            {
                if (this.userPreferencesRepository == null)
                {
                    this.userPreferencesRepository = new BaseRepository<UserPreferences>(context);
                }
                return userPreferencesRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
