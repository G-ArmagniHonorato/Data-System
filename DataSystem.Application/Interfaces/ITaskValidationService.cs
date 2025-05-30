using DataSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSystem.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> GetByIdAsync(int id);
        Task<TaskEntity> Create(TaskEntity task);
        Task<bool> UpdateAsync(TaskEntity task);
        Task<bool> DeleteAsync(int id);
    }
}
