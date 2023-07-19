﻿
using patitas_felices.APP;
using patitas_felices.APP.View;
using Syncfusion.Maui.Core.Hosting;


namespace patitas_felices.APP
{
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
                    fonts.AddFont("Sitka.ttc", "Sitka");
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<LoginFormViewModel>();
            builder.Services.AddTransient<FeedersPage>();

            return builder.Build();
        }
    }
}