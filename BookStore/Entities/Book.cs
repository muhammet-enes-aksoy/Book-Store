using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities;

public class Book
{
    // The 'Id' property represents the primary key of the 'Book' entity.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // 'AuthorId' property is a foreign key referencing the 'Author' entity.
    public int AuthorId { get; set; }

    // 'Author' property represents the navigation property for the related 'Author'.
    public Author Author { get; set; }

    // 'Title' property represents the title of the book.
    public string Title { get; set; }

    // 'GenreId' property is a foreign key referencing the 'Genre' entity.
    public int GenreId { get; set; }

    // 'Genre' property represents the navigation property for the related 'Genre'.
    public Genre Genre { get; set; }

    // 'PageCount' property represents the number of pages in the book.
    public int PageCount { get; set; }

    // 'PublishDate' property represents the publication date of the book.
    public DateTime PublishDate { get; set; }
}

