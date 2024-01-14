using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.UnitTests.TestsSetup;
public static class Genres
{
    public static void AddGenres(this BookStoreDbContext context) =>

    context.Genres.AddRange(
            new Genre { Name = "Classics" },
            new Genre { Name = "Graphic Novel" },
            new Genre { Name = "Mystery" },
            new Genre { Name = "Poetry" },
            new Genre { Name = "Novel" },
            new Genre { Name = "Humor" }
                );
}