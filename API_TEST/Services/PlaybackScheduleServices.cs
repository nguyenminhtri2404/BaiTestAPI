using API_TEST.Data;
using API_TEST.Models;
using API_TEST.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace API_TEST.Services
{
    public interface IPlaybackScheduleServices
    {
        string? AddSchedule(PlaybackScheduleRequest schedule);
        string? DeleteSchedule(int recordingID);
        string? UpdateSchedule(int id, PlaybackScheduleRequest request);
        PlaybackScheduleResponse GetPlaybackSchedule(int id);

        public List<PlaybackScheduleResponse> GetSchedules();

        //Tìm kiếm lịch phát theo ngày bắt đầu và ngày kết thúc
        List<PlaybackScheduleResponse> GetSchedules(DateTime startDate, DateTime endDate);
    }
    public class PlaybackScheduleServices : IPlaybackScheduleServices
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly IValidator<PlaybackScheduleRequest> validator;

        public PlaybackScheduleServices(IRepositoryWrapper repository, IMapper mapper, IValidator<PlaybackScheduleRequest> validator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        private bool IsTimeConflict(PlaybackScheduleRequest request)
        {
            // Lấy tất cả các lịch trùng lặp với ngày bắt đầu và ngày kết thúc lặp lại
            List<PlaybackSchedule> existingSchedules = repository.PlaybackSchedules.FindByCondition(ps =>
                ps.RepeatStart <= request.RepeatEnd && ps.RepeatEnd >= request.RepeatStart)
                .ToList();

            // Kiểm tra xung đột thời gian khi xem xét các ngày trong tuần
            foreach (PlaybackSchedule? schedule in existingSchedules)
            {
                // Kiểm tra xem có sự xung đột trong các ngày sử dụng 
                // phương thức Intersect để kiểm tra xem có ngày nào trùng nhau không khi dùng DateTime.DayOfWeek
                bool isDayConflict = schedule.RepeatDays.Intersect(request.RepeatDays).Any();
                if (isDayConflict)
                {
                    // Kiểm tra xung đột thời gian
                    bool isTimeConflict = schedule.StartTime <= request.EndTime && schedule.EndTime >= request.StartTime;
                    if (isTimeConflict)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public string? AddSchedule(PlaybackScheduleRequest schedule)
        {
            PlaybackSchedule newSchedule = mapper.Map<PlaybackSchedule>(schedule);

            if (IsTimeConflict(schedule))
            {
                return "Time conflict";
            }

            ValidationResult validationResult = validator.Validate(schedule);
            if (!validationResult.IsValid)
            {
                return validationResult.ToString();
            }

            repository.PlaybackSchedules.Create(newSchedule);
            repository.Save();
            return null;
        }

        public List<PlaybackScheduleResponse> GetSchedules()
        {
            List<PlaybackScheduleResponse> schedules = repository.PlaybackSchedules.FindAll().Select(p => new PlaybackScheduleResponse()
            {
                Title = p.Recording.Title,
                Artist = p.Recording.Artist,
                StartTime = p.StartTime,
                EndTime = p.EndTime,
                RepeatStart = p.RepeatStart,
                RepeatEnd = p.RepeatEnd,
                RepeatDays = p.RepeatDays,
            }).ToList();

            return schedules;
        }

        public string? UpdateSchedule(int id, PlaybackScheduleRequest request)
        {
            PlaybackSchedule? schedule = repository.PlaybackSchedules.FindByCondition(ps => ps.ID == id).FirstOrDefault();

            if (schedule == null)
            {
                return "Schedule not found";
            }

            //Kiểm tra xung đột thời gian
            if (IsTimeConflict(request))
            {
                return "Time conflict";
            }

            ValidationResult validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return validationResult.ToString();
            }

            //Map dữ liệu từ request vào schedule
            mapper.Map(request, schedule);

            repository.PlaybackSchedules.Update(schedule);
            repository.Save();
            return null;



        }

        public string? DeleteSchedule(int id)
        {
            PlaybackSchedule? schedule = repository.PlaybackSchedules.FindByCondition(ps => ps.ID == id).FirstOrDefault();

            if (schedule == null)
            {
                return "Schedule not found";
            }

            repository.PlaybackSchedules.Delete(schedule);
            repository.Save();
            return null;
        }

        public PlaybackScheduleResponse GetPlaybackSchedule(int id)
        {
            PlaybackScheduleResponse? schedule = repository.PlaybackSchedules.FindByCondition(ps => ps.ID == id)
                  .Select(ps => new PlaybackScheduleResponse()
                  {
                      Title = ps.Recording.Title,
                      Artist = ps.Recording.Artist,
                      StartTime = ps.StartTime,
                      EndTime = ps.EndTime,
                      RepeatStart = ps.RepeatStart,
                      RepeatEnd = ps.RepeatEnd,
                      RepeatDays = ps.RepeatDays,
                  }).FirstOrDefault();

            return schedule;
        }

        public List<PlaybackScheduleResponse> GetSchedules(DateTime startDate, DateTime endDate)
        {
            List<PlaybackScheduleResponse> schedules = repository.PlaybackSchedules.FindByCondition(ps =>
                ps.RepeatStart <= endDate && ps.RepeatEnd >= startDate)
                .Select(ps => new PlaybackScheduleResponse()
                {
                    Title = ps.Recording.Title,
                    Artist = ps.Recording.Artist,
                    StartTime = ps.StartTime,
                    EndTime = ps.EndTime,
                    RepeatStart = ps.RepeatStart,
                    RepeatEnd = ps.RepeatEnd,
                    RepeatDays = ps.RepeatDays,
                }).ToList();

            return schedules;
        }
    }
}
