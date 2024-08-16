namespace Api.Models
{
    public class EditPersonalInfoRequest
    {
        public string Name { get; set; }
        public Address[] Addresses { get; set; }
    }
}
