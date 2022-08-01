using System;
namespace Application.DTOs.Medication
{
    public class MedicationDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Dose { get; set; }
    }
}
