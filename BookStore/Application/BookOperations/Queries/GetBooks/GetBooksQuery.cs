using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBooks;

// Query class for retrieving a list of books in the application layer.
public class GetBooksQuery
{
    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _dbContext;

    // AutoMapper instance for mapping between Entity and ViewModel.
    private readonly IMapper _mapper;

    // Constructor: Initializes the query with the required dependencies.
    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Method: Handles the get books query and returns a list of books as ViewModels.
    public List<BooksViewModel> Handle()
    {
        // Retrieve the list of books with associated genre and author information from the database, ordered by their IDs.
        var bookList = _dbContext.Books.Include(x => x.Genre).Include(a => a.Author).OrderBy(x => x.Id).ToList<Book>();

        // Map the list of books to the list of BooksViewModel using AutoMapper.
        var vm = _mapper.Map<List<Book>, List<BooksViewModel>>(bookList);

        // Return the mapped ViewModel containing information about the books.
        return vm;
    }

    // ViewModel class for displaying information about books in a list.
    public class BooksViewModel
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

