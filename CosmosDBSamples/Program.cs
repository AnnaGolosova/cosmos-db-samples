using CosmosDBSamples;
using CosmosDBSamples.Repositories;

var configurations = new CosmosAccountConfigurations
{
    Endpoint = "",
    PrimaryKey = "",
    DatabaseId = "",
    ContainerId = "",
};

var documentId = "";
var partitionKey = "";

var httpRepository = new CosmosHttpQueryRepository(configurations);
var cosmosSdkRepository = new CosmosSdkQueryRepository(configurations);

var documentFromHttp = await httpRepository.GetDocumentAsync(documentId, partitionKey);
Console.WriteLine("Document that was retrieved using HttClient:");
Console.WriteLine(documentFromHttp);
Console.WriteLine();

var documentFromCosmosSdk = await cosmosSdkRepository.GetDocumentAsync(documentId, partitionKey);
Console.WriteLine("Document that was retrieved using Cosmos SDK:");
Console.WriteLine(documentFromCosmosSdk);