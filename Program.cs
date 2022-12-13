var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/user", () => new { name = "test", age = 25 });
app.MapGet("/addheader", (HttpResponse response) => response.Headers.Add("Test", "Content"));

app.Run();
