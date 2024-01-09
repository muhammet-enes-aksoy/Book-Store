using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.BookOperations.Commands.CreateBook;
public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
        if (book != null)
        {
            throw new InvalidOperationException("The book already exists");

        }
        book = new Book
        {
            Title = Model.Title,
            GenreId = Model.GenreId,
            PageCount = Model.PageCount,
            PublishDate = Model.PublishDate
        };
        book = _mapper.Map<Book>(Model); //AutoMapper
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}