namespace Api.Models
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address[] Addresses { get; set; }
        public string Phone { get; set; }
    }
}
