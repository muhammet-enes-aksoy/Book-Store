using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;
public class GetGenreDetailQueryTest(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;
    private readonly IMapper mapper = testFixture.Mapper;

    [Fact]
    public async Task WhenGenreNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var genreDetailQuery = new GetGenreDetailQuery(context, mapper)
        {
            GenreId = 999
        };

        // Act & Assert 
        await FluentActions
            .Invoking(async () => await genreDetailQuery.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Unable to find a genre with the specified ID: {genreDetailQuery.GenreId}.");
    }

    // Happy Path
    [Fact]
    public async Task WhenValidInputsAreGiven_Genre_ShouldBeReturn()
    {
        // Arrange
        var genreDetailQuery = new GetGenreDetailQuery(context, mapper)
        {
            GenreId = 2
        };

        // Act 
        await FluentActions.Invoking(() => genreDetailQuery.HandleAsync()).Invoke();

        // Assert
        var isGenreExists = await context.Genres.AnyAsync(genre => genre.Id.Equals(genreDetailQuery.GenreId));

        isGenreExists.Should().BeTrue();
    }
}