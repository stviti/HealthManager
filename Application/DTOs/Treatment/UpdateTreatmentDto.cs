using System;
using System.Collections.Generic;

namespace Application.DTOs.Treatment
{
    public class UpdateTreatmentDto : IBaseDto, ITreatmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int RepeatOffset { get; set; }
        public int RepeatOccurencies { get; set; }
        public List<DateTime> RepeatedDates { get; set; }
        public string Notes { get; set; }
    }
}