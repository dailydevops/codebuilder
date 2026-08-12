namespace NetEvolve.CodeBuilder.Tests.Integration;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task GenerateFromMixedCharSources_Should_ProduceCorrectOutput()
    {
        var arrayFragment = "field one".ToCharArray();
        var memoryFragment = "field two".AsMemory();
        var spanFragment = "field three".AsSpan();

        var builder = new CSharpCodeBuilder()
            .AppendLine("public class MixedSourceFragment")
            .Append("{")
            .Append("public string A => \"")
            .Append(arrayFragment)
            .AppendLine("\";")
            .Append("public string B => \"")
            .Append(arrayFragment, 0, 5)
            .AppendLine("\";")
            .Append("public string C => \"")
            .Append(memoryFragment)
            .AppendLine("\";")
            .Append("public string D => \"")
            .Append(memoryFragment, 0, 5)
            .AppendLine("\";")
            .Append("public string E => \"")
            .Append(spanFragment)
            .AppendLine("\";")
            .Append("public string F => \"")
            .Append(spanFragment, 0, 5)
            .AppendLine("\";")
            .Append("public string G => \"")
            .Append("full string field", 0, 4)
            .AppendLine("\";")
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result).ConfigureAwait(false);
    }

    [Test]
#pragma warning disable S6640 // Unsafe code is intentional to exercise the pointer-based overloads
    public unsafe Task GenerateFromPointerSource_Should_ProduceCorrectOutput()
    {
        var text = "pointer field";

        string result;
        fixed (char* pointer = text)
        {
            var builder = new CSharpCodeBuilder()
                .AppendLine("public class PointerSourceFragment")
                .Append("{")
                .Append("public string Value => \"")
                .Append(pointer, text.Length)
                .AppendLine("\";")
                .Append("}");

            result = builder.ToString();
        }

        return Verify(result);
    }
#pragma warning restore S6640

    [Test]
    public async Task GenerateWithEmptyAndNullSources_Should_BeIgnored()
    {
        var builder = new CSharpCodeBuilder()
            .Append(default(char[]))
            .Append(Array.Empty<char>())
            .Append(default(char[]), 0, 0)
            .Append(ReadOnlyMemory<char>.Empty)
            .Append(ReadOnlySpan<char>.Empty)
            .Append(default(string))
            .Append(string.Empty)
            .Append("\0");

        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task GenerateNestedBracesFromRawCharsAndStrings_Should_ProduceCorrectOutput()
    {
        // Exercises the special handling of '{', '}', '[', ']', '\n', '\r' for both the
        // char and string overloads of Append, as used when a generator emits raw braces
        // received from an external template rather than literal source code.
        var builder = new CSharpCodeBuilder()
            .AppendLine("public class RawBraceFragment")
            .Append('{')
            .Append("public int[] Values")
            .Append('[')
            .Append("public int Value")
            .Append('\n')
            .Append(']')
            .Append('\r')
            .Append("[")
            .Append("public int Other")
            .AppendLine("\r\n")
            .Append("]")
            .Append('}');

        var result = builder.ToString();

        _ = await Verify(result).ConfigureAwait(false);
    }
}
