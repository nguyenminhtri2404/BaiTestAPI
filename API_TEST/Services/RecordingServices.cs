using API_TEST.Data;
using API_TEST.Helpers;
using API_TEST.Models;
using API_TEST.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
namespace API_TEST.Services
{
    public interface IRecordingServices
    {
        Task<string?> CreateRecording(RecordingRequest recordingRequest);
        Task<string?> UpdateRecording(int id, RecordingRequest recordingRequest);
        string? DeleteRecording(int id);
        RecordingRespone GetRecording(int id);
        List<RecordingRespone> GetRecordings();

        //Lọc theo thể loại
        List<RecordingRespone> GetRecordingsByGenre(int genreÌD);
        //Lọc theo định dạng
        List<RecordingRespone> GetRecordingsByFormat(int formatID);
    }
    public class RecordingServices : IRecordingServices
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly IValidator<RecordingRequest> validator;

        public RecordingServices(IRepositoryWrapper repositoryWrapper, IMapper mapper, IValidator<RecordingRequest> validator)
        {
            this.repository = repositoryWrapper;
            this.mapper = mapper;
            this.validator = validator;
        }

        public async Task<string?> CreateRecording(RecordingRequest request)
        {
            Recording recording = mapper.Map<Recording>(request);
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return validationResult.ToString();
            }

            string uploadDirectory = Path.Combine("wwwroot");

            string imagePath = await FileHelper.SaveFileAsync(request.Thumbnail, uploadDirectory);
            if (imagePath != null)
            {
                recording.Thumbnail = imagePath;
            }

            string videoPath = await FileHelper.SaveFileAsync(request.VideoFile, uploadDirectory);
            if (videoPath != null)
            {
                recording.VideoPath = videoPath;
            }

            repository.Recordings.Create(recording);
            repository.Save();
            return null;
        }

        public async Task<string?> UpdateRecording(int id, RecordingRequest request)
        {
            Recording? recording = repository.Recordings.FindByCondition(r => r.ID == id).FirstOrDefault();

            if (recording == null)
            {
                return "Recording not found";
            }

            mapper.Map(request, recording);

            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return validationResult.ToString();
            }

            string uploadDirectory = Path.Combine("wwwroot");

            string imagePath = await FileHelper.SaveFileAsync(request.Thumbnail, uploadDirectory);

            if (imagePath != null)
            {
                // Set the image path in the user entity
                recording.Thumbnail = imagePath;
            }

            string videoPath = await FileHelper.SaveFileAsync(request.VideoFile, uploadDirectory);
            if (videoPath != null)
            {
                recording.VideoPath = videoPath;
            }

            repository.Recordings.Update(recording);
            repository.Save();
            return null;
        }

        public string? DeleteRecording(int id)
        {
            Recording? recording = repository.Recordings.FindByCondition(r => r.ID == id).FirstOrDefault();

            if (recording == null)
            {
                return "Recording not found";
            }

            repository.Recordings.Delete(recording);
            repository.Save();
            return null;
        }

        public RecordingRespone GetRecording(int id)
        {
            RecordingRespone? recording = repository.Recordings.FindByCondition(r => r.ID == id)
                                                        .Select(r => new RecordingRespone()
                                                        {
                                                            Title = r.Title,
                                                            Thumbnail = r.Thumbnail,
                                                            VideoPath = r.VideoPath,
                                                            Genre = r.Genre.GenreName,
                                                            Composer = r.Composer,
                                                            Artist = r.Artist,
                                                            Duration = r.Duration.ToString(),
                                                            Format = r.Format.FormatType,
                                                        }).FirstOrDefault();

            if (recording == null)
            {
                return null;
            }

            return recording;
        }

        public List<RecordingRespone> GetRecordings()
        {
            List<RecordingRespone> recordings = repository.Recordings.FindAll().Select(r => new RecordingRespone()
            {
                Title = r.Title,
                Thumbnail = r.Thumbnail,
                VideoPath = r.VideoPath,
                Genre = r.Genre.GenreName,
                Composer = r.Composer,
                Artist = r.Artist,
                Duration = r.Duration.ToString(),
                Format = r.Format.FormatType,
            }).ToList();

            return mapper.Map<List<RecordingRespone>>(recordings);
        }

        public List<RecordingRespone> GetRecordingsByGenre(int genreÌD)
        {
            List<RecordingRespone> recordings = repository.Recordings.FindByCondition(r => r.GenreID == genreÌD)
                .Select(r => new RecordingRespone()
                {
                    Title = r.Title,
                    Thumbnail = r.Thumbnail,
                    VideoPath = r.VideoPath,
                    Genre = r.Genre.GenreName,
                    Composer = r.Composer,
                    Artist = r.Artist,
                    Duration = r.Duration.ToString(),
                    Format = r.Format.FormatType,
                }).ToList();

            return mapper.Map<List<RecordingRespone>>(recordings);
        }

        public List<RecordingRespone> GetRecordingsByFormat(int formatID)
        {
            List<RecordingRespone> recordings = repository.Recordings.FindByCondition(r => r.FormatID == formatID)
                .Select(r => new RecordingRespone()
                {
                    Title = r.Title,
                    Thumbnail = r.Thumbnail,
                    VideoPath = r.VideoPath,
                    Genre = r.Genre.GenreName,
                    Composer = r.Composer,
                    Artist = r.Artist,
                    Duration = r.Duration.ToString(),
                    Format = r.Format.FormatType,
                }).ToList();

            return mapper.Map<List<RecordingRespone>>(recordings);
        }
    }
}
