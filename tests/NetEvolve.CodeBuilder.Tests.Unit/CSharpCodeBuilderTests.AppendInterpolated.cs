#if NET6_0_OR_GREATER
namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task AppendInterpolated_LiteralOnly_Should_Append_Correctly()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendInterpolated($"Hello World");

        _ = await Assert.That(builder.ToString()).IsEqualTo("Hello World");
    }

    [Test]
    public async Task AppendInterpolated_WithFormattedValue_Should_Append_Correctly()
    {
        var builder = new CSharpCodeBuilder();
        var typeName = "string";
        var memberName = "Name";

        _ = builder.AppendInterpolated($"public {typeName} {memberName}");

        _ = await Assert.That(builder.ToString()).IsEqualTo("public string Name");
    }

    [Test]
    public async Task AppendInterpolated_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();
        var value = 42;

        _ = builder.AppendLine().AppendInterpolated($"Value: {value}");

        var expected = Environment.NewLine + "    Value: 42";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendInterpolated_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendInterpolated($"Hello");

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendInterpolated_Empty_Should_Not_Append_Indentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();

        _ = builder.AppendLine().AppendInterpolated($"");

        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLineInterpolated_Should_Append_Line_Terminator()
    {
        var builder = new CSharpCodeBuilder();
        var typeName = "MyClass";

        _ = builder.AppendLineInterpolated($"public class {typeName}");

        _ = await Assert.That(builder.ToString()).IsEqualTo("public class MyClass" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineInterpolated_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();
        var value = 1;

        _ = builder.AppendLine().AppendLineInterpolated($"Value: {value}");

        var expected = Environment.NewLine + "    Value: 1" + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLineInterpolated_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLineInterpolated($"Hello");

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendInterpolated_FormattedValue_With_Format_Should_Use_InvariantCulture()
    {
        var builder = new CSharpCodeBuilder();
        var value = 1234.56m;

        _ = builder.AppendInterpolated($"{value:N2}");

        _ = await Assert.That(builder.ToString()).IsEqualTo("1,234.56");
    }

    [Test]
    public async Task AppendInterpolated_FormattedValue_With_Alignment_Should_PadLeft()
    {
        var builder = new CSharpCodeBuilder();
        var value = 42;

        _ = builder.AppendInterpolated($"{value, 6}");

        _ = await Assert.That(builder.ToString()).IsEqualTo("    42");
    }

    [Test]
    public async Task AppendLineInterpolated_Consecutive_Should_Indent_Each_Line()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();

        _ = builder.AppendLine().AppendLineInterpolated($"Line 1").AppendLineInterpolated($"Line 2");

        var expected = Environment.NewLine + "    Line 1" + Environment.NewLine + "    Line 2" + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }
}
#endif
