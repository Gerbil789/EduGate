namespace WebApplicationMVC
{
  public class CartService
  {
    private List<Product> Products = new List<Product>();

    public void Add(Product product)
    {
      this.Products.Add(product);
    }

    public void Remove(int id)
    {
      this.Products.RemoveAll(p => p.Id == id);
    }

    public List<Product> List()
    {
      return this.Products;
    }

    public int Count()
    {
      return this.Products.Count;
    }
  }
}
