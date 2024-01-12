using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.BookOperations.Commands.CreateBook;

// Command class for creating a new book in the application layer.
public class CreateBookCommand
{
    // Property: Model containing information for creating a new book.
    public CreateBookModel Model { get; set; }

    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _dbContext;

    // AutoMapper instance for mapping between Model and Entity.
    private readonly IMapper _mapper;

    // Constructor: Initializes the command with the required dependencies.
    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Method: Handles the create book command.
    public void Handle()
    {
        // Check if a book with the same title already exists in the database.
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        // If a book with the same title is found, raise an exception.
        if (book != null)
        {
            throw new InvalidOperationException("The book already exists");
        }

        // Map the data from the CreateBookModel to the Book entity using AutoMapper.
        book = _mapper.Map<Book>(Model);

        // Add the new book to the Books DbSet in the database context.
        _dbContext.Books.Add(book);

        // Save changes to persist the new book in the database.
        _dbContext.SaveChanges();
    }

    // Model class for creating a new book.
    public class CreateBookModel
    {
        // Property: Title of the book.
        public string Title { get; set; }

        // Property: Identifier of the author associated with the book.
        public int AuthorId { get; set; }

        // Property: Identifier of the genre associated with the book.
        public int GenreId { get; set; }

        // Property: Page count of the book.
        public int PageCount { get; set; }

        // Property: Publish date of the book.
        public DateTime PublishDate { get; set; }
    }
}

