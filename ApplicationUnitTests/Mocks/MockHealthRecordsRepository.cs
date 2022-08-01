using System;
using System.Collections.Generic;
using System.Linq;
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

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(entities);

            mockRepo.Setup(r => r.GetAll(It.IsAny<PaginatedFilter>())).ReturnsAsync((PaginatedFilter paginatedFilter) =>
            {
                return new PaginationResponse<HealthRecordEntity> { Data = entities };
            });

            mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return entities.FirstOrDefault(i => i.Id == id);
            });

            mockRepo.Setup(r => r.Add(It.IsAny<HealthRecordEntity>())).ReturnsAsync((HealthRecordEntity entity) =>
            {
                entities.Add(entity);
                return entity;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<HealthRecordEntity>())).Returns((HealthRecordEntity entity) =>
            {
                entities.Remove(entity);
                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<HealthRecordEntity>())).Returns((HealthRecordEntity entity) =>
            {
                var item = entities.FirstOrDefault(i => i.Id == entity.Id);
                item = entity;
                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
