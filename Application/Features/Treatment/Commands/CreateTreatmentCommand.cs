using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Treatment;
using Application.Exceptions;
using Application.Models.Responses;
using Application.Validators.DTOs.Treatment;
using AutoMapper;
using Domain.Entities.Treatment;
using MediatR;

namespace Application.Features.Treatment.Commands
{
    public class CreateTreatmentCommand : IRequest<BaseCommandResponse>
    {
        public CreateTreatmentDto CreateDto { get; set; }
    }


    public class CreateTreatmentCommandHandler : IRequestHandler<CreateTreatmentCommand, BaseCommandResponse>
    {
        private readonly ITreatmentsRepository _repository;
        private readonly IMapper _mapper;

        public CreateTreatmentCommandHandler(ITreatmentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateTreatmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTreatmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateDto, cancellationToken);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToDictionary());

            var newItem = _mapper.Map<TreatmentEntity>(request.CreateDto);

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
