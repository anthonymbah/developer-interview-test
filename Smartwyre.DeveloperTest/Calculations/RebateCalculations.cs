using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculations
{
    class RebateCalculations
    {
        public static CalculateRebateResult CalculateForFixedCashAmount(CalculationValues values)
        {
            return new CalculateRebateResult
            {
                Success = values.ProductSupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) &&
                            values.RebateAmount != 0,
                Amount = values.RebateAmount
            };
        }

        public static CalculateRebateResult CalculateForFixedRateRebate(CalculationValues values)
        {
            var success = false;
            decimal amount = 0;

            if (
                values.ProductSupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) &&
                values.RebatePercentage != 0 && 
                values.ProductPrice != 0 && 
                values.RequestVolume != 0
                )
            {
                amount = values.ProductPrice * values.RebatePercentage * values.RequestVolume;
                success = true;
            }

            return new CalculateRebateResult { Success = success, Amount = amount };
        }

        public static CalculateRebateResult CalculateForAmountPerUom(CalculationValues values)
        {
            var success = false;
            decimal amount = 0;

            if (
                values.ProductSupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) &&
                values.RebateAmount != 0 && 
                values.RequestVolume != 0
             )
            {
                amount = values.RebateAmount * values.RequestVolume;
                success = true;
            }

            return new CalculateRebateResult { Success = success, Amount = amount };
        }
    }
}
