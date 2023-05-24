using System.Text;
using FluentAssertions;
using Telegram.Cli.Extensions;

namespace Telegram.Cli.Test.Extensions;

public class ListExtensionsTests
{
    [Fact]
    public void AddAsSingleItem_ShouldAddItemToTheList()
    {
        // Arrange
        var list = new List<string>();
        var item = new List<string> {"item"};
        const string separator = "+";

        // Act
        list.AddAsSingleItem(item, separator);

        // Assert
        list.Should().NotBeEmpty();
        list.Should().HaveCount(1);
        list.Should().Contain("item");
    }

    [Fact]
    public void AddAsSingleItem_ShouldAddItemToTheListWithDefaultSeparator()
    {
        // Arrange
        var list = new List<string>();
        var item = new List<string> {"item"};

        // Act
        list.AddAsSingleItem(item);

        // Assert
        list.Should().NotBeEmpty();
        list.Should().HaveCount(1);
        list.Should().Contain("item");
    }
    
    [Fact]
    public void AddAsSingleItem_ShouldNotAddItemToTheList()
    {
        // Arrange
        var list = new List<string>();
        var item = new List<string>();

        // Act
        list.AddAsSingleItem(item);

        // Assert
        list.Should().BeEmpty();
    }

    [Fact]
    public void AddAsSingleItem_ShouldNotAddItemWhenListIsNull()
    {
        // Arrange
        List<string> list = null;
        var item = new List<string> {"item"};
        
        // Act
        list.AddAsSingleItem(item);

        // Assert
        list.Should().BeNull();
    }
    
    [Fact]
    public void AddAsSingleItem_ShouldNotAddItemWhenItemIsNull()
    {
        // Arrange
        var list = new List<string>();
        List<string> item = null;
        
        // Act
        list.AddAsSingleItem(item);

        // Assert
        list.Should().BeEmpty();
    }
    
    [Theory]
    [InlineData("item1", "item2", "item3")]
    [InlineData("item1", "item2", "item3", "item4")]
    [InlineData("item1", "item2", "item3", "item4", "item5")]
    public void AddAsSingleItem_ShouldAddItemsToTheList(params string[] items)
    {
        // Arrange
        var list = new List<string>();
        var item = items.ToList();

        // Act
        list.AddAsSingleItem(item);

        // Assert
        list.Should().NotBeEmpty();
        list.Should().HaveCount(1);
        list.Should().Contain(string.Join(" ", items));
    }
    
    [Theory]
    [InlineData("item1", "item2", "item3")]
    [InlineData("item1", "item2", "item3", "item4")]
    [InlineData("item1", "item2", "item3", "item4", "item5")]
    public void AddAsSingleItem_ShouldAddItemsToTheListWithSeparator(params string[] items)
    {
        // Arrange
        var list = new List<string>();
        var item = items.ToList();

        // Act
        list.AddAsSingleItem(item, ",");

        // Assert
        list.Should().NotBeEmpty();
        list.Should().HaveCount(1);
        list.Should().Contain(string.Join(",", items));
    }
    
    //ExtractMessage tests
    [Fact]
    public void ExtractMessage_ShouldReturnEmptyStringWhenMessageIsNull()
    {
        // Arrange
        const string[] args = null;

        // Act
        var result = args.ExtractMessage();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void ExtractMessage_ShouldReturnEmptyStringWhenMessageIsEmpty()
    {
        // Arrange
        var args = Array.Empty<string>();

        // Act
        var result = args.ExtractMessage();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void ExtractMessage_ShouldReturnEmptyStringWhenMessageIsWhitespace()
    {
        // Arrange
        var args = new[] {" "};

        // Act
        var result = args.ExtractMessage();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Theory]
    [InlineData("item1", "item2", "item3")]
    [InlineData("item1", "item2", "item3", "item4")]
    [InlineData("item1", "item2", "item3", "item4", "item5")]
    public void ExtractMessage_ShouldReturnMessage(params string[] args)
    {
        // Act
        var expectedMessage = string.Join(" ", args).Trim();
        var result = args.ExtractMessage();

        // Assert
        result.Should().NotBeEmpty();
        result.Should().Be(expectedMessage);
    }
}