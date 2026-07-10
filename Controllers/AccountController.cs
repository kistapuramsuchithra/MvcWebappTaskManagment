//using Microsoft.AspNetCore.Mvc;

//namespace MVCTaskManagmentApp.Controllers
//{
//    public class AccountController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using MVCTaskManagmentApp.Data;
using MVCTaskManagmentApp.Models;

namespace MVCTaskManagmentApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(x =>
                x.Username == model.Username &&
                x.Password == model.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View(model);
            }

            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Tasks");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Users user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var existingUser = _context.Users.FirstOrDefault(x => x.Username == user.Username);

            if (existingUser != null)
            {
                ViewBag.Error = "Username already exists.";
                return View(user);
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Registration successful. Please login.";

            return RedirectToAction("Login");
        }
    }
}