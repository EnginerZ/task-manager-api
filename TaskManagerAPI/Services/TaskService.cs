using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTOs;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public TaskResponseDto CreateTask(TaskDTO taskDto, int userId)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status,
                CreatedAt = DateTime.Now,
                UserId = userId
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status
            };
        }

        public List<TaskDTO> GetAllTasks()
        {
            return _context.Tasks
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
        }

        public bool UpdateTask(int id, TaskItem updatedTask)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return false;

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Status = updatedTask.Status;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return true;
        }
    }
}