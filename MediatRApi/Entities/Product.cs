namespace MediatRApi.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsShow { get; set; } = true;
    public bool IsCarousel { get; set; } = false;
    public byte[]? Image { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
  }
}
