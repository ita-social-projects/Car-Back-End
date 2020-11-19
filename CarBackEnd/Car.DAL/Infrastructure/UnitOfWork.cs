using Car.DAL.Context;
using Car.DAL.Interfaces;
using Car.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Car.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private CarContext context;
        private BaseRepository<User> userRepository;

        public UnitOfWork(CarContext _context)
        {
            context = _context;
        }
        public DbContext db { get => context; }
        public BaseRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new BaseRepository<User>(this);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
