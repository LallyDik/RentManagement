// RentManagementAPI/DTOs/RegisterUserDto.cs
using System.ComponentModel.DataAnnotations;

namespace RentManagementAPI.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "שם משתמש הוא שדה חובה.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "שם המשתמש חייב להיות בין 3 ל-50 תווים.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "סיסמא היא שדה חובה.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "הסיסמא חייבת להיות באורך של לפחות 6 תווים.")]
        public string Password { get; set; }
    }
}