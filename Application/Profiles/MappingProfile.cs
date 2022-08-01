using AutoMapper;
using Application.DTOs.DoctorVisit;
using Application.DTOs.HealthRecord;
using Application.DTOs.Medication;
using Application.DTOs.SleepRecord;
using Application.DTOs.Symptom;
using Application.DTOs.Treatment;
using Domain.Entities.DoctorVisit;
using Domain.Entities.HealthRecord;
using Domain.Entities.SleepRecord;
using Domain.Entities.Treatment;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DoctorVisitEntity, DoctorVisitDto>();
            CreateMap<CreateDoctorVisitDto, DoctorVisitEntity>();
            CreateMap<UpdateDoctorVisitDto, DoctorVisitEntity>();

            CreateMap<HealthRecordEntity, HealthRecordDto>();

            CreateMap<CreateHealthRecordDto, HealthRecordEntity>();
            CreateMap<UpdateHealthRecordDto, HealthRecordEntity>();

            CreateMap<SymptomDto, SymptomEntity>().ReverseMap();
            CreateMap<MedicationDto, MedicationEntity>().ReverseMap();

            CreateMap<SleepRecordEntity, SleepRecordDto>();
            CreateMap<CreateSleepRecordDto, SleepRecordEntity>();
            CreateMap<UpdateSleepRecordDto, SleepRecordEntity>();

            CreateMap<TreatmentEntity, TreatmentDto>();
            CreateMap<CreateTreatmentDto, TreatmentEntity>();
            CreateMap<UpdateTreatmentDto, TreatmentEntity>();
        }
    }
}
