using BookStore.Api;
using BookStore.Api.DbOperations;
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

        context.Books.AddRange(
                new Book
                {
                    Title = "Crime and Punishment",
                    GenreId = 5, // Novel
                    PageCount = 400,
                    PublishDate = new DateTime(1866, 1, 1)
                },
                new Book
                {
                    Title = "Good Omens",
                    GenreId = 6, // Humor
                    PageCount = 333,
                    PublishDate = new DateTime(1990, 5, 1)
                },
                new Book
                {
                    Title = "The Waste Land",
                    GenreId = 4, // Poetry
                    PageCount = 200,
                    PublishDate = new DateTime(1922, 10, 22)
                },
                new Book
                {
                    Title = "One Hundred Years of Solitude",
                    GenreId = 5, // Novel
                    PageCount = 417,
                    PublishDate = new DateTime(1967, 5, 30)
                },
                new Book
                {
                    Title = "Maus",
                    GenreId = 2, // Graphic Novel
                    PageCount = 296,
                    PublishDate = new DateTime(1986, 1, 1)
                }
        );
        context.SaveChanges();
    }
}
