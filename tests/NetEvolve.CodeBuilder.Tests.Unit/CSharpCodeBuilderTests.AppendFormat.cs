namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;
using System.Globalization;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task AppendFormat_OneArgument_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Value: {0}", 42);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Value: 42");
    }

    [Test]
    public async Task AppendFormat_OneArgument_Should_Use_InvariantCulture()
    {
        var builder = new CSharpCodeBuilder(10);
        var decimalValue = 1234.56m;

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Value: {0:N2}", decimalValue);

        // InvariantCulture uses period as decimal separator
        _ = await Assert.That(builder.ToString()).IsEqualTo("Value: 1,234.56");
    }

    [Test]
    public async Task AppendFormat_OneArgument_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();

        // First append a newline, then format text to see indentation
        _ = builder.AppendLine().AppendFormat(CultureInfo.InvariantCulture, "Value: {0}", 42);

        var expected = Environment.NewLine + "    Value: 42";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendFormat_OneArgument_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendFormat(CultureInfo.InvariantCulture, "Value: {0}", 42);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendFormat_OneArgument_Null_Format_Should_Throw_ArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendFormat(CultureInfo.InvariantCulture, null!, 42);
        });

        _ = await Assert.That(exception.ParamName).IsEqualTo("format");
    }

    [Test]
    public async Task AppendFormat_OneArgument_Invalid_Format_Should_Throw_FormatException() =>
        await Assert.ThrowsAsync<FormatException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Value: {1}", 42);
            return Task.CompletedTask;
        });

    [Test]
    public async Task AppendFormat_TwoArguments_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}", 42, "test");

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: 42, test");
    }

    [Test]
    public async Task AppendFormat_TwoArguments_Should_Use_InvariantCulture()
    {
        var builder = new CSharpCodeBuilder(10);
        var decimalValue = 1234.56m;

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0:N2}, {1}", decimalValue, "test");

        var result = builder.ToString();

        _ = await Assert.That(result).IsEqualTo("Values: 1,234.56, test");
    }

    [Test]
    public async Task AppendFormat_TwoArguments_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();

        // First append a newline, then format text to see indentation
        var result = builder
            .AppendLine()
            .AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}", 42, "test")
            .ToString();

        var expected = Environment.NewLine + "    Values: 42, test";
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendFormat_TwoArguments_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}", 42, "test");

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendFormat_TwoArguments_Null_Format_Should_Throw_ArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendFormat(CultureInfo.InvariantCulture, null!, 42, "test");
        });

        _ = await Assert.That(exception.ParamName).IsEqualTo("format");
    }

    [Test]
    public async Task AppendFormat_TwoArguments_Invalid_Format_Should_Throw_FormatException() =>
        await Assert.ThrowsAsync<FormatException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {2}", 42, "test");
            return Task.CompletedTask;
        });

    [Test]
    public async Task AppendFormat_ThreeArguments_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}, {2}", 42, "test", true);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: 42, test, True");
    }

    [Test]
    public async Task AppendFormat_ThreeArguments_Should_Use_InvariantCulture()
    {
        var builder = new CSharpCodeBuilder(10);
        var decimalValue = 1234.56m;

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0:N2}, {1}, {2}", decimalValue, "test", true);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: 1,234.56, test, True");
    }

    [Test]
    public async Task AppendFormat_ThreeArguments_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();

        // First append a newline, then format text to see indentation
        _ = builder.AppendLine().AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}, {2}", 42, "test", true);

        var expected = Environment.NewLine + "    Values: 42, test, True";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendFormat_ThreeArguments_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}, {2}", 42, "test", true);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendFormat_ThreeArguments_Null_Format_Should_Throw_ArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendFormat(CultureInfo.InvariantCulture, null!, 42, "test", true);
        });

        _ = await Assert.That(exception.ParamName).IsEqualTo("format");
    }

    [Test]
    public async Task AppendFormat_ThreeArguments_Invalid_Format_Should_Throw_FormatException() =>
        await Assert.ThrowsAsync<FormatException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}, {3}", 42, "test", true);
            return Task.CompletedTask;
        });

    [Test]
    public async Task AppendFormat_ParamsArray_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}, {2}, {3}", 42, "test", true, 3.14);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: 42, test, True, 3.14");
    }

    [Test]
    public async Task AppendFormat_ParamsArray_Should_Use_InvariantCulture()
    {
        var builder = new CSharpCodeBuilder(10);
        var decimalValue = 1234.56m;

        _ = builder.AppendFormat(
            CultureInfo.InvariantCulture,
            "Values: {0:N2}, {1}, {2}, {3:F2}",
            decimalValue,
            "test",
            true,
            3.14
        );

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: 1,234.56, test, True, 3.14");
    }

    [Test]
    public async Task AppendFormat_ParamsArray_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();

        // First append a newline, then format text to see indentation
        _ = builder
            .AppendLine()
            .AppendFormat(CultureInfo.InvariantCulture, "Values: {0}, {1}, {2}, {3}", 42, "test", true, 3.14);

        var expected = Environment.NewLine + "    Values: 42, test, True, 3.14";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendFormat_ParamsArray_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendFormat(
            CultureInfo.InvariantCulture,
            "Values: {0}, {1}, {2}, {3}",
            42,
            "test",
            true,
            3.14
        );

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendFormat_ParamsArray_Null_Format_Should_Throw_ArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendFormat(CultureInfo.InvariantCulture, null!, 42, "test", true, 3.14);
        });

        _ = await Assert.That(exception.ParamName).IsEqualTo("format");
    }

    [Test]
    public async Task AppendFormat_ParamsArray_Invalid_Format_Should_Throw_FormatException() =>
        await Assert.ThrowsAsync<FormatException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.AppendFormat(
                CultureInfo.InvariantCulture,
                "Values: {0}, {1}, {2}, {4}",
                42,
                "test",
                true,
                3.14
            );
            return Task.CompletedTask;
        });

    [Test]
    public void AppendFormat_EmptyArray_ThrowsFormatException()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = Assert.Throws<FormatException>(() =>
            builder.AppendFormat(CultureInfo.InvariantCulture, "No params: {0}", [])
        );
    }

    [Test]
    public async Task AppendFormat_WithProvider_OneArgument_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);
        var cultureInfo = new CultureInfo("de-DE");

        _ = builder.AppendFormat(cultureInfo, "Value: {0:N2}", 1234.56);

        // French culture uses comma as decimal separator
        _ = await Assert.That(builder.ToString()).IsEqualTo("Value: 1.234,56");
    }

    [Test]
    public async Task AppendFormat_WithProvider_TwoArguments_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);
        var cultureInfo = new CultureInfo("de-DE");

        _ = builder.AppendFormat(cultureInfo, "Values: {0:N2}, {1}", 1234.56, "test");

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: 1.234,56, test");
    }

    [Test]
    public async Task AppendFormat_WithProvider_ThreeArguments_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);
        var cultureInfo = new CultureInfo("de-DE");

        _ = builder.AppendFormat(cultureInfo, "Values: {0:N2}, {1}, {2}", 1234.56, "test", true);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: 1.234,56, test, True");
    }

    [Test]
    public async Task AppendFormat_WithProvider_ParamsArray_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(10);
        var cultureInfo = new CultureInfo("de-DE");

        _ = builder.AppendFormat(cultureInfo, "Values: {0:N2}, {1}, {2}, {3:F2}", 1234.56, "test", true, 3.14);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: 1.234,56, test, True, 3,14");
    }

    [Test]
    public async Task AppendFormat_WithProvider_NullProvider_Should_Use_CurrentCulture()
    {
        var builder = new CSharpCodeBuilder(10);
        var originalCulture = CultureInfo.CurrentCulture;

        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
#pragma warning disable S3220 // Method calls should not resolve ambiguously to overloads with "params"
            _ = builder.AppendFormat(null, "Value: {0:N2}", 1234.56);
#pragma warning restore S3220 // Method calls should not resolve ambiguously to overloads with "params"

            _ = await Assert.That(builder.ToString()).IsEqualTo("Value: 1.234,56");
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [Test]
    public async Task AppendFormat_Multiple_Calls_Should_Append_Sequentially()
    {
        var builder = new CSharpCodeBuilder(20);

        _ = builder
            .AppendFormat(CultureInfo.InvariantCulture, "First: {0}", 1)
            .AppendFormat(CultureInfo.InvariantCulture, " Second: {0}", 2)
            .AppendFormat(CultureInfo.InvariantCulture, " Third: {0}", 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("First: 1 Second: 2 Third: 3");
    }

    [Test]
    public async Task AppendFormat_With_NonSequentialFormatItems_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(20);

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Values: {1}, {0}, {2}", "A", "B", "C");

        _ = await Assert.That(builder.ToString()).IsEqualTo("Values: B, A, C");
    }

    [Test]
    public async Task AppendFormat_With_RepeatedFormatItems_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(20);

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Value: {0}, repeat: {0}", "test");

        _ = await Assert.That(builder.ToString()).IsEqualTo("Value: test, repeat: test");
    }

    [Test]
    public async Task AppendFormat_WithComplexFormatting_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder(30);
        var date = new DateTime(2023, 1, 15, 0, 0, 0, DateTimeKind.Utc);

        _ = builder.AppendFormat(CultureInfo.InvariantCulture, "Date: {0:yyyy-MM-dd}, Value: {1:X8}", date, 255);

        _ = await Assert.That(builder.ToString()).IsEqualTo("Date: 2023-01-15, Value: 000000FF");
    }
}
