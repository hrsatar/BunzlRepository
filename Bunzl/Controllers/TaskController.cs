using Data.Abstractions;
using Data.Models;
using Logic;
using Logic.Abstractions;
using Logic.DTOs.TaskDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bunzl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskLogic _taskLogic;
        public TaskController(ITaskLogic itasklogic)
        {
            _taskLogic = itasklogic;
        }

        [HttpGet]
        [HttpGet("getAllWithoutSubTasks")]
        public async Task<IActionResult> getAllWithoutSubTasks()
        {
            var obj = await _taskLogic.getAllWithoutSubTasks();
            return Ok(obj);
        }

        [HttpGet("getAllWithSubTasks")]
        public async Task<IActionResult> getAllWithSubTasks()
        {
            var obj = await _taskLogic.getAllWithSubTasks();
            return Ok(obj);
        }

        [HttpGet("{id}")]
        [HttpGet("getWithoutSubTasks/{id}")]
        public async Task<IActionResult> getTaskDetail(int id)
        {
            Logic.Message output = await _taskLogic.getTaskDetailWithoutSubTasks(id);
            
            if (output.obj == null)
            {
                return BadRequest(output.message);
            }
            return Ok(output.obj);
        }

        [HttpGet("getWithSubTasks/{id}")]
        public async Task<IActionResult> getTaskDetailWithSubTasks(int id)
        {
            Logic.Message output = await _taskLogic.getTaskDetailWithSubTasks(id);

            if (output.obj == null)
            {
                return BadRequest(output.message);
            }
            return Ok(output.obj);
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] Logic.DTOs.TaskDTOs.TaskCreateUpdateDTO task)
        {
            Logic.Message outPut = await _taskLogic.create(task);

            return Ok(outPut.obj);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Logic.DTOs.TaskDTOs.TaskCreateUpdateDTO task)
        {
            Logic.Message outPut=await _taskLogic.update(id,task);

            if(outPut.obj == null)
            {
                return BadRequest(outPut.message);
            }

            return Ok(outPut.obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Logic.Message outPut = await _taskLogic.delete(id);

            if (outPut.obj == null)
            {
                return BadRequest(outPut.message);
            }

            return NoContent();
        }
    }
}
