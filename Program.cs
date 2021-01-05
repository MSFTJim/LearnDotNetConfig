using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace SampleConfig
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            
            if (String.IsNullOrEmpty(env))
            {   //the env is null so run as production
                env = "Production";
            }

            var builder = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json", true, true)
                           .AddJsonFile($"appsettings.{env}.json", true, true)
                           .AddEnvironmentVariables()
                           .AddUserSecrets<Program>();

            Configuration = builder.Build();

            string myAppSetting = Configuration["AzureKeyVaultEndpoint"];
            string myDevAppSetting = Configuration["PetName"];
            string mySecretSetting = Configuration["SqlPassword"];
            string myDynamicAppSetting = Configuration["EnvironmentValue"];

            Console.WriteLine($"My environment name is: {env}");
            Console.WriteLine($"Setting from AppSettings: {myAppSetting}");
            Console.WriteLine($"Setting from AppDevSettings: {myDevAppSetting}");
            Console.WriteLine($"Setting from Secrets: {mySecretSetting}");
            Console.WriteLine($"Setting from AppSettings or AppDevSettings : {myDynamicAppSetting}");

        }
    }
}
