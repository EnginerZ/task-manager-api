using TaskManagerAPI.DTOs;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        TaskResponseDto CreateTask(TaskDTO taskDto, int userId);
        List<TaskDTO> GetAllTasks();
        bool UpdateTask(int id, TaskItem updatedTask);
        bool DeleteTask(int id);
    }
}