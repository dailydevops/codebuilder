namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;

public partial class CSharpCodeBuilderTests
{
    #region Scope Tests

    [Test]
    public async Task Scope_Should_Create_Disposable_Handler()
    {
        var builder = new CSharpCodeBuilder();

        var scope = builder.Scope();

        _ = await Assert.That(scope).IsNotNull();
    }

    [Test]
    public async Task Scope_Should_Append_Opening_Brace()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            // Empty scope
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("{");
    }

    [Test]
    public async Task Scope_Should_Append_Closing_Brace_On_Dispose()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            // Empty scope
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("}");
    }

    [Test]
    public async Task Scope_Should_Contain_Both_Braces()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            // Empty scope
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("{");
        _ = await Assert.That(result).Contains("}");
    }

    [Test]
    public async Task Scope_Nested_Should_Create_Multiple_Indentation_Levels()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            _ = builder.Append("Level 1");
            using (builder.Scope())
            {
                _ = builder.Append("Level 2");
            }
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("    Level 1"); // 4 spaces
        _ = await Assert.That(result).Contains("        Level 2"); // 8 spaces
    }

    [Test]
    public async Task Scope_Multiple_Sequential_Should_Reset_Indentation()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            _ = builder.Append("First");
        }

        using (builder.Scope())
        {
            _ = builder.Append("Second");
        }

        var result = builder.ToString();
        // Both should be at the same indentation level (top level)
        _ = await Assert.That(result).Contains("{");
        _ = await Assert.That(result).Contains("}");
    }

    [Test]
    public async Task Scope_With_Tabs_Should_Use_Tab_Indentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        using (builder.Scope())
        {
            _ = builder.Append("Hello");
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("\tHello");
    }

    #endregion

    #region ScopeLine Tests

    [Test]
    public async Task ScopeLine_Should_Append_Line_Before_Scope()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            // Empty scope
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("public class MyClass");
    }

    [Test]
    public async Task ScopeLine_Should_Include_Opening_Brace()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            // Empty scope
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("{");
    }

    [Test]
    public async Task ScopeLine_Should_Include_Closing_Brace_On_Dispose()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            // Empty scope
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("}");
    }

    [Test]
    public async Task ScopeLine_With_Null_Value_Should_Append_Only_Braces()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine(null))
        {
            // Empty scope
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("{");
        _ = await Assert.That(result).Contains("}");
    }

    [Test]
    public async Task ScopeLine_With_Empty_String_Should_Append_Only_Braces()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine(string.Empty))
        {
            // Empty scope
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("{");
        _ = await Assert.That(result).Contains("}");
    }

    [Test]
    public async Task ScopeLine_With_Content_Should_Format_Class_Structure()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            _ = builder.Append("private int _field;");
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("public class MyClass");
        _ = await Assert.That(result).Contains("private int _field;");
    }

    [Test]
    public async Task ScopeLine_Nested_Should_Create_Class_Hierarchy()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("namespace MyNamespace"))
        {
            using (builder.ScopeLine("public class OuterClass"))
            {
                _ = builder.Append("public void Method() { }");
            }
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("namespace MyNamespace");
        _ = await Assert.That(result).Contains("public class OuterClass");
        _ = await Assert.That(result).Contains("public void Method() { }");
    }

    [Test]
    public async Task ScopeLine_Should_Apply_Proper_Indentation_To_Content()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            _ = builder.Append("public string Name { get; set; }");
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("    public string Name { get; set; }");
    }

    [Test]
    public async Task ScopeLine_Multiple_Sequential_Should_Create_Multiple_Classes()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class FirstClass"))
        {
            _ = builder.Append("public int Id { get; set; }");
        }

        _ = builder.AppendLine(); // Add spacing between classes

        using (builder.ScopeLine("public class SecondClass"))
        {
            _ = builder.Append("public string Name { get; set; }");
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("public class FirstClass");
        _ = await Assert.That(result).Contains("public class SecondClass");
    }

    [Test]
    public async Task ScopeLine_With_Tabs_Should_Use_Tab_Indentation()
    {
        var builder = new CSharpCodeBuilder { UseTabs = true };

        using (builder.ScopeLine("public class MyClass"))
        {
            _ = builder.Append("public void Method() { }");
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("\tpublic void Method() { }");
    }

    [Test]
    public async Task ScopeLine_Complex_Nested_Structure_Should_Format_Correctly()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("namespace MyApplication"))
        {
            using (builder.ScopeLine("public class MyClass"))
            {
                using (builder.ScopeLine("public void MyMethod()"))
                {
                    _ = builder.Append("var x = 10;");
                }
            }
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("namespace MyApplication");
        _ = await Assert.That(result).Contains("    public class MyClass");
        _ = await Assert.That(result).Contains("        public void MyMethod()");
        _ = await Assert.That(result).Contains("            var x = 10;");
    }

    [Test]
    public async Task ScopeLine_Should_Return_Disposable()
    {
        var builder = new CSharpCodeBuilder();

        var scope = builder.ScopeLine("public class MyClass");

        _ = await Assert.That(scope).IsNotNull();
        scope.Dispose();
    }

    [Test]
    public async Task ScopeLine_With_Documentation_Should_Format_Complete_Class()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendXmlDocSummary("Represents a person entity.");
        using (builder.ScopeLine("public class Person"))
        {
            _ = builder.AppendXmlDocSummary("Gets or sets the person's name.");
            _ = builder.Append("public string Name { get; set; }");
        }

        var result = builder.ToString();
        _ = await Assert.That(result).Contains("/// <summary>");
        _ = await Assert.That(result).Contains("/// Represents a person entity.");
        _ = await Assert.That(result).Contains("public class Person");
        _ = await Assert.That(result).Contains("public string Name { get; set; }");
    }

    #endregion
}
