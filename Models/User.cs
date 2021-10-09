namespace vtb_backend.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    //we don't need the name to login, so let's omit it here
    public class loginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}