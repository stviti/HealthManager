using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.SleepRecord;
using Application.Models;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.SleepRecord.Queries
{
    public class GetSleepRecordListRequest : IRequest<PaginationResponse<SleepRecordDto>>
    {
        public PaginatedFilter PaginatedFilter { get; set; }
    }

    public class GetSleepRecordListRequestHandler : IRequestHandler<GetSleepRecordListRequest, PaginationResponse<SleepRecordDto>>
    {
        private readonly ISleepRecordsRepository _repository;
        private readonly IMapper _mapper;

        public GetSleepRecordListRequestHandler(ISleepRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<SleepRecordDto>> Handle(GetSleepRecordListRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(request.PaginatedFilter, cancellationToken);

            var dtos = _mapper.Map<List<SleepRecordDto>>(result.Data);

            var response = new PaginationResponse<SleepRecordDto>
            {
                Data = dtos,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages,
                TotalCount = result.TotalCount,
                PageSize = result.PageSize
            };

            return response;
        }
    }
}
