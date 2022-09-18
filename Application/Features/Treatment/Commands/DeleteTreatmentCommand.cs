using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Treatment.Commands
{
    public class DeleteTreatmentCommand : IRequest<BaseCommandResponse>
    {
        public Guid Id { get; set; }
    }


    public class DeleteTreatmentCommandHandler : IRequestHandler<DeleteTreatmentCommand, BaseCommandResponse>
    {
        private readonly ITreatmentsRepository _repository;

        public DeleteTreatmentCommandHandler(ITreatmentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResponse> Handle(DeleteTreatmentCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException("Id cannot be empty.");

            var existingEntity = await _repository.GetAsync(request.Id, cancellationToken);
            if (existingEntity == null)
                throw new NotFoundException(nameof(existingEntity), request.Id);

            _repository.Delete(existingEntity);

            await _repository.SaveAsync(cancellationToken);

            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Entity Deleted Successfully"
            };

            return response;
        }
    }
}
