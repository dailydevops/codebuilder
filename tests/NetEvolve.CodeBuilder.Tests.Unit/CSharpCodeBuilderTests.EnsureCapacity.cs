namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task EnsureCapacity_Smaller_Than_Current_Should_Not_Change_Capacity()
    {
        var builder = new CSharpCodeBuilder(100);
        var originalCapacity = builder.Capacity;

        var result = builder.EnsureCapacity(50);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.Capacity).IsEqualTo(originalCapacity);
    }

    [Test]
    public async Task EnsureCapacity_Larger_Than_Current_Should_Increase_Capacity()
    {
        var builder = new CSharpCodeBuilder(10);
        var originalCapacity = builder.Capacity;

        var result = builder.EnsureCapacity(200);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.Capacity).IsGreaterThanOrEqualTo(200);
        _ = await Assert.That(builder.Capacity).IsGreaterThan(originalCapacity);
    }

    [Test]
    public async Task EnsureCapacity_Equal_To_Current_Should_Not_Change_Capacity()
    {
        var builder = new CSharpCodeBuilder(100);
        var originalCapacity = builder.Capacity;

        var result = builder.EnsureCapacity(originalCapacity);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.Capacity).IsEqualTo(originalCapacity);
    }

    [Test]
    public async Task EnsureCapacity_Zero_Should_Not_Change_Capacity()
    {
        var builder = new CSharpCodeBuilder(50);
        var originalCapacity = builder.Capacity;

        var result = builder.EnsureCapacity(0);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.Capacity).IsEqualTo(originalCapacity);
    }

    [Test]
    public async Task EnsureCapacity_Should_Not_Affect_Content()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello World");
        var originalContent = builder.ToString();

        _ = builder.EnsureCapacity(200);

        _ = await Assert.That(builder.ToString()).IsEqualTo(originalContent);
        _ = await Assert.That(builder.Length).IsEqualTo(originalContent.Length);
    }

    [Test]
    public async Task EnsureCapacity_Should_Allow_Method_Chaining()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.EnsureCapacity(100).Append("Hello");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task EnsureCapacity_Multiple_Calls_Should_Work()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.EnsureCapacity(50);
        var capacity50 = builder.Capacity;

        _ = builder.EnsureCapacity(100);
        var capacity100 = builder.Capacity;

        _ = await Assert.That(capacity100).IsGreaterThanOrEqualTo(100);
        _ = await Assert.That(capacity100).IsGreaterThanOrEqualTo(capacity50);
    }

    [Test]
    public async Task EnsureCapacity_Negative_Value_Should_Throw_ArgumentOutOfRangeException() =>
        _ = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
        {
            var builder = new CSharpCodeBuilder();
            _ = builder.EnsureCapacity(-1);
            return Task.CompletedTask;
        });
}
