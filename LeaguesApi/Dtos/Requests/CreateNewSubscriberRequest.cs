using System.ComponentModel.DataAnnotations;

namespace LeaguesApi.Dtos.Requests;

public class CreateNewSubscriberRequest
{
    [Required]
    [MinLength(5, ErrorMessage= "Name should be more than 5 chars")]
    [MaxLength(45,ErrorMessage = "Name should be less than 45 chars")]
    public string Name { get; set; }
}