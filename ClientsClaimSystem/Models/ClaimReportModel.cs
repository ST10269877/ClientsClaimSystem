namespace ClientsClaimSystem.Models
{
    public class ClaimReportModel
    {
        public int ClaimID { get; set; }
        public string? LecturerName { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalPayment { get; set; }
    }
}