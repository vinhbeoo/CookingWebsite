using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.ObjectBussiness
{
    public class UserPaymentHistory
    {
        [Key]
        public int PaymentHistoryId { get; set; }
        public int UserId { get; set; }
        public string TransactionRef { get; set; }
        public virtual User User { get; set; }
    }
}
