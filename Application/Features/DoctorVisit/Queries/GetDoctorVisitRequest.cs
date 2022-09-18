using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.DoctorVisit;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.DoctorVisit.Queries
{
    public class GetDoctorVisitRequest : IRequest<DoctorVisitDto>
    {
        public Guid Id { get; set; }
    }

    public class GetDoctorVisitRequestHandler : IRequestHandler<GetDoctorVisitRequest, DoctorVisitDto>
    {
        private readonly IDoctorVisitsRepository _repository;
        private readonly IMapper _mapper;

        public GetDoctorVisitRequestHandler(IDoctorVisitsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DoctorVisitDto> Handle(GetDoctorVisitRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException("Id cannot be empty.");

            var entity = await _repository.GetAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(nameof(entity), request.Id);

            return _mapper.Map<DoctorVisitDto>(entity);
        }
    }
}
