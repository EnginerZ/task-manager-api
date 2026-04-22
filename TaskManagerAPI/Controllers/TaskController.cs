using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;
using TaskManagerAPI.DTOs;

namespace TaskManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public IActionResult CreateTask(TaskDTO taskDto)
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null) return Unauthorized();

            var result = _taskService.CreateTask(taskDto, int.Parse(userId));
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(_taskService.GetAllTasks());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            var updated = _taskService.UpdateTask(id, updatedTask);
            if (!updated) return NotFound();
            return Ok(new { message = "Tarea actualizada correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var deleted = _taskService.DeleteTask(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Tarea eliminada correctamente" });
        }
    }
}