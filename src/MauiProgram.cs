using Brilliant.Interfaces;
using Brilliant.Models;
using Brilliant.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace Brilliant;

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

        Board board = new Board();
        builder.Services.AddSingleton<IBoardService>(BoardService.GetInstance(board));
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
