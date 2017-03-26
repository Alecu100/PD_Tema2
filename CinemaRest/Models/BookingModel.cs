namespace CinemaRest.Models 
{
    class BookingModel : BaseModel
    {
        public string UserID { get; set; }
        public string ScheduleID { get; set; }
        public string Seats { get; set; }
    }
}