namespace PocketbaseNET.services.utils
{
    /// <summary>
    /// The superclass of all services.
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// The client this service references.
        /// </summary>
        public Client Client { get; protected set; }

        /// <summary>
        /// Create a new service.
        /// </summary>
        /// <param name="client">The referenced client of the service.</param>
        public BaseService(Client client) => Client = client;
    }
}