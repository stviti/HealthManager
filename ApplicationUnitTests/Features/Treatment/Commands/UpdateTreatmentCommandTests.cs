using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Treatment;
using Application.Exceptions;
using Application.Features.Treatment.Commands;
using Application.Models.Responses;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.Treatment.Commands
{
    public class UpdateTreatmentCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITreatmentsRepository> _mockRepository;
        private readonly UpdateTreatmentCommandHandler _handler;

        public UpdateTreatmentCommandTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockRepository = MockTreatmentsRepository.GetRepository();

            _handler = new UpdateTreatmentCommandHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Update_Treatment_Valid_Data_Returns_Response()
        {
            var _updateDto = new UpdateTreatmentDto
            {
                Id = Guid.Parse("3237fc56-6571-463d-bfb7-9e18d3afbeab"),
                Name = "Updated Name",
                StartDate = DateTime.Now,
                RepeatOffset = 1,
                RepeatOccurencies = 1,
                Notes = "Updated Notes"
            };

            var command = new UpdateTreatmentCommand() { UpdateDto = _updateDto };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
        }

        [Fact]
        public async Task Update_Treatment_InValid_Id_Throws_NotFoundException()
        {
            var _updateDto = new UpdateTreatmentDto
            {
                Id = Guid.NewGuid(),
                Name = "Updated Name",
                StartDate = DateTime.Now,
                RepeatOffset = 1,
                RepeatOccurencies = 1,
                Notes = "Updated Notes"
            };

            var command = new UpdateTreatmentCommand() { UpdateDto = _updateDto };

            await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "name", "08/18/2022 07:22:16", 1, 1, "notes")]
        [InlineData("3237fc56-6571-463d-bfb7-9e18d3afbeab", "", "08/18/2022 07:22:16", 1, 1, "notes")]
        [InlineData("3237fc56-6571-463d-bfb7-9e18d3afbeab", "name", "08/18/2018 07:22:16", 1, 1, "notes")]
        [InlineData("3237fc56-6571-463d-bfb7-9e18d3afbeab", "name", "08/18/2022 07:22:16", 0, 1, "notes")]
        [InlineData("3237fc56-6571-463d-bfb7-9e18d3afbeab", "name", "08/18/2022 07:22:16", 1, 0, "notes")]
        [InlineData("3237fc56-6571-463d-bfb7-9e18d3afbeab", "name", "08/18/2022 07:22:16", 1, 1, "Lorem ipsum commodo dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu")]
        public async Task Update_Treatment_InValid_Data_Throws_ValidationException(string id, string name, string startDateTime, int repeatOffset, int repeatOccurencies, string notes)
        {
            var _updateDto = new UpdateTreatmentDto
            {
                Id = Guid.Parse(id),
                Name = name,
                StartDate = DateTime.Parse(startDateTime),
                RepeatOffset = repeatOffset,
                RepeatOccurencies = repeatOccurencies,
                Notes = notes
            };

            var command = new UpdateTreatmentCommand() { UpdateDto = _updateDto };

            await Should.ThrowAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
