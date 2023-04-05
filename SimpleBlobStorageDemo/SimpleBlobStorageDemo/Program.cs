using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SimpleBlobStorageDemo
{
    public class Program
    {
        private static IConfigurationRoot _configuration;

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World");
            BuildOptions();

            var storageConnection = _configuration["Storage:ConnectionString"];
            Console.WriteLine($"Connection: {storageConnection}");

            // Create a BlobServiceClient object 
            var blobServiceClient = new BlobServiceClient(storageConnection);

            //create a container:

            //Create a unique name for the container
            string containerName = "images_" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            var containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);


        }

        private static void BuildOptions()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddUserSecrets<Program>()
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }
    }
}