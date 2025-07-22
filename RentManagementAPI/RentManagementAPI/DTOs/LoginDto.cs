// RentManagementAPI/DTOs/LoginDto.cs
using System.ComponentModel.DataAnnotations;

namespace RentManagementAPI.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "שם משתמש הוא שדה חובה.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "סיסמא היא שדה חובה.")]
        public string Password { get; set; }
    }
}