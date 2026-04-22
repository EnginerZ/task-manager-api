using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTOs;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public UserDTO CreateUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public string Login(User loginUser)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == loginUser.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
                return null;

            return GenerateJwtToken(user);
        }

        public List<UserDTO> GetAllUsers()
        {
            return _context.Users
                .Include(u => u.Tasks)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Email = u.Email
                })
                .ToList();
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}