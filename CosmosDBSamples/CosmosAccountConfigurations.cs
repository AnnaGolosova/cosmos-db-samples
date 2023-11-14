namespace CosmosDBSamples
{
    /// <summary>
    /// Configurations for Cosmos DB Account
    /// </summary>
    public class CosmosAccountConfigurations
    {
        /// <summary>
        /// The cosmos service endpoint to use. Like https://<your-account>.documents.azure.com
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// The cosmos account key or resource token to use to create the client
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Cosmos database name
        /// </summary>
        public string DatabaseId { get; set; }

        /// <summary>
        /// Cosmos container name
        /// </summary>
        public string ContainerId { get; set; }
    }
}