
using BookStore.BookOperations.GetBookDetail;
using FluentValidation;

namespace BookStore.Api.BookOperations.GetBookDetail;
public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        // GetBookDetailQuery validation rules

        // check if book id is greater than 0
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}
