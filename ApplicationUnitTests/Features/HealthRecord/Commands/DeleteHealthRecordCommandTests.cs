using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.HealthRecord.Commands;
using Application.Models.Responses;
using ApplicationUnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.HealthRecord.Commands
{
    public class DeleteHealthRecordCommandTests
    {
        private readonly Mock<IHealthRecordsRepository> _mockRepository;
        private readonly DeleteHealthRecordCommandHandler _handler;

        public DeleteHealthRecordCommandTests()
        {
            _mockRepository = MockHealthRecordsRepository.GetRepository();

            _handler = new DeleteHealthRecordCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Delete_HealthRecord_Valid_Id_Returns_Response()
        {
            var command = new DeleteHealthRecordCommand { Id = Guid.Parse("f85407ba-2362-4ccc-8ac0-aa1a8f17a9d8") };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            entities.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Delete_HealthRecord_InValid_Id_Throws_NotFoundExceptionAsync()
        {
            var command = new DeleteHealthRecordCommand { Id = Guid.NewGuid() };

            await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
