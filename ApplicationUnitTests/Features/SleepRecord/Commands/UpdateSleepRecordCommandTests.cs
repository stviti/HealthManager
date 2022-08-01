using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.SleepRecord;
using Application.Exceptions;
using Application.Features.SleepRecord.Commands;
using Application.Models.Responses;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.SleepRecord.Commands
{
    public class UpdateSleepRecordCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ISleepRecordsRepository> _mockRepository;
        private readonly UpdateSleepRecordCommandHandler _handler;

        public UpdateSleepRecordCommandTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockRepository = MockSleepRecordsRepository.GetRepository();

            _handler = new UpdateSleepRecordCommandHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Update_SleepRecord_Valid_Data_Returns_Response()
        {
            var _updateDto = new UpdateSleepRecordDto
            {
                Id = Guid.Parse("837b95af-9a84-4cd1-8daa-b10468aee5d5"),
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Notes = "Updated Notes"
            };

            var command = new UpdateSleepRecordCommand() { UpdateDto = _updateDto };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
        }

        [Fact]
        public async Task Update_SleepRecord_InValid_Id_Throws_NotFoundException()
        {
            var _updateDto = new UpdateSleepRecordDto
            {
                Id = Guid.NewGuid(),
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Notes = "Updated Notes"
            };

            var command = new UpdateSleepRecordCommand() { UpdateDto = _updateDto };

            await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "08/18/2018 00:00:00", "08/18/2019 00:00:00", "notes")]
        [InlineData("837b95af-9a84-4cd1-8daa-b10468aee5d5", "08/18/2018 00:00:00", "08/18/2012 00:00:00", "notes")]
        [InlineData("837b95af-9a84-4cd1-8daa-b10468aee5d5", "08/18/2018 00:00:00", "08/18/2019 00:00:00", "Lorem ipsum commodo dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu")]
        public async Task Update_SleepRecord_InValid_Data_Throws_ValidationException(string id, string startDateTime, string endDateTime, string notes)
        {
            var _updateDto = new UpdateSleepRecordDto
            {
                Id = Guid.Parse(id),
                StartDateTime = DateTime.Parse(startDateTime),
                EndDateTime = DateTime.Parse(endDateTime),
                Notes = notes
            };

            var command = new UpdateSleepRecordCommand() { UpdateDto = _updateDto };

            await Should.ThrowAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
