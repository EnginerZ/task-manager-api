using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;


namespace TaskManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE TASK
        [Authorize]
        [HttpPost]
        public IActionResult CreateTask(TaskDTO taskDto)
        {
            var userId = User.FindFirst("id")?.Value;

            if (userId == null)
                return Unauthorized();

            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                CreatedAt = DateTime.Now,
                UserId = int.Parse(userId)
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok(new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status
            });
        }

        // GET ALL TASKS
        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _context.Tasks
                .Include(t => t.User)
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    User = new UserDTO
                    {
                        Id = t.User.Id,
                        Email = t.User.Email
                    }
                })
                .ToList();

            return Ok(tasks);
        }

        // PUT TASK
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            try
            {
                var task = _context.Tasks.Find(id);

                if (task == null)
                {
                    return NotFound();
                }

                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.Status = updatedTask.Status;

                _context.SaveChanges();

                return Ok(new
                {
                    message = "Tarea actualizada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al actualizar",
                    error = ex.Message
                });
            }
        }

        //DELETE TASK
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                var task = _context.Tasks.Find(id);

                if (task == null)
                {
                    return NotFound();
                }

                _context.Tasks.Remove(task);
                _context.SaveChanges();

                return Ok(new
                {
                    message = "Tarea eliminada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al eliminar",
                    error = ex.Message
                });
            }
        }
    }
}