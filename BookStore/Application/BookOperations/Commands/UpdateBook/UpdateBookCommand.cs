using BookStore.DbOperations;

namespace BookStore.Application.BookOperations.Commands.UpdateBook;

// Command class for updating an existing book in the application layer.
public class UpdateBookCommand
{
    // Database context for interacting with the underlying data store.
    private readonly BookStoreDbContext _dbContext;

    // Property: Unique identifier for the book to be updated.
    public int BookId { get; set; }

    // Property: ViewModel containing updated information for the book.
    public UpdateBookViewModel Model { get; set; }

    // Constructor: Initializes the command with the required dependencies.
    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Method: Handles the update book command.
    public void Handle()
    {
        // Retrieve the book to be updated from the database based on the provided BookId.
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        // Check if the book with the provided ID exists.
        if (book is null)
        {
            throw new InvalidOperationException("No books found to update!");
        }

        // Update the book's GenreId if the updated model's GenreId is not the default value; otherwise, use the existing GenreId.
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

        // Update the book's Title if the updated model's Title is not the default value; otherwise, use the existing Title.
        book.Title = Model.Title != default ? Model.Title : book.Title;

        // Save changes to persist the updated book information in the database.
        _dbContext.SaveChanges();
    }

    // ViewModel class for updating an existing book.
    public class UpdateBookViewModel
    {
        // Property: Updated title of the book.
        public string Title { get; set; }

        // Property: Updated author identifier of the book.
        public int AuthorId { get; set; }

        // Property: Updated genre identifier of the book.
        public int GenreId { get; set; }

        // Property: Updated page count of the book.
        public int PageCount { get; set; }

        // Property: Updated publish date of the book.
        public DateTime PublishDate { get; set; }
    }
}

