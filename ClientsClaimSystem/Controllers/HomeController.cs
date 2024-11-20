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
            // Hash the password for security
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Create a new Lecturer object
            var lecturer = new Lecturer
            {
                Username = model.Username,
                Email = model.Email,  // Add email if necessary
                Password = hashedPassword
            };

            // Add Lecturer to the database
            _context.Lecturers.Add(lecturer);
            _context.SaveChanges();

            // Redirect to the Login page after successful registration
            return RedirectToAction("Login", "Home");
        }

        // Return the model to the view if validation fails
        return View(model);
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
            return RedirectToAction("Index", "Home");  // Redirect to Index page after successful login
        }

        ModelState.AddModelError("", "Invalid username or password.");  // Add error if login fails
        return View();  // Return to login view if login fails
    }

    // Index page: Use this for logged-in users
    public IActionResult Index()
    {
        // Optionally check if the user is logged in (by checking session)
        if (HttpContext.Session.GetString("UserRole") != null)
        {
            return View();  // Return Index page for logged-in users
        }

        // If not logged in, redirect to login page
        return RedirectToAction("Login");
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