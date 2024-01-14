using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

// Test class for validating DeleteAuthorCommand
public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    // Constructor with CommonTestFixture dependency injection
    public DeleteAuthorCommandTest(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    // Test case for when invalid AuthorId is given
    [Fact]
    public async Task WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange 
        // Create a DeleteAuthorCommand with an invalid AuthorId (0)
        var command = new DeleteAuthorCommand(context)
        {
            AuthorId = 0
        };

        // Act & Assert 
        // Verify that an InvalidOperationException is thrown with the expected message
        await FluentActions
            .Invoking(async () => await command.Handle())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Author with ID {command.AuthorId} not found.");
    }

    // Test case for when Author has associated books
    [Fact]
    public async Task WhenAuthorBooksExist_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        // Create a DeleteAuthorCommand for an Author with associated books
        var command = new DeleteAuthorCommand(context)
        {
            AuthorId = 4
        };

        // Act & Assert 
        // Verify that an InvalidOperationException is thrown with the expected message
        await FluentActions
            .Invoking(async () => await command.Handle())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("This author cannot be deleted because they have associated books. " +
                "Please remove the books associated with this author before attempting to delete.");
    }

    // Test case for when valid AuthorId is given
    [Fact]
    public async Task WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
        // Arrange
        // Create a DeleteAuthorCommand for an existing Author
        var command = new DeleteAuthorCommand(context)
        {
            AuthorId = 6
        };

        // Act 
        // Execute the command to delete the Author
        await FluentActions.Invoking(async () => await command.Handle()).Invoke();

        // Assert 
        // Verify that the Author is deleted from the database
        var isAuthorExists = await context.Authors.AnyAsync(a => a.Id == command.AuthorId);
        isAuthorExists.Should().BeFalse();
    }
}

