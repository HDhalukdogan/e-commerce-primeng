
namespace MediatRApi.Application.Common.Responses
{
  public class ProductResponse
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsShow { get; set; } = true;
    public bool IsCarousel { get; set; } = false;
    public int CategoryId { get; set; }
    public ICollection<ProductImageResponse> ProductImages { get; set; } = [];
  }
  public class ProductImageResponse
  {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string FileName { get; set; } = null!;
    public bool IsCover { get; set; }
    public byte[] Image { get; set; } = [];
  }

}
