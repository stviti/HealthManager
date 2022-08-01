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
    public class UpdateDoctorVisitCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDoctorVisitsRepository> _mockRepository;
        private readonly UpdateDoctorVisitCommandHandler _handler;

        public UpdateDoctorVisitCommandHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockRepository = MockDoctorVisitsRepository.GetRepository();

            _handler = new UpdateDoctorVisitCommandHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Update_DoctorVisit_Valid_Data_Returns_Response()
        {
            var _updateDto = new UpdateDoctorVisitDto
            {
                Id = Guid.Parse("101967a9-e5eb-4a52-a157-8352c7063b6f"),
                DateTime = DateTime.Now,
                Address = "Updated Address",
                Notes = "Updated Notes"
            };

            var command = new UpdateDoctorVisitCommand() { UpdateDto = _updateDto };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
        }

        [Fact]
        public async Task Update_DoctorVisit_InValid_Id_Throws_NotFoundException()
        {
            var _updateDto = new UpdateDoctorVisitDto
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                Address = "Updated Address",
                Notes = "Updated Notes"
            };

            var command = new UpdateDoctorVisitCommand() { UpdateDto = _updateDto };

            await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "08/18/2038 07:22:16", "address", "notes")]
        [InlineData("101967a9-e5eb-4a52-a157-8352c7063b6f", "08/18/2018 07:22:16", "address", "notes")]
        [InlineData("101967a9-e5eb-4a52-a157-8352c7063b6f", "08/18/2038 07:22:16", null, "notes")]
        [InlineData("101967a9-e5eb-4a52-a157-8352c7063b6f", "08/18/2038 07:22:16", "address", "Lorem ipsum commodo dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu")]
        public async Task Update_DoctorVisit_InValid_Data_Throws_ValidationException(string id, string dateTime, string address, string notes)
        {
            var _updateDto = new UpdateDoctorVisitDto
            {
                Id = Guid.Parse(id),
                DateTime = DateTime.Parse(dateTime),
                Address = address,
                Notes = notes
            };

            var command = new UpdateDoctorVisitCommand() { UpdateDto = _updateDto };

            await Should.ThrowAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
