using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.DTOs.SubTaskDTOs;

namespace Logic.DTOs.TaskDTOs
{
    public class TaskDetailWithSubTasksDTO
    {
        public int taskId { get; set; }
        public string name { get; set; }
        public bool completed { get; set; }
        public int subTasksCount { get; set; }
        public List<SubTaskDetailDTO> subTasks { get; set; }


    }
}
