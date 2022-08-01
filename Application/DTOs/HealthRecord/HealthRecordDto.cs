﻿using System;
using System.Collections.Generic;
using Application.DTOs.Medication;
using Application.DTOs.Symptom;

namespace Application.DTOs.HealthRecord
{
    public class HealthRecordDto : IBaseDto, IHealthRecordDto
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<SymptomDto> Symptoms { get; set; }
        public List<MedicationDto> Medications { get; set; }
        public string Notes { get; set; }
    }
}
