using System;
using System.Collections.Generic;

namespace Application.DTOs.Treatment
{
    public interface ITreatmentDto
    {
        string Name { get; set; }
        DateTime StartDate { get; set; }
        int RepeatOffset { get; set; }
        int RepeatOccurencies { get; set; }
        List<DateTime> RepeatedDates { get; set; }
        string Notes { get; set; }
    }
}
