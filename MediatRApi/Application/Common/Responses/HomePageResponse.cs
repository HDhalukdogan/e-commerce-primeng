namespace MediatRApi.Application.Common.Responses
{
  public class HomePageResponse
  {
    public IEnumerable<HomeProduct> HomeProducts { get; set; } = [];
    public IEnumerable<ProductImageResponse> CarouselImages { get; set; } = [];
  }
  public class HomeProduct
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public byte[] CoverImage { get; set; } = [];
    public int CategoryId { get; set; }
    public ICollection<ProductImageResponse> ProductImages { get; set; } = [];
  }
}
