namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task ToString_Empty_Should_Return_Empty_String()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToString_WithContent_Should_Return_Content()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello World");

        var result = builder.ToString();

        _ = await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task ToString_WithMultipleAppends_Should_Return_All_Content()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello").Append(" ").Append("World");

        var result = builder.ToString();

        _ = await Assert.That(result).IsEqualTo("Hello World");
    }

    [Test]
    public async Task ToString_WithIndentation_Should_Return_Content_With_Indentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();

        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "    Hello");
    }

    [Test]
    public async Task ToString_WithTabs_Should_Return_Content_With_Tab_Indentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };
        builder.IncrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();

        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "\tHello");
    }

    [Test]
    public async Task ToString_AfterClear_Should_Return_Empty_String()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello World");
        _ = builder.Clear();

        var result = builder.ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task ToString_Multiple_Calls_Should_Return_Same_Result()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello World");

        var result1 = builder.ToString();
        var result2 = builder.ToString();

        _ = await Assert.That(result1).IsEqualTo("Hello World");
        _ = await Assert.That(result2).IsEqualTo("Hello World");
        _ = await Assert.That(result1).IsEqualTo(result2);
    }
}
