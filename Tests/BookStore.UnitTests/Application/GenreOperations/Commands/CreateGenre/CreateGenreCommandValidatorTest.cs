using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre;
public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("abc")]
    [InlineData("ab")]
    [InlineData(" ")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
    {
        // Arrange
        var command = new CreateGenreCommand(null)
        {
            Model = new CreateGenreModel()
            {
                Name = name
            }
        };

        // Act
        var validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // Arrange
        var command = new CreateGenreCommand(null)
        {
            Model = new CreateGenreModel()
            {
                Name = "Biography"
            }
        };

        // Act
        var validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
    }
}