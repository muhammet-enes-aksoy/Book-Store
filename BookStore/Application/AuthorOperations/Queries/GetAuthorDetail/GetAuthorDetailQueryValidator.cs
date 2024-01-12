using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.GetAuthorDetail;
// Validator class for GetAuthorDetailQuery using FluentValidation library.
public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
{
	// Constructor: Configures validation rules for GetAuthorDetailQuery.
	public GetAuthorDetailQueryValidator()
	{
		// Rule: The 'AuthorId' property in GetAuthorDetailQuery should be greater than 0.
		// This ensures that a valid and positive AuthorId is provided for retrieving author details.
		RuleFor(query => query.AuthorId).GreaterThan(0);
	}
}

