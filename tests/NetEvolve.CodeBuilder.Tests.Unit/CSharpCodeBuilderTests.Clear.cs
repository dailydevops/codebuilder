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

        _ = await Assert.That(builder.UseTabs).IsTrue();
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

    [Test]
    public async Task Indent_Should_Append_Single_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Indent().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    Hello");
    }

    [Test]
    public async Task Indent_Should_Not_Affect_Indentation_Level()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Indent().AppendLine("First");
        _ = builder.Append("Second");

        var result = builder.ToString();
        // Second line should not be indented because Intend() doesn't change the level
        _ = await Assert.That(result).IsEqualTo("    First" + Environment.NewLine + "Second");
    }

    [Test]
    public async Task Indent_With_Tabs_Should_Append_Tab_Character()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        _ = builder.Indent().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("\tHello");
    }

    [Test]
    public async Task Indent_With_Spaces_Should_Append_Four_Spaces()
    {
        var builder = new CSharpCodeBuilder { UseTabs = false };

        _ = builder.Indent().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    Hello");
    }

    [Test]
    public async Task Indent_Multiple_Calls_Should_Append_Multiple_Indentations()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Indent().Indent().Indent().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("            Hello"); // 12 spaces (3 * 4)
    }

    [Test]
    public async Task Indent_Multiple_With_Tabs_Should_Append_Multiple_Tabs()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        _ = builder.Indent().Indent().Indent().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("\t\t\tHello");
    }

    [Test]
    public async Task Indent_Should_Return_Builder_For_Chaining()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.Indent();

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task Indent_In_Middle_Of_Line_Should_Append_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Append("Hello").Indent().Append("World");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("Hello    World");
    }

    [Test]
    public async Task Indent_After_NewLine_Should_Add_Manual_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendLine("First");
        _ = builder.Indent().Append("Second");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("First" + Environment.NewLine + "    Second");
    }

    [Test]
    public async Task Indent_With_Automatic_Indentation_Should_Stack()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent(); // Set automatic indentation level to 1

        _ = builder.AppendLine().Indent().Append("Hello");

        var result = builder.ToString();
        // Should have both automatic (4 spaces) and manual (4 spaces) indentation
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "        Hello");
    }

    [Test]
    public async Task Indent_Multiple_Mixed_With_Content_Should_Work()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder
            .Indent()
            .Append("Level 1")
            .AppendLine()
            .Indent()
            .Indent()
            .Append("Level 2")
            .AppendLine()
            .Indent()
            .Indent()
            .Indent()
            .Append("Level 3");

        var result = builder.ToString();
        _ = await Assert
            .That(result)
            .IsEqualTo(
                "    Level 1" + Environment.NewLine + "        Level 2" + Environment.NewLine + "            Level 3"
            );
    }

    [Test]
    public async Task Indent_Should_Work_With_Empty_Builder()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Indent();

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    ");
    }

    [Test]
    public async Task Clear_Should_Reset_IsNewline_State()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("text"); // sets _isNewline to false

        _ = builder.Clear(); // must reset _isNewline to true
        builder.IncrementIndent();
        _ = builder.Append("indented");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    indented");
    }

    [Test]
    public async Task Indent_Should_Work_After_Clear()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello");
        _ = builder.Clear();

        _ = builder.Indent().Append("World");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    World");
    }

    [Test]
    public async Task Indent_Combined_With_Scope_Should_Add_Extra_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            _ = builder.Indent().Append("Extra indented");
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            {
                    Extra indented}

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Indent_Combined_With_ScopeLine_Should_Add_Extra_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            _ = builder.Indent().Append("// Extra indented comment");
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            public class MyClass
            {
                    // Extra indented comment}

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }
}
