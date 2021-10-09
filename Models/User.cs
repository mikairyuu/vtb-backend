namespace vtb_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        //public string Salt { get; set; } - exists only in the DB
    }
}