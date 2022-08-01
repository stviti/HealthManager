using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.HealthRecord;
using Application.Models;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.HealthRecord.Queries
{
    public class GetHealthRecordListRequest : IRequest<PaginationResponse<HealthRecordDto>>
    {
        public PaginatedFilter PaginatedFilter { get; set; }
    }

    public class GetHealthRecordListRequestHandler : IRequestHandler<GetHealthRecordListRequest, PaginationResponse<HealthRecordDto>>
    {
        private readonly IHealthRecordsRepository _repository;
        private readonly IMapper _mapper;

        public GetHealthRecordListRequestHandler(IHealthRecordsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<HealthRecordDto>> Handle(GetHealthRecordListRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAll(request.PaginatedFilter);

            var dtos = _mapper.Map<List<HealthRecordDto>>(result.Data);

            var response = new PaginationResponse<HealthRecordDto>
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
