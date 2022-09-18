using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.DoctorVisit;
using Application.Exceptions;
using Application.Models.Responses;
using Application.Validators.DTOs.DoctorVisit;
using AutoMapper;
using MediatR;

namespace Application.Features.DoctorVisit.Commands
{
    public class UpdateDoctorVisitCommand : IRequest<BaseCommandResponse>
    {
        public UpdateDoctorVisitDto UpdateDto { get; set; }
    }

    public class UpdateDoctorVisitCommandHandler : IRequestHandler<UpdateDoctorVisitCommand, BaseCommandResponse>
    {
        private readonly IDoctorVisitsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDoctorVisitCommandHandler(IDoctorVisitsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDoctorVisitCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDoctorVisitDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString());

            var existingEntity = await _repository.GetAsync(request.UpdateDto.Id, cancellationToken);
            if (existingEntity is null)
                throw new NotFoundException(nameof(existingEntity), request.UpdateDto.Id);

            _mapper.Map(request.UpdateDto, existingEntity);

            _repository.Update(existingEntity);
            await _repository.SaveAsync(cancellationToken);

            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Entity Updated Successfully."
            };

            return response;
        }
    }
}
