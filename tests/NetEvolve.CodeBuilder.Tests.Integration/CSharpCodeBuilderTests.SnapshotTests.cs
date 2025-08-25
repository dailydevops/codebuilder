namespace NetEvolve.CodeBuilder.Tests.Integration;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task GenerateCompleteClass_Should_MatchSnapshot()
    {
        var builder = new CSharpCodeBuilder();

        // Build a complete class structure
        _ = builder
            .AppendLine("using System;")
            .AppendLine("using System.Collections.Generic;")
            .AppendLine()
            .AppendLine("namespace MyApplication.Models")
            .Append("{")
            .AppendLine("public class Customer")
            .Append("{")
            .AppendLine("public string Id { get; set; }")
            .Append("public string Name { get; set; }")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result);
    }

    [Test]
    [MatrixDataSource]
    public async Task GenerateWithDifferentFormats_Should_MatchSnapshots(bool useTabs)
    {
        // Test with spaces
        var spacesBuilder = new CSharpCodeBuilder { UseTabs = useTabs };
        _ = spacesBuilder
            .AppendLine("public class TestClass")
            .Append("{")
            .Append("public void Method() { }")
            .Append("}");

        _ = await Verify(spacesBuilder.ToString()).HashParameters().UseParameters(useTabs);
    }
}
