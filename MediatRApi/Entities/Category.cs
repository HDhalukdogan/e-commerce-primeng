namespace MediatRApi.Entities
{
  public class Category
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public byte[]? Image { get; set; }
    public ICollection<Product> Products { get; set; } = [];
  }
}
