using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.IO;
using ClientsClaimSystem.Models;

namespace ClientsClaimSystem.Services
{
    public class PdfService
    {
        // Define the path for the generated PDF
        private readonly string _pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdf_reports");
        private readonly string _outputPath;

        public PdfService()
        {
            // Set the output path for the generated PDF
            _outputPath = Path.Combine(_pdfDirectory, "GeneratedReport.pdf");
        }

        public void GeneratePdfReport(List<Claim> approvedClaims)
        {
            // Check if the pdf_reports directory exists; if not, create it
            if (!Directory.Exists(_pdfDirectory))
            {
                Directory.CreateDirectory(_pdfDirectory);
            }

            // Generate the PDF document
            using (var writer = new PdfWriter(_outputPath))
            {
                using (var pdfDocument = new PdfDocument(writer))
                {
                    var document = new Document(pdfDocument);
                    document.Add(new Paragraph("Approved Claims Report"));

                    // Create a table for report data
                    var table = new Table(5); // 5 columns in your case
                    table.AddCell("Claim ID");
                    table.AddCell("Lecturer Name");
                    table.AddCell("Hours Worked");
                    table.AddCell("Hourly Rate");
                    table.AddCell("Total Payment");

                    foreach (var claim in approvedClaims)
                    {
                        table.AddCell(claim.ClaimID.ToString());
                        table.AddCell(claim.LecturerName);
                        table.AddCell(claim.HoursWorked.ToString());
                        table.AddCell(claim.HourlyRate.ToString());
                        table.AddCell(claim.TotalPayment.ToString());
                    }

                    document.Add(table);
                    document.Close();
                }
            }
        }

        // Method to get the PDF file path
        public string GetPdfFilePath()
        {
            return _outputPath;
        }
    }
}
