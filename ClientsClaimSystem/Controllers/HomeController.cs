using ClientsClaimSystem.Data;
using ClientsClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Register page
    public IActionResult Register()
    {
        return View();
    }

    // Login page
    public IActionResult Login()
    {
        return View();
    }

    // Handle Register form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(Lecturer model)
    {
        if (ModelState.IsValid)
        {
            // Hash the password for security
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Create a new Lecturer object
            var lecturer = new Lecturer
            {
                Username = model.Username,
                Email = model.Email,  // Optional: include email if needed
                Password = hashedPassword
            };

            // Add Lecturer to the database
            _context.Lecturers.Add(lecturer);
            _context.SaveChanges();

            // Redirect to Login page after successful registration
            return RedirectToAction("Login", "Home");
        }

        // Return the model to the view if validation fails
        return View(model);
    }

    // Handle Login form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string username, string password)
    {
        _logger.LogInformation($"Attempting login with Username: {username}");

        var lecturer = _context.Lecturers.FirstOrDefault(l => l.Username == username);  // Find lecturer by username

        if (lecturer != null && BCrypt.Net.BCrypt.Verify(password, lecturer.Password))  // Verify password
        {
            // Store user role in session after successful login
            HttpContext.Session.SetString("UserRole", "Lecturer");

            // Redirect to Index page after successful login
            return RedirectToAction("Index", "Home");
        }

        // Add error message if login fails
        ModelState.AddModelError("", "Invalid username or password.");
        return View();  // Return to login view if login fails
    }

    // Index page - for logged-in users only
    public IActionResult Index()
    {
        // Check if the session has an active user role
        var userRole = HttpContext.Session.GetString("UserRole");

        if (!string.IsNullOrEmpty(userRole))
        {
            // Show Index page if logged in
            return View();
        }

        // If not logged in, redirect to login page
        return RedirectToAction("Login", "Home");
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
