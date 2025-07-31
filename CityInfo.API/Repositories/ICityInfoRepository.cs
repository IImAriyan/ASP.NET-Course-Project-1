using CityInfo.API.Entities;

namespace CityInfo.API.Repositories;

public interface ICityInfoRepository
{

    Task<IEnumerable<City>> GetCitiesAsync();
    Task<City?> GetCityAsync(int cityId, bool includePointOfInterest);
    Task<bool> CityExistAsync(int cityId);
    Task<PointOfInterest?> GetPointsOfInterestForCityAsync(
        int cityId, int pointOfInterestId);
    Task<IEnumerable<PointOfInterest>> 
        GetPointsOfInterestForCityAsync(int cityId);
    Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
    
    void DeletePointOfInterest(PointOfInterest pointOfInterest);
    
    Task<bool> SaveChangesAsync();
    
    
}