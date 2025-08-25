namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task Scope_Should_Increment_Indentation_On_Creation()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            _ = builder.AppendLine().Append("test");
        }

        var expected = Environment.NewLine + "    test";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Decrement_Indentation_On_Disposal()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            _ = builder.AppendLine().Append("indented");
        }
        _ = builder.AppendLine().Append("normal");

        var expected = Environment.NewLine + "    indented" + Environment.NewLine + "normal";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Support_Nested_Scopes()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            _ = builder.AppendLine().Append("level 1");
            using (builder.Scope())
            {
                _ = builder.AppendLine().Append("level 2");
                using (builder.Scope())
                {
                    _ = builder.AppendLine().Append("level 3");
                }
                _ = builder.AppendLine().Append("back to level 2");
            }
            _ = builder.AppendLine().Append("back to level 1");
        }
        _ = builder.AppendLine().Append("level 0");

        var expected =
            Environment.NewLine
            + "    level 1"
            + Environment.NewLine
            + "        level 2"
            + Environment.NewLine
            + "            level 3"
            + Environment.NewLine
            + "        back to level 2"
            + Environment.NewLine
            + "    back to level 1"
            + Environment.NewLine
            + "level 0";

        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Work_With_Tabs()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        using (builder.Scope())
        {
            _ = builder.AppendLine().Append("test");
        }

        var expected = Environment.NewLine + "\ttest";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Handle_Multiple_Sequential_Scopes()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            _ = builder.AppendLine().Append("first scope");
        }

        using (builder.Scope())
        {
            _ = builder.AppendLine().Append("second scope");
        }

        _ = builder.AppendLine().Append("no scope");

        var expected =
            Environment.NewLine
            + "    first scope"
            + Environment.NewLine
            + "    second scope"
            + Environment.NewLine
            + "no scope";

        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Handle_Empty_Scope()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            // Empty scope - should not affect builder content
        }

        _ = builder.AppendLine().Append("test");

        var expected = Environment.NewLine + "test";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Handle_Exception_Within_Scope()
    {
        var builder = new CSharpCodeBuilder();

        try
        {
            using (builder.Scope())
            {
                _ = builder.AppendLine().Append("before exception");
                throw new InvalidOperationException("Test exception");
#pragma warning disable CS0162 // Unreachable code detected
                _ = builder.AppendLine().Append("after exception");
#pragma warning restore CS0162 // Unreachable code detected
            }
        }
        catch (InvalidOperationException)
        {
            // Expected exception - indentation should still be properly decremented
        }

        _ = builder.AppendLine().Append("after scope");

        var expected = Environment.NewLine + "    before exception" + Environment.NewLine + "after scope";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Work_With_Existing_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        // Manually add some indentation first
        builder.IncrementIndent();
        _ = builder.AppendLine().Append("existing indent");

        using (builder.Scope())
        {
            _ = builder.AppendLine().Append("scope indent");
        }

        _ = builder.AppendLine().Append("back to existing");

        var expected =
            Environment.NewLine
            + "    existing indent"
            + Environment.NewLine
            + "        scope indent"
            + Environment.NewLine
            + "    back to existing";

        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Work_With_Complex_Code_Generation()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendLine("public class TestClass");
        using (builder.Scope())
        {
            _ = builder.Append("public void TestMethod()").AppendLine();
            using (builder.Scope())
            {
                _ = builder.Append("if (true)").AppendLine();
                using (builder.Scope())
                {
                    _ = builder.Append("Console.WriteLine(\"Hello, World!\");");
                }
            }
        }

        var expected =
            "public class TestClass"
            + Environment.NewLine
            + "    public void TestMethod()"
            + Environment.NewLine
            + "        if (true)"
            + Environment.NewLine
            + "            Console.WriteLine(\"Hello, World!\");";

        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Handle_Deep_Nesting()
    {
        var builder = new CSharpCodeBuilder();
        const int nestingLevels = 5;

        // Create deep nesting
        var scopes = new ScopeHandler[nestingLevels];
        for (int i = 0; i < nestingLevels; i++)
        {
            scopes[i] = builder.Scope();
            _ = builder.AppendLine().Append($"Level {i + 1}");
        }

        // Dispose scopes in reverse order (as would happen with nested using statements)
        for (int i = nestingLevels - 1; i >= 0; i--)
        {
            scopes[i].Dispose();
        }

        _ = builder.AppendLine().Append("Back to level 0");

        var expected =
            Environment.NewLine
            + "    Level 1"
            + Environment.NewLine
            + "        Level 2"
            + Environment.NewLine
            + "            Level 3"
            + Environment.NewLine
            + "                Level 4"
            + Environment.NewLine
            + "                    Level 5"
            + Environment.NewLine
            + "Back to level 0";

        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Disposal_Should_Be_Safe_To_Call_Multiple_Times()
    {
        var builder = new CSharpCodeBuilder();

        var scope = builder.Scope();
        _ = builder.AppendLine().Append("test");

        // Dispose multiple times should be safe
        scope.Dispose();
        scope.Dispose();
        scope.Dispose();

        _ = builder.AppendLine().Append("after");

        var expected = Environment.NewLine + "    test" + Environment.NewLine + "after";
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }
}
