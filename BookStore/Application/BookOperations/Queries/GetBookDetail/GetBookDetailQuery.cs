using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail;
public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
        if (book is null)
        {
            throw new InvalidOperationException("Book not found!");
        }
        var vm = _mapper.Map<Book, BookDetailViewModel>(book);
        return vm;
    }

    //View Model

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}