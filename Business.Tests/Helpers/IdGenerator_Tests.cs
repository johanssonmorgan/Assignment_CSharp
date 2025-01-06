using Business.Helpers;

namespace Business.Tests.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void Generate_ShouldReturnFirstPartOfGuid()
    {
        // Arrange

        // Act
        string id = IdGenerator.Generate();

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(id));
        Assert.True(id.Length == 8);
    }
}
