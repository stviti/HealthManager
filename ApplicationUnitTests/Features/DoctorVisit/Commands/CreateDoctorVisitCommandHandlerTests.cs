using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.DoctorVisit;
using Application.Exceptions;
using Application.Features.DoctorVisit.Commands;
using Application.Models.Responses;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.DoctorVisit.Commands
{
    public class CreateDoctorVisitCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDoctorVisitsRepository> _mockRepository;
        private readonly CreateDoctorVisitCommandHandler _handler;

        public CreateDoctorVisitCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mockRepository = MockDoctorVisitsRepository.GetRepository();

            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateDoctorVisitCommandHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Create_DoctorVisit_Valid_Data_Returns_Response()
        {
            var _createDto = new CreateDoctorVisitDto
            {
                DateTime = DateTime.Now,
                Address = "New Address",
                Notes = "New Notes"
            };

            var command = new CreateDoctorVisitCommand { CreateDto = _createDto };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAllAsync(CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();

            entities.Count.ShouldBe(4);
        }

        [Theory]
        [InlineData("08/18/2018 07:22:16", "address", "notes")]
        [InlineData("08/18/2038 07:22:16", "", "notes")]
        [InlineData("08/18/2038 07:22:16", "address", "Lorem ipsum commodo dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu")]
        public async Task Create_DoctorVisit_InValid_Data_Throws_ValidationException(string dateTime, string address, string notes)
        {
            var _createDto = new CreateDoctorVisitDto
            {
                DateTime = DateTime.Parse(dateTime),
                Address = address,
                Notes = notes,
            };

            var command = new CreateDoctorVisitCommand { CreateDto = _createDto };

            await Should.ThrowAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
