using AutoMapper;
using BookStore.Api.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.BookOperations.GetBooks;
public class GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
{
    private readonly BookStoreDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<List<BooksViewModel>> HandleAsync()
    {
        var bookList = await dbContext.Books.OrderBy(x => x.Id).ToListAsync();

        return mapper.Map<List<BooksViewModel>>(bookList);
    }
}
