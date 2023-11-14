using System.Net;

namespace CosmosDBSamples.Repositories
{
    /// <summary>
    /// Repository implementation using <see cref="HttpClient"/>
    /// Code samples taken from Git repo https://github.com/Azure-Samples/cosmos-db-rest-samples
    /// </summary>
    public class CosmosHttpQueryRepository : ICosmosQueryRepository
    {
        private readonly CosmosAccountConfigurations _configurations;

        public CosmosHttpQueryRepository(CosmosAccountConfigurations configurations)
        {
            _configurations = configurations;
        }

        /// <inheritdoc/>
        public async Task<string> GetDocumentAsync(string documentId, string partitionKey)
        {
            var httpClient = new HttpClient();
            var method = HttpMethod.Get;
            var resourceType = ResourceType.docs;
            var resourceLink = $"dbs/{_configurations.DatabaseId}/colls/{_configurations.ContainerId}/docs/{documentId}";
            var requestDateString = DateTime.UtcNow.ToString("r");
            var auth = GenerateMasterKeyAuthorizationSignature(method, resourceType, resourceLink, requestDateString, _configurations.PrimaryKey);

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("authorization", auth);
            httpClient.DefaultRequestHeaders.Add("x-ms-date", requestDateString);
            httpClient.DefaultRequestHeaders.Add("x-ms-version", "2018-12-31");
            httpClient.DefaultRequestHeaders.Add("x-ms-documentdb-partitionkey", $"[\"{partitionKey}\"]");

            var requestUri = new Uri($"{_configurations.Endpoint}/{resourceLink}");
            var httpRequest = new HttpRequestMessage { Method = method, RequestUri = requestUri };

            var httpResponse = await httpClient.SendAsync(httpRequest);

            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            return responseContent;
        }

        string GenerateMasterKeyAuthorizationSignature(HttpMethod verb, ResourceType resourceType, string resourceLink, string date, string key)
        {
            var keyType = "master";
            var tokenVersion = "1.0";
            var payload = $"{verb.ToString().ToLowerInvariant()}\n{resourceType.ToString().ToLowerInvariant()}\n{resourceLink}\n{date.ToLowerInvariant()}\n\n";

            var hmacSha256 = new System.Security.Cryptography.HMACSHA256 { Key = Convert.FromBase64String(key) };
            var hashPayload = hmacSha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payload));
            var signature = Convert.ToBase64String(hashPayload);
            var authSet = WebUtility.UrlEncode($"type={keyType}&ver={tokenVersion}&sig={signature}");

            return authSet;
        }

        enum ResourceType
        {
            dbs,
            colls,
            docs,
            sprocs,
            pkranges,
        }
    }
}