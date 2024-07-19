using Data.Abstractions;
using Logic.Abstractions;
using Microsoft.AspNetCore.Mvc;


namespace Bunzl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTaskController : ControllerBase
    {
        ISubTaskLogic _subTaskLogic;
        public SubTaskController(ISubTaskLogic isubtasklogic)
        {
            _subTaskLogic = isubtasklogic;
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> getAll(int taskId)
        {
            var obj = await _subTaskLogic.getAllSubTasksOfTask(taskId);
            return Ok(obj);
        }

        [HttpGet("getSubTask/{id}")]
        public async Task<IActionResult> getSubTask(int id)
        {
            Logic.Message output = await _subTaskLogic.getSubTask(id);

            if (output.obj == null)
            {
                return BadRequest(output.message);
            }
            return Ok(output.obj);
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] Logic.DTOs.SubTaskDTOs.SubTaskCreateUpdateDTO subTask)
        {

            Logic.Message outPut = await _subTaskLogic.create(subTask);

            if(outPut.obj == null)
            {  
                return BadRequest(outPut.message); 
            }
            return Ok(outPut.obj);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Logic.DTOs.SubTaskDTOs.SubTaskCreateUpdateDTO subTask)
        {
            Logic.Message outPut = await _subTaskLogic.update(id, subTask);

            if (outPut.obj == null)
            {
                return BadRequest(outPut.message);
            }

            return Ok(outPut.obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Logic.Message outPut = await _subTaskLogic.delete(id);

            if (outPut.obj == null)
            {
                return BadRequest(outPut.message);
            }

            return NoContent();
        }
    }
}
