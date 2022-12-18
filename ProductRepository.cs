public class ProductRepository
{
  public ProductRepository()
  {
    this.Products = new List<Product>();
  }
  public List<Product> Products { get; set; }

  public void Add(Product product)
  {
    Products.Add(product);
  }

  public Product GetBy(string code)
  {
    return Products.FirstOrDefault(p => p.Code == code, null);
  }

  public List<Product> GetAll()
  {
    return Products;
  }

  public void Remove(Product product)
  {
    Products.Remove(product);
  }
}