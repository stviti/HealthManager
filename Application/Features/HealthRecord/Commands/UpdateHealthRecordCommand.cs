using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.HealthRecord;
using Application.Exceptions;
using Application.Models.Responses;
using Application.Validators.DTOs.HealthRecord;
using AutoMapper;
using MediatR;

namespace Application.Features.HealthRecord.Commands
{
    public class UpdateHealthRecordCommand : IRequest<BaseCommandResponse>
    {
        public UpdateHealthRecordDto UpdateDto { get; set; }
    }

    public class UpdateHealthRecordCommandHandler : IRequestHandler<UpdateHealthRecordCommand, BaseCommandResponse>
    {
        private readonly IHealthRecordsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateHealthRecordCommandHandler(IHealthRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateHealthRecordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateHealthRecordDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToDictionary());

            var existingEntity = await _repository.Get(request.UpdateDto.Id);
            if (existingEntity is null)
                throw new NotFoundException(nameof(existingEntity), request.UpdateDto.Id);

            _mapper.Map(request.UpdateDto, existingEntity);

            await _repository.Update(existingEntity);
            await _repository.Save();

            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Entity Updated Successfully."
            };

            return response;
        }
    }
}
