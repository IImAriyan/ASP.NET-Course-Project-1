using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repositories;

public class CityInfoRepository : ICityInfoRepository
{
    private readonly CityInfoDbContext _context;

    public CityInfoRepository(CityInfoDbContext context)
    {
        _context = context ?? throw new ArgumentException(nameof(context));
    }
    
    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities
            .OrderBy(c=>c.Name).ToListAsync();
    }

    public async Task<City?> GetCityAsync(int cityId, bool includePointOfInterest)
    {
        if (includePointOfInterest == true)
        {
            return await _context.Cities.Include(c=>c.PointOfInterest)
                .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
    }

    public async Task<bool> CityExistAsync(int cityId)
    {
        return await _context.Cities.AnyAsync(c=>c.Id == cityId);
    }


    public async Task<PointOfInterest?> GetPointsOfInterestForCityAsync(
        int cityId,
        int pointOfInterestId)
    {
        return await _context.PointOfInterest
            .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
    {
        return await _context.PointOfInterest
            .Where(p => p.CityId == cityId )
            .ToListAsync();
    }

    public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
    {
        var city = await GetCityAsync(cityId,true);
        if (city != null)
        {
         city.PointOfInterest.Add(pointOfInterest);   
        }
        
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
    
}