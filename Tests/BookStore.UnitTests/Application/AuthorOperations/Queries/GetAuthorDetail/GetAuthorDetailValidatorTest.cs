using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentAssertions;

namespace BookStore.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;
public class GetAuthorDetailValidatorTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
    {
        // Arrange
        var authorDetailQuery = new GetAuthorDetailQuery(null, null)
        {
            AuthorId = authorId
        };

        // Act 
        var validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(authorDetailQuery);

        // Assert 
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        // Arrange
        var authorDetailQuery = new GetAuthorDetailQuery(null, null)
        {
            AuthorId = 1
        };

        // Act 
        var validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(authorDetailQuery);

        // Assert 
        result.Errors.Count.Should().Be(0);
    }
}