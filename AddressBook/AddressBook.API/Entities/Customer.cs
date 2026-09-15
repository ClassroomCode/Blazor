using System.ComponentModel.DataAnnotations;

public class Customer : IValidatableObject
{
    [Length(5, 5, ErrorMessage = "ID must be 5 characters")]
    public string CustomerID { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? Fax { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
        
        if (CompanyName.StartsWith('x')) {
            yield return new ValidationResult(
                "Company name cannot start with x",
                [nameof(CompanyName)]);
        }

        if (ContactName?.StartsWith('x') == true) {
            yield return new ValidationResult(
                "Contact name cannot start with x",
                [nameof(ContactName)]);
        }
    }
}
