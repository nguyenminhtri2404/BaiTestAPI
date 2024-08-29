using API_TEST.Models;
using API_TEST.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaybackScheduleController : ControllerBase
    {
        private readonly IPlaybackScheduleServices playbackScheduleServices;

        public PlaybackScheduleController(IPlaybackScheduleServices playbackScheduleServices)
        {
            this.playbackScheduleServices = playbackScheduleServices;
        }

        [HttpPost]
        public IActionResult CreatePlaybackSchedule(PlaybackScheduleRequest playbackScheduleRequest)
        {
            string? response = playbackScheduleServices.AddSchedule(playbackScheduleRequest);

            if (response != null)
            {
                return BadRequest(response);
            }

            return Ok("Success");
        }

        [HttpGet]
        public IActionResult GetPlaybackSchedules()
        {
            return Ok(playbackScheduleServices.GetSchedules());
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlaybackSchedule(int id, PlaybackScheduleRequest playbackScheduleRequest)
        {
            string? response = playbackScheduleServices.UpdateSchedule(id, playbackScheduleRequest);

            if (response != null)
            {
                return BadRequest(response);
            }

            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlaybackSchedule(int id)
        {
            string? response = playbackScheduleServices.DeleteSchedule(id);

            if (response != null)
            {
                return BadRequest(response);
            }

            return Ok("Success");
        }

        //Lấy thông tin của một lịch phát theo id
        [HttpGet("{id}")]
        public IActionResult GetPlaybackSchedule(int id)
        {
            PlaybackScheduleResponse playbackSchedule = playbackScheduleServices.GetPlaybackSchedule(id);

            return Ok(playbackSchedule);
        }

        //Lấy danh sách các lịch phát trong khoảng thời gian startDate và endDate
        [HttpGet("GetPlaybackSchedules")]
        public IActionResult GetPlaybackSchedules(DateTime startDate, DateTime endDate)
        {
            List<PlaybackScheduleResponse> playbackSchedules = playbackScheduleServices.GetSchedules(startDate, endDate);

            return Ok(playbackSchedules);
        }
    }
}
