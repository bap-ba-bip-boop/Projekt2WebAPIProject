using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.Infrastructure.Validation;

public class ValidIdAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var lowerIdLimit = 0;
        var IdToValidate = (int)value!;
        return IdToValidate <= lowerIdLimit ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
    }
}