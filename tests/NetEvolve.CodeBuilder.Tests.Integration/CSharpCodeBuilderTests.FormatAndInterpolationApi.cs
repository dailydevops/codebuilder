namespace NetEvolve.CodeBuilder.Tests.Integration;

using System;
using System.Globalization;

public partial class CSharpCodeBuilderTests
{
    [Test]
#pragma warning disable CA1305, MA0011 // Culture is intentionally omitted on some calls to exercise the convenience overloads
    public async Task GenerateFormattedMembers_Should_ProduceCorrectOutput()
    {
        var index = 3;
        var typeName = "decimal";
        FormattableString formattable = $"public {typeName} FieldFormattable;";
        FormattableString lineFormattable = $"public {typeName} LineFieldFormattable;";

        var builder = new CSharpCodeBuilder()
            .AppendLine("public class FormattedMembersFragment")
            .Append("{")
            .AppendFormat("public {0} Field0;", typeName)
            .AppendLine()
            .AppendFormat("public {0} Field{1};", typeName, index)
            .AppendLine()
            .AppendFormat(CultureInfo.InvariantCulture, "public {0} Field{1}_{2};", typeName, index, index + 1)
            .AppendLine()
            .AppendFormat(formattable)
            .AppendLine()
            .AppendLineFormat("public {0} LineField0;", typeName)
            .AppendLineFormat("public {0} LineField{1};", typeName, index)
            .AppendLineFormat(CultureInfo.InvariantCulture, "public {0} LineField{1}_{2};", typeName, index, index + 1)
            .AppendLineFormat(lineFormattable)
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result).ConfigureAwait(false);
    }
#pragma warning restore CA1305, MA0011

    [Test]
    public async Task GenerateFormattedMembers_WithNullFormattable_Should_OnlyAppendLineTerminator()
    {
        var builder = new CSharpCodeBuilder()
            .AppendFormat(default(FormattableString))
            .AppendLine("marker")
            .AppendLineFormat(default(FormattableString));

        var result = builder.ToString();

        _ = await Assert.That(result).Contains("marker");
    }

    [Test]
    public async Task GenerateWithInterpolatedHandler_Should_ProduceCorrectOutput()
    {
        var className = "InterpolatedFragment";
        var propertyName = "Total";
        var value = 42;

        var builder = new CSharpCodeBuilder();
        _ = builder.AppendLineInterpolated($"public class {className}");
        _ = builder.Append("{");
        _ = builder.AppendInterpolated($"public int {propertyName} => {value, 6:D3};");
        _ = builder.AppendLine();
        _ = builder.AppendLineInterpolated($"public string Empty => \"{string.Empty}\";");
        _ = builder.Append("}");

        var result = builder.ToString();

        _ = await Verify(result).ConfigureAwait(false);
    }
}
