using Microsoft.AspNetCore.Mvc;
namespace dotnetapp.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult BatchEnrollmentForm(int id)
        {
            return View();
        }
        public IActionResult BatchEnrollmentForm(int id,string name,string email)
        {
            return View()
        }
    }
}