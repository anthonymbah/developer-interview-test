using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System;
using Smartwyre.DeveloperTest.Calculations;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebateDataStore = RebateDataStore.Instance;
        var productDataStore = ProductDataStore.Instance;

        Rebate rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = productDataStore.GetProduct(request.ProductIdentifier);
        var result = new CalculateRebateResult();
        var values = new CalculationValues() 
        { 
            ProductPrice = product.Price, 
            RebateAmount = rebate.Amount, 
            RebatePercentage = rebate.Percentage, 
            RequestVolume = request.Volume,
            ProductSupportedIncentives = product.SupportedIncentives,
        };

        Dictionary<IncentiveType, Func<CalculationValues, CalculateRebateResult>> calculationDictionary =
            new Dictionary<IncentiveType, Func<CalculationValues, CalculateRebateResult>>()
        {
            { IncentiveType.FixedCashAmount, RebateCalculations.CalculateForFixedCashAmount },
            { IncentiveType.FixedRateRebate, RebateCalculations.CalculateForFixedRateRebate },
            { IncentiveType.AmountPerUom, RebateCalculations.CalculateForAmountPerUom },
        };

        if (calculationDictionary.ContainsKey(rebate.Incentive))
        {
            result = calculationDictionary[rebate.Incentive].Invoke(values);
        }

        if (result.Success)
        {
            rebateDataStore.StoreCalculationResult(rebate, result.Amount);
        }

        return result;
    }
}
