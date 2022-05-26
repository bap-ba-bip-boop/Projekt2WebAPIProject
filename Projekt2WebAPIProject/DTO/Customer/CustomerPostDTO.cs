using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.Customer;

public class CustomerPostDTO
{
    [Required(ErrorMessage = "The Customer must have a name")]
    [MaxLength(30, ErrorMessage = "Can't be longer than 30 characters")]
    public string? CustomerName { get; set; }
}
