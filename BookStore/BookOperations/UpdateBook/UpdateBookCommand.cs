using BookStore.Api.DbOperations;

namespace BookStore.Api.BookOperations.UpdateBook;
public class UpdateBookCommand(BookStoreDbContext dbContext)
{
    private readonly BookStoreDbContext dbContext = dbContext;

    public int BookId { get; set; }
    public UpdateBookModel? Model { get; set; }

    public async Task HandleAsync()
    {
        var book =
            await dbContext.Books.FindAsync(BookId)
                ?? throw new InvalidOperationException($"Book not found with id {BookId}");

        book.Title = Model?.Title != default ? Model.Title : book.Title;
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        dbContext.SaveChanges();
    }
}