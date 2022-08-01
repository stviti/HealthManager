using System;

namespace Domain.Entities.SleepRecord
{
    public class SleepRecordEntity : BasePrivateEntity
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Notes { get; set; }
    }
}
