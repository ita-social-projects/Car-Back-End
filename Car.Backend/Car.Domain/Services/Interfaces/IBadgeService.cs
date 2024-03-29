﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IBadgeService
    {
        Task UpdateStatisticsAsync();

        Task<UserStatistic> GetUserStatisticByUserIdAsync(int userId);

        Task<UserStatistic> GetUserStatistic();
    }
}
