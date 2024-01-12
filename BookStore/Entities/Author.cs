using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities;

public class Author
{
	// The 'Id' property represents the primary key of the 'Author' entity.
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	// 'Name' property represents the first name of the author.
	public string Name { get; set; }

	// 'Surname' property represents the last name or surname of the author.
	public string Surname { get; set; }

	// 'Birthday' property represents the birthdate of the author.
	public DateTime Birthday { get; set; }
}

