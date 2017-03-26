namespace CinemaRest.Models 
{
    class ScheduleModel : BaseModel
    {
        public string MovieID { get; set; }
        public string RoomID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}