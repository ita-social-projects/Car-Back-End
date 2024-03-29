﻿using System.Collections.Generic;
using AutoFixture;
using AutoMapper;
using Car.Domain.Mapping;

namespace Car.UnitTests.Base
{
    public class TestBase
    {
        protected Fixture Fixture { get; set; }

        protected IMapper Mapper { get; set; }

        public TestBase()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            Mapper = new Mapper(new MapperConfiguration(options =>
                options.AddProfiles(new List<Profile>
                    {
                        new JourneyMapper(),
                        new CarMapper(),
                        new BrandMapper(),
                        new ModelMapper(),
                        new UserMapper(),
                        new NotificationMapper(),
                        new ChatMapper(),
                        new LocationMapper(),
                        new RequestMapper(),
                        new UserPreferencesMapper(),
                        new JourneyUserMapper(),
                    })));
        }
    }
}
