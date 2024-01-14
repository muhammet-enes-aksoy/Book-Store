using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

// Test class for validating DeleteAuthorCommandValidator
public class DeleteAuthorCommandValidatorTest
{
    // Test case for when invalid AuthorId values are given
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
    {
        // Arrange
        // Create a DeleteAuthorCommand with an invalid AuthorId
        var command = new DeleteAuthorCommand(null)
        {
            AuthorId = authorId
        };

        // Act
        // Validate the command using DeleteAuthorCommandValidator
        var validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        // Assert
        // Verify that there are errors returned by the validator
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Test case for when a valid AuthorId is given
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        // Create a DeleteAuthorCommand with a valid AuthorId
        var command = new DeleteAuthorCommand(null)
        {
            AuthorId = 1
        };

        // Act
        // Validate the command using DeleteAuthorCommandValidator
        var validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        // Assert
        // Verify that there are no errors returned by the validator
        result.Errors.Count.Should().Be(0);
    }
}

