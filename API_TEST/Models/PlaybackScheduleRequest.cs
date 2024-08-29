namespace API_TEST.Models
{
    public class PlaybackScheduleRequest
    {
        public int RecordingID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime RepeatStart { get; set; }
        public DateTime RepeatEnd { get; set; }
        public List<DayOfWeek> RepeatDays { get; set; } //1: Monday, 2: Tuesday, 3: Wednesday, 4: Thursday, 5: Friday, 6: Saturday, 0: Sunday
    }
}
