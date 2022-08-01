using System;
namespace Application.DTOs.Symptom
{
    public class SymptomDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
