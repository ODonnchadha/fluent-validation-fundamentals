namespace Api.Repositories
{
    public class StateRepository
    {
        public string[] GetAll()
        {
            return new[] { "VA", "DC" };
        }
    }
}
