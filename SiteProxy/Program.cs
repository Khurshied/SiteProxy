using ProxyService.Interface;
using ProxyService.Implementation;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddSingleton<ISiteProxy>(new SiteProxy("http://localhost:5000"));

app.MapGet("/", () => "Hello World!");

app.Run();
