using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.DoctorVisit;
using Application.Features.DoctorVisit.Queries;
using Application.Models;
using Application.Models.Responses;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.DoctorVisit.Queries
{
    public class GetDoctorVisitListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDoctorVisitsRepository> _mockRepository;
        private readonly GetDoctorVisitListRequestHandler _handler;

        public GetDoctorVisitListRequestHandlerTests()
        {
            _mockRepository = MockDoctorVisitsRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetDoctorVisitListRequestHandler(_mockRepository.Object, _mapper);

        }

        [Fact]
        public async Task Get_DoctorVisit_List_Valid_Data_Returns_Dto_Response()
        {
            var command = new GetDoctorVisitListRequest { PaginatedFilter = new PaginatedFilter { } };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAllAsync(CancellationToken.None);

            result.ShouldBeOfType<PaginationResponse<DoctorVisitDto>>();

            entities.Count.ShouldBe(3);
        }

    }
}
