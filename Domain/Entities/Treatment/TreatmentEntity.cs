using System;

namespace Domain.Entities.Treatment
{
    public class TreatmentEntity : BasePrivateEntity
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public int RepeatOffset { get; set; }

        public int RepeatOccurencies { get; set; }

        public string Notes { get; set; }
    }
}