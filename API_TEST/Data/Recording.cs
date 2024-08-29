namespace API_TEST.Data
{
    public class Recording
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int? GenreID { get; set; }
        public Genre Genre { get; set; }
        public int? FormatID { get; set; }
        public Format Format { get; set; }
        public string Composer { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Thumbnail { get; set; }
        public string? VideoPath { get; set; }
        public ICollection<PlaybackSchedule> PlaybackSchedules { get; set; }
    }

}
