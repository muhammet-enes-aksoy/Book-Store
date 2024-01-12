using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
// Validator class for UpdateAuthorCommand using FluentValidation library.
public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
	// Constructor: Configures validation rules for UpdateAuthorCommand.
	public UpdateAuthorCommandValidator()
	{
		// Rule: The 'Name' property in UpdateAuthorCommand's Model should have a minimum length of 2 characters and should not be empty.
		RuleFor(a => a.Model.Name).MinimumLength(2).NotEmpty();

		// Rule: The 'Surname' property in UpdateAuthorCommand's Model should have a minimum length of 2 characters and should not be empty.
		RuleFor(a => a.Model.Surname).MinimumLength(2).NotEmpty();
	}
}

