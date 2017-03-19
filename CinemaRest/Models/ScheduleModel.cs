namespace CinemaRest.Models 
{
    class ScheduleModel 
    {
        public string ID { get; set; }
        public string MovieID { get; set; }
        public string RoomID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}