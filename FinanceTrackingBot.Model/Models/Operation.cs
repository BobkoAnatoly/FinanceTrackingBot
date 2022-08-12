using FinanceTrackingBot.Common.Enums;
namespace FinanceTrackingBot.Model.Models
{
    public class Operation :BaseModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public OperationType Type { get; set; }
        public bool IsFinished { get; set; }

        public User User { get; set; }
        public int? UserId { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
