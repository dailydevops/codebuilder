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

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            {
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Append_Closing_Brace_On_Dispose()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            // Empty scope
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            {
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Scope_Should_Contain_Both_Braces()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.Scope())
        {
            // Empty scope
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            {
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
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

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            {
                Level 1{
                    Level 2}
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
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

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            {
                First}
            {
                Second}

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
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
        _ = await Assert.That(result).IsEqualTo("{" + Environment.NewLine + "\tHello}" + Environment.NewLine);
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

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            public class MyClass
            {
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ScopeLine_Should_Include_Opening_Brace()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            // Empty scope
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            public class MyClass
            {
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ScopeLine_Should_Include_Closing_Brace_On_Dispose()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            // Empty scope
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            public class MyClass
            {
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ScopeLine_With_Null_Value_Should_Append_Only_Braces()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine(null))
        {
            // Empty scope
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """

            {
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ScopeLine_With_Empty_String_Should_Append_Only_Braces()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine(string.Empty))
        {
            // Empty scope
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """

            {
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ScopeLine_With_Content_Should_Format_Class_Structure()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            _ = builder.Append("private int _field;");
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            public class MyClass
            {
                private int _field;}

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
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

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            namespace MyNamespace
            {
                public class OuterClass
                {
                    public void Method() { }}
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ScopeLine_Should_Apply_Proper_Indentation_To_Content()
    {
        var builder = new CSharpCodeBuilder();

        using (builder.ScopeLine("public class MyClass"))
        {
            _ = builder.Append("public string Name { get; set; }");
        }

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            public class MyClass
            {
                public string Name { get; set; }}

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
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

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            public class FirstClass
            {
                public int Id { get; set; }}

            public class SecondClass
            {
                public string Name { get; set; }}

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
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
        _ = await Assert
            .That(result)
            .IsEqualTo(
                "public class MyClass"
                    + Environment.NewLine
                    + "{"
                    + Environment.NewLine
                    + "\tpublic void Method() { }}"
                    + Environment.NewLine
            );
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

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            namespace MyApplication
            {
                public class MyClass
                {
                    public void MyMethod()
                    {
                        var x = 10;}
                }
            }

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
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

        var result = builder.ToString().Replace("\r\n", "\n", StringComparison.Ordinal);
        var expected = """
            /// <summary>
            /// Represents a person entity.
            /// </summary>
            public class Person
            {
                /// <summary>
                /// Gets or sets the person's name.
                /// </summary>
                public string Name { get; set; }}

            """.Replace("\r\n", "\n", StringComparison.Ordinal);
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    #endregion
}
