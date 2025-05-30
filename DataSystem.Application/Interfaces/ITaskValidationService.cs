using DataSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSystem.Application.Interfaces
{
    public interface ITaskValidationService
    {
        void ValidateCompletionDate(TaskEntity newTask, TaskEntity existingTask);
    }
}
