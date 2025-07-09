namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task AppendLine_Empty_Should_Append_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine();

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_String_Should_Append_String_With_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine("Hello World");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("Hello World" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_String_Null_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine((string?)null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_String_Empty_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine(string.Empty);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_ReadOnlyMemory_Should_Append_Characters_With_Newline()
    {
        var builder = new CSharpCodeBuilder();
        var memory = "Hello World".AsMemory();

        var result = builder.AppendLine(memory);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("Hello World" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_ReadOnlyMemory_Empty_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder();
        var memory = ReadOnlyMemory<char>.Empty;

        var result = builder.AppendLine(memory);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_ReadOnlyMemory_With_StartIndex_And_Count_Should_Append_Subset_With_Newline()
    {
        var builder = new CSharpCodeBuilder();
        var memory = "Hello World".AsMemory();

        var result = builder.AppendLine(memory, 6, 5);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("World" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_CharArray_Should_Append_Characters_With_Newline()
    {
        var builder = new CSharpCodeBuilder();
        var chars = "Hello".ToCharArray();

        var result = builder.AppendLine(chars);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("Hello" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_CharArray_Null_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine((char[]?)null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_CharArray_Empty_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder();
        var chars = Array.Empty<char>();

        var result = builder.AppendLine(chars);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_CharArray_With_StartIndex_And_Count_Should_Append_Subset_With_Newline()
    {
        var builder = new CSharpCodeBuilder();
        var chars = "Hello World".ToCharArray();

        var result = builder.AppendLine(chars, 6, 5);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("World" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_Char_Should_Append_Character_With_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine('X');

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("X" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_Char_With_RepeatCount_Should_Append_Repeated_Character_With_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine('X', 3);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("XXX" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_Bool_True_Should_Append_True_With_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine(true);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("true" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_Bool_False_Should_Append_False_With_Newline()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendLine(false);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("false" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_Multiple_Should_Create_Multiple_Lines()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendLine("First").AppendLine("Second").AppendLine("Third");

        var expected =
            "First"
            + Environment.NewLine
            + "Second"
            + Environment.NewLine
            + "Third"
            + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLine_With_Indentation_Should_Apply_Indentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();

        _ = builder.AppendLine().AppendLine("Hello");

        var expected = Environment.NewLine + "    Hello" + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLine_Unsafe_CharPointer_Should_Append_Characters_With_Newline()
    {
        var builder = new CSharpCodeBuilder();
        var text = "Hello";
        CSharpCodeBuilder result;
        string builderResult;

        unsafe
        {
            fixed (char* ptr = text)
            {
                result = builder.AppendLine(ptr, text.Length);
            }
        }

        builderResult = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builderResult).IsEqualTo("Hello" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_Unsafe_CharPointer_Null_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder();
        CSharpCodeBuilder result;
        string builderResult;

        unsafe
        {
            result = builder.AppendLine(null, 0);
        }

        builderResult = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builderResult).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_Unsafe_CharPointer_Negative_Length_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder();
        var text = "Hello";
        CSharpCodeBuilder result;
        string builderResult;

        unsafe
        {
            fixed (char* ptr = text)
            {
                result = builder.AppendLine(ptr, -1);
            }
        }

        builderResult = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builderResult).IsEqualTo(Environment.NewLine);
    }
}
