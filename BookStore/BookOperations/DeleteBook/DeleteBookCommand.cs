using BookStore.Api.DbOperations;

namespace BookStore.Api.BookOperations.DeleteBook;

public class DeleteBookCommand(BookStoreDbContext dbContext)
{
    private readonly BookStoreDbContext dbContext = dbContext;
    public int BookId { get; set; }

    public async Task HandleAsync()
    {
        // if book id is less than or equal to 0, throw exception
        if (BookId <= 0)
            throw new InvalidOperationException("Book Id must be greater than 0");

        // if book is not found, throw exception
        var book = await dbContext.Books.FindAsync(BookId)
            ?? throw new InvalidOperationException($"Book not found with Id: {BookId}");

        dbContext.Books.Remove(book);
        dbContext.SaveChanges();
    }
}
