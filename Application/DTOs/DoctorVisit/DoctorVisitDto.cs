using System;
namespace Application.DTOs.DoctorVisit
{
    public class DoctorVisitDto : IBaseDto, IDoctorVisitDto
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
    }
}
