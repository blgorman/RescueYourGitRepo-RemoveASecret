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

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            BuildOptions();

            var storageConnection = _configuration["Storage:ConnectionString"];
            Console.WriteLine($"Connection: {storageConnection}");
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