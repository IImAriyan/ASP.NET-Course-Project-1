using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Repositories;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;
[Route("api/cities/{cityId}/pointsofinterest")]
[ApiController]
public class PointsOfInterestController : ControllerBase
{
    private readonly ILogger<PointsOfInterestController> _logger;
    private readonly IMailService _localMailService;
    private readonly CitiesDataStore citiesDataStore;
    private readonly ICityInfoRepository _cityInfoRepository;
    private readonly IMapper _mapper;

    
    #region Constructor

    public PointsOfInterestController(
        ILogger<PointsOfInterestController> logger,
        IMailService localMailService,
        CitiesDataStore citiesDataStore,
        ICityInfoRepository cityInfoRepository,
        IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _localMailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
        this.citiesDataStore = citiesDataStore;
        _cityInfoRepository = cityInfoRepository ??
                              throw new ArgumentException(nameof(cityInfoRepository));
        _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
    }

    #endregion
    
    #region Get Point Of Interest With City ID
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
    {
        if (!await _cityInfoRepository.CityExistAsync(cityId))
        {
            _logger.LogInformation($"{cityId} Not Found  .....!");
            return NotFound();
        }
        
        var pointsOfInterestForCity = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);

        return Ok(_mapper
            .Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
    }
    
    #endregion


    #region Get Point Of Interest With ID
    // Get Point Of Interest With ID
    [HttpGet("{pointOfInterestId}",Name ="GetPointOfInterest")]
    public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(
        int cityId,
        int pointOfInterestId)
    {
        if (!await _cityInfoRepository.CityExistAsync(cityId))
        {
            return NotFound();
        }

        var pointOfInterest = await _cityInfoRepository
            .GetPointsOfInterestForCityAsync(cityId, pointOfInterestId);

        
        // If point of interest is null, return NotFoundگ
        
        if (pointOfInterest == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
    }
    
    
    #endregion
    
    #region Post

    [HttpPost]
    public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
        int cityId,
        PointOfInterestForCreationDto pointOfInterest
    )
    {
        if (!await _cityInfoRepository.CityExistAsync(cityId))
        {
            return NotFound();
        }

        var finalPoint = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);
        await _cityInfoRepository
            .AddPointOfInterestForCityAsync(
                cityId,finalPoint);

        await _cityInfoRepository.SaveChangesAsync();

        var createdpoint = _mapper.Map<Models.PointOfInterestDto>(finalPoint);
        
        return CreatedAtRoute("GetPointOfInterest",new
        {
            cityId = cityId,
            pointOfInterestId = createdpoint.Id
            
        },createdpoint);
    }
    #endregion
    
    #region Edit

    [HttpPut("{pontiOfInterestId}")]
    public async Task<ActionResult> UpdatePointOfInterest(int cityId,
        int pontiOfInterestId,
        PointOfInterestForUpdateDto pointOfInterest)
    {
        
        if(!await _cityInfoRepository.CityExistAsync(cityId))
        {
            return NotFound();
        }

        var point = await _cityInfoRepository
            .GetPointsOfInterestForCityAsync(cityId,pontiOfInterestId);
           
        if(point == null)
        {
            return NotFound();
        }

        _mapper.Map(pointOfInterest, point);

        await _cityInfoRepository.SaveChangesAsync();

        return NoContent();

    }
    #endregion

    #region Edit with patch
    [HttpPatch("{pointOfInterestId}")]
    public async Task<ActionResult> PartiallyUpdatePointOfInterest(
        int cityId,
        int pointOfInterestId,
        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
            )
    {
        if (!await _cityInfoRepository.CityExistAsync(cityId))
        {
            return NotFound();
        }
        var pointEntity = await _cityInfoRepository
            .GetPointsOfInterestForCityAsync(cityId, pointOfInterestId);
        if (pointEntity == null)
        {
            return NotFound();
        }
        var pointToPatch = _mapper.Map<PointOfInterestForUpdateDto>
            (pointEntity);
        patchDocument.ApplyTo(pointToPatch, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!TryValidateModel(pointToPatch))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(pointToPatch, pointEntity);
        await _cityInfoRepository.SaveChangesAsync();
        
        return NoContent();
    }
    #endregion
    
    #region Delete
    

    [HttpDelete("{pointOfInterestId}")]
    public async Task<ActionResult> DeletePointOfInterest(
        int cityId,
        int pointOfInterestId)
    {
        if (!await _cityInfoRepository.CityExistAsync(cityId))
        {
            return NotFound();
        }

        var pointOfInterestEntity =
            await _cityInfoRepository
                .GetPointsOfInterestForCityAsync(cityId, pointOfInterestId);

        if (pointOfInterestEntity == null)
        {
            return NotFound();
        }
        
        
        _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
        await _cityInfoRepository.SaveChangesAsync();
        
        _localMailService
            .Send(
                "Point Of Interest deleted",
                $"Point Of Interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} "
                );
        return NoContent();
    }
    #endregion
}