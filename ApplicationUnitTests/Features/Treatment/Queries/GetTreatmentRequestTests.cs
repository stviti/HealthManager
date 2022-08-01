using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Treatment;
using Application.Exceptions;
using Application.Features.Treatment.Queries;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.Treatment.Queries
{
    public class GetTreatmentRequestTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITreatmentsRepository> _mockRepository;
        private readonly GetTreatmentRequestHandler _handler;

        public GetTreatmentRequestTests()
        {
            _mockRepository = MockTreatmentsRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetTreatmentRequestHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Get_Treatment_Item_Valid_Id_Returns_Dto()
        {
            var command = new GetTreatmentRequest { Id = Guid.Parse("3237fc56-6571-463d-bfb7-9e18d3afbeab") };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<TreatmentDto>();

            result.Id.ToString().ShouldBe("3237fc56-6571-463d-bfb7-9e18d3afbeab");
        }

        [Fact]
        public async Task Get_Treatment_Item_InValid_Id_Throws_NotFoundException()
        {
            var command = new GetTreatmentRequest { Id = Guid.NewGuid() };

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
