using System;
namespace Application.DTOs.DoctorVisit
{
    public interface IDoctorVisitDto
    {
        DateTime DateTime { get; set; }
        string Address { get; set; }
        string Notes { get; set; }
    }
}
