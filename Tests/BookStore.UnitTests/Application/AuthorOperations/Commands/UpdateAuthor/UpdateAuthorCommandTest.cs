using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

// Test class for validating UpdateAuthorCommand
public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
{
    // Instance of the BookStoreDbContext from the test fixture
    private readonly BookStoreDbContext context;

    // Constructor accepting the test fixture as a parameter
    public UpdateAuthorCommandTest(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    // Test case for when invalid AuthorId is given
    [Fact]
    public async Task WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        // Create an UpdateAuthorCommand with an invalid AuthorId
        var command = new UpdateAuthorCommand(context)
        {
            AuthorId = 0
        };

        // Act & Assert 
        // Validate that an InvalidOperationException is thrown
        await FluentActions.Invoking(async () => await command.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Author with ID {command.AuthorId} not found.");
    }

    // Test case for when valid inputs are given, and author details should be updated
    [Fact]
    public async Task WhenValidInputsAreGiven_Author_ShouldBeUpdate()
    {
        // Arrange 
        // Create an UpdateAuthorCommand with valid AuthorId and model
        var command = new UpdateAuthorCommand(context)
        {
            AuthorId = 2,
            Model = new UpdateAuthorModel()
            {
                Firstname = "Updated Firstname",
                Lastname = "Updated Lastname",
            }
        };

        // Act 
        // Execute the command to update author details
        await FluentActions.Invoking(async () => await command.HandleAsync()).Invoke();

        // Assert
        // Verify that author details have been updated in the database
        var author = await context.Authors.FindAsync(command.AuthorId);
        author.Firstname.Should().Be(command.Model.Firstname);
        author.Lastname.Should().Be(command.Model.Lastname);
    }
}

