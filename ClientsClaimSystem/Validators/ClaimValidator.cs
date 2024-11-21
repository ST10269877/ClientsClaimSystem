using ClientsClaimSystem.Models;
using FluentValidation;
using ClientsClaimSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class ClaimValidator : AbstractValidator<Claim>
{
    public ClaimValidator()
    {
        RuleFor(c => c.HoursWorked)
            .InclusiveBetween(1, 200)
            .WithMessage("Hours worked must be between 1 and 200.");

        RuleFor(c => c.HourlyRate)
            .GreaterThan(0)
            .WithMessage("Hourly rate must be greater than 0.");
    }
}
