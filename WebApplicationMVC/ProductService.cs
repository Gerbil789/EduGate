namespace WebApplicationMVC
{
  public class ProductService
  {
    public IEnumerable<Product> List()
    {
      return Product.GetProducts();
    }

    public Product Find(int id)
    {
      return Product.GetProducts().FirstOrDefault(p => p.Id == id);
    }
  }
}
