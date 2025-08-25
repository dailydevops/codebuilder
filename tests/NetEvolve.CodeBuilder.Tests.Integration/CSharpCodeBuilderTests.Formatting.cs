namespace NetEvolve.CodeBuilder.Tests.Integration;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task GenerateClassWithFormatting_UsingSpaces_Should_ProduceCorrectIndentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = false };

        _ = builder
            .AppendLine("public class TestClass")
            .Append("{")
            .AppendLine("public void Method1()")
            .Append("{")
            .AppendLine("Console.WriteLine(\"Hello World\");")
            .AppendLine("if (true)")
            .Append("{")
            .Append("Console.WriteLine(\"Nested\");")
            .Append("}")
            .Append("}")
            .AppendLine()
            .AppendLine("public void Method2()")
            .Append("{")
            .Append("try")
            .Append("{")
            .Append("DoSomething();")
            .Append("}")
            .Append("catch (Exception ex)")
            .Append("{")
            .Append("Console.WriteLine($\"Error: {ex.Message}\");")
            .Append("}")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result);
    }

    [Test]
    public async Task GenerateClassWithFormatting_UsingTabs_Should_ProduceCorrectIndentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        _ = builder
            .AppendLine("public class TestClass")
            .Append("{")
            .AppendLine("public void Method1()")
            .Append("{")
            .AppendLine("Console.WriteLine(\"Hello World\");")
            .AppendLine("if (true)")
            .Append("{")
            .Append("Console.WriteLine(\"Nested\");")
            .Append("}")
            .Append("}")
            .AppendLine()
            .AppendLine("public void Method2()")
            .Append("{")
            .Append("try")
            .Append("{")
            .Append("DoSomething();")
            .Append("}")
            .Append("catch (Exception ex)")
            .Append("{")
            .Append("Console.WriteLine($\"Error: {ex.Message}\");")
            .Append("}")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result);
    }

    [Test]
    public async Task GenerateEnum_WithDocumentation_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder
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
            .Append("Delivered = 8")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result);
    }
}
