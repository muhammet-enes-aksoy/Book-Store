using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre;
public class DeleteGenreCommandValidatorTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-123)]
    [InlineData(-2)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
    {
        // Arrange
        var command = new DeleteGenreCommand(null)
        {
            GenreId = genreId
        };

        // Act
        var validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        var command = new DeleteGenreCommand(null)
        {
            GenreId = 1
        };

        // Act
        var validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
    }
}