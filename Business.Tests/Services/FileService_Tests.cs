using Business.Interfaces;
using Business.Models;
using Business.Services;
using System.Text.Json;

namespace Business.Tests.Services;

public class FileService_Tests
{
    [Fact]
    public void SaveListToFile_ShouldSaveListToAFile()
    {
        // Arrange
        var fileName = $"{Guid.NewGuid().ToString()}.json";
        var filePath = Path.Combine("Data", fileName);
        List<Contact> expected = new List<Contact>()
        {
            new Contact
            {
                Id = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                Phone = "Test",
                StreetAddress = "Test",
                PostalCode = "Test",
                City = "Test",
            }
        };

        IFileService fileService = new FileService("Data", fileName);

        // Act
        try
        {

            var result = fileService.SaveListToFile(expected);

        // Assert
            Assert.True(result);
            Assert.True(File.Exists(filePath));
        }
        finally
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnListOfContactsFromFile()
    {
        // Arrange
        var fileName = $"{Guid.NewGuid().ToString()}.json";
        var filePath = Path.Combine("Data", fileName);
        List<Contact> expected = new List<Contact>()
        {
            new Contact
            {
                Id = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test",
                Phone = "Test",
                StreetAddress = "Test",
                PostalCode = "Test",
                City = "Test",
            }
        };

        var json = JsonSerializer.Serialize(expected);
        File.WriteAllText(filePath, json);

        IFileService fileService = new FileService("Data", fileName);

        // Act
        try
        {
            var result = fileService.LoadListFromFile();

        // Assert
            Assert.Equal(expected[0].Id, result[0].Id);
            Assert.Equal(expected[0].FirstName, result[0].FirstName);
            Assert.Equal(expected[0].LastName, result[0].LastName);
            Assert.Equal(expected[0].Email, result[0].Email);
            Assert.Equal(expected[0].Phone, result[0].Phone);
            Assert.Equal(expected[0].StreetAddress, result[0].StreetAddress);
            Assert.Equal(expected[0].PostalCode, result[0].PostalCode);
            Assert.Equal(expected[0].City, result[0].City);
        }
        finally
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnEmptyList_WhenNoFileExist()
    {
        // Arrange
        var fileName = $"{Guid.NewGuid().ToString()}.json";
        var filePath = Path.Combine("Data", fileName);

        IFileService fileService = new FileService("Data", fileName);

        // Act

        var result = fileService.LoadListFromFile();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}