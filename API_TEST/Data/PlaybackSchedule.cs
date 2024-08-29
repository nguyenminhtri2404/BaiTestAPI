namespace API_TEST.Data
{
    public class PlaybackSchedule
    {
        public int ID { get; set; }
        public int RecordingID { get; set; }
        public Recording Recording { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime RepeatStart { get; set; }
        public DateTime RepeatEnd { get; set; }
        public List<DayOfWeek> RepeatDays { get; set; }
    }
}
