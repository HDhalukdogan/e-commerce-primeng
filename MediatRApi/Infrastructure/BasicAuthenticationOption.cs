namespace MediatRApi.Infrastructure
{
  public class BasicAuthenticationOption
  {
    public const string OptionsName = "BasicAuthentication";

    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
  }
}
