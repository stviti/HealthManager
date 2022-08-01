using System;

namespace Domain.Entities.DoctorVisit
{
    public class DoctorVisitEntity : BasePrivateEntity
    {
        public DateTime DateTime { get; set; }

        public string Address { get; set; }

        public string Notes { get; set; }
    }
}
