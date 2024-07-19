using Data;
using Data.Abstractions;
using Data.Models;
using Logic.Abstractions;
using Logic.DTOs.SubTaskDTOs;
using Logic.DTOs.TaskDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logics
{
    public class SubTaskLogic : ISubTaskLogic
    {
        IUnitOfWork _uow;
        public SubTaskLogic(IUnitOfWork unitofwork)
        {
            _uow = unitofwork;
        }
        public async Task<List<SubTaskDetailDTO>> getAllSubTasksOfTask(int taskId)
        {
            List<SubTaskDetailDTO> returnList = new List<SubTaskDetailDTO>();
            var subTasks = await _uow.SubTask.getAll(filter: s => s.taskId == taskId);

            foreach (var subTask in subTasks)
            {
                SubTaskDetailDTO Obj = new SubTaskDetailDTO();
                Obj.name = subTask.name;
                Obj.subTaskId = subTask.subTaskId;
                Obj.taskId = subTask.taskId;
                Obj.completed = subTask.completed;

                returnList.Add(Obj);
            }

            return returnList;
        }

        public async Task<Message> getSubTask(int id)
        {
            Message outputMessage = new Message();

            var obj = await _uow.SubTask.get(id);
            if (obj == null)
            {
                outputMessage.message = $"Error: subtask with id {id} not found";
                outputMessage.obj = null;
                return outputMessage;
            }

            SubTaskDetailDTO returnObj = new SubTaskDetailDTO();

            returnObj.subTaskId = obj.taskId;
            returnObj.name = obj.name;
            returnObj.completed = obj.completed;
            returnObj.taskId = obj.taskId;

            outputMessage.message = $"Successfull";
            outputMessage.obj = returnObj;
            return outputMessage;

        }

        public async Task<Message> create(SubTaskCreateUpdateDTO subTask)
        {
            Message outputMessage = new Message();

            Taskk taskObj = await _uow.Taskk.get(subTask.taskId);
            if (taskObj == null)
            {
                outputMessage.message = $"Error: invalid taskId";
                outputMessage.obj = null;
                return outputMessage;
            }

            SubTask subTaskObj = new SubTask { name = subTask.name, completed = subTask.completed, taskId = subTask.taskId };
            _uow.SubTask.create(subTaskObj);
            await _uow.SaveAsync();

            var returnObj = new SubTaskDetailDTO();
            returnObj.name = subTaskObj.name;
            returnObj.taskId = subTaskObj.taskId;
            returnObj.completed = subTaskObj.completed;
            returnObj.subTaskId = subTaskObj.subTaskId;

            outputMessage.message = $"Successfull";
            outputMessage.obj = returnObj;

            return outputMessage;

        }

        public async Task<Message> update(int subTaskId, SubTaskCreateUpdateDTO subtask)
        {
            Message outputMessage = new Message();

            SubTask subTaskObj = await _uow.SubTask.get(subTaskId);

            if (subTaskObj == null)
            {
                outputMessage.message = $"Error: subTask with id {subTaskId} not found";
                outputMessage.obj = null;
                return outputMessage;
            }

            Taskk taskObj = await _uow.Taskk.get(subtask.taskId);
            if (taskObj == null)
            {
                outputMessage.message = $"Error: invalid taskId";
                outputMessage.obj = null;
                return outputMessage;
            }

            subTaskObj.name = subtask.name;
            subTaskObj.completed = subtask.completed;
            subTaskObj.taskId = subtask.taskId;

            _uow.SubTask.update(subTaskObj);
            await _uow.SaveAsync();

            var returnObj = new SubTaskDetailDTO();
            returnObj.name = subTaskObj.name;
            returnObj.taskId = subTaskObj.taskId;
            returnObj.completed = subTaskObj.completed;
            returnObj.subTaskId = subTaskObj.subTaskId;

            outputMessage.message = $"Successfull";
            outputMessage.obj = returnObj;

            return outputMessage;
        }

        public async Task<Message> delete(int subTaskId)
        {
            Message outputMessage = new Message();

            SubTask subTaskObj = await _uow.SubTask.get(subTaskId);

            if (subTaskObj == null)
            {
                outputMessage.message = $"Error: subtask with id {subTaskId} not found";
                outputMessage.obj = null;
                return outputMessage;
            }


            _uow.SubTask.delete(subTaskObj);
            await _uow.SaveAsync();

            outputMessage.message = $"Successfull";
            outputMessage.obj = new object();

            return outputMessage;
        }
    }
}
