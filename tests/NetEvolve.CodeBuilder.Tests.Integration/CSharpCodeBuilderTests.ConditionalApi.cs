namespace NetEvolve.CodeBuilder.Tests.Integration;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task GenerateConfigurableClass_AllFlagsEnabled_Should_ProduceCorrectOutput()
    {
        var result = BuildConfigurableClass(enabled: true);

        _ = await Verify(result).ConfigureAwait(false);
    }

    [Test]
    public async Task GenerateConfigurableClass_AllFlagsDisabled_Should_ProduceCorrectOutput()
    {
        var result = BuildConfigurableClass(enabled: false);

        _ = await Verify(result).ConfigureAwait(false);
    }

    // Exercises every AppendIf/AppendLineIf overload for both the "condition true" and
    // "condition false" branches, mirroring how a real source generator toggles optional
    // code fragments (feature flags, generated attributes, diagnostics, ...).
    private static string BuildConfigurableClass(bool enabled)
    {
        var repeatChar = '-';
        var charArray = "// array-comment".ToCharArray();
        var memory = "// memory-comment".AsMemory();
        var span = "// span-comment".AsSpan();

        var builder = new CSharpCodeBuilder()
            .AppendLineIf(enabled, "// generated diagnostics header")
            .AppendLineIf(enabled)
            .AppendLine("public class ConfigurableService")
            .Append("{")
            .AppendIf(enabled, repeatChar, 20)
            .AppendLineIf(enabled)
            .AppendIf(enabled, "public bool IsEnabled")
            .AppendLineIf(enabled, " => true;")
            .AppendLineIf(!enabled, "public bool IsEnabled => false;")
            .AppendIf(enabled, true)
            .AppendLineIf(enabled)
            .AppendIf(!enabled, false)
            .AppendLineIf(!enabled)
            .AppendLineIf(enabled, charArray)
            .AppendLineIf(enabled, charArray, 3, 5)
            .AppendLineIf(enabled, memory)
            .AppendLineIf(enabled, memory, 3, 6)
            .AppendLineIf(enabled, span)
            .AppendLineIf(enabled, span, 3, 6)
            .AppendIf(enabled, memory)
            .AppendIf(enabled, memory, 3, 6)
            .AppendIf(enabled, span)
            .AppendIf(enabled, span, 3, 6)
            .AppendIf(enabled, charArray)
            .AppendIf(enabled, charArray, 3, 5)
            .AppendIf(enabled, 'x')
            .AppendLineIf(enabled, 'y')
            .AppendLineIf(enabled, 'z', 3)
            .AppendIf(enabled, "trailing-marker", 0, 8)
            .AppendLineIf(enabled, "trailing-marker-line", 0, 13)
            .Append("}");

        return builder.ToString();
    }

    [Test]
#pragma warning disable S6640 // Unsafe code is intentional to exercise the pointer-based overloads
    public unsafe Task GenerateConfigurableClass_WithPointerFragments_Should_ProduceCorrectOutput()
    {
        var text = "// pointer-comment";

        string result;
        fixed (char* pointer = text)
        {
            var builder = new CSharpCodeBuilder()
                .AppendLine("public class PointerBackedFragment")
                .Append("{")
                .AppendIf(true, pointer, text.Length)
                .AppendLineIf(true)
                .AppendIf(false, pointer, text.Length)
                .AppendLineIf(true, pointer, text.Length)
                .AppendLineIf(false, pointer, text.Length)
                .Append("}");

            result = builder.ToString();
        }

        return Verify(result);
    }
#pragma warning restore S6640
}
