using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail;

// Query class for retrieving detailed information about a book in the application layer.
public class GetBookDetailQuery
{
    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _dbContext;

    // AutoMapper instance for mapping between Entity and ViewModel.
    private readonly IMapper _mapper;

    // Property: Unique identifier for the book whose details are to be retrieved.
    public int BookId { get; set; }

    // Constructor: Initializes the query with the required dependencies.
    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Method: Handles the get book detail query and returns the detailed information as a ViewModel.
    public BookDetailViewModel Handle()
    {
        // Retrieve the book with associated genre and author information from the database based on the provided BookId.
        var book = _dbContext.Books.Include(x => x.Genre).Include(a => a.Author).Where(book => book.Id == BookId).SingleOrDefault();

        // Check if the book with the provided ID exists.
        if (book is null)
        {
            throw new InvalidOperationException("Book not found!");
        }

        // Map the Book entity to the BookDetailViewModel using AutoMapper.
        var vm = _mapper.Map<Book, BookDetailViewModel>(book);

        // Return the mapped ViewModel containing detailed information about the book.
        return vm;
    }

    // ViewModel class for displaying detailed information about a book.
    public class BookDetailViewModel
    {
        // Property: Title of the book.
        public string Title { get; set; }

        // Property: Name of the author of the book.
        public string AuthorName { get; set; }

        // Property: Genre of the book.
        public string Genre { get; set; }

        // Property: Page count of the book.
        public int PageCount { get; set; }

        // Property: Publish date of the book (formatted as a string for simplicity).
        public string PublishDate { get; set; }
    }
}

