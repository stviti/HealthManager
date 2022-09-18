using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.HealthRecord;
using Application.Exceptions;
using Application.Features.HealthRecord.Commands;
using Application.Models.Responses;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.HealthRecord.Commands
{
    public class CreateHealthRecordCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IHealthRecordsRepository> _mockRepository;
        private readonly CreateHealthRecordCommandHandler _handler;

        public CreateHealthRecordCommandTests()
        {

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mockRepository = MockHealthRecordsRepository.GetRepository();

            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateHealthRecordCommandHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Create_HealthRecord_Valid_Data_Returns_Response()
        {
            var _createDto = new CreateHealthRecordDto
            {
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Notes = "New notes"
            };

            var command = new CreateHealthRecordCommand { CreateDto = _createDto };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAllAsync(CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();

            entities.Count.ShouldBe(4);
        }

        [Theory]
        [InlineData("08/18/2018 00:00:00", "08/18/2012 00:00:00", "notes")]
        [InlineData("08/18/2018 00:00:00", "08/18/2019 00:00:00", "Lorem ipsum commodo dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu")]
        public async Task Create_HealthRecord_InValid_Data_Throws_ValidationException(string startDateTime, string endDateTime, string notes)
        {
            var _createDto = new CreateHealthRecordDto
            {
                StartDateTime = DateTime.Parse(startDateTime),
                EndDateTime = DateTime.Parse(endDateTime),
                Notes = notes
            };

            var command = new CreateHealthRecordCommand { CreateDto = _createDto };

            await Should.ThrowAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
