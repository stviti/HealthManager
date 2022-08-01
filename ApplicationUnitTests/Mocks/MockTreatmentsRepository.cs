using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Models;
using Application.Models.Responses;
using Domain.Entities.Treatment;
using Moq;

namespace ApplicationUnitTests.Mocks
{
    public static class MockTreatmentsRepository
    {
        public static Mock<ITreatmentsRepository> GetRepository()
        {
            var entities = new List<TreatmentEntity>
            {
                new TreatmentEntity
                {
                    Id = new Guid("3237fc56-6571-463d-bfb7-9e18d3afbeab"),
                    StartDate = DateTime.Now,
                    RepeatOffset = 1,
                    RepeatOccurencies = 5,
                    Notes = "Test Notes 1"
                },
                new TreatmentEntity
                {
                    Id = new Guid("5b56d307-3922-4743-9021-f0337d943505"),
                    StartDate = DateTime.Now,
                    RepeatOffset = 1,
                    RepeatOccurencies = 5,
                    Notes = "Test Notes 2"
                },
                new TreatmentEntity
                {
                    Id = new Guid("57fff2c6-0d80-4559-aaa1-181d11dca3d8"),
                    StartDate = DateTime.Now,
                    RepeatOffset = 1,
                    RepeatOccurencies = 5,
                    Notes = "Test Notes 3"
                }
            };

            var mockRepo = new Mock<ITreatmentsRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(entities);

            mockRepo.Setup(r => r.GetAll(It.IsAny<PaginatedFilter>())).ReturnsAsync((PaginatedFilter paginatedFilter) =>
            {
                return new PaginationResponse<TreatmentEntity> { Data = entities };
            });

            mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return entities.FirstOrDefault(i => i.Id == id);
            });

            mockRepo.Setup(r => r.Add(It.IsAny<TreatmentEntity>())).ReturnsAsync((TreatmentEntity entity) =>
            {
                entities.Add(entity);
                return entity;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<TreatmentEntity>())).Returns((TreatmentEntity entity) =>
            {
                entities.Remove(entity);
                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<TreatmentEntity>())).Returns((TreatmentEntity entity) =>
            {
                var item = entities.FirstOrDefault(i => i.Id == entity.Id);
                item = entity;
                return Task.CompletedTask;
            });

            return mockRepo;
        }
    }
}
