using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DbOperations;
public static class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new BookStoreDbContext
            (serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>());

        if (context.Books.Any())
            return;

        context.Authors.AddRange(
            new Author
            {
                //Id = 1,
                Name = "Fyodor",
                Surname = "Dostoevsky",
                Birthday = new DateTime(1821, 11, 11)
            },
            new Author
            {
                //Id = 2,
                Name = "Terry",
                Surname = "Pratchett",
                Birthday = new DateTime(1948, 4, 28)
            },
            new Author
            {
                //Id = 3,
                Name = "T.S.",
                Surname = "Eliot",
                Birthday = new DateTime(1888, 9, 26)
            },
            new Author
            {
                //Id = 4,
                Name = "Gabriel",
                Surname = "García Márquez",
                Birthday = new DateTime(1927, 3, 6)
            },
            new Author
            {
                //Id = 5,
                Name = "Art",
                Surname = "Spiegelman",
                Birthday = new DateTime(1948, 2, 15)
            }
        );
        context.Genres.AddRange(
                new Genre
                {
                    Name = "Classics"
                },
                new Genre
                {
                    Name = "GraphicNovel"
                },
                new Genre
                {
                    Name = "Mystery"
                },
                new Genre
                {
                    Name = "Poetry"
                },
                new Genre
                {
                    Name = "Novel"
                },
                new Genre
                {
                    Name = "Humor"
                }
        );

        context.Books.AddRange(
                new Book
                {
                    Title = "Crime and Punishment",
                    GenreId = 5, // Novel
                    AuthorId = 1,
                    PageCount = 400,
                    PublishDate = new DateTime(1866, 1, 1)
                },
                new Book
                {
                    Title = "Good Omens",
                    GenreId = 6, // Humor
                    AuthorId = 2,
                    PageCount = 333,
                    PublishDate = new DateTime(1990, 5, 1)
                },
                new Book
                {
                    Title = "The Waste Land",
                    GenreId = 4, // Poetry
                    AuthorId = 3,
                    PageCount = 200,
                    PublishDate = new DateTime(1922, 10, 22)
                },
                new Book
                {
                    Title = "One Hundred Years of Solitude",
                    GenreId = 5, // Novel
                    AuthorId = 4,
                    PageCount = 417,
                    PublishDate = new DateTime(1967, 5, 30)
                },
                new Book
                {
                    Title = "Maus",
                    GenreId = 2, // Graphic Novel
                    AuthorId = 5,
                    PageCount = 296,
                    PublishDate = new DateTime(1986, 1, 1)
                }
        );
        context.SaveChanges();
    }
}
