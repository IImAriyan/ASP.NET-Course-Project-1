using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/Cities")]
public class CitiesController: ControllerBase
{
    private readonly ICityInfoRepository _cityInfoRepository;
    private readonly IMapper _mapper;
    
    
    public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
    {
        _cityInfoRepository = cityInfoRepository ??
                              throw new ArgumentException(nameof(cityInfoRepository));

        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
    {

        var Cities = await _cityInfoRepository.GetCitiesAsync();

        return Ok(
            _mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(Cities)
            );

    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> 
        GetCity(int id, bool includePointOfInterest = false)
    {
        var city = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);
        if (city == null)
        {
            return NotFound();
        }
        
        if (includePointOfInterest)
        {
            return Ok(_mapper.Map<CityDto>(city));
        }
        return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));
    }
}