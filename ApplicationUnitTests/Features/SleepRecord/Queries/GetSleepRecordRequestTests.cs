using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.SleepRecord;
using Application.Exceptions;
using Application.Features.SleepRecord.Queries;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.SleepRecord.Queries
{
    public class GetSleepRecordRequestTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ISleepRecordsRepository> _mockRepository;
        private readonly GetSleepRecordRequestHandler _handler;

        public GetSleepRecordRequestTests()
        {
            _mockRepository = MockSleepRecordsRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetSleepRecordRequestHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Get_SleepRecord_Item_Valid_Id_Returns_Dto()
        {
            var command = new GetSleepRecordRequest { Id = Guid.Parse("837b95af-9a84-4cd1-8daa-b10468aee5d5") };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<SleepRecordDto>();

            result.Id.ToString().ShouldBe("837b95af-9a84-4cd1-8daa-b10468aee5d5");
        }

        [Fact]
        public async Task Get_SleepRecord_Item_InValid_Id_Throws_NotFoundException()
        {
            var command = new GetSleepRecordRequest { Id = Guid.NewGuid() };

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
