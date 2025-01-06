using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class UserFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnUserRegistrationForm()
    {
        // Arrange - Set up the necessary conditions, data, and objects required for the test.

        // Act - Execute the method or action that is being tested.
        UserRegistrationForm result = UserFactory.Create();

        // Assert - Verify that the expected outcome or result is achieved.
        Assert.IsType<UserRegistrationForm>(result);
    }

    [Theory]
    [InlineData("John", "Doe", "john.doe@example.com", "0701234567", "Elm Street 123", "90210", "Beverly Hills")]
    [InlineData("Alice", "Smith", "alice.smith@domain.com", "0319876543", "Oak Avenue 456", "30301", "Gothenburg")]
    [InlineData("Maria", "González", "maria.gonzalez@espanol.com", "0762345678", "Calle Mayor 12", "28013", "Madrid")]
    public void Create_ShouldReturnUser_WhenUserRegistrationFormIsSupplied(string firstName, string lastName, string email, string phone, string streetAddress, string postalCode, string city)
    {
        // Arrange - Set up the necessary conditions, data, and objects required for the test.
        UserRegistrationForm userRegistrationForm = new() { FirstName = firstName, LastName = lastName, Email = email, Phone = phone, StreetAddress =streetAddress, PostalCode = postalCode, City = city};

        // Act - Execute the method or action that is being tested.
        User result = UserFactory.Create(userRegistrationForm);

        // Assert - Verify that the expected outcome or result is achieved.
        Assert.IsType<User>(result);
        Assert.Equal(userRegistrationForm.FirstName, result.FirstName);
        Assert.Equal(userRegistrationForm.LastName, result.LastName);
        Assert.Equal(userRegistrationForm.Email, result.Email);
        Assert.Equal(userRegistrationForm.Phone, result.Phone);
        Assert.Equal(userRegistrationForm.StreetAddress, result.StreetAddress);
        Assert.Equal(userRegistrationForm.PostalCode, result.PostalCode);
        Assert.Equal(userRegistrationForm.City, result.City);
        Assert.False(string.IsNullOrWhiteSpace(result.Id));
        Assert.True(result.Id.Length == 8);
    }
}
