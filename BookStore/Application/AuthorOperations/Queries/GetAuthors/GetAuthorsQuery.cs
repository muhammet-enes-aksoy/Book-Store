using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.AuthorOperations.Commands.GetAuthors;
public class GetAuthorsQuery
{
	private readonly BookStoreDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public List<AuthorsViewModel> Handle()
	{
		var authorList = _dbContext.Authors.OrderBy(a => a.Id).ToList();
		var viewModel = _mapper.Map<List<AuthorsViewModel>>(authorList);

		return viewModel;
	}

	public class AuthorsViewModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Birthday { get; set; }
	}
}