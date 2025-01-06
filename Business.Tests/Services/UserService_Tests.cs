using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class UserService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly IUserService _userService;

    public UserService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _userService = new UserService(_fileServiceMock.Object);
    }

    [Fact]
    public void AddUser_ShouldReturnTrueAndSaveToFile_WhenUserIsAddedToList()
    {
        // Arrange
        var userRegistrationForm = new UserRegistrationForm();
        _fileServiceMock.Setup(fileService => fileService.SaveListToFile(It.IsAny<List<User>>())).Returns(true);

        // Act
        var result = _userService.AddUser(userRegistrationForm);

        // Assert
        Assert.True(result);
        _fileServiceMock.Verify(fileService => fileService.SaveListToFile(It.IsAny<List<User>>()), Times.Once);
    }
}


// Mocking test for UserService.

/*
     private readonly Mock<IUserService> _userServiceMock;
    private readonly UserService _userService;

    public UserService_Tests()
    {
        _userServiceMock = new Mock<IUserService>();
        _userService = new UserService();
    }

    [Fact]
    public void AddUser_ShouldReturnTrue_WhenUserIsAddedToList()
    {
        // Arrange
        var userRegistrationForm = new UserRegistrationForm { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "0701234567", StreetAddress = "Elm Street 123", PostalCode = "90210", City = "Beverly Hills" };
        _userServiceMock.Setup(us => us.AddUser(userRegistrationForm)).Returns(true);

        // Act
        var result = _userServiceMock.Object.AddUser(userRegistrationForm);

        // Assert
        Assert.True(result);
        _userServiceMock.Verify(us => us.AddUser(userRegistrationForm), Times.Once);
    }

    [Fact]
    public void GetUsers_ShouldReturnListOfUsers()
    {
        // Arrange
        var users = new List<User>()
        {
            new() { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "0701234567", StreetAddress = "Elm Street 123", PostalCode = "90210", City = "Beverly Hills" },
            new() { FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com", Phone = "0319876543", StreetAddress = "Oak Avenue 456", PostalCode = "30301", City = "Gothenburg" }
        };

        _userServiceMock.Setup(us => us.GetUsers()).Returns(users);

        // Act
        var result = _userServiceMock.Object.GetUsers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("John", result.First().FirstName);
    }
 */