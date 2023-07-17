using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void Calculate_WithFixedCashAmount_UnsupportedIncentive_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT02",
            RebateIdentifier = "RB01",
            //Volume = 9
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;

        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithFixedCashAmount_RebateAmountOfZero_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT01",
            RebateIdentifier = "RB04",
            //Volume = 9
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;

        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithFixedCashAmountIncentive_Success()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT01",
            RebateIdentifier = "RB01",
            //Volume = 9
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;

        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithFixedRateRebate_UnsupportedIncentive_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT01",
            RebateIdentifier = "RB02",
            //Volume = 9
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;


        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithFixedRateRebate_RebatePrecentageIsZero_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT01",
            RebateIdentifier = "RB05",
            //Volume = 9
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;


        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithFixedRateRebate_RebatePriceIsZero_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT04",
            RebateIdentifier = "RB02",
            //Volume = 9
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;


        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithFixedRateRebate_RequestVolumnIsZero_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT02",
            RebateIdentifier = "RB02",
            Volume = 0
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;


        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithFixedRateRebate_Success()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT02",
            RebateIdentifier = "RB02",
            Volume = 2
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;


        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(20.75m, previousAmount);
        Assert.Equal(311.844m, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithAmountPerUom_UnsupportedIncentive_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT02",
            RebateIdentifier = "RB03",
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;

        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithAmountPerUom_RebateAmountOfZero_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT06",
            RebateIdentifier = "RB03",
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;

        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithAmountPerUom_RequestVolumnIsZero_Failure()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT03",
            RebateIdentifier = "RB03",
            Volume = 0
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;

        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(previousAmount, rebate.Amount);
    }

    [Fact]
    public void Calculate_WithAmountPerUom_Success()
    {
        // Arrange
        var request = new CalculateRebateRequest()
        {
            ProductIdentifier = "PDT03",
            RebateIdentifier = "RB03",
            Volume = 5
        };
        var rebaseService = new RebateService();
        var previousAmount = RebateDataStore.Instance.GetRebate(request.RebateIdentifier).Amount;

        // Act
        var result = rebaseService.Calculate(request);
        var rebate = RebateDataStore.Instance.GetRebate(request.RebateIdentifier);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(30.25m, previousAmount);
        Assert.Equal(151.25m, rebate.Amount);
    }
}
