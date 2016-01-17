using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
 
namespace ConfigTest
{
    public class Settings
    {
        public string TestSetting { get; set; }
    }

    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            services.AddOptions();
            services.ConfigureOptions<Settings>(); // errors here!
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IOptions<Settings> settings)
        {
            System.Diagnostics.Debug.Write(settings.Value.TestSetting); // test value here
            app.UseIISPlatformHandler();

        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            var application = new WebApplicationBuilder()
                .UseConfiguration(WebApplicationConfiguration.GetDefault(args))
                .UseStartup<Startup>()
                .Build();

            application.Run();
        }
    }
}
