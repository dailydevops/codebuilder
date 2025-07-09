namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task Constructor_Default_Should_Create_Instance()
    {
        var builder = new CSharpCodeBuilder();

        _ = await Assert.That(builder).IsNotNull();
        _ = await Assert.That(builder.Length).IsEqualTo(0);
        _ = await Assert.That(builder.Capacity).IsGreaterThan(0);
        _ = await Assert.That(builder.UseTabs).IsEqualTo(false);
    }

    [Test]
    public async Task Constructor_WithCapacity_Should_Create_Instance_With_Specified_Capacity()
    {
        var initialCapacity = 100;
        var builder = new CSharpCodeBuilder(initialCapacity);

        _ = await Assert.That(builder).IsNotNull();
        _ = await Assert.That(builder.Length).IsEqualTo(0);
        _ = await Assert.That(builder.Capacity).IsGreaterThanOrEqualTo(initialCapacity);
        _ = await Assert.That(builder.UseTabs).IsEqualTo(false);
    }

    [Test]
    public async Task Constructor_WithZeroCapacity_Should_Create_Instance()
    {
        var builder = new CSharpCodeBuilder(0);

        _ = await Assert.That(builder).IsNotNull();
        _ = await Assert.That(builder.Length).IsEqualTo(0);
        _ = await Assert.That(builder.UseTabs).IsEqualTo(false);
    }

    [Test]
    public async Task Constructor_WithNegativeCapacity_Should_Throw_ArgumentOutOfRangeException() =>
        _ = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
        {
            _ = new CSharpCodeBuilder(-1);
            return Task.CompletedTask;
        });
}
