namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task AppendLineIf_NoParameters_Condition_True_Should_Append_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true);

        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_NoParameters_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(false);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_NoParameters_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendLineIf(true);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_String_Condition_True_Should_Append_String_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, "test");

        _ = await Assert.That(builder.ToString()).IsEqualTo("test" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_String_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(false, "test");

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_String_Null_Condition_True_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, (string?)null);

        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_String_Empty_Condition_True_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, string.Empty);

        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_String_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendLineIf(true, "test");

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_ReadOnlyMemory_Condition_True_Should_Append_Characters_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abc".AsMemory();

        _ = builder.AppendLineIf(true, memory);

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_ReadOnlyMemory_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abc".AsMemory();

        _ = builder.AppendLineIf(false, memory);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_ReadOnlyMemory_Empty_Condition_True_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = ReadOnlyMemory<char>.Empty;

        _ = builder.AppendLineIf(true, memory);

        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_ReadOnlyMemory_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abc".AsMemory();

        var result = builder.AppendLineIf(true, memory);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_ReadOnlyMemory_With_StartIndex_And_Count_Condition_True_Should_Append_Specified_Characters_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abcde".AsMemory();

        _ = builder.AppendLineIf(true, memory, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("bcd" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_ReadOnlyMemory_With_StartIndex_And_Count_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abcde".AsMemory();

        _ = builder.AppendLineIf(false, memory, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_ReadOnlyMemory_With_StartIndex_And_Count_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abcde".AsMemory();

        var result = builder.AppendLineIf(true, memory, 1, 3);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_CharArray_Condition_True_Should_Append_All_Characters_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c' };

        _ = builder.AppendLineIf(true, chars);

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_CharArray_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c' };

        _ = builder.AppendLineIf(false, chars);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_CharArray_Null_Condition_True_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, (char[]?)null);

        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_CharArray_Empty_Condition_True_Should_Append_Only_Newline()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = Array.Empty<char>();

        _ = builder.AppendLineIf(true, chars);

        _ = await Assert.That(builder.ToString()).IsEqualTo(Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_CharArray_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c' };

        var result = builder.AppendLineIf(true, chars);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_CharArray_With_StartIndex_And_Count_Condition_True_Should_Append_Specified_Characters_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c', 'd', 'e' };

        _ = builder.AppendLineIf(true, chars, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("bcd" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_CharArray_With_StartIndex_And_Count_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c', 'd', 'e' };

        _ = builder.AppendLineIf(false, chars, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_CharArray_With_StartIndex_And_Count_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c', 'd', 'e' };

        var result = builder.AppendLineIf(true, chars, 1, 3);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_CharPointer_Condition_True_Should_Append_Characters_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = "abc".ToCharArray();

        unsafe
        {
            fixed (char* ptr = chars)
            {
                _ = builder.AppendLineIf(true, ptr, chars.Length);
            }
        }

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_CharPointer_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = "abc".ToCharArray();

        unsafe
        {
            fixed (char* ptr = chars)
            {
                _ = builder.AppendLineIf(false, ptr, chars.Length);
            }
        }

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_CharPointer_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = "abc".ToCharArray();
        CSharpCodeBuilder result;

        unsafe
        {
            fixed (char* ptr = chars)
            {
                result = builder.AppendLineIf(true, ptr, chars.Length);
            }
        }

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_Char_Condition_True_Should_Append_Character_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, 'x');

        _ = await Assert.That(builder.ToString()).IsEqualTo("x" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_Char_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(false, 'x');

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_Char_Should_Handle_Special_Characters_When_Condition_True()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, '{');

        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("{" + Environment.NewLine + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_Char_Should_Not_Handle_Special_Characters_When_Condition_False()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(false, '{');

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_Char_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendLineIf(true, 'x');

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_Char_With_RepeatCount_Condition_True_Should_Append_Repeated_Character_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, 'x', 5);

        _ = await Assert.That(builder.ToString()).IsEqualTo("xxxxx" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_Char_With_RepeatCount_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(false, 'x', 5);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_Char_With_RepeatCount_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendLineIf(true, 'x', 3);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_Boolean_True_Condition_True_Should_Append_TrueString_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, true);

        _ = await Assert.That(builder.ToString()).IsEqualTo("true" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_Boolean_False_Condition_True_Should_Append_FalseString_With_Newline()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(true, false);

        _ = await Assert.That(builder.ToString()).IsEqualTo("false" + Environment.NewLine);
    }

    [Test]
    public async Task AppendLineIf_Boolean_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendLineIf(false, true);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendLineIf_Boolean_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendLineIf(true, true);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendLineIf_Multiple_Calls_Should_Respect_Conditions()
    {
        var builder = new CSharpCodeBuilder(50);

        _ = builder
            .AppendLineIf(true, "Line 1")
            .AppendLineIf(false, "Skipped Line")
            .AppendLineIf(true, "Line 2")
            .AppendLineIf(true);

        var expected =
            "Line 1" + Environment.NewLine + "Line 2" + Environment.NewLine + Environment.NewLine;

        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLineIf_Should_Apply_Indentation_When_Condition_True()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();
        _ = builder.AppendLine();

        _ = builder.AppendLineIf(true, "test");

        var expected = Environment.NewLine + "    test" + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLineIf_Should_Not_Apply_Indentation_When_Condition_False()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();
        _ = builder.AppendLine();

        _ = builder.AppendLineIf(false, "test");

        var expected = Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLineIf_Chaining_Should_Work_Correctly()
    {
        var builder = new CSharpCodeBuilder(50);

        var result = builder
            .AppendLineIf(true, "a")
            .AppendLineIf(false, "b")
            .AppendLineIf(true, "c");

        _ = await Assert.That(result).IsEqualTo(builder);

        var expected = "a" + Environment.NewLine + "c" + Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendLineIf_Mixed_With_AppendIf_Should_Work_Correctly()
    {
        var builder = new CSharpCodeBuilder(50);

        _ = builder
            .AppendIf(true, "Start: ")
            .AppendLineIf(true, "First Line")
            .AppendIf(false, "Skipped")
            .AppendLineIf(true, "Second Line")
            .AppendIf(true, "End");

        var expected =
            "Start: First Line" + Environment.NewLine + "Second Line" + Environment.NewLine + "End";

        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }
}
