namespace NetEvolve.CodeBuilder.Tests.Integration;

using System;
using System.Globalization;

public partial class CSharpCodeBuilderIntegrationTests
{
    [Test]
    public async Task GenerateCompleteClass_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder();

        // Build a complete class with using statements, namespace, and methods
        builder
            .AppendLine("using System;")
            .AppendLine("using System.Collections.Generic;")
            .AppendLine()
            .AppendLine("namespace MyApplication.Models")
            .Append("{")
            .AppendLine("/// <summary>")
            .AppendLine("/// Represents a customer entity.")
            .AppendLine("/// </summary>")
            .AppendLine("public class Customer")
            .Append("{")
            .AppendLine("private readonly string _id;")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// Initializes a new instance of the Customer class.")
            .AppendLine("/// </summary>")
            .AppendLine("/// <param name=\"id\">The customer identifier.</param>")
            .AppendLine("public Customer(string id)")
            .Append("{")
            .AppendLine("_id = id ?? throw new ArgumentNullException(nameof(id));")
            .Append("}")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets the customer identifier.")
            .AppendLine("/// </summary>")
            .AppendLine("public string Id => _id;")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets or sets the customer name.")
            .AppendLine("/// </summary>")
            .AppendLine("public string? Name { get; set; }")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets or sets the customer email address.")
            .AppendLine("/// </summary>")
            .AppendLine("public string? Email { get; set; }")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        // Basic verification - check that the output contains expected elements
        _ = await Assert.That(result).Contains("using System;");
        _ = await Assert.That(result).Contains("namespace MyApplication.Models");
        _ = await Assert.That(result).Contains("public class Customer");
        _ = await Assert.That(result).Contains("public Customer(string id)");
        _ = await Assert.That(result).Contains("public string Id => _id;");
        _ = await Assert.That(result).Contains("public string? Name { get; set; }");
        _ = await Assert.That(result).Contains("public string? Email { get; set; }");

        // Ensure proper indentation is applied
        _ = await Assert.That(result).Contains("    public class Customer");
        _ = await Assert.That(result).Contains("        private readonly string _id;");
    }

    [Test]
    public async Task GenerateInterface_WithMultipleMethods_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder();

        builder
            .AppendLine("using System;")
            .AppendLine("using System.Threading.Tasks;")
            .AppendLine()
            .AppendLine("namespace MyApplication.Services")
            .Append("{")
            .AppendLine("/// <summary>")
            .AppendLine("/// Defines the contract for customer service operations.")
            .AppendLine("/// </summary>")
            .AppendLine("public interface ICustomerService")
            .Append("{")
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets a customer by their identifier.")
            .AppendLine("/// </summary>")
            .AppendLine("/// <param name=\"id\">The customer identifier.</param>")
            .AppendLine("/// <returns>The customer if found; otherwise, null.</returns>")
            .AppendLine("Task<Customer?> GetCustomerAsync(string id);")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// Creates a new customer.")
            .AppendLine("/// </summary>")
            .AppendLine("/// <param name=\"customer\">The customer to create.</param>")
            .AppendLine("/// <returns>A task representing the asynchronous operation.</returns>")
            .AppendLine("Task CreateCustomerAsync(Customer customer);")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// Updates an existing customer.")
            .AppendLine("/// </summary>")
            .AppendLine("/// <param name=\"customer\">The customer to update.</param>")
            .AppendLine("/// <returns>A task representing the asynchronous operation.</returns>")
            .AppendLine("Task UpdateCustomerAsync(Customer customer);")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        // Basic verification - check that the output contains expected elements
        _ = await Assert.That(result).Contains("using System.Threading.Tasks;");
        _ = await Assert.That(result).Contains("namespace MyApplication.Services");
        _ = await Assert.That(result).Contains("public interface ICustomerService");
        _ = await Assert.That(result).Contains("Task<Customer?> GetCustomerAsync(string id);");
        _ = await Assert.That(result).Contains("Task CreateCustomerAsync(Customer customer);");
        _ = await Assert.That(result).Contains("Task UpdateCustomerAsync(Customer customer);");

        // Ensure proper indentation is applied
        _ = await Assert.That(result).Contains("    public interface ICustomerService");
        _ = await Assert.That(result).Contains("        Task<Customer?> GetCustomerAsync(string id);");
    }
}
