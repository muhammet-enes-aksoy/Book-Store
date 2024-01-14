using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;
public class UpdateAuthorCommandValidatorTest
{
    [Theory]
    [InlineData(0, "", "L")]
    [InlineData(1, " ", " ")]
    [InlineData(0, "Stanislaw", " ")]
    [InlineData(1, "St", "Le")]
    [InlineData(0, "Stan", "Le")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId, string firstname, string lastname)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);

        command.AuthorId = authorId;

        command.Model = new UpdateAuthorModel()
        {
            Firstname = firstname,
            Lastname = lastname
        };

        var validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Happy path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        var command = new UpdateAuthorCommand(null)
        {
            AuthorId = 1,
            Model = new UpdateAuthorModel()
            {
                Firstname = "Stanislaw",
                Lastname = "Lem"
            }
        };

        var validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}