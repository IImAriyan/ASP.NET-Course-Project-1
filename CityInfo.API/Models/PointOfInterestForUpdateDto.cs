using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models;

public class PointOfInterestForUpdateDto
{
    [Required(ErrorMessage = "Name Field Is Required!")]
    [MaxLength(50, ErrorMessage = "Max Length of Name Field is 50")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "Max Length of Description Field is 200")]
    public string? Description { get; set; }
    

}

