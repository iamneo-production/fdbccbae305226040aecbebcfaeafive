using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
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
        //
         public IActionResult BatchEnrollmentForm(Student student)
        {
            ViewBag.BatchID=student.studentid;
            ViewBag.Name=student.name;
            ViewBag.Email=student.email;
           // return View();
            return RedirectToAction("EnrollmentConfirmation");
        }
        
        public IActionResult EnrollmentConfirmation(int studentid)
        {
            return View();
        }
    }
}