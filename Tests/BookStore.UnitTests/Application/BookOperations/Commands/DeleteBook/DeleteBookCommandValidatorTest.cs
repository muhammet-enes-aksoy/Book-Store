using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook;
public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]

    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
    {
        // Arrange
        var command = new DeleteBookCommand(null)
        {
            BookId = bookId
        };

        // Act 
        var validator = new DeleteBookCommandValidator();

        var result = validator.Validate(command);

        // Assert 
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        var command = new DeleteBookCommand(null)
        {
            BookId = 1
        };

        // Act 
        var validator = new DeleteBookCommandValidator();

        var result = validator.Validate(command);

        // Assert 
        result.Errors.Count.Should().Be(0);
    }
}