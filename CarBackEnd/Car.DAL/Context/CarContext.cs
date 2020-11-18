using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Car.DAL.Context
{
    class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options) { }
    }
}
