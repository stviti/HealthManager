using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.SleepRecord.Commands;
using Application.Models.Responses;
using ApplicationUnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.SleepRecord.Commands
{
    public class DeleteSleepRecordCommandTests
    {
        private readonly Mock<ISleepRecordsRepository> _mockRepository;
        private readonly DeleteSleepRecordCommandHandler _handler;

        public DeleteSleepRecordCommandTests()
        {
            _mockRepository = MockSleepRecordsRepository.GetRepository();

            _handler = new DeleteSleepRecordCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Delete_SleepRecord_Valid_Id_Returns_Response()
        {
            var command = new DeleteSleepRecordCommand { Id = Guid.Parse("837b95af-9a84-4cd1-8daa-b10468aee5d5") };

            var result = await _handler.Handle(command, CancellationToken.None);

            var entities = await _mockRepository.Object.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            entities.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Delete_SleepRecord_InValid_Id_Throws_NotFoundExceptionAsync()
        {
            var command = new DeleteSleepRecordCommand { Id = Guid.NewGuid() };

            await Should.ThrowAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
