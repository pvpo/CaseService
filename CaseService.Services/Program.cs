using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CaseService.Services;
using Microsoft.Azure.Documents;
using CaseService.Services.Data;
using CaseService.Services.Domain;

namespace CaseService.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Commencing startup sequence");
            Console.WriteLine("1. Begin WebHostBuilder configuration");
            var host = new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>()
            .Build();

            Console.WriteLine("2. Running configured host.");
            Console.WriteLine("3. Initializing CosmosDB Connection");
            InitializeDatabaseAsync();
            host.Run();
            Console.WriteLine("Initialization complete.");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void InitializeDatabaseAsync() 
        {
            DBInitializer.Init();

            DBInitializer.RegisterDocumentCollection(Specimen.collectionName);
            DBInitializer.RegisterDocumentCollection(Requestor.collectionName);
            DBInitializer.RegisterDocumentCollection(Patient.collectionName);
            DBInitializer.RegisterDocumentCollection(Case.collectionName);
        }
    }
}
