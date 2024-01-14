using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTest(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;

    [Fact]
    public async Task WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new DeleteGenreCommand(context)
        {
            GenreId = 999
        };

        // Act & Assert
        await FluentActions.Invoking(async () => await command.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Genre not found");
    }

    [Fact]
    public async Task WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
        // Arrange
        var command = new DeleteGenreCommand(context)
        {
            GenreId = 1
        };

        // Act
        await FluentActions
            .Invoking(async () => await command.HandleAsync())
            .Invoke();

        // Assert
        var genre = context.Genres.Any(genre => genre.Id.Equals(command.GenreId));

        genre.Should().BeFalse();
    }
}