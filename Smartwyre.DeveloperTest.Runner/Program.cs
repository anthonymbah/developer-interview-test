using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome! Enter 'exit' to quit.");
        var rebaseService = new RebateService();

        while (true)
        {
            var request = new CalculateRebateRequest();
            Console.Write("Enter Rebate Identifier: ");
            request.RebateIdentifier = Console.ReadLine().ToUpper();

            if (request.RebateIdentifier.ToLower() == "exit")
            {
                break; // Exit the loop if 'exit' is entered
            }

            Console.Write("Enter Product Identifier: ");
            request.ProductIdentifier = Console.ReadLine().ToUpper();

            if (request.ProductIdentifier.ToLower() == "exit")
            {
                break; // Exit the loop if 'exit' is entered
            }

            Console.Write("Enter Volume: ");
            string volumeInput = Console.ReadLine();

            if (volumeInput.ToLower() == "exit")
            {
                break; // Exit the loop if 'exit' is entered
            }

            decimal volume;
            if (!decimal.TryParse(volumeInput, out volume))
            {
                Console.Write("Invalid volume. Please enter a valid decimal value.");
                continue; // Restart the loop to prompt for inputs again
            }

            request.Volume = volume;

            Console.WriteLine($"Rebate Identifier: {request.RebateIdentifier}");
            Console.WriteLine($"Product Identifier: {request.ProductIdentifier}");
            Console.WriteLine($"Volume: {volume}");

            var result = rebaseService.Calculate(request);
            Console.WriteLine($"Calculation success: {result.Success}");
            Console.WriteLine($"Calculated amount: {result.Amount}");
        }

        Console.WriteLine("Goodbye!");

    }
}
