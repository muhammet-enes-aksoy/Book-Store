using FluentValidation;

namespace BookStore.BookOperations.CreateBook;
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        // CreateBookCommand validation rules

        // check if genre id is greater than 0
        RuleFor(command => command.Model.GenreId).GreaterThan(0);

        // check if page count is greater than 0
        RuleFor(command => command.Model.PageCount).GreaterThan(0);

        // check if publish date is not empty and less than today
        RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);

        // check if title is not empty and minimum length is 4
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
    }
}