using Data.Models;
using Logic.DTOs.SubTaskDTOs;
using Logic.DTOs.TaskDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Abstractions
{
    public interface ITaskLogic
    {
        Task<List<TaskDetailWithoutSubTaskDTO>> getAllWithoutSubTasks();

        Task<List<TaskDetailWithSubTasksDTO>> getAllWithSubTasks();
        Task<Message> getTaskDetailWithoutSubTasks(int id);

        Task<Message> getTaskDetailWithSubTasks(int id);

        Task<Message> create(TaskCreateUpdateDTO task);

        Task<Message> update(int taskId, TaskCreateUpdateDTO task);

        Task<Message> delete(int taskId);
    }
}
