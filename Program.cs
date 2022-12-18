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
});

app.MapGet("/product", ([FromQuery] string? dateStart, [FromQuery] string? dateEnd) =>
{
  return productRepository.GetAll();
});

app.MapPut("/product/{code}", ([FromRoute] string code, [FromBody] Product product) =>
{
  var productExists = productRepository.GetBy(code);
  productExists.Name = product.Name;
});

app.MapDelete("/product/{code}", ([FromRoute] string code) =>
{
  var product = productRepository.GetBy(code);
  if (product is null)
    throw new Exception("Produto não encontrado");

  productRepository.Remove(product);

});

app.MapGet("/product/{code}", ([FromRoute] string code) =>
{
  return productRepository.GetBy(code);
});

app.Run();


public class Product
{
  public string? Code { get; set; }
  public string? Name { get; set; }
}