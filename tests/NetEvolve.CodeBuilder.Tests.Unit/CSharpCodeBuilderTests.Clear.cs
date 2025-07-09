namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task Clear_Empty_Builder_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.Clear();

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.Length).IsEqualTo(0);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Clear_Builder_With_Content_Should_Remove_All_Content()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello World");

        var result = builder.Clear();

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.Length).IsEqualTo(0);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Clear_Should_Reset_Indentation_Level()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();
        builder.IncrementIndent();
        _ = builder.AppendLine().Append("Hello");

        _ = builder.Clear();
        _ = builder.AppendLine().Append("World");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "World");
    }

    [Test]
    public async Task Clear_Should_Allow_Method_Chaining()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello");

        var result = builder.Clear().Append("World");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("World");
    }

    [Test]
    public async Task Clear_Multiple_Times_Should_Work()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("First");
        _ = builder.Clear();
        _ = builder.Append("Second");
        _ = builder.Clear();
        _ = builder.Append("Third");

        var result = builder.ToString();

        _ = await Assert.That(result).IsEqualTo("Third");
    }

    [Test]
    public async Task Clear_Should_Preserve_UseTabs_Setting()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };
        _ = builder.Append("Hello");

        _ = builder.Clear();

        _ = await Assert.That(builder.UseTabs).IsEqualTo(true);
    }

    [Test]
    public async Task Clear_Should_Preserve_Capacity()
    {
        var builder = new CSharpCodeBuilder(100);
        var originalCapacity = builder.Capacity;
        _ = builder.Append("Hello World");

        _ = builder.Clear();

        _ = await Assert.That(builder.Capacity).IsEqualTo(originalCapacity);
    }
}
