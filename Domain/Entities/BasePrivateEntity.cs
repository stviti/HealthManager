using System;
namespace Domain.Entities
{
    public class BasePrivateEntity : BaseEntity
    {
        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
