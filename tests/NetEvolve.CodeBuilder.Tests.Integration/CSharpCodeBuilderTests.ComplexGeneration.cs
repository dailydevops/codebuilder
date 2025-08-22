namespace NetEvolve.CodeBuilder.Tests.Integration;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task GenerateCompleteClass_Should_ProduceCorrectOutput()
    {
        // Build a complete class with using statements, namespace, and methods
        var builder = new CSharpCodeBuilder()
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
            .Append("public string? Email { get; set; }")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result);
    }

    [Test]
    public async Task GenerateInterface_WithMultipleMethods_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder()
            .AppendLine("using System;")
            .AppendLine("using System.Threading.Tasks;")
            .AppendLine()
            .AppendLine("namespace MyApplication.Services")
            .Append("{")
            .AppendXmlDocSummary("Defines the contract for customer service operations.")
            .AppendLine("public interface ICustomerService")
            .Append("{")
            .AppendXmlDocSummary("Gets a customer by their identifier.")
            .AppendXmlDocParam("id", "The customer identifier.")
            .AppendXmlDocReturns("The customer if found; otherwise, null.")
            .AppendLine("Task<Customer?> GetCustomerAsync(string id);")
            .AppendLine()
            .AppendXmlDocSummary("Creates a new customer.")
            .AppendXmlDocParam("customer", "The customer to create.")
            .AppendXmlDocReturns("A task representing the asynchronous operation.")
            .AppendLine("Task CreateCustomerAsync(Customer customer);")
            .AppendLine()
            .AppendXmlDocSummary("Updates an existing customer.")
            .AppendXmlDocParam("customer", "The customer to update.")
            .AppendXmlDocReturns("A task representing the asynchronous operation.")
            .Append("Task UpdateCustomerAsync(Customer customer);")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result);
    }
}
