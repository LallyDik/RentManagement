// RentManagementAPI/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using RentManagementAPI.Models;
using RentManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger; // הוספת לוגר

        public AuthController(ApplicationDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            _logger.LogInformation("Attempting to register user: {Username}", registerDto.Username);

            // ולידציה בסיסית: בדיקה אם שם המשתמש כבר קיים
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                _logger.LogWarning("Registration failed: Username '{Username}' already exists.", registerDto.Username);
                return Conflict("שם משתמש זה כבר קיים. אנא בחר שם משתמש אחר."); // מחזיר 409 Conflict
            }

            // יצירת אובייקט User מה-DTO
            var user = new User
            {
                Username = registerDto.Username,
                // אזהרה: סיסמאות לא צריכות להישמר כטקסט רגיל!
                // בשלב מתקדם יותר נשתמש בגיבוב (hashing) ואבטחת סיסמאות.
                Password = registerDto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User '{Username}' registered successfully. User ID: {UserId}", user.Username, user.Id);

            // מחזיר את פרטי המשתמש החדש (ללא סיסמא)
            return StatusCode(201, new UserDto { Id = user.Id, Username = user.Username }); // מחזיר 201 Created
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation("Attempting to log in user: {Username}", loginDto.Username);

            // חיפוש המשתמש בבסיס הנתונים
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);

            // בדיקה אם המשתמש קיים והסיסמא נכונה
            if (user == null || user.Password != loginDto.Password) // אזהרה: השוואת סיסמאות בטקסט רגיל אינה מאובטחת!
            {
                _logger.LogWarning("Login failed for user '{Username}': Invalid credentials.", loginDto.Username);
                return Unauthorized("שם משתמש או סיסמא שגויים."); // מחזיר 401 Unauthorized
            }

            // בשלב מתקדם יותר, כאן נפיק טוקן JWT
            // בינתיים, נחזיר הודעת הצלחה ופרטי המשתמש
            _logger.LogInformation("User '{Username}' logged in successfully.", loginDto.Username);
            return Ok(new UserDto { Id = user.Id, Username = user.Username }); // מחזיר 200 OK
        }
    }
}