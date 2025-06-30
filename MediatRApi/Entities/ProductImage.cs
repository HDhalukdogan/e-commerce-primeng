namespace MediatRApi.Entities
{
  public class ProductImage
  {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string FileName { get; set; } = null!;
    public bool IsCover { get; set; }
    public byte[] Image { get; set; } = [];
  }
}
