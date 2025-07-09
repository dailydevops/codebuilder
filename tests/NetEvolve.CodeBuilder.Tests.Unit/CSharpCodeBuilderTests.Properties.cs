namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task Capacity_Get_Should_Return_Current_Capacity()
    {
        var builder = new CSharpCodeBuilder(100);

        var capacity = builder.Capacity;

        _ = await Assert.That(capacity).IsGreaterThanOrEqualTo(100);
    }

    [Test]
    public async Task Capacity_After_EnsureCapacity_Should_Be_Updated()
    {
        var builder = new CSharpCodeBuilder(10);
        var originalCapacity = builder.Capacity;

        _ = builder.EnsureCapacity(200);

        _ = await Assert.That(builder.Capacity).IsGreaterThan(originalCapacity);
        _ = await Assert.That(builder.Capacity).IsGreaterThanOrEqualTo(200);
    }

    [Test]
    public async Task Length_Empty_Builder_Should_Return_Zero()
    {
        var builder = new CSharpCodeBuilder();

        var length = builder.Length;

        _ = await Assert.That(length).IsEqualTo(0);
    }

    [Test]
    public async Task Length_After_Append_Should_Return_Correct_Length()
    {
        var builder = new CSharpCodeBuilder();
        var text = "Hello World";

        _ = builder.Append(text);

        _ = await Assert.That(builder.Length).IsEqualTo(text.Length);
    }

    [Test]
    public async Task Length_After_Multiple_Appends_Should_Return_Total_Length()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.Append("Hello").Append(" ").Append("World");

        _ = await Assert.That(builder.Length).IsEqualTo(11);
    }

    [Test]
    public async Task Length_After_Clear_Should_Return_Zero()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello World");

        _ = builder.Clear();

        _ = await Assert.That(builder.Length).IsEqualTo(0);
    }

    [Test]
    public async Task Length_After_AppendLine_Should_Include_Newline_Characters()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendLine("Hello");

        _ = await Assert.That(builder.Length).IsEqualTo(5 + Environment.NewLine.Length);
    }

    [Test]
    public async Task UseTabs_Default_Should_Be_False()
    {
        var builder = new CSharpCodeBuilder();

        var useTabs = builder.UseTabs;

        _ = await Assert.That(useTabs).IsEqualTo(false);
    }

    [Test]
    public async Task UseTabs_Set_To_True_Should_Update_Property()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        _ = await Assert.That(builder.UseTabs).IsEqualTo(true);
    }

    [Test]
    public async Task UseTabs_Set_To_False_Should_Update_Property()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        builder.UseTabs = false;

        _ = await Assert.That(builder.UseTabs).IsEqualTo(false);
    }

    [Test]
    public async Task UseTabs_True_Should_Use_Tab_Character_For_Indentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };
        builder.IncrementIndent();

        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "\tHello");
    }

    [Test]
    public async Task UseTabs_False_Should_Use_Spaces_For_Indentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = false };
        builder.IncrementIndent();

        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "    Hello");
    }

    [Test]
    public async Task UseTabs_Multiple_Indent_Levels_With_Tabs_Should_Use_Multiple_Tabs()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };
        builder.IncrementIndent();
        builder.IncrementIndent();
        builder.IncrementIndent();

        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "\t\t\tHello");
    }

    [Test]
    public async Task UseTabs_Multiple_Indent_Levels_With_Spaces_Should_Use_Multiple_Space_Groups()
    {
        var builder = new CSharpCodeBuilder { UseTabs = false };
        builder.IncrementIndent();
        builder.IncrementIndent();
        builder.IncrementIndent();

        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "            Hello"); // 12 spaces (3 * 4)
    }
}
