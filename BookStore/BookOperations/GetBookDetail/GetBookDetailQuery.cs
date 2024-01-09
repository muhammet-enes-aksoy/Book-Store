using AutoMapper;
using BookStore.Api.BookOperations.GetBookDetail;
using BookStore.Api.DbOperations;

namespace BookStore.BookOperations.GetBookDetail;
public class GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
{
    private readonly BookStoreDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public int BookId { get; set; }

    public async Task<BookDetailViewModel> HandleAsync()
    {
        var book = await dbContext.Books.FindAsync(BookId)
            ?? throw new InvalidOperationException($"Book Not Found with id: {BookId}");

        return mapper.Map<BookDetailViewModel>(book);
    }
}