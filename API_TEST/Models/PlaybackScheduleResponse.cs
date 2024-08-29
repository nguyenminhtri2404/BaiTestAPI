namespace API_TEST.Models
{
    public class PlaybackScheduleResponse
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime RepeatStart { get; set; }
        public DateTime RepeatEnd { get; set; }
        public List<DayOfWeek> RepeatDays { get; set; }
    }
}
