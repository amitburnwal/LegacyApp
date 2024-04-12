namespace LegacyApp
{
    public class Client
    {
        public int Id { get; set; }

        public ClientNameType Name { get; set; }

        public ClientStatus ClientStatus { get; set; }
    }
    public enum ClientNameType
    {
        VeryImportantClient,
        ImportantClient
    }
}
