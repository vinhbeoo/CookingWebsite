using System.ComponentModel.DataAnnotations;

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
