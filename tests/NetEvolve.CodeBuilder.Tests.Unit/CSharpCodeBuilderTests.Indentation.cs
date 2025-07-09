namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task IncrementIndent_Should_Increase_Indentation_Level()
    {
        var builder = new CSharpCodeBuilder();

        builder.IncrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "    Hello");
    }

    [Test]
    public async Task IncrementIndent_Multiple_Times_Should_Increase_Indentation_Level()
    {
        var builder = new CSharpCodeBuilder();

        builder.IncrementIndent();
        builder.IncrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "        Hello"); // 8 spaces (2 * 4)
    }

    [Test]
    public async Task IncrementIndent_With_Tabs_Should_Use_Tab_Characters()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        builder.IncrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "\tHello");
    }

    [Test]
    public async Task IncrementIndent_Multiple_With_Tabs_Should_Use_Multiple_Tab_Characters()
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
    public async Task DecrementIndent_Should_Decrease_Indentation_Level()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();
        builder.IncrementIndent();

        builder.DecrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "    Hello"); // 4 spaces (1 * 4)
    }

    [Test]
    public async Task DecrementIndent_To_Zero_Should_Remove_All_Indentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();

        builder.DecrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "Hello");
    }

    [Test]
    public async Task DecrementIndent_Below_Zero_Should_Handle_Gracefully()
    {
        var builder = new CSharpCodeBuilder();

        builder.DecrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "Hello");
    }

    [Test]
    public async Task IncrementIndent_And_DecrementIndent_Should_Balance()
    {
        var builder = new CSharpCodeBuilder();

        builder.IncrementIndent();
        builder.IncrementIndent();
        builder.DecrementIndent();
        _ = builder.AppendLine().Append("Hello");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "    Hello"); // 4 spaces (1 * 4)
    }

    [Test]
    public async Task IncrementIndent_Should_Not_Affect_Existing_Content()
    {
        var builder = new CSharpCodeBuilder();
        _ = builder.Append("Hello");

        builder.IncrementIndent();
        _ = builder.AppendLine().Append("World");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo("Hello" + Environment.NewLine + "    World");
    }

    [Test]
    public async Task DecrementIndent_Should_Not_Affect_Existing_Content()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();
        _ = builder.AppendLine().Append("Hello");

        builder.DecrementIndent();
        _ = builder.AppendLine().Append("World");

        var result = builder.ToString();
        _ = await Assert.That(result).IsEqualTo(Environment.NewLine + "    Hello" + Environment.NewLine + "World");
    }

    [Test]
    public async Task IncrementIndent_And_DecrementIndent_Multiple_Operations_Should_Work()
    {
        var builder = new CSharpCodeBuilder();

        // Simulate nested code blocks - the braces automatically handle indentation
        _ = builder.Append("class MyClass");
        _ = builder.Append("{"); // This automatically increments indent and adds newline
        _ = builder.Append("public void Method()");
        _ = builder.Append("{"); // This automatically increments indent and adds newline
        _ = builder.Append("Console.WriteLine(\"Hello\");");
        _ = builder.Append("}"); // This automatically decrements indent but doesn't add newline
        _ = builder.Append("}"); // This automatically decrements indent but doesn't add newline

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);

        // The actual output based on the CSharpCodeBuilder's behavior
        var expected = """
            class MyClass{
                public void Method(){
                    Console.WriteLine("Hello");
                }
            }
            """;

        _ = await Assert.That(result).IsEqualTo(expected);
    }
}
