using System.Collections.Generic;
using AntiFraud.Orders.Models;

namespace AntiFraud.Orders.Factories
{
    public interface IAntiFraudPolicyFactory
    {
        List<IAntiFraudPolicy> GetAntiFraudPolicy();
        double GetFractorOfAverageAmmount();
    }
}
