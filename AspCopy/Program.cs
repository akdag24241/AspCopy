using AspCopy;
using AspCopy.DependencyInjection;
using AspCopy.Middlewares.Base;
using AspCopy.Middlewares.Builder;
using AspCopy.Middlewares.Internal;
using Controllers;
using System.Net;

HttpListener CreateNewListener()
{
    HttpListener listener = new HttpListener();
    // Add the prefixes.

    listener.Prefixes.Add("http://localhost:33333/");

    listener.Start();
    Console.WriteLine("Listening...");

    return listener;
}

/* DIContainer starting build */
var listener = CreateNewListener();
var diContainer = new DIContainer();

diContainer.AddSingleton(listener);
diContainer.AddSingleton<ILogger, Logger>();
diContainer.AddScoped<DepChain>();
diContainer.AddScoped<DepChain2>();
diContainer.AddScoped<DepChain3>();
diContainer.AddScoped<DepChain4>();
diContainer.AddScoped<IUserDatabase, UserDatabase>();


/* Middleware starting build */
var smb = new ServiceMethodBuilder(diContainer);
smb.Add<RequestRetriever>();
smb.Add<Loggerware>();
smb.Add<SomeMiddleware>();
smb.Add<ResponseRetriever>();
smb.Add<EmptyServiceMethod>();
await smb.Run();


