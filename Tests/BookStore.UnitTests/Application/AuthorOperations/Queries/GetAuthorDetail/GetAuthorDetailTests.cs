using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;
public class GetAuthorDetailQueryTest(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;

    private readonly IMapper mapper = testFixture.Mapper;

    [Fact]
    public async Task WhenAuthorNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        var authorDetailQuery = new GetAuthorDetailQuery(context, mapper)
        {
            AuthorId = 100
        };

        // Act & Assert
        await FluentActions
            .Invoking(async () => await authorDetailQuery.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Author with ID {authorDetailQuery.AuthorId} not found.");
    }

    // Happy Path
    [Fact]
    public async Task WhenValidInputsAreGiven_Author_ShouldBeReturn()
    {
        // Arrange
        var authorDetailQuery = new GetAuthorDetailQuery(context, mapper)
        {
            AuthorId = 1
        };

        // Act 
        await FluentActions.Invoking(async () => await authorDetailQuery.HandleAsync()).Invoke();

        // Assert 
        var isAuthorExists = await context.Authors.AnyAsync(author => author.Id == authorDetailQuery.AuthorId);
        isAuthorExists.Should().BeTrue();
    }
}