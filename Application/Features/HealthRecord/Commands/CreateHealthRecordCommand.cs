using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.HealthRecord;
using Application.Exceptions;
using Application.Models.Responses;
using Domain.Entities.HealthRecord;
using AutoMapper;
using Application.Validators.DTOs.HealthRecord;
using MediatR;

namespace Application.Features.HealthRecord.Commands
{
    public class CreateHealthRecordCommand : IRequest<BaseCommandResponse>
    {
        public CreateHealthRecordDto CreateDto { get; set; }
    }

    public class CreateHealthRecordCommandHandler : IRequestHandler<CreateHealthRecordCommand, BaseCommandResponse>
    {
        private readonly IHealthRecordsRepository _repository;
        private readonly IMapper _mapper;

        public CreateHealthRecordCommandHandler(IHealthRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateHealthRecordCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateHealthRecordDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToDictionary());

            var newItem = _mapper.Map<HealthRecordEntity>(request.CreateDto);

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
