using FluentAssertions;
using Telegram.Cli.Extensions;

namespace Telegram.Cli.Test.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("1 234 56789", 5, new[] {"1", "234", "56789"})]
    [InlineData("1234 56789", 5, new[] {"1234", "56789"})]
    [InlineData("1234567890", 10, new[] {"1234567890"})]
    [InlineData("1 2 3 4 5 6 7 8 9 0", 1, new[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"})]
    [InlineData("1234567890", 0, new [] {"1234567890" })]
    [InlineData("1234567890", -1, new [] {"1234567890" })]
    [InlineData("1234567890", 11, new[] {"1234567890"})]
    public void Split_ShouldSplitMessage(string message, int maxChunkSize, string[] expected)
    {
        // Arrange
        // Act
        var result = message.Split(maxChunkSize);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Split_ShouldReturnEmptyArrayWhenMessageIsNull()
    {
        // Arrange
        const string message = null;
        const int maxChunkSize = 5;

        // Act
        var result = message.Split(maxChunkSize);

        // Assert
        result.Should().BeEmpty();
    }
}