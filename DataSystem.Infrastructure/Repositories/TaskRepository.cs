using DataSystem.Domain.Entities;
using DataSystem.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSystem.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerDbContext _context;

        public TaskRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _context.SysTask.ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            return await _context.SysTask.FindAsync(id);
        }

        public async Task AddAsync(TaskEntity task)
        {
            await _context.SysTask.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TaskEntity task)
        {
            _context.SysTask.Update(task);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.SysTask.FindAsync(id);
            if (task == null)
                return false;

            _context.SysTask.Remove(task);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

    }
}
