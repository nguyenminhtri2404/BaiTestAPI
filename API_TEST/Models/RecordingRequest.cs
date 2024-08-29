namespace API_TEST.Models
{
    public class RecordingRequest
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int GenreID { get; set; }
        public int FormatID { get; set; }
        public string Composer { get; set; }
        public TimeSpan Duration { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public IFormFile? VideoFile { get; set; }
    }
}
