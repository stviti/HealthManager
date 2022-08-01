using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.SleepRecord;
using Application.Exceptions;
using Application.Models.Responses;
using Application.Validators.DTOs.SleepRecord;
using AutoMapper;
using MediatR;

namespace Application.Features.SleepRecord.Commands
{
    public class UpdateSleepRecordCommand : IRequest<BaseCommandResponse>
    {
        public UpdateSleepRecordDto UpdateDto { get; set; }
    }


    public class UpdateSleepRecordCommandHandler : IRequestHandler<UpdateSleepRecordCommand, BaseCommandResponse>
    {
        private readonly ISleepRecordsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSleepRecordCommandHandler(ISleepRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSleepRecordCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSleepRecordDtoValidator();
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
