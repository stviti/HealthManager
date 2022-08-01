using System;
namespace Application.DTOs.DoctorVisit
{
    public class CreateDoctorVisitDto : IDoctorVisitDto
    {
        public DateTime DateTime { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
    }
}
