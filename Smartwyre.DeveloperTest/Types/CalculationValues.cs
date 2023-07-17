namespace Smartwyre.DeveloperTest.Types
{
    class CalculationValues
    {
        public decimal RebateAmount { get; set; }
        public decimal RebatePercentage { get; set; }

        public decimal RequestVolume { get; set; }
        public decimal ProductPrice { get; set; }

        public SupportedIncentiveType ProductSupportedIncentives { get; set; }
    }
}

