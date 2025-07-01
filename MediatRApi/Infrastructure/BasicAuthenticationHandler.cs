using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace MediatRApi.Infrastructure
{
  public class BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, IOptions<BasicAuthenticationOption> basicAuthOption, ILoggerFactory logger, UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
  {
    private readonly BasicAuthenticationOption _basicAuthOption = basicAuthOption.Value;

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      if (!Request.Headers.TryGetValue("Authorization", out var value))
        return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

      try
      {
        var authHeader = AuthenticationHeaderValue.Parse(value);
        var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
        var credentials = Encoding.UTF8.GetString(credentialBytes).Split([':'], 2);
        var username = credentials[0];
        var password = credentials[1];

        if (_basicAuthOption.UserName != username || _basicAuthOption.Password != password)
          return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));
      }
      catch
      {
        return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
      }

      var claims = new[] {
            new Claim(ClaimTypes.Name,_basicAuthOption.UserName),
        };
      var identity = new ClaimsIdentity(claims, Scheme.Name);
      var principal = new ClaimsPrincipal(identity);
      var ticket = new AuthenticationTicket(principal, Scheme.Name);

      return Task.FromResult(AuthenticateResult.Success(ticket));
    }
  }
}
