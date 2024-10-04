//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Brokers.DateTimes;
using LMS.Web.Brokers.Loggings;
using LMS.Web.Brokers.Storages;
using LMS.Web.Components;
using LMS.Web.Services.Foundations;
using LMS.Web.Services.Foundations.Users;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddTransient<IStorageBroker, StorageBroker>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
        builder.Services.AddTransient<IDateTimeBroker,DateTimeBroker >();


        var app = builder.Build();
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
