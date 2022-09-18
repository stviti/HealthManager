using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync((CancellationToken cancellationToken) =>
            {
                return entities;
            });

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<PaginatedFilter>(), It.IsAny<CancellationToken>())).ReturnsAsync((PaginatedFilter paginatedFilter, CancellationToken cancellationToken) =>
            {
                return new PaginationResponse<TreatmentEntity> { Data = entities };
            });

            mockRepo.Setup(r => r.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Guid id, CancellationToken cancellationToken) =>
            {
                return entities.FirstOrDefault(i => i.Id == id);
            });

            mockRepo.Setup(r => r.AddAsync(It.IsAny<TreatmentEntity>(), It.IsAny<CancellationToken>())).ReturnsAsync((TreatmentEntity entity, CancellationToken cancellationToken) =>
            {
                entities.Add(entity);
                return entity;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<TreatmentEntity>())).Callback((TreatmentEntity entity) =>
            {
                entities.Remove(entity);
            });

            mockRepo.Setup(r => r.Update(It.IsAny<TreatmentEntity>())).Callback((TreatmentEntity entity) =>
            {
                var item = entities.FirstOrDefault(i => i.Id == entity.Id);
                item = entity;
            });

            return mockRepo;
        }
    }
}
