using Business.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class ContactRegistrationForm : IContactRegistrationForm, IDataErrorInfo, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _firstName;
    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[a-öA-Ö\s'-]{2,}$", ErrorMessage = "First name must have at least 2 valid characters")]
    public string FirstName
    {
        get => _firstName!;
        set
        {
            if (_firstName != value)
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
    }

    private string? _lastName;
    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression(@"^[a-öA-Ö\s'-]{2,}$", ErrorMessage = "Last name must have at least 2 valid characters")]
    public string LastName
    {
        get => _lastName!;
        set
        {
            if (_lastName != value)
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
    }

    private string? _email;
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[^\s@]+@[a-öA-Ö0-9.-]+\.[a-öA-Ö]{2,}$", ErrorMessage = "Enter a valid email address")]
    public string Email
    {
        get => _email!;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }

    private string? _phone;
    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^(\+46|0046|0)?[1-9]\d{1,2}[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$", ErrorMessage = "Enter a valid Swedish phone number")]
    public string Phone
    {
        get => _phone!;
        set
        {
            if (_phone != value)
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
    }

    private string? _streetAddress;
    [Required(ErrorMessage = "Street address is required")]
    [RegularExpression(@"^[a-öA-Ö0-9\s,.'-:]+$", ErrorMessage = "Enter a valid street address")]
    public string StreetAddress
    {
        get => _streetAddress!;
        set
        {
            if (_streetAddress != value)
            {
                _streetAddress = value;
                OnPropertyChanged(nameof(StreetAddress));
            }
        }
    }

    private string? _postalCode;
    [Required(ErrorMessage = "Postal code is required")]
    [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "Enter a valid postal code (e.g., 123 45)")]
    public string PostalCode
    {
        get => _postalCode!;
        set
        {
            if (_postalCode != value)
            {
                _postalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }
    }

    private string? _city;
    [Required(ErrorMessage = "City is required")]
    [RegularExpression(@"^[a-öA-Ö\s-]+$", ErrorMessage = "City name must have only letters")]
    public string City
    {
        get => _city!;
        set
        {
            if (_city != value)
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
    }

    // IDataErrorInfo implementation for WPF validation. I had help creating this from Chat GPT,
    public string Error => null!;

    public string this[string columnName]
    {
        get
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(this) { MemberName = columnName };

            if (!Validator.TryValidateProperty(GetType().GetProperty(columnName)?.GetValue(this), context, validationResults))
            {
                return validationResults.First().ErrorMessage!;
            }

            return string.Empty;
        }
    }
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
