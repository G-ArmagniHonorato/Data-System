using DataSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSystem.Infrastructure.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity?> GetByIdAsync(int id);
        Task AddAsync(TaskEntity task);
        Task<bool> UpdateAsync(TaskEntity task);
        Task<bool> DeleteAsync(int id);
    }

}
