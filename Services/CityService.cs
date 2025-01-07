using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.Models.Dtos.City;
using TagerProject.Models.Entities;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;


        public CityService(ApplicationDbContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CityResponse>> GetAllCity()
        {
            var cities = await _dbContext.Cities.ToListAsync();

            return _mapper.Map<List<CityResponse>>(cities);
        }

        public async Task<CityResponse?> GetCityById(int id)
        {
            var city = await _dbContext.Cities.FindAsync(id);
            return _mapper.Map<CityResponse>(city);
        }

        public async Task<CityResponse?> AddCity(CityAddRequest cityAddRequest)
        {
            City city = new City() 
            {
                NameAr = cityAddRequest.NameAr,
                NameEn = cityAddRequest.NameEn,
            };

            await _dbContext.AddAsync(city);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CityResponse>(city);
        }
        public async Task<CityResponse?> UpdateCity(int id, CityUpdateRequest cityUpdateRequest)
        {
            var city = await _dbContext.Cities.FindAsync(id);

            if (city is null) 
            {
                return null;
            }

            city.NameAr = cityUpdateRequest.NameAr;
            city.NameEn = cityUpdateRequest.NameEn;
            city.IsActive = cityUpdateRequest.IsActive;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CityResponse>(city);
        }

        public async Task<CityResponse?> DeleteCity(int id)
        {
            var city = await _dbContext.Cities.FindAsync(id);

            if (city is null)
            {
                return null;
            }

            _dbContext.Remove(city);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CityResponse>(city);
        }

    }
}
