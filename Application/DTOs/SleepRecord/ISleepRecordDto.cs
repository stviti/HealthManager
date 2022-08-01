using System;
namespace Application.DTOs.SleepRecord
{
    public interface ISleepRecordDto
    {
        DateTime StartDateTime { get; set; }
        DateTime EndDateTime { get; set; }
        string Notes { get; set; }
    }
}
