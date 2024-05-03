using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TileShop.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected string? UserId => 
        User.Identity.IsAuthenticated 
        ? User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        : null;
}
