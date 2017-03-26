namespace CinemaRest.Models 
{
    class UserModel : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}