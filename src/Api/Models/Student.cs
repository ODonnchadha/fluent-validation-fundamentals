namespace Api.Models
{
    public class Student
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address[] Addresses { get; set; }
    }
}
