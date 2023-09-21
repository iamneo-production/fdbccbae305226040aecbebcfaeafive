using Microsoft.AspNetCore.Mvc;
namespace dotnetapp.Controllers
{
    public class BatchController : Controller
    {
        private readonly ApplicationDbContext _context;
          public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
    public IActionResult AvailableBatches()
    {
        var data=_context.Batches.ToList();
        return View(data);
    }
public IActionResult BookedBatches()
{
    return View();
}
    }
}