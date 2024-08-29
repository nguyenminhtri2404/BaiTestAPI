using API_TEST.Models;
using FluentValidation;

namespace API_TEST.Validation
{
    public class ScheduleValidator : AbstractValidator<PlaybackScheduleRequest>
    {

        public ScheduleValidator()
        {
            //Thời gian StartTime < EndTime
            RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithMessage("Start time must be less than end time");

            //Thời gian RepeatStart < RepeatEnd
            RuleFor(x => x.RepeatStart).LessThan(x => x.RepeatEnd).WithMessage("Repeat start must be less than repeat end");

            //RepeatDays không được rỗng
            RuleFor(x => x.RepeatDays).NotEmpty().WithMessage("Repeat days is required");

            //Thời gian StartTime và EndTime phải nằm trong khoảng 00:00 - 23:59
            RuleFor(x => x.StartTime).InclusiveBetween(new TimeSpan(0, 0, 0), new TimeSpan(23, 59, 59)).WithMessage("Start time must be between 00:00 - 23:59");
            RuleFor(x => x.EndTime).InclusiveBetween(new TimeSpan(0, 0, 0), new TimeSpan(23, 59, 59)).WithMessage("End time must be between 00:00 - 23:59");

            ////RepeatStart không được nhỏ hơn ngày hiện tại, ngày RepeatEnd không được nhỏ hơn ngày RepeatStart
            //RuleFor(x => x.RepeatStart).GreaterThanOrEqualTo(DateTime.Now).WithMessage("Repeat start must be greater than or equal to current date");
            //RuleFor(x => x.RepeatEnd).GreaterThanOrEqualTo(x => x.RepeatStart).WithMessage("Repeat end must be greater than or equal to repeat start");

        }
    }
}
