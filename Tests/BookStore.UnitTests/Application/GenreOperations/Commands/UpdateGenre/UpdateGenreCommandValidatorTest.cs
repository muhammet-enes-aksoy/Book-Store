using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre;
public class UpdateGenreCommandValidatorTest(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0, " ")]
    [InlineData(1, " ")]
    [InlineData(1, "Ab")]
    [InlineData(0, "Cd")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string name)
    {
        // Arrange
        var command = new UpdateGenreCommand(null)
        {
            GenreId = genreId,

            Model = new UpdateGenreModel()
            {
                Name = name
            }
        };

        // Act
        var validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        var command = new UpdateGenreCommand(null)
        {
            GenreId = 1,

            Model = new UpdateGenreModel()
            {
                Name = "Test"
            }
        };

        // Act
        var validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
    }
}