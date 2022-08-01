using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Treatment;
using Application.Models;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Treatment.Queries
{
    public class GetTreatmentListRequest : IRequest<PaginationResponse<TreatmentDto>>
    {
        public PaginatedFilter PaginatedFilter { get; set; }
    }

    public class GetTreatmentListRequestHandler : IRequestHandler<GetTreatmentListRequest, PaginationResponse<TreatmentDto>>
    {
        private readonly ITreatmentsRepository _repository;
        private readonly IMapper _mapper;

        public GetTreatmentListRequestHandler(ITreatmentsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<TreatmentDto>> Handle(GetTreatmentListRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAll(request.PaginatedFilter);

            var dtos = _mapper.Map<List<TreatmentDto>>(result.Data);

            var response = new PaginationResponse<TreatmentDto>
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
