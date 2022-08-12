using FinanceTrackingBot.Common.Enums;
using System.Text.Json.Serialization;
namespace FinanceTrackingBot.Model.Models
{
    public class Category:BaseModel
    {
        public string Name { get; set; }
        public OperationType Type { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public int? UserId { get; set; }
    }
}
