using BookStore.Api.DbOperations;
using BookStore.Common;

namespace BookStore.BookOperations.GetBookDetail;
public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
        if (book is null)
        {
            throw new InvalidOperationException("Kitap Bulunamadı!");
        }
        BookDetailViewModel vm = new BookDetailViewModel
        {
            Title = book.Title,
            Genre = ((GenreEnum)book.GenreId).ToString(),
            PageCount = book.PageCount,
            PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
        };
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