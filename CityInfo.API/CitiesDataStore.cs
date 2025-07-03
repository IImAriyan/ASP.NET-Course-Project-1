using CityInfo.API.Models;

namespace CityInfo.API;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }


    //public static CitiesDataStore current { get; } = new CitiesDataStore();
    
    public CitiesDataStore()
    {
        Cities = new List<CityDto>()
        {
            new CityDto() { Id = 1, Name = "Tehran",
                Description = "This is Tehran",PointOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Jaye Didanie 1",
                        Description = "This Is Jaye Didanie 1"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Jaye Didanie 2",
                        Description = "This Is Jaye Didanie 2"
                    },
                }
            },
            new CityDto() { Id = 2, Name = "Shiraz",
                Description = "This is Shiraz",PointOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 3,
                        Name = "Jaye Didanie 3",
                        Description = "This Is Jaye Didanie 3"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 4,
                        Name = "Jaye Didanie 4",
                        Description = "This Is Jaye Didanie 4"
                    },
                } },


            new CityDto() { Id = 3, Name = "Sirjan",
                Description = "This is My City",PointOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 5,
                        Name = "Jaye Didanie 5",
                        Description = "This Is Jaye Didanie 5"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 6,
                        Name = "Jaye Didanie 6",
                        Description = "This Is Jaye Didanie 6"
                    },
                } },
                
            
        };
    }
    
}