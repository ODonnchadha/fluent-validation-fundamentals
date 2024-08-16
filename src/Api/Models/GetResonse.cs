namespace Api.Models
{
    public class GetResonse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address[] Addresses { get; set; }
        public CourseEnrollment[] Enrollments { get; set; }
    }
}
