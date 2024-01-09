using BookStore.Application.GenreOperations.Queries;
using FluentValidation;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        RuleFor(query => query.GenreId).GreaterThan(0);
    }
}