namespace NetEvolve.CodeBuilder.Tests.Integration;

using System;
using System.Globalization;

public partial class CSharpCodeBuilderIntegrationTests
{
    [Test]
    public async Task GenerateClassWithFormatting_UsingSpaces_Should_ProduceCorrectIndentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = false };

        builder
            .AppendLine("public class TestClass")
            .Append("{")
            .AppendLine("public void Method1()")
            .Append("{")
            .AppendLine("Console.WriteLine(\"Hello World\");")
            .AppendLine("if (true)")
            .Append("{")
            .AppendLine("Console.WriteLine(\"Nested\");")
            .Append("}")
            .Append("}")
            .AppendLine()
            .AppendLine("public void Method2()")
            .Append("{")
            .AppendLine("try")
            .Append("{")
            .AppendLine("DoSomething();")
            .Append("}")
            .AppendLine("catch (Exception ex)")
            .Append("{")
            .AppendLine("Console.WriteLine($\"Error: {ex.Message}\");")
            .Append("}")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        // Verify spaces-based indentation
        _ = await Assert.That(result).Contains("public class TestClass");
        _ = await Assert.That(result).Contains("    public void Method1()"); // 4 spaces indentation
        _ = await Assert.That(result).Contains("        Console.WriteLine(\"Hello World\");"); // 8 spaces indentation
        _ = await Assert.That(result).Contains("            Console.WriteLine(\"Nested\");"); // 12 spaces indentation
    }

    [Test]
    public async Task GenerateClassWithFormatting_UsingTabs_Should_ProduceCorrectIndentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        builder
            .AppendLine("public class TestClass")
            .Append("{")
            .AppendLine("public void Method1()")
            .Append("{")
            .AppendLine("Console.WriteLine(\"Hello World\");")
            .AppendLine("if (true)")
            .Append("{")
            .AppendLine("Console.WriteLine(\"Nested\");")
            .Append("}")
            .Append("}")
            .AppendLine()
            .AppendLine("public void Method2()")
            .Append("{")
            .AppendLine("try")
            .Append("{")
            .AppendLine("DoSomething();")
            .Append("}")
            .AppendLine("catch (Exception ex)")
            .Append("{")
            .AppendLine("Console.WriteLine($\"Error: {ex.Message}\");")
            .Append("}")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        // Verify tabs-based indentation
        _ = await Assert.That(result).Contains("public class TestClass");
        _ = await Assert.That(result).Contains("\tpublic void Method1()"); // 1 tab indentation
        _ = await Assert.That(result).Contains("\t\tConsole.WriteLine(\"Hello World\");"); // 2 tabs indentation
        _ = await Assert.That(result).Contains("\t\t\tConsole.WriteLine(\"Nested\");"); // 3 tabs indentation
    }

    [Test]
    public async Task GenerateEnum_WithDocumentation_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder();

        builder
            .AppendLine("using System;")
            .AppendLine()
            .AppendLine("namespace MyApplication.Enums")
            .Append("{")
            .AppendLine("/// <summary>")
            .AppendLine("/// Represents the status of an order.")
            .AppendLine("/// </summary>")
            .AppendLine("[Flags]")
            .AppendLine("public enum OrderStatus")
            .Append("{")
            .AppendLine("/// <summary>")
            .AppendLine("/// The order is pending.")
            .AppendLine("/// </summary>")
            .AppendLine("Pending = 1,")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// The order is being processed.")
            .AppendLine("/// </summary>")
            .AppendLine("Processing = 2,")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// The order has been shipped.")
            .AppendLine("/// </summary>")
            .AppendLine("Shipped = 4,")
            .AppendLine()
            .AppendLine("/// <summary>")
            .AppendLine("/// The order has been delivered.")
            .AppendLine("/// </summary>")
            .AppendLine("Delivered = 8")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        // Basic verification for enum generation
        _ = await Assert.That(result).Contains("namespace MyApplication.Enums");
        _ = await Assert.That(result).Contains("[Flags]");
        _ = await Assert.That(result).Contains("public enum OrderStatus");
        _ = await Assert.That(result).Contains("Pending = 1,");
        _ = await Assert.That(result).Contains("Processing = 2,");
        _ = await Assert.That(result).Contains("Shipped = 4,");
        _ = await Assert.That(result).Contains("Delivered = 8");

        // Ensure proper indentation is applied
        _ = await Assert.That(result).Contains("    public enum OrderStatus");
        _ = await Assert.That(result).Contains("        Pending = 1,");
    }
}
