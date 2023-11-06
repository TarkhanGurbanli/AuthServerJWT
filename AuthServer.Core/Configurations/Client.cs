namespace AuthServer.Core.Configurations
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }

        //Api lardan hansina eriseceyini gosterecek
        public List<string> Audiences { get; set; }
    }
}
