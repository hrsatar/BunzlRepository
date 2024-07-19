using Bunzl.Controllers;
using Data;
using Data.Abstractions;
using Data.AppContext;
using Data.Models;
using Logic.Abstractions;
using Logic.Logics;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic.DTOs.TaskDTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LogicTest.Tests
{
    public class TaskControllerTests
    {
        private readonly UnitOfWork _uow;
        private readonly TaskLogic _taskLogic;
        private readonly ApplicationDbContext _context;
        private readonly TaskController _taskController;
        private readonly DbContextOptions _options;

        public TaskControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new ApplicationDbContext(options);
            _uow=new UnitOfWork(_context);
            _taskLogic = new TaskLogic(_uow);
            _taskController=new TaskController(_taskLogic);

            seedDatabase();
        }

        void seedDatabase()
        {
            _context.Tasks.AddRange(
                new Taskk { name = "task1", taskId = 1, completed = false },
                new Taskk { name = "task2", taskId = 2, completed = false },
                new Taskk { name = "task3", taskId = 3, completed = false }
                );

            _context.SubTasks.AddRange(
               new SubTask { name = "subtask1", taskId = 1, completed = false,subTaskId=1 },
               new SubTask { name = "subtask2", taskId = 1, completed = true, subTaskId = 2 },
               new SubTask { name = "subtask3", taskId = 2, completed = true, subTaskId = 3 },
               new SubTask { name = "subtask4", taskId = 2, completed = true, subTaskId = 4 }
               );

            _context.SaveChanges();
        }

        //to test that all task are returned and their count of subtasks is returned correctly
        [Fact]
        public async void test_getAllWithoutSubTasks()
        {
            
            var result = await _taskController.getAllWithoutSubTasks() as OkObjectResult;

            var  output = result.Value as List<TaskDetailWithoutSubTaskDTO>;

            //test number of tasks returned
            output.Count().Should().Be( 3 );
            //test subtask count field
            output[0].subTasksCount.Should().Be( 2 );
        }

        //to test that tasks and their sutasks are returned correctly
        [Fact]
        public async void test_getAllWithSubTasks()
        {
            var result = await _taskController.getAllWithSubTasks() as OkObjectResult;

            var output = result.Value as List<TaskDetailWithSubTasksDTO>;

            //test a subtask of returned tasks
            output[0].subTasks[0].name.Should().Be("subtask1");
            //test number of tasks returned
            output.Count().Should().Be(3);
            

        }

        //to test gettask(without subtasks) bahaviour when providing valid and invalid taskids
        //and also to test the subtask count field of returned task
        [Fact]
        public async void test_gettask_with_valid_and_invalid_taskids()
        {
            //test action behaviour when invalid taskid is provided
            var result = await _taskController.getTaskDetail(500);
            result.Should().BeOfType<BadRequestObjectResult>();

            //test action behaviour when valid taskid is provided
            result = await _taskController.getTaskDetail(2);
            result.Should().BeOfType<OkObjectResult>();


            //test subtask count field of returned task
            var result1 = await _taskController.getTaskDetail(2) as OkObjectResult;
            var output = result1.Value as TaskDetailWithoutSubTaskDTO;
            output.subTasksCount.Should().Be( 2 );
        }

        //to test gettask(with subtasks) bahaviour when providing valid and invalid taskids
        //and also to test the subtasks returned for task
        [Fact]
        public async void test_getTaskDetailWithSubTasks_with_valid_and_invalid_taskids()
        {
            //test action behaviour when invalid taskid is provided
            var result = await _taskController.getTaskDetailWithSubTasks(500);
            result.Should().BeOfType<BadRequestObjectResult>();

            //test action behaviour when valid taskid is provided
            result = await _taskController.getTaskDetailWithSubTasks(2);
            result.Should().BeOfType<OkObjectResult>();


            //test number of subtasks and name of first subtask of returned task
            var result1 = await _taskController.getTaskDetailWithSubTasks(2) as OkObjectResult;
            var output = result1.Value as TaskDetailWithSubTasksDTO;
            output.subTasks.Count().Should().Be(2);
            output.subTasks[0].name.Should().Be("subtask3");
        }

        //to test that correct input is actually inserted
        [Fact]
        public async void test_create()
        {

            //test action behaviour when correct data is provided
            var result1 = await _taskController.create(new TaskCreateUpdateDTO { name = "testTaskName", completed = false }) as OkObjectResult;
            var output=result1.Value as TaskDetailWithoutSubTaskDTO;
            result1.Should().BeOfType<OkObjectResult>();


            //check if task is inserted into database
            var result2 =await _taskController.getTaskDetail(output.taskId) as OkObjectResult;
            var output2 = result2.Value as TaskDetailWithoutSubTaskDTO;
            output2.name.Should().Be("testTaskName");

        }

        //to test that task with not completed subtaskes can be update to completed
        //to test the action behaviour when invalid taskid is provided
        [Fact]
        public async void test_update()
        {

            //test action behaviour when a task with not completed subtasks want to update to completed
            var result1 = await _taskController.Put(1,new TaskCreateUpdateDTO { name = "updatedTaskName", completed = true });
            result1.Should().BeOfType<BadRequestObjectResult>();

            //test action behaviour when a task with completed subtasks want to update to completed
            var result2 = await _taskController.Put(2, new TaskCreateUpdateDTO { name = "updatedTaskName", completed = true });
            result2.Should().BeOfType<OkObjectResult>();

            //test action behaviour when an invalid taskid is provided
            var result3 = await _taskController.Put(20, new TaskCreateUpdateDTO { name = "updatedTaskName", completed = true });
            result3.Should().BeOfType<BadRequestObjectResult>();
        }

        //to test that task with subtaskes can be deleted
        //to test the action behaviour when invalid taskid is provided
        [Fact]
        public async void test_delete()
        {

            //test action behaviour when a task with subtasks is deleted
            var result1 = await _taskController.Delete (1);
            result1.Should().BeOfType<BadRequestObjectResult>();

            //test action behaviour when a task with no subtasks is deleted
            var result2 = await _taskController.Delete(3);
            result2.Should().BeOfType<NoContentResult>();

            //test action behaviour when an invalid taskid is provided
            var result3 = await _taskController.Delete(3);
            result3.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}