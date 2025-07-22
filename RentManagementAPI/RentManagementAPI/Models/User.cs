using System.Collections.Generic; // נדרש עבור ICollection

namespace RentManagementAPI.Models // ודאי שזה ה-namespace הנכון
{
    public class User // שם הקלאס ביחיד
    {
        public int Id { get; set; } // מפתח ראשי - עמודת Id בטבלת Users
        public string Username { get; set; } // שם משתמש - עמודת Username בטבלת Users
        public string Password { get; set; } // סיסמא - עמודת Password בטבלת Users

        // נכס ניווט (Navigation Property) עבור קשר 1-לרבים:
        // משתמש אחד יכול להיות אחראי על כמה שוכרים.
        public ICollection<Tenant> Tenants { get; set; } // רשימת השוכרים שהמשתמש אחראי עליהם
    }
}