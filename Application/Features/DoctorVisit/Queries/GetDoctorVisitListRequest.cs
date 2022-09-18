using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.DoctorVisit;
using Application.Models;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.DoctorVisit.Queries
{
    public class GetDoctorVisitListRequest : IRequest<PaginationResponse<DoctorVisitDto>>
    {
        public PaginatedFilter PaginatedFilter { get; set; }
    }

    public class GetDoctorVisitListRequestHandler : IRequestHandler<GetDoctorVisitListRequest, PaginationResponse<DoctorVisitDto>>
    {
        private readonly IDoctorVisitsRepository _repository;
        private readonly IMapper _mapper;

        public GetDoctorVisitListRequestHandler(IDoctorVisitsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<DoctorVisitDto>> Handle(GetDoctorVisitListRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(request.PaginatedFilter, cancellationToken);

            var dtos = _mapper.Map<List<DoctorVisitDto>>(result.Data);

            var response = new PaginationResponse<DoctorVisitDto>
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
