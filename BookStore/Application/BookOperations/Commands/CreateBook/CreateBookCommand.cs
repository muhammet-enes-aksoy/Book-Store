using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

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
        // Check if the book with the same title already exists
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if (book != null)
        {
            throw new InvalidOperationException("The book already exists");
        }

        // Map the CreateBookModel to the Book entity using AutoMapper
        book = _mapper.Map<Book>(Model);

        // Add the new book to the database
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}