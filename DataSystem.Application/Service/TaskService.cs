using DataSystem.Application.Interfaces;
using DataSystem.Infrastructure.Repositories;
using DataSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSystem.Application.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly ITaskValidationService _validation;

        public TaskService(ITaskRepository repository, ITaskValidationService validation)
        {
            _repository = repository;
            _validation = validation;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TaskEntity> Create(TaskEntity task)
        {
            await _repository.AddAsync(task);
            return task;
        }

        public async Task<bool> UpdateAsync(TaskEntity task)
        {
            var existingTask = await _repository.GetByIdAsync(task.Id);

            if (existingTask == null) return false;

            _validation.ValidateCompletionDate(task, existingTask);

            return await _repository.UpdateAsync(task);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
