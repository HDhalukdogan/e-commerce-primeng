using MediatRApi.DTOs;
using MediatRApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace MediatRApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController(IOptions<BasicAuthenticationOption> basicAuthOption) : ControllerBase
  {
    [HttpPost]
    [ProducesResponseType<string>(200)]
    public async Task<IActionResult> Post(LoginDto loginDto)
    {
      var userName = basicAuthOption.Value.UserName;
      var password = basicAuthOption.Value.Password;

      if (loginDto.UserName != userName || loginDto.Password != password)
      {
        return Unauthorized("Invalid credentials.");
      }
      var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{password}"));

      return Ok(token);
    }
  }
}
