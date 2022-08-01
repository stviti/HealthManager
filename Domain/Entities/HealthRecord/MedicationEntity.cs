using System.Collections.Generic;

namespace Domain.Entities.HealthRecord
{
    public class MedicationEntity : BasePrivateEntity
    {
        public string Name { get; set; }

        public string Dose { get; set; }

        public List<HealthRecordEntity> HealthRecords { get; set; }
    }
}
