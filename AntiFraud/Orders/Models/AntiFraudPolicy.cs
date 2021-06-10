using System;
namespace AntiFraud.Orders.Models
{
    public class AntiFraudPolicy : IAntiFraudPolicy
    {
        public bool IsNewUser { get; set; }
        public string DissalowedCountry { get; set; }
        public int MaximumAmount { get; set; }
    }
}
