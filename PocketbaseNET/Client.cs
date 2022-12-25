using System;
using PocketbaseNET.services;
using PocketbaseNET.stores;

namespace PocketbaseNET
{
    /// <summary>
    /// Pocketbase C# Client
    /// </summary>
    public class Client
    {
        /// <summary>
        /// The base PocketBase backend url address (eg. 'http://127.0.0.1.8090').
        /// </summary>
        public string BaseURL { get; set; } = "/";

        /// <summary>
        /// The hook that get triggered right before sending the fetch request, 
        /// allowing you to inspect/modify the request config.
        /// </summary>
        public Func<Dictionary<string, string>, string, Dictionary<string, string>>? BeforeSend { get; set; }

        /// <summary>
        /// The hook that get triggered after successfully sending the fetch request, 
        /// allowing you to inspect/modify the response object and its parsed data.
        /// </summary>
        public Action<HttpResponseMessage, HttpContent>? AfterSend { get; set; }

        /// <summary>
        /// Optional language code (default to `en-US`) that will be sent with 
        /// the requests to the server as `Accept-Language` header.
        /// </summary>
        public string Lang { get; set; } = "en-US";

        /// <summary>
        /// A replaceable instance of the local auth store service.
        /// </summary>
        public BaseAuthStore AuthStore { get; set; }

        /// <summary>
        /// An instance of the service that handles the <b>Settings APIs</b>.
        /// </summary>
        public SettingsService Settings { get; private set; }

        /// <summary>
        ///  An instance of the service that handles the <b>Admin APIs</b>.
        /// </summary>
        public AdminService Admins { get; private set; }

        /// <summary>
        /// An instance of the service that handles the <b>Collection APIs</b>.
        /// </summary>
        public CollectionService Collections { get; private set; }

        /// <summary>
        /// An instance of the service that handles the <b>Log APIs</b>.
        /// </summary>
        public LogService Logs { get; private set; }

        /// <summary>
        ///  An instance of the service that handles the <b>Realtime APIs</b>.
        /// </summary>
        public RealtimeService Realtime { get; private set; }

        /// <summary>
        /// An instance of the service that handles the <b>Health APIs</b>.
        /// </summary>
        public HealthService Health { get; private set; }

        /// <summary>
        /// Creare a new client.
        /// </summary>
        /// <param name="baseUrl">The base url of the Pocketbase instance.</param>
        /// <param name="authStore">Overwrite the default AuthStore.</param>
        /// <param name="lang">The language of the Pocketbase instance (default is en-US).</param>
        public Client(string? baseUrl = null, BaseAuthStore? authStore = null, string? lang = null)
        {
            BaseURL = baseUrl ?? BaseURL;
            Lang = lang ?? Lang;
            AuthStore = authStore ?? new LocalAuthStore();

            // services
            Admins = new(this);
            Collections = new(this);
            Logs = new(this);
            Settings = new(this);
            Realtime = new(this);
            Health = new(this);
        }

    }
}