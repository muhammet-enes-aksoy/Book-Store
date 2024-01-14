using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook;
public class DeleteBookCommandTests(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;

    [Fact]
    public async Task WhenBookIsNotFound_Handle_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = new DeleteBookCommand(context)
        {
            BookId = -5
        };

        // Act & Assert
        await FluentActions.Invoking(async () => await command.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>().WithMessage("Book Id must be greater than 0");
    }

    [Fact]
    public async Task WhenValidInputsAreGiven_Book_ShouldBeDeleted()
    {
        // Arrange
        var command = new DeleteBookCommand(context)
        {
            BookId = 1
        };

        // Act
        await FluentActions.Invoking(async () => await command.HandleAsync()).Invoke();

        // Assert
        var book = await context.Books.AnyAsync(book => book.Id.Equals(command.BookId));
        book.Should().BeFalse();
    }
}