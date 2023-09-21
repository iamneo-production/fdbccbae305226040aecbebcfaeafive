using Microsoft.AspNetCore.Mvc;
namespace dotnetapp.Controllers
{
    public class BookingController : Controller
         { private readonly ApplicationDbContext _context;
          public BookingController(ApplicationDbContext context)
         {
             _context = context;
          }
   
        public IActionResult BatchEnrollmentForm(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult BatchEnrollmentForm()
        {
            return View();
        }
        [HttpPost]
         public IActionResult BatchEnrollmentForm(int id,string name,string email)
        {
            return View();
        }
        public IActionResult EnrollmentConfirmation(int studentid)
        {
            return View();
        }
    }
}