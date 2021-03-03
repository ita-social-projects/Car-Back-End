using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> brandRepository;

        public BrandService(IRepository<Brand> brandRepository) =>
            this.brandRepository = brandRepository;

        public Task<List<Brand>> GetAllAsync() =>
            brandRepository.Query().ToListAsync();
    }
}
