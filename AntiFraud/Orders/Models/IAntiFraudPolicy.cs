namespace AntiFraud.Orders.Models
{
    public interface IAntiFraudPolicy
    {
        bool IsNewUser { get; set; }
        string DissalowedCountry { get; set; }
        int MaximumAmount { get; set; }
    }
}