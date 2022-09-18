using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Models;
using Application.Models.Responses;
using Domain.Entities.HealthRecord;
using Moq;

namespace ApplicationUnitTests.Mocks
{
    public static class MockHealthRecordsRepository
    {
        public static Mock<IHealthRecordsRepository> GetRepository()
        {
            var entities = new List<HealthRecordEntity>
            {
                new HealthRecordEntity
                {
                    Id = new Guid("f85407ba-2362-4ccc-8ac0-aa1a8f17a9d8"),
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now,
                    Notes = "Test Notes 1"
                },
                new HealthRecordEntity
                {
                    Id = new Guid("9faf528f-4849-4f43-a7ce-b63101d7b91e"),
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now,
                    Notes = "Test Notes 2"
                },
                new HealthRecordEntity
                {
                    Id = new Guid("8ea8d451-9c3d-4759-b67a-a74b575d5c0d"),
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now,
                    Notes = "Test Notes 3"
                }
            };

            var mockRepo = new Mock<IHealthRecordsRepository>();

            mockRepo.Setup(r => r.GetAllAsync(CancellationToken.None)).ReturnsAsync((CancellationToken cancellationToken)=> {
                return entities;
                });

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<PaginatedFilter>(), It.IsAny<CancellationToken>())).ReturnsAsync((PaginatedFilter paginatedFilter, CancellationToken cancellationToken) =>
            {
                return new PaginationResponse<HealthRecordEntity> { Data = entities };
            });

            mockRepo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Guid id, CancellationToken cancellationToken) =>
            {
                return entities.FirstOrDefault(i => i.Id == id);
            });

            mockRepo.Setup(r => r.AddAsync(It.IsAny<HealthRecordEntity>(), It.IsAny<CancellationToken>())).ReturnsAsync((HealthRecordEntity entity, CancellationToken cancellationToken) =>
            {
                entities.Add(entity);
                return entity;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<HealthRecordEntity>())).Callback((HealthRecordEntity entity) =>
            {
                entities.Remove(entity);
            });

            mockRepo.Setup(r => r.Update(It.IsAny<HealthRecordEntity>())).Callback((HealthRecordEntity entity) =>
            {
                var item = entities.FirstOrDefault(i => i.Id == entity.Id);
                item = entity;
            });

            return mockRepo;
        }
    }
}
