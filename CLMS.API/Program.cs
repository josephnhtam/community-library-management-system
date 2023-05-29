
namespace CLMS.API {
    public class Program {
        public static async Task Main (string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureServices();

            var app = builder.Build();

            app.ConfigurePipeline();

            await app.InitializeAsync();

            await app.RunAsync();
        }
    }
}