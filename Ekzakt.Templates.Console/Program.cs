using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EmailTemplateProvider.Console;
using Ekzakt.Templates.Console.Utilities;

var services = new ServiceCollection();

var host = BuildHost(services);


var runner = host.Services.GetRequiredService<TaskRunner>();
var ch = host.Services.GetRequiredService<ConsoleHelpers>();


List<string> taskList = new()
{
    "Do something."
};


while (true)
{
    var key = ch.WriteTaskList(taskList);

    switch (key.Key)
    {
        case ConsoleKey.A:
            await runner.DoSomething();
            break;
        default:
            break;
    }

    if (key.Key.Equals(ConsoleKey.Q))
    {
        break;
    }
}

IHost BuildHost(ServiceCollection serviceCollection)
{
    var host = Host
        .CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(config =>
            config.AddJsonFile(
                path: "appsettings.Development.json",
                optional: false,
                reloadOnChange: true)
            )
        .ConfigureServices((context, services) =>
        {
            services.AddScoped<TaskRunner>();
            services.AddScoped<ConsoleHelpers>();
        })
        .Build();

    return host;
}

