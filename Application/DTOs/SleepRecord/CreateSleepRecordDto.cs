using System;
namespace Application.DTOs.SleepRecord
{
    public class CreateSleepRecordDto : ISleepRecordDto
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Notes { get; set; }
    }
}
