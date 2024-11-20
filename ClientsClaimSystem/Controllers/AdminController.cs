using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClientsClaimSystem.Data;
using ClientsClaimSystem.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly ApplicationDbContext _context;

    public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Approve a claim and set IsApproved to true
    [HttpPost]
    [Route("Admin/ApproveClaim")]
    public IActionResult ApproveClaim(int claimID)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == claimID);
        if (claim != null)
        {
            claim.Status = "Approved";
            claim.IsApproved = true;  // Set IsApproved to true
            _context.SaveChanges();
        }
        return RedirectToAction("Verification"); // Redirect to the verification page
    }

    // Reject a claim and set IsApproved to false
    [HttpPost]
    [Route("Admin/RejectClaim")]
    public IActionResult RejectClaim(int claimID)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == claimID);
        if (claim != null)
        {
            claim.Status = "Rejected";
            claim.IsApproved = false;  // Set IsApproved to false
            _context.SaveChanges();
        }
        return RedirectToAction("Verification"); // Redirect to the verification page
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(Admin model)
    {
        if (ModelState.IsValid)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var admin = new Admin
            {
                Username = model.Username,
                Password = hashedPassword
            };

            _context.Admins.Add(admin);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string username, string password)
    {
        _logger.LogInformation($"Attempting login with Username: {username}");
        var admin = _context.Admins.FirstOrDefault(a => a.Username == username);

        if (admin != null && BCrypt.Net.BCrypt.Verify(password, admin.Password))
        {
            HttpContext.Session.SetString("UserRole", "Admin");
            return RedirectToAction("Verification");
        }

        ModelState.AddModelError("", "Invalid username or password.");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Verification()
    {
        var pendingClaims = await _context.Claims
            .Where(c => c.Status == "Pending")
            .ToListAsync();
        return View(pendingClaims);
    }

    [HttpGet]
    [Route("Admin/CoordinatorClaimsLanding")]
    public IActionResult CoordinatorClaimsLanding()
    {
        var claims = _context.Claims.ToList(); // Adjust this as necessary
        return View(claims); // Make sure you have a corresponding view for this
    }

    public IActionResult Logout()
    {
        // Clear the session when the user logs out
        HttpContext.Session.Clear();

        _logger.LogInformation("Admin logged out.");

        // Redirect to Home controller's Index page after logout
        return RedirectToAction("Index", "Home");
    }



}
