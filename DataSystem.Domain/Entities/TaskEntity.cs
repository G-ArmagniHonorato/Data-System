using DataSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSystem.Domain.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Desc { get; set; }
        public DateTime DtCreate { get; set; } = DateTime.UtcNow;
        public DateTime? DtComplete { get; set; }
        public TaskEnumStatus Status { get; set; } = TaskEnumStatus.Pendente;
    }
}
