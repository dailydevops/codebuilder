namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task AppendIf_Boolean_True_Condition_True_Should_Append_TrueString()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, true);

        _ = await Assert.That(builder.ToString()).IsEqualTo("true");
    }

    [Test]
    public async Task AppendIf_Boolean_False_Condition_True_Should_Append_FalseString()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, false);

        _ = await Assert.That(builder.ToString()).IsEqualTo("false");
    }

    [Test]
    public async Task AppendIf_Boolean_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(false, true);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_Boolean_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendIf(true, true);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_Char_Condition_True_Should_Append_Character()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, 'x');

        _ = await Assert.That(builder.ToString()).IsEqualTo("x");
    }

    [Test]
    public async Task AppendIf_Char_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(false, 'x');

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_Char_Should_Handle_Special_Characters_When_Condition_True()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, '{');

        _ = await Assert.That(builder.ToString()).IsEqualTo("{" + Environment.NewLine);
    }

    [Test]
    public async Task AppendIf_Char_Should_Not_Handle_Special_Characters_When_Condition_False()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(false, '{');

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_Char_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendIf(true, 'x');

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_Char_With_RepeatCount_Condition_True_Should_Append_Repeated_Character()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, 'x', 5);

        _ = await Assert.That(builder.ToString()).IsEqualTo("xxxxx");
    }

    [Test]
    public async Task AppendIf_Char_With_RepeatCount_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(false, 'x', 5);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_Char_With_RepeatCount_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendIf(true, 'x', 3);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_CharArray_Condition_True_Should_Append_All_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c' };

        _ = builder.AppendIf(true, chars);

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task AppendIf_CharArray_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c' };

        _ = builder.AppendIf(false, chars);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_CharArray_Null_Condition_True_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, (char[]?)null);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_CharArray_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c' };

        var result = builder.AppendIf(true, chars);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_CharArray_With_StartIndex_And_Count_Condition_True_Should_Append_Specified_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c', 'd', 'e' };

        _ = builder.AppendIf(true, chars, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("bcd");
    }

    [Test]
    public async Task AppendIf_CharArray_With_StartIndex_And_Count_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c', 'd', 'e' };

        _ = builder.AppendIf(false, chars, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_CharArray_With_StartIndex_And_Count_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c', 'd', 'e' };

        var result = builder.AppendIf(true, chars, 1, 3);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_CharPointer_Condition_True_Should_Append_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = "abc".ToCharArray();

        unsafe
        {
            fixed (char* ptr = chars)
            {
                _ = builder.AppendIf(true, ptr, chars.Length);
            }
        }

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task AppendIf_CharPointer_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = "abc".ToCharArray();

        unsafe
        {
            fixed (char* ptr = chars)
            {
                _ = builder.AppendIf(false, ptr, chars.Length);
            }
        }

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_CharPointer_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = "abc".ToCharArray();
        CSharpCodeBuilder result;

        unsafe
        {
            fixed (char* ptr = chars)
            {
                result = builder.AppendIf(true, ptr, chars.Length);
            }
        }

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_ReadOnlyMemory_Condition_True_Should_Append_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abc".AsMemory();

        _ = builder.AppendIf(true, memory);

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task AppendIf_ReadOnlyMemory_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abc".AsMemory();

        _ = builder.AppendIf(false, memory);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_ReadOnlyMemory_Empty_Condition_True_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = ReadOnlyMemory<char>.Empty;

        _ = builder.AppendIf(true, memory);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_ReadOnlyMemory_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abc".AsMemory();

        var result = builder.AppendIf(true, memory);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_ReadOnlyMemory_With_StartIndex_And_Count_Condition_True_Should_Append_Specified_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abcde".AsMemory();

        _ = builder.AppendIf(true, memory, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("bcd");
    }

    [Test]
    public async Task AppendIf_ReadOnlyMemory_With_StartIndex_And_Count_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abcde".AsMemory();

        _ = builder.AppendIf(false, memory, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_ReadOnlyMemory_With_StartIndex_And_Count_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abcde".AsMemory();

        var result = builder.AppendIf(true, memory, 1, 3);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_String_Condition_True_Should_Append_Characters()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, "abc");

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task AppendIf_String_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(false, "abc");

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_String_Null_Condition_True_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, (string?)null);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_String_Empty_Condition_True_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, string.Empty);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_String_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendIf(true, "abc");

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_String_With_StartIndex_And_Count_Condition_True_Should_Append_Specified_Characters()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(true, "abcdef", 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("bcd");
    }

    [Test]
    public async Task AppendIf_String_With_StartIndex_And_Count_Condition_False_Should_Not_Append()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.AppendIf(false, "abcdef", 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task AppendIf_String_With_StartIndex_And_Count_Should_Return_Same_Instance()
    {
        var builder = new CSharpCodeBuilder(10);

        var result = builder.AppendIf(true, "abcdef", 1, 3);

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task AppendIf_Multiple_Calls_Should_Respect_Conditions()
    {
        var builder = new CSharpCodeBuilder(20);

        _ = builder.AppendIf(true, "Hello").AppendIf(false, " Skipped").AppendIf(true, " World").AppendIf(true, '!');

        _ = await Assert.That(builder.ToString()).IsEqualTo("Hello World!");
    }

    [Test]
    public async Task AppendIf_Should_Apply_Indentation_When_Condition_True()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();
        _ = builder.AppendLine();

        _ = builder.AppendIf(true, "test");

        var expected = Environment.NewLine + "    test";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendIf_Should_Not_Apply_Indentation_When_Condition_False()
    {
        var builder = new CSharpCodeBuilder(10);
        builder.IncrementIndent();
        _ = builder.AppendLine();

        _ = builder.AppendIf(false, "test");

        var expected = Environment.NewLine;
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendIf_Chaining_Should_Work_Correctly()
    {
        var builder = new CSharpCodeBuilder(30);

        var result = builder.AppendIf(true, "a").AppendIf(false, "b").AppendIf(true, "c");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("ac");
    }
}
