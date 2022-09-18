using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.HealthRecord;
using Application.Features.HealthRecord.Queries;
using Application.Models;
using Application.Models.Responses;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.HealthRecord.Queries
{
    public class GetHealthRecordListRequestTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IHealthRecordsRepository> _mockRepository;
        private readonly GetHealthRecordListRequestHandler _handler;

        public GetHealthRecordListRequestTests()
        {
            _mockRepository = MockHealthRecordsRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetHealthRecordListRequestHandler(_mockRepository.Object, _mapper);

        }

        [Fact]
        public async Task Get_HealthRecord_List_Valid_Data_Returns_Dto_List()
        {
            var command = new GetHealthRecordListRequest { PaginatedFilter = new PaginatedFilter { } };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAllAsync(CancellationToken.None);

            result.ShouldBeOfType<PaginationResponse<HealthRecordDto>>();

            entities.Count.ShouldBe(3);
        }
    }
}
