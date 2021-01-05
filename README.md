# SampleConfig
Sample C# console app showing basics of using configuration, including appsettings, environment and secrets.


	• Launch new Codespace
		○ Readme, gitignore, etc
	• Add some Extensions
		○ C#
		○ .NET Core User Secrets
		○ Git Graph
	• Dotnet new console
		○ Optionally use -o or -n 
	• Step 1
		○ Update launch.json with environment variable
		○ "env" : 
	                {
	                    "ASPNETCORE_ENVIRONMENT": "Development",
	                    "DOTNET_ENVIRONMENT" : "Development"
	                }
		○ var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
		○ if (String.IsNullOrEmpty(env))
	        {
	            //the env is null so run as production
	            env = "Production";
	        }
		
		○ Console.WriteLine($"My environment name is: {env}");
	• Step 2
		○ dotnet add package Microsoft.Extensions.Configuration 
		○ dotnet add package Microsoft.Extensions.Configuration.Json
		○ dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
		○ dotnet add package Microsoft.Extensions.Configuration.UserSecrets
		
		○ using Microsoft.Extensions.Configuration;
		○ using Microsoft.Extensions.Configuration.UserSecrets;
		
		○ public static IConfiguration Configuration { get; set; }
			§ Add above main
		○ var builder = new ConfigurationBuilder()
		                            .AddJsonFile("appsettings.json", true, true)   
		                            .AddJsonFile($"appsettings.{env}.json", true, true)
		                            .AddEnvironmentVariables()
		                            .AddUserSecrets<Program>();
		○ Configuration = builder.Build();
		○ string myAppSetting = Configuration["AzureKeyVaultEndpoint"];
		○ string myDevAppSetting = Configuration["PetName"];
		○ string mySecretSetting = Configuration["SqlPassword"];
		○ string myDynamicAppSetting = Configuration["EnvironmentValue"];
		○ Console.WriteLine($"Setting from AppSettings: {myAppSetting}");
		○ Console.WriteLine($"Setting from AppDevSettings: {myDevAppSetting}");
		○ Console.WriteLine($"Setting from Secrets: {mySecretSetting}");
		○ Console.WriteLine($"Setting from AppSettings or AppDevSettings : {myDynamicAppSetting}");
		
		○ Add to csproj file:
			§ <ItemGroup>
			§     <None Update="appsettings*.json">
			§     <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
			§     </None>    
			§   </ItemGroup>
		○ Add your settings files:
			§ appsettings.json
			§ appsettings.Development.json
			§ appsettings.QA.json
			§ secrets.json
		○ Update launch.json
			§ Create a new launch config and name per environment
			§ Add desired environment sections accordingly
				□  "env" : 
			            {
			                "ASPNETCORE_ENVIRONMENT": "QA",
			                "DOTNET_ENVIRONMENT" : "QA"
			            }
	
	• Step 3
		○ Support for complex sections
		○ Add to settings file
			§ "EmailSettings": {
			        "EmailTemplatesPath": "EmailTemplates",
			        "Email": "aa@aa.com"
			    },
			§ "WeatherClientConfig": {
			        "IsEnabled": true,
			        "WeatherAPIUrl": "https://localhost:12345",
			        "Timeout": 5000
			    }
		○ Add class
			§ public class WeatherClientConfig
		        {
		            public bool IsEnabled { get; set; };
		            public string WeatherAPIUrl { get; set; }
		            public string Timeout { get; set; }
				  
				  public override string ToString()
		            {                
		                return "Is Enabled: " + IsEnabled + ", WeatherAPIUrl: " + WeatherAPIUrl + ", Timeout: " + Timeout;                   }
		        }
		○ Add package
			§ dotnet add package Microsoft.Extensions.Configuration.Binder
			
		○ Add code
			§ var testValue = Configuration["EmailSettings:Email"]; 
			§ var section = Configuration.GetSection(nameof(WeatherClientConfig)); 
			§ var weatherClientConfig = section.Get<WeatherClientConfig>();
			 
			§  Console.WriteLine($"Setting from complex AppSettings via direct reference: {testValue}");
			§  Console.WriteLine($"Setting from complex AppSettings via C# class: {weatherClientConfig}");
			


