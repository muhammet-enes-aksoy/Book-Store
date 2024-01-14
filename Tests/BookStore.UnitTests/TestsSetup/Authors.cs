using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.UnitTests.TestsSetup;
public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context) =>

        context.Authors.AddRange(
                new Author
                {
                    Firstname = "Oğuz",
                    Lastname = "Atay",
                    DateOfBirth = new DateTime(1934, 10, 12)
                },
                new Author
                {
                    Firstname = "Douglas",
                    Lastname = "Adams",
                    DateOfBirth = new DateTime(1952, 3, 11)
                },
                new Author
                {
                    Firstname = "Edip",
                    Lastname = "Cansever",
                    DateOfBirth = new DateTime(1928, 8, 8)
                },
                new Author
                {
                    Firstname = "Ahmet",
                    Lastname = "Hamdi Tanpınar",
                    DateOfBirth = new DateTime(1901, 6, 23)
                },
                new Author
                {
                    Firstname = "Neil",
                    Lastname = "Gaiman",
                    DateOfBirth = new DateTime(1960, 11, 10)
                },
                new Author
                {
                    Firstname = "Delete",
                    Lastname = "Delete",
                    DateOfBirth = new DateTime(1990, 10, 10)
                },
                new Author
                {
                    Firstname = "Delete",
                    Lastname = "Delete",
                    DateOfBirth = new DateTime(1990, 10, 10)
                });

}