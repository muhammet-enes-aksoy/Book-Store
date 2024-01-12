using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor;
// Validator class for CreateAuthorCommand using FluentValidation library.
public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
	public CreateAuthorCommandValidator()
	{
		// Rule: The 'Name' property of the 'Model' in CreateAuthorCommand should have a minimum length of 2 characters and should not be empty.
		RuleFor(a => a.Model.Name).MinimumLength(2).NotEmpty();

		// Rule: The 'Surname' property of the 'Model' in CreateAuthorCommand should have a minimum length of 2 characters and should not be empty.
		RuleFor(a => a.Model.Surname).MinimumLength(2).NotEmpty();

		// Rule: The 'Birthday.Date' property of the 'Model' in CreateAuthorCommand should be a date earlier than the current date.
		RuleFor(a => a.Model.Birthday.Date).LessThan(DateTime.Now.Date);
	}
}