using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

// Test class for the CreateAuthorCommand
public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
{
    // Test fixture provides a consistent context for the tests
    private readonly BookStoreDbContext context = testFixture.Context;
    private readonly IMapper mapper = testFixture.Mapper;

    // Test for handling the case when an author with the same name already exists
    [Fact]
    public async Task WhenAlreadyExistAuthor_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var author = new Author()
        {
            Name = "Name",
            Surname = "Surname",
            Birthday = new DateTime(1970, 1, 1)
        };

        // Add an author to the database
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();

        // Create a command with the existing author's details
        var command = new CreateAuthorCommand(context, mapper)
        {
            Model = new CreateAuthorCommand.CreateAuthorViewModel
            {
                Name = author.Name,
                Surname = author.Surname,
                Birthday = author.Birthday
            }
        };

        // Act & Assert 
        // Verify that handling the command throws an InvalidOperationException
        await FluentActions.Invoking(async () => await command.Handle())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"An author with the name '{command.Model.Name} {command.Model.Surname}' already exists");
    }

    // Test for creating a new author with valid inputs
    [Fact]
    public async Task WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        // Arrange 
        // Create a new CreateAuthorCommand and a model with valid details
        CreateAuthorCommand command = new CreateAuthorCommand(context, mapper);
        CreateAuthorCommand.CreateAuthorViewModel model = new CreateAuthorCommand.CreateAuthorViewModel
        {
            Firstname = "Create Author Firstname",
            Lastname = "Create Author Lastname",
            DateOfBirth = DateTime.Now.Date.AddYears(-40)
        };
        command.Model = model;

        // Act 
        // Execute the command
        await FluentActions.Invoking(async () => await command.Handle()).Invoke();

        // Assert 
        // Verify that the new author is created in the database with the expected details
        var author = await context.Authors.SingleOrDefaultAsync(a => a.Firstname == model.Firstname && a.Lastname == model.Lastname);
        author.Should().NotBeNull();
        author.DateOfBirth.Should().Be(model.DateOfBirth.Date);
    }
}

