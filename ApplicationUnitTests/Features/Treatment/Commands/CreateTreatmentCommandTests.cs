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
    public class CreateTreatmentCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITreatmentsRepository> _mockRepository;
        private readonly CreateTreatmentCommandHandler _handler;

        public CreateTreatmentCommandTests()
        {

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mockRepository = MockTreatmentsRepository.GetRepository();

            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateTreatmentCommandHandler(_mockRepository.Object, _mapper);

        }

        [Fact]
        public async Task Create_Treatment_Valid_Data_Returns_Response()
        {
            var _createDto = new CreateTreatmentDto
            {
                Name = "New name",
                StartDate = DateTime.Now,
                RepeatOffset = 1,
                RepeatOccurencies = 5,
                Notes = "New notes"
            };

            var command = new CreateTreatmentCommand { CreateDto = _createDto };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            entities.Count.ShouldBe(4);
        }

        [Theory]
        [InlineData("", "08/18/2022 07:22:16", 1, 1, "notes")]
        [InlineData("name", "08/18/2018 07:22:16", 1, 1, "notes")]
        [InlineData("name", "08/18/2022 07:22:16", 0, 1, "notes")]
        [InlineData("name", "08/18/2022 07:22:16", 1, 0, "notes")]
        [InlineData("name", "08/18/2022 07:22:16", 1, 1, "Lorem ipsum commodo dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu")]
        public async Task Create_Treatment_InValid_Data_Throws_ValidationException(string name, string startDateTime, int repeatOffset, int repeatOccurencies, string notes)
        {
            var _createDto = new CreateTreatmentDto
            {
                Name = name,
                StartDate = DateTime.Parse(startDateTime),
                RepeatOffset = repeatOffset,
                RepeatOccurencies = repeatOccurencies,
                Notes = notes
            };

            var command = new CreateTreatmentCommand { CreateDto = _createDto };

            await Should.ThrowAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
