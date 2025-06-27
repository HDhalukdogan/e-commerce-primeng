namespace MediatRApi.Application.Common.Responses
{
  public class CategoryResponse
  {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public byte[]? Image { get; set; }
  }
}
