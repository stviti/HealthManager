using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.SleepRecord;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.SleepRecord.Queries
{
    public class GetSleepRecordRequest : IRequest<SleepRecordDto>
    {
        public Guid Id { get; set; }
    }


    public class GetSleepRecordRequestHandler : IRequestHandler<GetSleepRecordRequest, SleepRecordDto>
    {
        private readonly ISleepRecordsRepository _repository;
        private readonly IMapper _mapper;

        public GetSleepRecordRequestHandler(ISleepRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SleepRecordDto> Handle(GetSleepRecordRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException("Id cannot be empty.");

            var entity = await _repository.Get(request.Id);
            if (entity is null)
                throw new NotFoundException(nameof(entity), request.Id);

            return _mapper.Map<SleepRecordDto>(entity);
        }
    }
}
