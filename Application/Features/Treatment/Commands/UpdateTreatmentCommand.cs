using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Treatment;
using Application.Exceptions;
using Application.Models.Responses;
using Application.Validators.DTOs.Treatment;
using AutoMapper;
using MediatR;

namespace Application.Features.Treatment.Commands
{
    public class UpdateTreatmentCommand : IRequest<BaseCommandResponse>
    {
        public UpdateTreatmentDto UpdateDto { get; set; }
    }

    public class UpdateTreatmentCommandHandler : IRequestHandler<UpdateTreatmentCommand, BaseCommandResponse>
    {
        private readonly ITreatmentsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateTreatmentCommandHandler(ITreatmentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateTreatmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTreatmentDtoValidator();
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
