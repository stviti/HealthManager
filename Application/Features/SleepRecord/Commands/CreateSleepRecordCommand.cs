using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.SleepRecord;
using Application.Exceptions;
using Application.Models.Responses;
using Application.Validators.DTOs.SleepRecord;
using AutoMapper;
using Domain.Entities.SleepRecord;
using MediatR;

namespace Application.Features.SleepRecord.Commands
{
    public class CreateSleepRecordCommand : IRequest<BaseCommandResponse>
    {
        public CreateSleepRecordDto CreateDto { get; set; }
    }


    public class CreateSleepRecordCommandHandler : IRequestHandler<CreateSleepRecordCommand, BaseCommandResponse>
    {
        private readonly ISleepRecordsRepository _repository;
        private readonly IMapper _mapper;

        public CreateSleepRecordCommandHandler(ISleepRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSleepRecordCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSleepRecordDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToDictionary());

            var newItem = _mapper.Map<SleepRecordEntity>(request.CreateDto);

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
