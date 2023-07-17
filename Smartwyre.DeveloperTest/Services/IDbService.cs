using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IDbService
{
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}
