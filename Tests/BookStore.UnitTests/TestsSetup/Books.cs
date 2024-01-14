using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.UnitTests.TestsSetup;
public static class Books
{
    public static void AddBooks(this BookStoreDbContext context) =>

        context.Books.AddRange(
            new Book
            {
                Title = "Tutunamayanlar",
                GenreId = 5,
                AuthorId = 1,
                PageCount = 200,
                PublishDate = new DateTime(1982, 1, 1)
            },
            new Book
            {
                Title = "The Hitchhiker's Guide to the Galaxy",
                GenreId = 6,
                AuthorId = 2,
                PageCount = 250,
                PublishDate = new DateTime(1979, 10, 12)
            },
            new Book
            {
                Title = "Sonrası Kalır",
                GenreId = 4,
                AuthorId = 3,
                PageCount = 540,
                PublishDate = new DateTime(2000, 5, 20)
            },
            new Book
            {
                Title = "Saatleri Ayarlama Enstitüsü",
                GenreId = 5,
                AuthorId = 4,
                PageCount = 464,
                PublishDate = new DateTime(1961, 5, 28)
            },
            new Book
            {
                Title = "Sandman",
                GenreId = 2,
                AuthorId = 5,
                PageCount = 464,
                PublishDate = new DateTime(1989, 1, 1)
            });
}