using System; // נדרש עבור DateTime

namespace RentManagementAPI.Models // ודאי שזה ה-namespace הנכון
{
    public class Payment // שם הקלאס ביחיד
    {
        public int Id { get; set; } // מפתח ראשי - עמודת Id בטבלת Payments
        public int GregorianMonth { get; set; } // חודש לועזי
        public string HebrewMonth { get; set; } // חודש עברי
        public int Year { get; set; } // שנה (הוספה משלימה, נחוץ לתאריך מלא)

        // מפתח זר (Foreign Key) לטבלת Tenants:
        public int TenantId { get; set; } // עמודת TenantId בטבלת Payments

        // נכס ניווט (Navigation Property) עבור קשר רבים-לאחד:
        // תשלום אחד משויך לשוכר אחד.
        public Tenant Tenant { get; set; } // השוכר שאליו שייך התשלום הזה

        public decimal RentAmount { get; set; } // דמי שכירות ששולמו בפועל
        public decimal Electricity { get; set; } // חשמל ששולם
        public decimal Water { get; set; } // מים ששולמו (הוספה משלימה)
        public decimal Gas { get; set; } // גז ששולם
        public bool IsPaid { get; set; } // שולם - בוליאני
        public string PaymentMethod { get; set; } // שיטת תשלום
        public DateTime PaymentDate { get; set; } // תאריך התשלום בפועל (הוספה משלימה)
    }
}