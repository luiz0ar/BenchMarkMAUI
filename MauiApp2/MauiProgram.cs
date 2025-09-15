using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MauiApp2;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream("MauiApp2.appsettings.json");

        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();
            builder.Configuration.AddConfiguration(config);
        }
        else
        {
            throw new FileNotFoundException(
                "O arquivo de configuração 'appsettings.json' não foi encontrado como recurso inserido.",
                "MauiApp2.appsettings.json");
        }
        builder.Services.AddSingleton<BenchmarkService>();
        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}