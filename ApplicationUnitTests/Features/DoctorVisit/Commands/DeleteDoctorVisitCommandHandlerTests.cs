using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.DoctorVisit.Commands;
using Application.Models.Responses;
using ApplicationUnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.DoctorVisit.Commands
{
    public class DeleteDoctorVisitCommandHandlerTests
    {
        private readonly Mock<IDoctorVisitsRepository> _mockRepository;
        private readonly DeleteDoctorVisitCommandHandler _handler;

        public DeleteDoctorVisitCommandHandlerTests()
        {
            _mockRepository = MockDoctorVisitsRepository.GetRepository();

            _handler = new DeleteDoctorVisitCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Delete_DoctorVisit_Valid_Id_Returns_Response()
        {
            var command = new DeleteDoctorVisitCommand { Id = Guid.Parse("0dd7277d-c2d2-48e6-ba48-4e19b52dcd05") };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAllAsync(CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();

            entities.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Delete_DoctorVisit_InValid_Id_Throws_NotFoundExceptionAsync()
        {
            var command = new DeleteDoctorVisitCommand { Id = Guid.NewGuid() };

            await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
