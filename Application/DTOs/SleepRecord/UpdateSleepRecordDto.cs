using System;
namespace Application.DTOs.SleepRecord
{
    public class UpdateSleepRecordDto : IBaseDto, ISleepRecordDto
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Notes { get; set; }
    }
}
