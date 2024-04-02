using CodingAssessment.Domain.Model;
using FluentValidation;

namespace CodingAssessment.Filters
{
    public class PagedRequestFluentValidator : AbstractValidator<PagedRequest>
    {

        public PagedRequestFluentValidator()
        {
            RuleFor(x => x.pageNumber)
                                      .GreaterThan(0)
                                      .WithMessage("page number must be greater than 0.");
            RuleFor(x => x.pageSize)
                                    .GreaterThan(0)
                                    .WithMessage("page size must be greater than 0.");
        }
    }
}
