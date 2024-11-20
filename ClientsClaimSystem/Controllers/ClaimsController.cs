using Azure.Core;
using ClientsClaimSystem.Data;
using Microsoft.AspNetCore.Mvc;
using ClientsClaimSystem.Models;

public class ClaimsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClaimsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Claims/Submit
    public IActionResult Submit()
    {
        return View(new Claim());
    }

    // POST: /Claims/Submit
    [HttpPost]
    public async Task<IActionResult> Submit(Claim claim)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState)
            {
                Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }
            return View(claim);
        }

        var file = Request.Form.Files["document"];
        if (file != null && file.Length > 0)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, file.FileName);
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                claim.DocumentPath = file.FileName;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "File upload failed: " + ex.Message);
                return View(claim);
            }
        }
        else
        {
            ModelState.AddModelError("", "Please upload a document.");
            return View(claim);
        }

        claim.SubmissionDate = DateTime.Now;
        _context.Claims.Add(claim);
        await _context.SaveChangesAsync();

        return RedirectToAction("Success", new { claimId = claim.ClaimID });
    }

    public IActionResult Success(int claimId)
    {
        var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == claimId);
        if (claim == null)
        {
            return NotFound();
        }
        return View(claim);
    }

    [Route("Claims/Approved")]
    public IActionResult ApprovedClaims()
    {
        var approvedClaims = _context.Claims
                                     .Where(c => c.Status == "Approved")
                                     .ToList();
        return View(approvedClaims); // Ensure the view is named ApprovedClaims.cshtml
    }

    [Route("Claims/Rejected")]
    public IActionResult RejectedClaims()
    {
        var rejectedClaims = _context.Claims
                                     .Where(c => c.Status == "Rejected")
                                     .ToList();
        return View(rejectedClaims); // Ensure the view is named RejectedClaims.cshtml
    }


    public IActionResult ViewClaims()
    {
        var claims = _context.Claims.ToList();
        return View(claims);
    }
}
