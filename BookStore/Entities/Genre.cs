using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities;

public class Genre
{
    // The 'Id' property represents the primary key of the 'Genre' entity.
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // 'Name' property represents the name of the genre.
    public string Name { get; set; }

    // 'IsActive' property indicates whether the genre is active or not. It defaults to 'true'.
    public bool IsActive { get; set; } = true;
}

