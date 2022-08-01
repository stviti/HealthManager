using System;
using System.Collections.Generic;

namespace Domain.Entities.HealthRecord
{
    public class HealthRecordEntity : BasePrivateEntity
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public List<SymptomEntity> Symptoms { get; set; }

        public List<MedicationEntity> Medications { get; set; }

        public string Notes { get; set; }

    }
}
