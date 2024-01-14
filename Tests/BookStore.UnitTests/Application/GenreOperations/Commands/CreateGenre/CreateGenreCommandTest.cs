using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.CreateGenre;
public class CreateGenreCommandTest(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;

    [Fact]
    public async Task WhenAlreadyExistGenre_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var genre = new Genre()
        {
            Name = "Test Genre"
        };

        await context.Genres.AddAsync(genre);
        await context.SaveChangesAsync();

        var command = new CreateGenreCommand(context)
        {
            Model = new CreateGenreModel()
            {
                Name = genre.Name
            }
        };

        // Act - Assert
        await FluentActions.Invoking(async () => await command.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Genre name: {command.Model.Name} already exists");
    }

    // Happy Path
    [Fact]
    public async Task WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        // Arrange
        var command = new CreateGenreCommand(context)
        {
            Model = new CreateGenreModel()
            {
                Name = "Test"
            }
        };

        // Act
        await FluentActions.Invoking(async () => await command.HandleAsync()).Invoke();

        // Assert
        var genre = context.Genres.SingleOrDefault(genre => genre.Name == command.Model.Name);

        genre.Should().NotBeNull();
    }
}