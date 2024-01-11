using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor;
public class CreateAuthorCommand
{
	public CreateAuthorViewModel Model { get; set; }
	private readonly BookStoreDbContext _dbContext;
	private readonly IMapper _mapper;

	public CreateAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public void Handle()
	{
		// Check if the author with the same name already exists
		var author = _dbContext.Authors.SingleOrDefault(a => a.Name == Model.Name);

		if (author is not null)
			throw new InvalidOperationException("Author is already added.");

		// Map the ViewModel to the Author entity
		author = _mapper.Map<Author>(Model);

		// Add the new author to the database
		_dbContext.Authors.Add(author);
		_dbContext.SaveChanges();
	}

	public class CreateAuthorViewModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime Birthday { get; set; }
	}
}