using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.Customer;

public class CustomerPutDTO
{
    [MaxLength(30)]
    public string? Name { get; set; }
}
