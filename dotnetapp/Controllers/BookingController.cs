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
            var data=from b in _context.Batches where b.BatchID==id select b;
            if(data.Capacity==5)
            throw Exception();
            return View();
        }
         
        
        [HttpPost]
        //
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