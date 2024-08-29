using API_TEST.Data;
using API_TEST.Models;
using AutoMapper;

namespace API_TEST.Mapping
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<RecordingRequest, Recording>();
            CreateMap<Recording, RecordingRespone>().ReverseMap();

            CreateMap<PlaybackScheduleRequest, PlaybackSchedule>();
            CreateMap<PlaybackSchedule, PlaybackScheduleResponse>().ReverseMap();

        }
    }
}
