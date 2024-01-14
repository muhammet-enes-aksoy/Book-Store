using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre;
public class UpdateGenreCommandTest(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;

    [Fact]
    public async Task WhenGivenIdIsNotInDb_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateGenreCommand(context)
        {
            GenreId = 999
        };

        // Act & Assert
        await FluentActions
            .Invoking(async () => await command.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>().WithMessage($"Genre with ID {command.GenreId} not found.");
    }

    [Fact]
    public async Task WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var command = new UpdateGenreCommand(context)
        {
            GenreId = 3,

            Model = new UpdateGenreModel()
            {
                Name = "Novel"
            }
        };

        // Act & Assert
        await FluentActions
            .Invoking(async () => await command.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>().WithMessage($"A genre already exists with the name {command.Model.Name}");
    }

    [Fact]
    public async Task WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        //Arrange
        var command = new UpdateGenreCommand(context);

        var model = new UpdateGenreModel()
        {
            Name = "New Genre"
        };

        command.Model = model;
        command.GenreId = 3;

        //Act
        await FluentActions.Invoking(async () => await command.HandleAsync()).Invoke();

        //Assert
        var genre = await context.Genres.FindAsync(command.GenreId);

        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);
    }
}