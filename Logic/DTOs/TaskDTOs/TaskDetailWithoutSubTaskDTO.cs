using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTOs.TaskDTOs
{
    public class TaskDetailWithoutSubTaskDTO
    {
        public int taskId { get; set; }
        public string name { get; set; }
        public bool completed { get; set; }
        public int subTasksCount { get; set; }

    }
}
