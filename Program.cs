using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/user", () => new { name = "test", age = 25 });
app.MapGet("/addheader", (HttpResponse response) => response.Headers.Add("Test", "Content"));


ProductRepository productRepository = new ProductRepository();

app.MapPost("/product", (Product product) =>
{
  productRepository.Add(product);
  return Results.Created("/product", product.Code);
});

app.MapGet("/product", ([FromQuery] string? dateStart, [FromQuery] string? dateEnd) =>
{
  return productRepository.GetAll();
});

app.MapPut("/product/{code}", ([FromRoute] string code, [FromBody] Product product) =>
{
  var productExists = productRepository.GetBy(code);

  if (productExists != null)
  {
    productExists.Name = product.Name;
    Results.NoContent();
  }

  return Results.NotFound();
});

app.MapDelete("/product/{code}", ([FromRoute] string code) =>
{
  var product = productRepository.GetBy(code);
  if (product is null)
    return Results.NotFound();

  productRepository.Remove(product);
  return Results.NoContent();

});

app.MapGet("/product/{code}", ([FromRoute] string code) =>
{
  var product = productRepository.GetBy(code);

  if (product is not null) return Results.Ok(product);

  return Results.NotFound();
});

app.Run();


public class Product
{
  public string? Code { get; set; }
  public string? Name { get; set; }
}