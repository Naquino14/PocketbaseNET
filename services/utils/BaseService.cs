namespace PocketbaseNET.services.utils
{
    public abstract class BaseService
    {
        public Client Client { get; protected set; }

        public BaseService(Client client) => Client = client;
    }
}
