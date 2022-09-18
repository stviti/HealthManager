using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Treatment;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.Treatment.Queries
{
    public class GetTreatmentRequest : IRequest<TreatmentDto>
    {
        public Guid Id { get; set; }
    }


    public class GetTreatmentRequestHandler : IRequestHandler<GetTreatmentRequest, TreatmentDto>
    {
        private readonly ITreatmentsRepository _repository;
        private readonly IMapper _mapper;

        public GetTreatmentRequestHandler(ITreatmentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TreatmentDto> Handle(GetTreatmentRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException("Id cannot be empty.");

            var entity = await _repository.GetAsync(request.Id, cancellationToken);
            if (entity is null)
                throw new NotFoundException(nameof(entity), request.Id);

            return _mapper.Map<TreatmentDto>(entity);
        }
    }
}
