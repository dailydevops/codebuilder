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

        _ = await Assert.That(builder.UseTabs).IsEqualTo(true);
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
    public async Task Intend_Should_Append_Single_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Intend().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    Hello");
    }

    [Test]
    public async Task Intend_Should_Not_Affect_Indentation_Level()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Intend().AppendLine("First");
        _ = builder.Append("Second");

        var result = builder.ToString();
        // Second line should not be indented because Intend() doesn't change the level
        _ = await Assert.That(result).IsEqualTo("    First" + Environment.NewLine + "Second");
    }

    [Test]
    public async Task Intend_With_Tabs_Should_Append_Tab_Character()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        _ = builder.Intend().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("\tHello");
    }

    [Test]
    public async Task Intend_With_Spaces_Should_Append_Four_Spaces()
    {
        var builder = new CSharpCodeBuilder { UseTabs = false };

        _ = builder.Intend().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    Hello");
    }

    [Test]
    public async Task Intend_Multiple_Calls_Should_Append_Multiple_Indentations()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Intend().Intend().Intend().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("            Hello"); // 12 spaces (3 * 4)
    }

    [Test]
    public async Task Intend_Multiple_With_Tabs_Should_Append_Multiple_Tabs()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        _ = builder.Intend().Intend().Intend().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("\t\t\tHello");
    }

    [Test]
    public async Task Intend_Should_Return_Builder_For_Chaining()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.Intend();

        _ = await Assert.That(result).IsEqualTo(builder);
    }

    [Test]
    public async Task Intend_In_Middle_Of_Line_Should_Append_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Append("Hello").Intend().Append("World");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("Hello    World");
    }

    [Test]
    public async Task Intend_After_NewLine_Should_Add_Manual_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendLine("First");
        _ = builder.Intend().Append("Second");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("First" + Environment.NewLine + "    Second");
    }

    [Test]
    public async Task Intend_With_Automatic_Indentation_Should_Stack()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent(); // Set automatic indentation level to 1

        _ = builder.AppendLine().Intend().Append("Hello");

        var result = builder.ToString();
        // Should have both automatic (4 spaces) and manual (4 spaces) indentation
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "        Hello");
    }

    [Test]
    public async Task Intend_Multiple_Mixed_With_Content_Should_Work()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder
            .Intend()
            .Append("Level 1")
            .AppendLine()
            .Intend()
            .Intend()
            .Append("Level 2")
            .AppendLine()
            .Intend()
            .Intend()
            .Intend()
            .Append("Level 3");

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("    Level 1");
        _ = await Assert.That(result).Contains("    Level 2");
        _ = await Assert.That(result).Contains("     Level 3");
    }

    [Test]
    public async Task Intend_Should_Work_With_Empty_Builder()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Intend();

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    ");
    }

    [Test]
    public async Task Intend_Should_Work_After_Clear()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello");
        _ = builder.Clear();

        _ = builder.Intend().Append("World");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("    World");
    }

    [Test]
    public async Task Intend_Combined_With_Scope_Should_Add_Extra_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            _ = builder.Intend().Append("Extra indented");
        }

        var result = builder.ToString();
        // Should have both scope indentation (4 spaces) and manual indentation (4 spaces)
        _ = await Assert.That(result).Contains("      Extra indented"); // 8 spaces
    }

    [Test]
    public async Task Intend_Combined_With_ScopeLine_Should_Add_Extra_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            _ = builder.Intend().Append("// Extra indented comment");
        }

        var result = builder.ToString();
        // Should have both scope indentation and manual indentation
        _ = await Assert.That(result).Contains("        // Extra indented comment"); // 8 spaces
    }
}
