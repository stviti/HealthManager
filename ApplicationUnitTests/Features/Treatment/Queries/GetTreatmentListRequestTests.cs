using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Treatment;
using Application.Features.Treatment.Queries;
using Application.Models;
using Application.Models.Responses;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.Treatment.Queries
{
    public class GetTreatmentListRequestTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITreatmentsRepository> _mockRepository;
        private readonly GetTreatmentListRequestHandler _handler;

        public GetTreatmentListRequestTests()
        {
            _mockRepository = MockTreatmentsRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetTreatmentListRequestHandler(_mockRepository.Object, _mapper);

        }

        [Fact]
        public async Task Get_Treatment_List_Valid_Data_Returns_Dto_List()
        {
            var command = new GetTreatmentListRequest { PaginatedFilter = new PaginatedFilter { } };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAllAsync(CancellationToken.None);

            result.ShouldBeOfType<PaginationResponse<TreatmentDto>>();

            entities.Count.ShouldBe(3);
        }
    }
}
