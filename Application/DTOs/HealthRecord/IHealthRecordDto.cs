using System;
using System.Collections.Generic;
using Application.DTOs.Medication;
using Application.DTOs.Symptom;

namespace Application.DTOs.HealthRecord
{
    public interface IHealthRecordDto
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        List<SymptomDto> Symptoms { get; set; }
        List<MedicationDto> Medications { get; set; }
        string Notes { get; set; }
    }
}
