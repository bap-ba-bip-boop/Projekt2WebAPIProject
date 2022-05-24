using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.Customer;

public class CustomerPostDTO
{
    [MaxLength(30)]
    public string? CustomerName { get; set; }
}
