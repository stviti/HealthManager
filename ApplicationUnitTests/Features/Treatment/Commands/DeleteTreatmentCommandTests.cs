using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Treatment.Commands;
using Application.Models.Responses;
using ApplicationUnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.Treatment.Commands
{
    public class DeleteTreatmentCommandTests
    {
        private readonly Mock<ITreatmentsRepository> _mockRepository;
        private readonly DeleteTreatmentCommandHandler _handler;

        public DeleteTreatmentCommandTests()
        {
            _mockRepository = MockTreatmentsRepository.GetRepository();

            _handler = new DeleteTreatmentCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Delete_Treatment_Valid_Id_Returns_Response()
        {
            var command = new DeleteTreatmentCommand { Id = Guid.Parse("3237fc56-6571-463d-bfb7-9e18d3afbeab") };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            entities.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Delete_Treatment_InValid_Id_Throws_NotFoundExceptionAsync()
        {
            var command = new DeleteTreatmentCommand { Id = Guid.NewGuid() };

            await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
