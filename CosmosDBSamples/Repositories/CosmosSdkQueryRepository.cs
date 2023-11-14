using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;

namespace CosmosDBSamples.Repositories
{
    /// <summary>
    /// Repository implementation using <see cref="CosmosClient"/>
    /// </summary>
    public class CosmosSdkQueryRepository : ICosmosQueryRepository
    {
        private readonly CosmosAccountConfigurations _configurations;

        public CosmosSdkQueryRepository(CosmosAccountConfigurations configurations)
        {
            _configurations = configurations;
        }

        /// <inheritdoc/>
        public async Task<string> GetDocumentAsync(string documentId, string partitionKey)
        {
            CosmosClient client = new CosmosClient(_configurations.Endpoint, _configurations.PrimaryKey);

            var container = client.GetContainer(_configurations.DatabaseId, _configurations.ContainerId);

            ItemResponse<JObject> response = await container.ReadItemAsync<JObject>(
                id: documentId,
                partitionKey: new PartitionKey(partitionKey)
            );

            return response.Resource.ToString();
        }
    }
}