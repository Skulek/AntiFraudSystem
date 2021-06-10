using System;
using System.Collections.Generic;
using AntiFraud.Orders.Models;
using Microsoft.Extensions.Configuration;

namespace AntiFraud.Orders.Factories
{
    public class AntiFraudPolicyFactory : IAntiFraudPolicyFactory
    {
        private readonly IConfiguration configuration;

        public AntiFraudPolicyFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private double StartupFactorOfAmmount = 5;

        public List<IAntiFraudPolicy> GetAntiFraudPolicy()
        {
            var policies = new List<IAntiFraudPolicy>();
            configuration.GetSection("AntiFraudPolicies").Bind(policies);
            return policies;
        }

        public double GetFractorOfAverageAmmount()
        {
            double.TryParse(configuration["FractorOfAverageAmount"], out StartupFactorOfAmmount);
            return StartupFactorOfAmmount;
        }
    }
}
