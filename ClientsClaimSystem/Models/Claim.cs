using System;

namespace ClientsClaimSystem.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public string? LecturerName { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime SubmissionDate { get; set; }
        public string? DocumentPath { get; set; }
        public bool IsApproved { get; set; } // Add this property

    }
}
