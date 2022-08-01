using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.DoctorVisit;
using Application.Exceptions;
using Application.Features.DoctorVisit.Queries;
using Application.Profiles;
using ApplicationUnitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Features.DoctorVisit.Queries
{
    public class GetDoctorVisitRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDoctorVisitsRepository> _mockRepository;
        private readonly GetDoctorVisitRequestHandler _handler;

        public GetDoctorVisitRequestHandlerTests()
        {
            _mockRepository = MockDoctorVisitsRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetDoctorVisitRequestHandler(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task Get_DoctorVisit_Item_Valid_Id_Returns_Dto()
        {
            var command = new GetDoctorVisitRequest { Id = Guid.Parse("53f2bf62-4329-470f-9495-66dc9a4baec3") };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<DoctorVisitDto>();

            result.Id.ToString().ShouldBe("53f2bf62-4329-470f-9495-66dc9a4baec3");
        }

        [Fact]
        public async Task Get_DoctorVisit_Item_InValid_Id_Throws_NotFoundException()
        {
            var command = new GetDoctorVisitRequest { Id = Guid.NewGuid() };

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
