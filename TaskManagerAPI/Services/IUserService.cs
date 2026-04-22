using TaskManagerAPI.DTOs;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public interface IUserService
    {
        UserDTO CreateUser(User user);
        string Login(User loginUser);
        List<UserDTO> GetAllUsers();
    }
}