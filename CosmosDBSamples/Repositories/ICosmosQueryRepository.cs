namespace CosmosDBSamples.Repositories
{
    /// <summary>
    /// Repository to query cosmos db data
    /// </summary>
    public interface ICosmosQueryRepository
    {
        /// <summary>
        /// Returns document as Json string for provided id and partition key
        /// </summary>
        /// <returns></returns>
        public Task<string> GetDocumentAsync(string documentId, string partitionKey);
    }
}