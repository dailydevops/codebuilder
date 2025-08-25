namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task Append_Boolean_True_Should_Append_TrueString()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append(true);

        _ = await Assert.That(builder.ToString()).IsEqualTo("true");
    }

    [Test]
    public async Task Append_Boolean_False_Should_Append_FalseString()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append(false);

        _ = await Assert.That(builder.ToString()).IsEqualTo("false");
    }

    [Test]
    public async Task Append_Char_Should_Append_Character()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append('x');

        _ = await Assert.That(builder.ToString()).IsEqualTo("x");
    }

    [Test]
    public async Task Append_Char_Multiple_Should_Append_All_Characters()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append("a").Append('b').Append('c');

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task Append_Multiple_Should_Same()
    {
        var builder = new CSharpCodeBuilder(10);

        var builder2 = builder.Append('a').Append('b').Append('c');

        _ = await Assert.That(builder).IsEqualTo(builder2);
    }

    [Test]
    public async Task Append_Char_With_RepeatCount_Should_Append_Character_Repeated_Times()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append('x', 5);

        _ = await Assert.That(builder.ToString()).IsEqualTo("xxxxx");
    }

    [Test]
    public async Task Append_Char_With_RepeatCount_Zero_Should_Not_Append_Anything()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append('x', 0);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task Append_Char_With_RepeatCount_Negative_Should_Throw_ArgumentOutOfRangeException()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var builder = new CSharpCodeBuilder(10);
            _ = builder.Append('x', -1);
        });

        _ = await Assert.That(exception.ParamName).IsEqualTo("repeatCount");
    }

    [Test]
    public async Task Append_CharArray_Should_Append_All_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c' };

        _ = builder.Append(chars);

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task Append_CharArray_Null_Should_Not_Change_Builder()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append((char[]?)null);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task Append_CharArray_Empty_Should_Not_Change_Builder()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append((char[])[]);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task Append_CharArray_With_StartIndex_And_Count_Should_Append_Specified_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var chars = new[] { 'a', 'b', 'c', 'd', 'e' };

        _ = builder.Append(chars, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("bcd");
    }

    [Test]
    public async Task Append_CharArray_With_StartIndex_And_Count_Null_Should_Not_Change_Builder()
    {
        var builder = new CSharpCodeBuilder(10);
        char[]? chars = null;

        _ = builder.Append(chars, 0, 0);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task Append_ReadOnlyMemory_Should_Append_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abc".AsMemory();

        _ = builder.Append(memory);

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task Append_ReadOnlyMemory_Empty_Should_Not_Change_Builder()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = ReadOnlyMemory<char>.Empty;

        _ = builder.Append(memory);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task Append_ReadOnlyMemory_With_StartIndex_And_Count_Should_Append_Specified_Characters()
    {
        var builder = new CSharpCodeBuilder(10);
        var memory = "abcde".AsMemory();

        _ = builder.Append(memory, 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("bcd");
    }

    [Test]
    public async Task Append_String_Should_Append_Characters()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append("abc");

        _ = await Assert.That(builder.ToString()).IsEqualTo("abc");
    }

    [Test]
    public async Task Append_String_Null_Should_Not_Change_Builder()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append((string?)null);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task Append_String_Empty_Should_Not_Change_Builder()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append(string.Empty);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task Append_String_With_StartIndex_And_Count_Should_Append_Specified_Characters()
    {
        var builder = new CSharpCodeBuilder(10);

        _ = builder.Append("abcdef", 1, 3);

        _ = await Assert.That(builder.ToString()).IsEqualTo("bcd");
    }

    [Test]
    public async Task Append_String_With_StartIndex_And_Count_Null_Should_Not_Change_Builder()
    {
        var builder = new CSharpCodeBuilder(10);
        string? text = null;

        _ = builder.Append(text, 0, 0);

        _ = await Assert.That(builder.ToString()).IsEqualTo("");
    }

    [Test]
    public async Task Append_Multiple_Calls_Should_Append_In_Sequence()
    {
        var builder = new CSharpCodeBuilder(20);

        _ = builder.Append("Hello").Append(' ').Append("World").Append('!');

        _ = await Assert.That(builder.ToString()).IsEqualTo("Hello World!");
    }

    [Test]
    public async Task Append_With_Capacity_Expansion_Should_Work_Correctly()
    {
        // Start with small capacity to force expansion
        var builder = new CSharpCodeBuilder(5);
        var longString = new string('x', 100);

        _ = builder.Append(longString);

        _ = await Assert.That(builder.ToString()).IsEqualTo(longString);
    }
}
