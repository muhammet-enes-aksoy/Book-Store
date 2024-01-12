using FluentValidation;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;
// Validator class for GetGenreDetailQuery using FluentValidation library.
public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
    // Constructor: Configures validation rules for GetGenreDetailQuery.
    public GetGenreDetailQueryValidator()
    {
        // Rule: The 'GenreId' property in the GetGenreDetailQuery should be greater than 0.
        // This ensures that a valid and positive genre ID is provided for retrieving genre details.
        RuleFor(query => query.GenreId).GreaterThan(0)
            .WithMessage("Invalid genre ID. Please provide a valid genre ID greater than 0.");
    }
}

