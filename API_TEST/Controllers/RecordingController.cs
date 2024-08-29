using API_TEST.Models;
using API_TEST.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordingController : ControllerBase
    {
        private readonly IRecordingServices recordingServices;

        public RecordingController(IRecordingServices recordingServices)
        {
            this.recordingServices = recordingServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecording([FromForm] RecordingRequest recordingRequest)
        {
            string? response = await recordingServices.CreateRecording(recordingRequest);

            if (response != null)
            {
                return BadRequest(response);
            }

            return Ok("Success");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecording(int id, [FromForm] RecordingRequest recordingRequest)
        {
            string? response = await recordingServices.UpdateRecording(id, recordingRequest);

            if (response != null)
            {
                return BadRequest(response);
            }

            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecording(int id)
        {
            string? response = recordingServices.DeleteRecording(id);

            if (response != null)
            {
                return BadRequest(response);
            }

            return Ok("Success");
        }

        [HttpGet("{id}")]
        public IActionResult GetRecording(int id)
        {
            RecordingRespone recording = recordingServices.GetRecording(id);

            return Ok(recording);
        }

        [HttpGet]
        public IActionResult GetRecordings()
        {
            List<RecordingRespone> recordings = recordingServices.GetRecordings();

            return Ok(recordings);
        }

        [HttpGet("SortGenre/{genre}")]
        public IActionResult GetRecordingsByGenre(int genreID)
        {
            List<RecordingRespone> recordings = recordingServices.GetRecordingsByGenre(genreID);

            return Ok(recordings);
        }

        [HttpGet("SortFormat/{format}")]
        public IActionResult GetRecordingsByFormat(int formatId)
        {
            List<RecordingRespone> recordings = recordingServices.GetRecordingsByFormat(formatId);

            return Ok(recordings);
        }
    }
}
