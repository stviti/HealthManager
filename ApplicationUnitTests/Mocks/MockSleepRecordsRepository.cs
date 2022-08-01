using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Models;
using Application.Models.Responses;
using Domain.Entities.SleepRecord;
using Moq;



namespace ApplicationUnitTests.Mocks
{
    public static class MockSleepRecordsRepository
    {
        public static Mock<ISleepRecordsRepository> GetRepository()
        {
            var entities = new List<SleepRecordEntity>
            {
                new SleepRecordEntity
                {
                    Id = new Guid("837b95af-9a84-4cd1-8daa-b10468aee5d5"),
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now,
                    Notes = "Test Notes 1"
                },
                new SleepRecordEntity
                {
                    Id = new Guid("f354d779-266c-4f71-9e40-23ac3139c3b1"),
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now,
                    Notes = "Test Notes 2"
                },
                new SleepRecordEntity
                {
                    Id = new Guid("1f97bea2-af78-4fd0-b0cb-e72fd06fad1a"),
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now,
                    Notes = "Test Notes 3"
                }
            };

            var mockRepo = new Mock<ISleepRecordsRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(entities);

            mockRepo.Setup(r => r.GetAll(It.IsAny<PaginatedFilter>())).ReturnsAsync((PaginatedFilter paginatedFilter) =>
            {
                return new PaginationResponse<SleepRecordEntity> { Data = entities };
            });

            mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return entities.FirstOrDefault(i => i.Id == id);
            });

            mockRepo.Setup(r => r.Add(It.IsAny<SleepRecordEntity>())).ReturnsAsync((SleepRecordEntity entity) =>
            {
                entities.Add(entity);
                return entity;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<SleepRecordEntity>())).Returns((SleepRecordEntity entity) =>
            {
                entities.Remove(entity);
                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<SleepRecordEntity>())).Returns((SleepRecordEntity entity) =>
            {
                var item = entities.FirstOrDefault(i => i.Id == entity.Id);
                item = entity;
                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
