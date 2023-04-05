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
            //Console.WriteLine($"Connection: {storageConnection}");

            // Create a BlobServiceClient object 
            var blobServiceClient = new BlobServiceClient(storageConnection);

            //create a container:

            //Create a unique name for the container
            string containerName = "images";

            // Create the container if it doesn't exist
            var exists = false;
            var containers = blobServiceClient.GetBlobContainers().AsPages();
            foreach (var containerPage in containers)
            {
                foreach (var containerItem in containerPage.Values)
                {
                    if (containerItem.Name.Equals("notes"))
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists) break;
            }
            if (!exists)
            {
                blobServiceClient.CreateBlobContainer(containerName);
            }
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            // Create a local file in the ./data/ directory for uploading and downloading

            
            //upload
            var path = "./images/puertovallarta.jpg";
            var blobClient = containerClient.GetBlobClient("beach.jpg");
            var fileBytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(fileBytes);
            blobClient.Upload(ms, overwrite: true);

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