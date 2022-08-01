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
    public class UpdateHealthRecordCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IHealthRecordsRepository> _mockRepository;
        private readonly UpdateHealthRecordCommandHandler _handler;

        public UpdateHealthRecordCommandTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockRepository = MockHealthRecordsRepository.GetRepository();

            _handler = new UpdateHealthRecordCommandHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Update_HealthRecord_Valid_Data_Returns_Response()
        {
            var _updateDto = new UpdateHealthRecordDto
            {
                Id = Guid.Parse("8ea8d451-9c3d-4759-b67a-a74b575d5c0d"),
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Notes = "Updated Notes"
            };

            var command = new UpdateHealthRecordCommand() { UpdateDto = _updateDto };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
        }

        [Fact]
        public async Task Update_HealthRecord_InValid_Id_Throws_NotFoundException()
        {
            var _updateDto = new UpdateHealthRecordDto
            {
                Id = Guid.NewGuid(),
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
                Notes = "Updated Notes"
            };

            var command = new UpdateHealthRecordCommand() { UpdateDto = _updateDto };

            await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "08/18/2018 00:00:00", "08/18/2019 00:00:00", "notes")]
        [InlineData("8ea8d451-9c3d-4759-b67a-a74b575d5c0d", "08/18/2018 00:00:00", "08/18/2012 00:00:00", "notes")]
        [InlineData("8ea8d451-9c3d-4759-b67a-a74b575d5c0d", "08/18/2018 00:00:00", "08/18/2019 00:00:00", "Lorem ipsum commodo dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu")]
        public async Task Update_HealthRecord_InValid_Data_Throws_ValidationException(string id, string startDateTime, string endDateTime, string notes)
        {
            var _updateDto = new UpdateHealthRecordDto
            {
                Id = Guid.Parse(id),
                StartDateTime = DateTime.Parse(startDateTime),
                EndDateTime = DateTime.Parse(endDateTime),
                Notes = notes
            };

            var command = new UpdateHealthRecordCommand() { UpdateDto = _updateDto };

            await Should.ThrowAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
