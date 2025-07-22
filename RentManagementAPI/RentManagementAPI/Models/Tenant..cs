using System.Collections.Generic; // נדרש עבור ICollection

namespace RentManagementAPI.Models // ודאי שזה ה-namespace הנכון
{
    public class Tenant // שם הקלאס ביחיד
    {
        public int Id { get; set; } // מפתח ראשי - עמודת Id בטבלת Tenants
        public string Name { get; set; } // שם השוכר - עמודת Name בטבלת Tenants

        // מפתח זר (Foreign Key) לטבלת Users:
        public int? ResponsibleUserId { get; set; } // עמודת ResponsibleUserID בטבלת Tenants
                                                    // הסימן ? הופך את השדה ל-nullable (מאפשר ערך NULL)

        // נכס ניווט (Navigation Property) עבור קשר רבים-לאחד:
        // שוכר אחד משויך למשתמש אחראי אחד.
        public User ResponsibleUser { get; set; } // המשתמש האחראי על השוכר הזה

        public decimal RentFee { get; set; } // דמי שכירות
        public decimal BuildingCommitteeFee { get; set; } // ועד בית
        public decimal Electricity { get; set; } // חשמל
        public decimal Gas { get; set; } // גז
        public decimal WaterMeterReading { get; set; } // מונה מים
        public decimal ElectricityMeterReading { get; set; } // מונה חשמל
        public decimal GasMeterReading { get; set; } // מונה גז

        // נכס ניווט (Navigation Property) עבור קשר 1-לרבים:
        // שוכר אחד יכול לבצע כמה תשלומים.
        public ICollection<Payment> Payments { get; set; } // רשימת התשלומים ששויכו לשוכר זה
    }
}