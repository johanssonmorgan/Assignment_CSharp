using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _contactService = new ContactService(_fileServiceMock.Object);
    }

    [Theory]
    [InlineData("John", "Doe", "john.doe@example.com", "0701234567", "Elm Street 123", "90210", "Beverly Hills")]
    [InlineData("Alice", "Smith", "alice.smith@domain.com", "0319876543", "Oak Avenue 456", "30301", "Gothenburg")]
    [InlineData("Maria", "González", "maria.gonzalez@espanol.com", "0762345678", "Calle Mayor 12", "28013", "Madrid")]
    public void AddContact_ShouldReturnTrue_AddContactToListAndSaveToFile(string firstName, string lastName, string email, string phone, string streetAddress, string postalCode, string city)
    {
        // Arrange
        var contactRegistrationForm = new ContactRegistrationForm()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            StreetAddress = streetAddress,
            PostalCode = postalCode,
            City = city
        };

        _fileServiceMock.Setup(fs => fs.SaveListToFile(It.IsAny<List<Contact>>())).Returns(true);

        // Act
        var result = _contactService.AddContact(contactRegistrationForm);

        // Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.SaveListToFile(It.IsAny<List<Contact>>()), Times.Once);
    }


    [Theory]
    [InlineData("1", "John", "Doe", "john.doe@example.com", "0701234567", "Elm Street 123", "90210", "Beverly Hills")]
    [InlineData("2", "Alice", "Smith", "alice.smith@domain.com", "0319876543", "Oak Avenue 456", "30301", "Gothenburg")]
    [InlineData("3", "Maria", "González", "maria.gonzalez@espanol.com", "0762345678", "Calle Mayor 12", "28013", "Madrid")]
    public void GetContacts_ShouldReturnListOfContacts(string id, string firstName, string lastName, string email, string phone, string streetAddress, string postalCode, string city)
    {
        // Arrange
        List<Contact> expected = [new Contact { Id = id, FirstName = firstName, LastName = lastName, Email = email, Phone = phone, StreetAddress = streetAddress, PostalCode = postalCode, City = city }];
        
        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(expected);

        // Act
        var result = _contactService.GetContacts();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(expected[0].Id, result.First().Id);
        Assert.Equal(expected[0].FirstName, result.First().FirstName);
        Assert.Equal(expected[0].LastName, result.First().LastName);
        Assert.Equal(expected[0].Email, result.First().Email);
        Assert.Equal(expected[0].Phone, result.First().Phone);
        Assert.Equal(expected[0].StreetAddress, result.First().StreetAddress);
        Assert.Equal(expected[0].PostalCode, result.First().PostalCode);
        Assert.Equal(expected[0].City, result.First().City);
    }

    [Fact]
    public void DeleteContact_ShouldRemoveContactAndReturnTrue()
    {
        // Arrange
        Contact contact1 = new()
        {
            Id = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "0701234567",
            StreetAddress = "Elm Street 123",
            PostalCode = "90210",
            City = "Beverly Hills"
        };

        Contact contact2 = new()
        {
            Id = "2",
            FirstName = "Alice",
            LastName = "Smith",
            Email = "alice.smith@domain.com",
            Phone = "0319876543",
            StreetAddress = "Oak Avenue 456",
            PostalCode = "30301",
            City = "Gothenburg"
        };

        List<Contact> contacts = [contact1, contact2];

        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(contacts);

        // Act
        var result = _contactService.DeleteContact("1");

        // Assert

        Assert.True(result);
        Assert.Single(contacts);
        Assert.DoesNotContain(contact1, contacts);
        _fileServiceMock.Verify(fs => fs.SaveListToFile(It.Is<List<Contact>>(contacts => contacts.Count == 1 && contacts[0].Id == "2")), Times.Once);
    }

    [Fact]
    public void UpdateContact_ShouldUpdateContactAndReturnTrue()
    {
        // Arrange
        Contact contact = new()
        {
            Id = "1",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "0701234567",
            StreetAddress = "Elm Street 123",
            PostalCode = "90210",
            City = "Beverly Hills"
        };

        Contact updatedContact = new()
        {
            Id = "1",
            FirstName = "Morgan",
            LastName = "Johansson",
            Email = "john.doe@example.com",
            Phone = "0701234567",
            StreetAddress = "Elm Street 123",
            PostalCode = "90210",
            City = "Beverly Hills"
        };

        List<Contact> contacts = [contact];
        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns(contacts);

        // Act
        var result = _contactService.UpdateContact("1", updatedContact);

        // Assert
        Assert.True(result);
        Assert.Equal("Morgan", contacts[0].FirstName);
        Assert.Equal("Johansson", contacts[0].LastName);
    }
}