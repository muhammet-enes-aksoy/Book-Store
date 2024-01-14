using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook;
public class UpdateBookCommandValidatorTest(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;

    [Theory]
    [InlineData(0, "Lor", 1)]
    [InlineData(0, "Lord", 0)]
    [InlineData(0, "", 0)]
    [InlineData(1, "", 1)]
    [InlineData(1, "", 0)]
    [InlineData(1, " ", 1)]
    [InlineData(1, "Lord of The Rings", 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int genreId)
    {
        // Arrange
        var command = new UpdateBookCommand(context)
        {
            BookId = bookId,

            Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
            }
        };

        // Act 
        var validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        // Assert 
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    // Happy Path
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldBeUpdated()
    {
        var command = new UpdateBookCommand(null)
        {
            BookId = 1,

            Model = new UpdateBookModel()
            {
                Title = "Updated Book",
                GenreId = 1,
            }
        };

        var validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
