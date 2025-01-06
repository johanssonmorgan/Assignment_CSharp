using Business.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class UserRegistrationForm : IUserRegistrationForm
{
    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[a-öA-Ö\s'-]{2,}$", ErrorMessage = "First name must have at least 2 valid characters")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression(@"^[a-öA-Ö\s'-]{2,}$", ErrorMessage = "Last name must have at least 2 valid characters")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[^\s@]+@[a-öA-Ö0-9.-]+\.[a-öA-Ö]{2,}$", ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^(\+46|0046|0)?[1-9]\d{1,2}[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$", ErrorMessage = "Enter a valid Swedish phone number")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Street address is required")]
    [RegularExpression(@"^[a-öA-Ö0-9\s,.'-:]+$", ErrorMessage = "Enter a valid street address")]
    public string StreetAddress { get; set; } = null!;

    [Required(ErrorMessage = "Postal code is required")]
    [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "Enter a valid postal code (e.g., 123 45)")]
    public string PostalCode { get; set; } = null!;

    [Required(ErrorMessage = "City is required")]
    [RegularExpression(@"^[a-öA-Ö\s-]+$", ErrorMessage = "City name must have only letters")]
    public string City { get; set; } = null!;
}