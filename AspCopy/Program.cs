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
var listener = CreateNewListener();
var diContainer = new DIContainer();
diContainer.Add(listener);
diContainer.Add<DepChain>();
diContainer.Add<DepChain2>();
diContainer.Add<DepChain3>();
diContainer.Add<DepChain4>();
diContainer.Add<IUserDatabase, UserDatabase>();

var smb = new ServiceMethodBuilder(diContainer);
smb.Add<RequestRetriever>();
smb.Add<ControllerInfoRetriever>();
smb.Add<Loggerware>();
smb.Add<SomeMiddleware>();
smb.Add<ResponseRetriever>();
smb.Add<EmptyServiceMethod>();
await smb.Run();


