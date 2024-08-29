using API_TEST.Models;
using FluentValidation;

namespace API_TEST.Validation
{
    public class RecordingValidator : AbstractValidator<RecordingRequest>
    {
        public RecordingValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50).WithMessage("Title is required");
            RuleFor(x => x.Artist).NotEmpty().MaximumLength(50).WithMessage("Artist is required");
            RuleFor(x => x.GenreID).NotEmpty().WithMessage("Genre is required");
            RuleFor(x => x.FormatID).NotEmpty().WithMessage("Format is required");
            RuleFor(x => x.Composer).MaximumLength(50).WithMessage("Composer is required");
            RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration is required");
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(new TimeSpan(0, 0, 1)).WithMessage("Duration must be greater than or equal to 1 second");
        }
    }
}
