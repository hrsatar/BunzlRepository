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
    public class TaskLogic : ITaskLogic
    {
        IUnitOfWork _uow;
        public TaskLogic(IUnitOfWork unitofwork)
        {
            _uow = unitofwork;
        }
        public async Task<List<TaskDetailWithoutSubTaskDTO>> getAllWithoutSubTasks()
        {
            List<TaskDetailWithoutSubTaskDTO> returnList = new List<TaskDetailWithoutSubTaskDTO>();
            var taskList = await _uow.Taskk.getAllWithSubTaskCount();

            foreach (var task in taskList)
            {
                TaskDetailWithoutSubTaskDTO Obj = new TaskDetailWithoutSubTaskDTO();
                Obj.taskId = task.taskId;
                Obj.name = task.name;
                Obj.completed = task.completed;
                Obj.subTasksCount = task.subTaskCount;

                returnList.Add(Obj);
            }

            return returnList;

        }

        public async Task<List<TaskDetailWithSubTasksDTO>> getAllWithSubTasks()
        {
            List<TaskDetailWithSubTasksDTO> returnList = new List<TaskDetailWithSubTasksDTO>();
            var taskList = await _uow.Taskk.getAllWithSubTasks();

            foreach (var task in taskList)
            {
                TaskDetailWithSubTasksDTO Obj = new TaskDetailWithSubTasksDTO();
                Obj.taskId = task.taskId;
                Obj.name = task.name;
                Obj.completed = task.completed;
                Obj.subTasksCount = task.subTaskCount;

                Obj.subTasks = new List<SubTaskDetailDTO>() { };
                foreach (var subTask in task.subTasks)
                {
                    Obj.subTasks.Add(new SubTaskDetailDTO { subTaskId = subTask.subTaskId, name = subTask.name, completed = subTask.completed, taskId = subTask.taskId });
                }
                Obj.subTasksCount = Obj.subTasks.Count();

                returnList.Add(Obj);
            }

            return returnList;

        }
        public async Task<Message> getTaskDetailWithoutSubTasks(int id)
        {
            Message outputMessage = new Message();

            var obj = await _uow.Taskk.get(id);
            if (obj == null)
            {
                outputMessage.message = $"Error: task with id {id} not found";
                outputMessage.obj = null;
                return outputMessage;
            }

            TaskDetailWithoutSubTaskDTO returnObj = new TaskDetailWithoutSubTaskDTO();
            returnObj.taskId = obj.taskId;
            returnObj.name = obj.name;
            returnObj.completed = obj.completed;
            returnObj.subTasksCount = obj.subTaskCount;


            outputMessage.message = $"Successfull";
            outputMessage.obj = returnObj;
            return outputMessage;

        }

        public async Task<Message> getTaskDetailWithSubTasks(int id)
        {
            Message outputMessage = new Message();

            var obj = await _uow.Taskk.getTaskWithSubTasks(id);
            if (obj == null)
            {
                outputMessage.message = $"Error: task with id {id} not found";
                outputMessage.obj = null;
                return outputMessage;
            }

            TaskDetailWithSubTasksDTO returnObj = new TaskDetailWithSubTasksDTO();
            returnObj.taskId = obj.taskId;
            returnObj.name = obj.name;
            returnObj.completed = obj.completed;

            returnObj.subTasks = new List<SubTaskDetailDTO>() { };
            foreach (var subTask in obj.subTasks)
            {
                returnObj.subTasks.Add(new SubTaskDetailDTO { subTaskId = subTask.subTaskId, name = subTask.name, completed = subTask.completed, taskId = subTask.taskId });
            }
            returnObj.subTasksCount = returnObj.subTasks.Count();

            outputMessage.message = $"Successfull";
            outputMessage.obj = returnObj;
            return outputMessage;

        }

        public async Task<Message> create(TaskCreateUpdateDTO task)
        {
            Message outputMessage = new Message();

            Taskk taskObj = new Taskk { name = task.name, completed = false };
            _uow.Taskk.create(taskObj);
            await _uow.SaveAsync();

            var returnObj = new TaskDetailWithoutSubTaskDTO();
            returnObj.name = taskObj.name;
            returnObj.taskId = taskObj.taskId;
            returnObj.completed = taskObj.completed;
            returnObj.subTasksCount = 0;

            outputMessage.message = $"Successfull";
            outputMessage.obj = returnObj;

            return outputMessage;

        }

        public async Task<Message> update(int taskId, TaskCreateUpdateDTO task)
        {
            Message outputMessage = new Message();

            Taskk taskObj = await _uow.Taskk.getTaskWithSubTasks(taskId);

            if (taskObj == null)
            {
                outputMessage.message = $"Error: task with id {taskId} not found";
                outputMessage.obj = null;
                return outputMessage;
            }

            if (!taskObj.completed && task.completed)
            {
                if (taskObj.subTasks.Any(t => !t.completed))
                {

                    outputMessage.message = $"Error: Unable to change the task completion status. One or more subtasks are not completed.";
                    outputMessage.obj = null;
                    return outputMessage;
                }
            }

            taskObj.name = task.name;
            taskObj.completed = task.completed;

            _uow.Taskk.update(taskObj);
            await _uow.SaveAsync();

            var returnObj = new TaskDetailWithoutSubTaskDTO();
            returnObj.name = taskObj.name;
            returnObj.taskId = taskObj.taskId;
            returnObj.completed = taskObj.completed;
            returnObj.subTasksCount = taskObj.subTasks.Count();

            outputMessage.message = $"Successfull";
            outputMessage.obj = returnObj;

            return outputMessage;
        }

        public async Task<Message> delete(int taskId)
        {
            Message outputMessage = new Message();

            Taskk taskObj = await _uow.Taskk.getTaskWithSubTasks(taskId);

            if (taskObj == null)
            {
                outputMessage.message = $"Error: task with id {taskId} not found";
                outputMessage.obj = null;
                return outputMessage;
            }


            if (taskObj.subTasks.Count() != 0)
            {

                outputMessage.message = $"Error: Unable to remove the task. It has one or more subtasks.";
                outputMessage.obj = null;
                return outputMessage;
            }


            _uow.Taskk.delete(taskObj);
            await _uow.SaveAsync();

            outputMessage.message = $"Successfull";
            outputMessage.obj = new object();

            return outputMessage;
        }

    }
}
