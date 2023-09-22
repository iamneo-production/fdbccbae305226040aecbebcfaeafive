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
            ViewBag.a=id;
            return View();
        }
         
        
        [HttpPost]
         public IActionResult BatchEnrollmentForm(int id,string name,string email)
        {
            ViewBag.BatchID=id;
            ViewBag.Name=name;
            ViewBag.Email=email;
           // return View();
            return RedirectToAction("EnrollmentConfirmation");
        }
        
        public IActionResult EnrollmentConfirmation(int studentid)
        {
            return View();
        }
    }
}