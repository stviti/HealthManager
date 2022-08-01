using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.HealthRecord.Commands
{
    public class DeleteHealthRecordCommand : IRequest<BaseCommandResponse>
    {
        public Guid Id { get; set; }
    }

    public class DeleteHealthRecordCommandHandler : IRequestHandler<DeleteHealthRecordCommand, BaseCommandResponse>
    {
        private readonly IHealthRecordsRepository _repository;

        public DeleteHealthRecordCommandHandler(IHealthRecordsRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResponse> Handle(DeleteHealthRecordCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException("Id cannot be empty.");

            var existingEntity = await _repository.Get(request.Id);

            if (existingEntity == null)
                throw new NotFoundException(nameof(existingEntity), request.Id);

            await _repository.Delete(existingEntity);

            await _repository.Save();

            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Entity Deleted Successfully"
            };

            return response;
        }
    }
}
