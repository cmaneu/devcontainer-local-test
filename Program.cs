using System.Text;
using Azure.Storage.Blobs;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, remote container!");

string connectionString = "UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://host.docker.internal";
string containerName = "test-devcontainer";

// Create a client that can authenticate with a connection string
BlobServiceClient service = new BlobServiceClient(connectionString);

// Make a service request to verify we've successfully authenticated
var blobServiceProperties = await service.GetPropertiesAsync();

Console.WriteLine($"Connected to blob. {blobServiceProperties.GetRawResponse().ToString()}");

BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
await container.CreateIfNotExistsAsync();

BlobClient blob = container.GetBlobClient($"{Guid.NewGuid()}.txt");
await blob.UploadAsync(new BinaryData(Encoding.UTF8.GetBytes("Hello from container")));
Console.WriteLine("Blob created");