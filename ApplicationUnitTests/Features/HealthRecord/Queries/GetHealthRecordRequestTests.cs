using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.HealthRecord;
using Application.Exceptions;
using Application.Features.HealthRecord.Queries;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.HealthRecord.Queries
{
    public class GetHealthRecordRequestTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IHealthRecordsRepository> _mockRepository;
        private readonly GetHealthRecordRequestHandler _handler;

        public GetHealthRecordRequestTests()
        {
            _mockRepository = MockHealthRecordsRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetHealthRecordRequestHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Get_HealthRecord_Item_Valid_Id_Returns_Dto()
        {
            var command = new GetHealthRecordRequest { Id = Guid.Parse("f85407ba-2362-4ccc-8ac0-aa1a8f17a9d8") };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<HealthRecordDto>();

            result.Id.ToString().ShouldBe("f85407ba-2362-4ccc-8ac0-aa1a8f17a9d8");
        }

        [Fact]
        public async Task Get_HealthRecord_Item_InValid_Id_Throws_NotFoundException()
        {
            var command = new GetHealthRecordRequest { Id = Guid.NewGuid() };

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
