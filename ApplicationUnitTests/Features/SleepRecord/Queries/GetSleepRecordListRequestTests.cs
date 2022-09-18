using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.SleepRecord;
using Application.Features.SleepRecord.Queries;
using Application.Models;
using Application.Models.Responses;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.SleepRecord.Queries
{
    public class GetSleepRecordListRequestTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ISleepRecordsRepository> _mockRepository;
        private readonly GetSleepRecordListRequestHandler _handler;

        public GetSleepRecordListRequestTests()
        {
            _mockRepository = MockSleepRecordsRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetSleepRecordListRequestHandler(_mockRepository.Object, _mapper);

        }

        [Fact]
        public async Task Get_SleepRecord_List_Valid_Data_Returns_Dto_List()
        {
            var command = new GetSleepRecordListRequest { PaginatedFilter = new PaginatedFilter { } };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAllAsync(CancellationToken.None);

            result.ShouldBeOfType<PaginationResponse<SleepRecordDto>>();

            entities.Count.ShouldBe(3);
        }
    }
}
