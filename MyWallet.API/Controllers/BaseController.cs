using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MyWallet.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
