using Microsoft.AspNetCore.Mvc;
namespace dotnetapp.Controllers
{
    public class BatchController : Controller
    {
public IActionResult AvailableBatches()
{
    return View();
}
public IActionResult BookedBatches()
{
    return View();
}
    }
}