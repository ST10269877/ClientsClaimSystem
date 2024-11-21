using ClientsClaimSystem.Data;
using ClientsClaimSystem.Models;
using ClientsClaimSystem.Services; // Correct namespace for PdfService
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class HRController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PdfService _pdfService; // Inject PdfService

    // Constructor with PdfService dependency injection
    public HRController(ApplicationDbContext context, PdfService pdfService)
    {
        _context = context;
        _pdfService = pdfService;  // Assign PdfService to the controller
    }

    // Action to generate reports for approved claims
    public IActionResult GenerateReports()
    {
        // Fetching approved claims from the database
        var approvedClaims = _context.Claims
            .Where(c => c.IsApproved)
            .Select(c => new ClaimReportModel
            {
                ClaimID = c.ClaimID,
                LecturerName = c.LecturerName,
                HoursWorked = c.HoursWorked,
                HourlyRate = c.HourlyRate,
                TotalPayment = c.HoursWorked * c.HourlyRate
            })
            .ToList();

        return View(approvedClaims); // Return the list to the view
    }

    // Action to download the PDF report
    public IActionResult DownloadReport()
    {
        // Fetching approved claims from the database
        var approvedClaims = _context.Claims.Where(c => c.IsApproved).ToList();

        // Generate the PDF report using PdfService
        _pdfService.GeneratePdfReport(approvedClaims);  // Call PdfService to generate the report

        // Get the path of the generated PDF file
        var pdfPath = _pdfService.GetPdfFilePath();

        // Serve the file to the client for download
        return PhysicalFile(pdfPath, "application/pdf", "GeneratedReport.pdf");
    }
}
