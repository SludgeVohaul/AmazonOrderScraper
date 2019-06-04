using System;
using System.Net;
using System.Net.Http;
using Factories;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;
using Infrastructure.Anonymizer;
using Infrastructure.ClientHandling;
using Infrastructure.DataCollectors;
using Infrastructure.DataCollectors.WebScraping.AngleSharp.RequestHandlers.LoginUser;
using Infrastructure.DataCollectors.WebScraping.AngleSharp.RequestHandlers.LogoutUser;
using Infrastructure.DataCollectors.WebScraping.AngleSharp.RequestHandlers.OrderDetails;
using Infrastructure.DataCollectors.WebScraping.AngleSharp.WebClient;
using Infrastructure.IdentityMapping;
using Infrastructure.UserCredentials;
using InterfaceAdapters.Processors.Responses;
using InterfaceAdapters.Processors.Transactions;
using Microsoft.Extensions.Configuration;
using UseCases;
using UseCases.OrderDetails;
using UseCases.OrderDetails.RequestHandlers.LoginUser;
using UseCases.OrderDetails.RequestHandlers.LogoutUser;
using UseCases.OrderDetails.RequestHandlers.OrderDetails;

namespace ConsoleApp
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var serviceProvider = ConfigureServices(args))
            {
                var applicationController = serviceProvider.GetService<ApplicationController>();
                applicationController.RunAsync().Wait();
            }

            Console.ReadLine();
        }

        private static IConfigurationRoot CreateConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                   .SetBasePath(@"C:\Users\mn\Documents")
                   .AddJsonFile("AmazonScraperSettings2.json", false, false)
                   .AddCommandLine(args)
                   .Build();
        }

        private static ServiceProvider ConfigureServices(string[] args)
        {
            var services = new ServiceCollection();

            // Controllers.
            services.AddSingleton<ITransactionProcessor, TransactionProcessor>();
            services.AddSingleton<IOrderDetailsInteractorObserver, ResponseProcessor>();
            services.AddSingleton<ApplicationController>();

            // Client related stuff.
            AddClientServices<AngleSharpClient>();

            // Handlers.
            services.AddTransient<ILoginUserHandler, LoginUserHandler>();
            services.AddTransient<ILogoutUserHandler, LogoutUserHandler>();
            services.AddTransient<IOrderDetailsHandler, OrderDetailsHandler>();

            // Use cases.
            services.AddTransient<IOrderDetailsInteractor, OrderDetailsInteractor>();

            // Factories.
            services.AddSingleton<IInteractorFactory, InteractorFactory>();

            // Misc.
            services.AddTransient<ICredentialsCollector, CredentialsCollector>();
            services.AddTransient<IDocumentAnonymizer, DocumentAnonymizer>();

            // Configuration.
            var configuration          = CreateConfiguration(args);
            var accountMappingsSection = configuration.GetSection("IdentityMappings");
            var accountMappings        = accountMappingsSection.Get<IdentityMapping[]>();
            var accountMap             = accountMappingsSection.Get<IdentityMap>();
            accountMap.IdentityMappings = accountMappings;
            services.AddSingleton(accountMap);

            return services.BuildServiceProvider();

            void AddClientServices<TClient>() where TClient : class, IClient
            {
                // Controllers.
                services.AddSingleton<ClientController<TClient>>();
                services.AddSingleton<IClientProvider<TClient>>(x => x.GetRequiredService<ClientController<TClient>>());
                services.AddSingleton<IClientAllocator>(x => x.GetRequiredService<ClientController<TClient>>());

                // Factories.
                services.AddSingleton<IClientFactory<TClient>, ClientFactory<TClient>>();

                // Client.
                services.AddHttpClient<TClient>(c =>
                                                         {
                                                             //c.DefaultRequestHeaders.Add("Referer", "https://we-came-to-wreck-everything.com");
                                                             // FYI: Accept-Language is mandatory in order to log in.
                                                             c.DefaultRequestHeaders.Add("Accept-Language", "de-DE");
                                                         })
                        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                        {
                            AllowAutoRedirect = true,
                            UseCookies        = true,
                            CookieContainer   = new CookieContainer(),
                            //Proxy             = new WebProxy("127.0.0.1", 8888)
                        });
            }
        }
    }
}