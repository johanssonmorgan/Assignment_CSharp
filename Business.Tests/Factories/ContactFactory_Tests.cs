using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnContactRegistrationForm()
    {
        // Arrange - Set up the necessary conditions, data, and objects required for the test.

        // Act - Execute the method or action that is being tested.
        ContactRegistrationForm result = ContactFactory.Create();

        // Assert - Verify that the expected outcome or result is achieved.
        Assert.NotNull(result);
        Assert.IsType<ContactRegistrationForm>(result);
    }

    [Theory]
    [InlineData("John", "Doe", "john.doe@example.com", "0701234567", "Elm Street 123", "90210", "Beverly Hills")]
    [InlineData("Alice", "Smith", "alice.smith@domain.com", "0319876543", "Oak Avenue 456", "30301", "Gothenburg")]
    [InlineData("Maria", "González", "maria.gonzalez@espanol.com", "0762345678", "Calle Mayor 12", "28013", "Madrid")]
    public void Create_ShouldReturnContact_WhenContactRegistrationFormIsSupplied(string firstName, string lastName, string email, string phone, string streetAddress, string postalCode, string city)
    {
        // Arrange - Set up the necessary conditions, data, and objects required for the test.
        ContactRegistrationForm contactRegistrationForm = new() { FirstName = firstName, LastName = lastName, Email = email, Phone = phone, StreetAddress =streetAddress, PostalCode = postalCode, City = city};

        // Act - Execute the method or action that is being tested.
        Contact result = ContactFactory.Create(contactRegistrationForm);

        // Assert - Verify that the expected outcome or result is achieved.
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
        Assert.Equal(contactRegistrationForm.FirstName, result.FirstName);
        Assert.Equal(contactRegistrationForm.LastName, result.LastName);
        Assert.Equal(contactRegistrationForm.Email, result.Email);
        Assert.Equal(contactRegistrationForm.Phone, result.Phone);
        Assert.Equal(contactRegistrationForm.StreetAddress, result.StreetAddress);
        Assert.Equal(contactRegistrationForm.PostalCode, result.PostalCode);
        Assert.Equal(contactRegistrationForm.City, result.City);
        Assert.False(string.IsNullOrWhiteSpace(result.Id));
        Assert.Equal(8, result.Id.Length);
    }
}
