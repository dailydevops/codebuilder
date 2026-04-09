namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;
using System.Globalization;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task AppendLineFormat_OneArgument_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineFormat(CultureInfo.InvariantCulture, "Value: {0}", 42);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Value: 42" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineFormat_OneArgument_Should_Use_InvariantCulture()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineFormat("Value: {0}", (object?)42);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Value: 42" + Environment.NewLine);
    }

    // CA1305 is intentionally suppressed: the purpose of this test is to verify that the
    // no-provider overload resolves correctly and delegates to InvariantCulture.
#pragma warning disable CA1305
    [Test]
    public async Task AppendLineFormat_MultipleArguments_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineFormat("public {0} {1}", "string", "Name");

        _ = await Assert.That(builder.ToString()).IsEqualTo("public string Name" + Environment.NewLine);
    }
#pragma warning restore CA1305

    [Test]
    public async Task AppendLineFormat_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();

        _ = builder.AppendLine().AppendLineFormat("Value: {0}", (object?)42);

        var expected = Environment.NewLine + "    Value: 42" + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLineFormat_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendLineFormat("Value: {0}", (object?)42);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineFormat_WithProvider_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineFormat(CultureInfo.InvariantCulture, "{0:N2}", 1234.56m);

        _ = await Assert.That(builder.ToString()).IsEqualTo("1,234.56" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineFormat_NullFormat_Should_Throw_ArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendLineFormat(CultureInfo.InvariantCulture, null!, 42);
        });

        _ = await Assert.That(exception).IsNotNull();
    }

    [Test]
    public async Task AppendLineFormat_Should_Set_IsNewline_True_After_Call()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();

        _ = builder.AppendLineFormat("Value: {0}", (object?)1).AppendLineFormat("Value: {0}", (object?)2);

        // Both lines are indented: _isNewline=true at builder start, so both calls get indentation applied.
        var expected = "    Value: 1" + Environment.NewLine + "    Value: 2" + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLineFormat_FormattableString_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);
        var typeName = "string";
        var memberName = "Name";

        _ = builder.AppendLineFormat((FormattableString)$"public {typeName} {memberName}");

        _ = await Assert.That(builder.ToString()).IsEqualTo("public string Name" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineFormat_FormattableString_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();
        var value = 42;

        _ = builder.AppendLine().AppendLineFormat((FormattableString)$"Value: {value}");

        var expected = Environment.NewLine + "    Value: 42" + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLineFormat_FormattableString_Null_Should_Append_Only_LineTerminator()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineFormat((FormattableString?)null);

        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLineFormat_FormattableString_Should_Use_InvariantCulture()
    {
        var builder = new CSharpCodeBuilder(10);
        var value = 1234.56m;

        _ = builder.AppendLineFormat((FormattableString)$"{value:N2}");

        _ = await Assert.That(builder.ToString()).IsEqualTo("1,234.56" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineFormat_FormattableString_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var text = "Hello";

        var result = builder.AppendLineFormat((FormattableString)$"{text}");

        _ = await Assert.That(result).IsEqualTo(builder);
    }
}
