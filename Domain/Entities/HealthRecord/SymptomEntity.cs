using System.Collections.Generic;

namespace Domain.Entities.HealthRecord
{
    public class SymptomEntity : BasePrivateEntity
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        public List<HealthRecordEntity> HealthRecords { get; set; }
    }
}
