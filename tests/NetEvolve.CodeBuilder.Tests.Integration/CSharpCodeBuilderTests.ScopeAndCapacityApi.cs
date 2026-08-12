namespace NetEvolve.CodeBuilder.Tests.Integration;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task GenerateNestedScopes_WithScopeLine_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder().AppendLine("namespace MyApplication.Scopes;").AppendLine();

        using (builder.ScopeLine("public static class ScopedFragment"))
        {
            using (builder.ScopeLine("public static void Run()"))
            {
                using (builder.Scope())
                {
                    _ = builder.AppendLine("// nested block, e.g. an if-body");
                }

                _ = builder.AppendLine("DoWork();");
            }
        }

        var result = builder.ToString();

        _ = await Verify(result).ConfigureAwait(false);
    }

    [Test]
    public async Task GenerateFragment_ReusingBuilderAfterClear_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder(16);
        _ = builder.EnsureCapacity(256);

        _ = builder.AppendLine("public class DiscardedFragment").Append("{").Append("}");

        _ = builder.Clear();

#pragma warning disable CS0618 // Intend() is obsolete; deliberately exercised here for coverage
        _ = builder
            .Indent()
            .AppendLine("// manually indented comment before the class")
            .AppendLine("public class ReusedFragment")
            .Append("{")
            .Intend()
            .AppendLine("// manually indented comment inside the class")
            .Append("}");
#pragma warning restore CS0618

        var result = builder.ToString();

        _ = await Verify(result).ConfigureAwait(false);
    }

    [Test]
    public async Task EnsureCapacity_Should_IncreaseUnderlyingCapacity()
    {
        var builder = new CSharpCodeBuilder(4);

        _ = builder.EnsureCapacity(1024);

        _ = await Assert.That(builder.Capacity).IsGreaterThanOrEqualTo(1024);
    }

    [Test]
    public async Task Clear_Should_ResetLengthAndIndentation()
    {
        var builder = new CSharpCodeBuilder().AppendLine("public class Fragment").Append("{").AppendLine("Field();");

        _ = builder.Clear();

        _ = await Assert.That(builder.Length).IsEqualTo(0);

        _ = builder.AppendLine("// after clear, indentation should be back at zero");

        _ = await Assert.That(builder.ToString()).DoesNotStartWith(" ");
    }
}
