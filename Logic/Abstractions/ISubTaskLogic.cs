using Data.Models;
using Logic.DTOs.SubTaskDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Abstractions
{
    public interface ISubTaskLogic
    {
        Task<List<DTOs.SubTaskDTOs.SubTaskDetailDTO>> getAllSubTasksOfTask(int taskId);

        Task<Message> getSubTask(int id);

        Task<Message> create(SubTaskCreateUpdateDTO subTask);

        Task<Message> update(int subTaskId, SubTaskCreateUpdateDTO subtask);
        Task<Message> delete(int subTaskId);
    }
}
