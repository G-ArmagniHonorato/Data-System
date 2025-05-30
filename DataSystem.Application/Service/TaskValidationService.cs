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
    public class TaskValidationService : ITaskValidationService
    {
        public void ValidateCompletionDate(TaskEntity newTask, TaskEntity existingTask)
        {
            if (newTask.DtComplete.HasValue && newTask.DtComplete < existingTask.DtCreate)
                throw new Exception("A data de conclusão não pode ser anterior a data de criação ");
        }
    }
}
