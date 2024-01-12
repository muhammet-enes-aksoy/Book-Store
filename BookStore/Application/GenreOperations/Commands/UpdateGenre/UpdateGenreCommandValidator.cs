using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre;
// Validator class for UpdateGenreCommand using FluentValidation library.
public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    // Constructor: Configures validation rules for UpdateGenreCommand.
    public UpdateGenreCommandValidator()
    {
        // Rule: The 'Name' property in the UpdateGenreModel of UpdateGenreCommand should have a minimum length of 4 characters.
        // This rule is applied only when the 'Name' property is not an empty string.
        RuleFor(command => command.Model.Name)
            .MinimumLength(4)
            .When(x => !string.IsNullOrEmpty(x.Model.Name))
            .WithMessage("Genre name must be at least 4 characters long when provided.");
    }
}

