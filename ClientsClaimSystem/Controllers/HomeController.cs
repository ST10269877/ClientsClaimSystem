using ClientsClaimSystem.Data;
using ClientsClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClientsClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Initial landing page for Register view (for Lecturer)
        public IActionResult Register()
        {
            return View();
        }

        // Login page for Lecturer
        public IActionResult Login()
        {
            return View();
        }

        // Handle Register form submission for Lecturer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Lecturer model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);  // Hash password for security
                var lecturer = new Lecturer
                {
                    Username = model.Username,
                    Email = model.Email,  // Add email if necessary
                    Password = hashedPassword
                };

                _context.Lecturers.Add(lecturer);  // Add Lecturer to the database
                _context.SaveChanges();
                return RedirectToAction("Login");  // Redirect to login page after successful registration
            }
            return View(model);  // Return the model to the view if validation fails
        }

        // Handle Login form submission for Lecturer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            _logger.LogInformation($"Attempting login with Username: {username}");
            var lecturer = _context.Lecturers.FirstOrDefault(l => l.Username == username);  // Look up the Lecturer by username

            if (lecturer != null && BCrypt.Net.BCrypt.Verify(password, lecturer.Password))  // Check if password matches
            {
                HttpContext.Session.SetString("UserRole", "Lecturer");  // Store role in session
                return RedirectToAction("Index");  // Redirect to index page or any protected page
            }

            ModelState.AddModelError("", "Invalid username or password.");  // Add error if login fails
            return View();  // Return to login view if login fails
        }

        // Redirect to the Register page when landing on the home page
        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
