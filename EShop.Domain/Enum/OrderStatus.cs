using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Enum
{
    public enum OrderStatus
    {
        Pending,       // در انتظار پرداخت
        Paid,          // پرداخت شده
        Failed,        // پرداخت ناموفق
        Shipped,       // ارسال شده
        Delivered,     // تحویل داده شده
        Cancelled      // لغو شده
    }
}
