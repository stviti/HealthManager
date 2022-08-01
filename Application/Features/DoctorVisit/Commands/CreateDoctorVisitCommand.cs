using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.DoctorVisit;
using Application.Exceptions;
using Application.Models.Responses;
using Application.Validators.DTOs.DoctorVisit;
using AutoMapper;
using Domain.Entities.DoctorVisit;
using MediatR;

namespace Application.Features.DoctorVisit.Commands
{
    public class CreateDoctorVisitCommand : IRequest<BaseCommandResponse>
    {
        public CreateDoctorVisitDto CreateDto { get; set; }
    }

    public class CreateDoctorVisitCommandHandler : IRequestHandler<CreateDoctorVisitCommand, BaseCommandResponse>
    {
        private readonly IDoctorVisitsRepository _repository;
        private readonly IMapper _mapper;

        public CreateDoctorVisitCommandHandler(IDoctorVisitsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDoctorVisitCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDoctorVisitDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToDictionary());

            var newItem = _mapper.Map<DoctorVisitEntity>(request.CreateDto);

            await _repository.Add(newItem);
            await _repository.Save();

            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Entity Created Successfully"
            };

            return response;
        }
    }

}
