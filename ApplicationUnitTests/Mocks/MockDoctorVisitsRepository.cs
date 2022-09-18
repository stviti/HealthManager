using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Models;
using Application.Models.Responses;
using Domain.Entities.DoctorVisit;
using Moq;

namespace ApplicationUnitTests.Mocks
{
    public static class MockDoctorVisitsRepository 
    {
        public static Mock<IDoctorVisitsRepository> GetRepository()
        {
            var entities = new List<DoctorVisitEntity>
            {
                new DoctorVisitEntity
                {
                    Id = new Guid("0dd7277d-c2d2-48e6-ba48-4e19b52dcd05"),
                    DateTime = DateTime.Now,
                    Address = "Test Address 1",
                    Notes = "Test Notes 1"
                },
                new DoctorVisitEntity
                {
                    Id = new Guid("101967a9-e5eb-4a52-a157-8352c7063b6f"),
                    DateTime = DateTime.Now,
                    Address = "Test Address 2",
                    Notes = "Test Notes 2"
                },
                new DoctorVisitEntity
                {
                    Id = new Guid("53f2bf62-4329-470f-9495-66dc9a4baec3"),
                    DateTime = DateTime.Now,
                    Address = "Test Address 3",
                    Notes = "Test Notes 3"
                }
            };

            var mockRepo = new Mock<IDoctorVisitsRepository>();

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync((CancellationToken cancellationToken) => { 
                return entities;
                });

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<PaginatedFilter>(), It.IsAny<CancellationToken>())).ReturnsAsync((PaginatedFilter paginatedFilter, CancellationToken cancellationToken) =>
            {
                return new PaginationResponse<DoctorVisitEntity> { Data = entities };
            });

            mockRepo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Guid id, CancellationToken cancellationToken) =>
            {
                return entities.FirstOrDefault(i => i.Id == id);
            });

            mockRepo.Setup(r => r.AddAsync(It.IsAny<DoctorVisitEntity>(), It.IsAny<CancellationToken>())).ReturnsAsync((DoctorVisitEntity entity, CancellationToken cancellationToken) =>
            {
                entities.Add(entity);
                return entity;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<DoctorVisitEntity>())).Callback((DoctorVisitEntity entity) =>
            {
                entities.Remove(entity);
            });

            mockRepo.Setup(r => r.Update(It.IsAny<DoctorVisitEntity>())).Callback((DoctorVisitEntity entity) =>
            {
                var item = entities.FirstOrDefault(i => i.Id == entity.Id);
                item = entity;
            });

            return mockRepo;
        }
    }

}
