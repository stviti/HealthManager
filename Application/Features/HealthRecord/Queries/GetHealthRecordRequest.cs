using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.HealthRecord;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.HealthRecord.Queries
{
    public class GetHealthRecordRequest : IRequest<HealthRecordDto>
    {
        public Guid Id { get; set; }
    }


    public class GetHealthRecordRequestHandler : IRequestHandler<GetHealthRecordRequest, HealthRecordDto>
    {
        private readonly IHealthRecordsRepository _repository;
        private readonly IMapper _mapper;

        public GetHealthRecordRequestHandler(IHealthRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HealthRecordDto> Handle(GetHealthRecordRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException("Id cannot be empty.");

            var entity = await _repository.GetAsync(request.Id, cancellationToken);
            if (entity is null)
                throw new NotFoundException(nameof(entity), request.Id);

            return _mapper.Map<HealthRecordDto>(entity);
        }
    }
}
