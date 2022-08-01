﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.SleepRecord.Commands
{
    public class DeleteSleepRecordCommand : IRequest<BaseCommandResponse>
    {
        public Guid Id { get; set; }
    }


    public class DeleteSleepRecordCommandHandler : IRequestHandler<DeleteSleepRecordCommand, BaseCommandResponse>
    {
        private readonly ISleepRecordsRepository _repository;

        public DeleteSleepRecordCommandHandler(ISleepRecordsRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResponse> Handle(DeleteSleepRecordCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new BadRequestException("Id cannot be empty.");

            var existingEntity = await _repository.Get(request.Id);

            if (existingEntity == null)
                throw new NotFoundException(nameof(existingEntity), request.Id);

            await _repository.Delete(existingEntity);

            await _repository.Save();

            var response = new BaseCommandResponse
            {
                Success = true,
                Message = "Entity Deleted Successfully"
            };

            return response;
        }
    }
}
