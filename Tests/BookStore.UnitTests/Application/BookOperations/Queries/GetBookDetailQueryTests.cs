using AutoMapper;
using BookStore.Application.BookOperations.Query.GetBookDetail;
using BookStore.DbOperations;
using BookStore.UnitTests.TestsSetup;
using FluentAssertions;

namespace BookStore.UnitTests.Application.BookOperations.Queries;
public class GetBookDetailQueryTests(CommonTestFixture testFixture) : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context = testFixture.Context;
    private readonly IMapper mapper = testFixture.Mapper;

    [Fact]
    public async Task WhenBookNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange
        GetBookDetailQuery bookDetailQuery = new GetBookDetailQuery(context, mapper);

        bookDetailQuery.BookId = 999;

        // Act & Assert 
        await FluentActions
            .Invoking(async () => await bookDetailQuery.HandleAsync())
            .Should().ThrowAsync<InvalidOperationException>().WithMessage($"Book Not Found with id: {bookDetailQuery.BookId}");
    }

    // Happy Path
    [Fact]
    public async Task WhenValidInputsAreGiven_Book_ShouldBeReturn()
    {
        // Arrange
        var bookDetailQuery = new GetBookDetailQuery(context, mapper)
        {
            BookId = 5
        };

        // Act
        await FluentActions.Invoking(async () => await bookDetailQuery.HandleAsync()).Invoke();

        // Assert 
        var book = context.Books.FindAsync(bookDetailQuery.BookId);
        book.Should().NotBeNull();
    }
}
