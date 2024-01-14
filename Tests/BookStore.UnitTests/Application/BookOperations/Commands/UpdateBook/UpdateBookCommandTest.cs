using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook;
public class UpdateBookCommandTest(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;

    [Fact]
    public async Task WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateBookCommand(context)
        {
            BookId = 999,

            Model = new UpdateBookModel()
            {
                Title = "Updated Title",
                GenreId = 2
            }
        };

        // Act & Assert
        await FluentActions
            .Invoking(async () => await command.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>().WithMessage($"Book not found with id {command.BookId}");
    }

    // Happy Path
    [Fact]
    public async Task WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        // Arrange
        var command = new UpdateBookCommand(context)
        {
            BookId = 3,

            Model = new UpdateBookModel()
            {
                Title = "Updated Title",
                GenreId = 1
            }
        };

        // Act;
        await FluentActions.Invoking(async () => await command.HandleAsync()).Invoke();

        //Assert 
        var book = await context.Books.FindAsync(command.BookId);

        book.Title.Should().Be(command.Model.Title);
        book.GenreId.Should().Be(command.Model.GenreId);
    }
}