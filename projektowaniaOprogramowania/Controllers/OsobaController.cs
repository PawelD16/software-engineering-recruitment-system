using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;

namespace projektowaniaOprogramowania.Controllers
{
    public class OsobaController : Controller
    {
        private readonly MyDbContext _context;

        public OsobaController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AuthenticateUser(long userId)
        {
            var user = _context.Osoby.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                HttpContext.Session.SetLong("UserId", user.Id);
                HttpContext.Session.SetString("UserName", $"{user.Imie} {user.Nazwisko}");

                return Json(new { success = true, message = "User authenticated successfully" });
            }

            return Json(new { success = false, message = "User not found" });
        }

        public IActionResult GetAllUsers()
        {
            return PartialView("_OsobaChoice", _context.Osoby.ToList());
        }

    }
}
