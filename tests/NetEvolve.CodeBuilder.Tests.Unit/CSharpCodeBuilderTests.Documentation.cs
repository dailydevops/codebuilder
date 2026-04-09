namespace NetEvolve.CodeBuilder.Tests.Unit;

using System;
using System.Collections.Generic;
using System.Linq;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task AppendXmlDoc_WithContent_Should_AppendSingleLineComment()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDoc("This is a comment");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("/// This is a comment" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDoc_WithNullContent_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDoc(null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDoc_WithEmptyContent_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDoc("");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocSummary_WithSingleLine_Should_AppendSummaryElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocSummary("This is a summary")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <summary>
            /// This is a summary
            /// </summary>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocSummary_WithMultipleLines_Should_AppendSummaryElementWithAllLines()
    {
        var builder = new CSharpCodeBuilder();
        var summaryLines = new[] { "First line", "Second line", "Third line" };

        var result = builder
            .AppendXmlDocSummary(summaryLines)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <summary>
            /// First line
            /// Second line
            /// Third line
            /// </summary>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocSummary_WithEmptyAndNullLines_Should_SkipEmptyLines()
    {
        var builder = new CSharpCodeBuilder();
        var summaryLines = new List<string?> { "First line", "", null, "Last line" };

        var result = builder
            .AppendXmlDocSummary(summaryLines.Where(x => x is not null)!)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <summary>
            /// First line
            /// Last line
            /// </summary>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocSummary_WithOnlyEmptyLines_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var summaryLines = new[] { "", "", "" };

        var result = builder.AppendXmlDocSummary(summaryLines).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocSummary_WithEmptyCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocSummary(Array.Empty<string>()).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocParam_WithValidParameters_Should_AppendParamElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocParam("paramName", "Parameter description");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <param name=\"paramName\">Parameter description</param>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocParam_WithNullName_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocParam(null, "Parameter description");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocParams_WithMultipleParameters_Should_AppendAllParamElements()
    {
        var builder = new CSharpCodeBuilder();
        var parameters = new List<(string Name, string Description)>
        {
            ("param1", "First parameter"),
            ("param2", "Second parameter"),
        };

        var result = builder.AppendXmlDocParams(parameters).ToString().Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <param name="param1">First parameter</param>
            /// <param name="param2">Second parameter</param>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocReturns_WithDescription_Should_AppendReturnsElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocReturns("The result value");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <returns>The result value</returns>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithSingleLine_Should_AppendRemarksElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocRemarks("This is a remark")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <remarks>
            /// This is a remark
            /// </remarks>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithMultipleLines_Should_AppendRemarksElementWithAllLines()
    {
        var builder = new CSharpCodeBuilder();
        var remarksLines = new[] { "First remark line", "Second remark line", "Third remark line" };

        var result = builder
            .AppendXmlDocRemarks(remarksLines)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <remarks>
            /// First remark line
            /// Second remark line
            /// Third remark line
            /// </remarks>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithEmptyAndNullLines_Should_SkipEmptyLines()
    {
        var builder = new CSharpCodeBuilder();
        var remarksLines = new[] { "First remark", "", "Third remark" };

        var result = builder
            .AppendXmlDocRemarks(remarksLines)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <remarks>
            /// First remark
            /// Third remark
            /// </remarks>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithOnlyEmptyLines_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var remarksLines = new[] { "", "", "" };

        var result = builder.AppendXmlDocRemarks(remarksLines).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithNullCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocRemarks((IEnumerable<string>?)null).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithEmptyCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var remarksLines = Array.Empty<string>();

        var result = builder.AppendXmlDocRemarks(remarksLines).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocExample_WithContent_Should_AppendExampleElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocExample("var example = new Example();")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <example>
            /// var example = new Example();
            /// </example>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExample_WithMultipleLines_Should_AppendExampleElementWithAllLines()
    {
        var builder = new CSharpCodeBuilder();
        var exampleLines = new[]
        {
            "var builder = new CSharpCodeBuilder();",
            "builder.AppendLine(\"Hello World\");",
            "var result = builder.ToString();",
        };

        var result = builder
            .AppendXmlDocExample(exampleLines)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <example>
            /// var builder = new CSharpCodeBuilder();
            /// builder.AppendLine("Hello World");
            /// var result = builder.ToString();
            /// </example>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExample_WithEmptyAndNullLines_Should_SkipEmptyLines()
    {
        var builder = new CSharpCodeBuilder();
        var exampleLines = new[] { "var x = 1;", "", "var y = 2;" };

        var result = builder
            .AppendXmlDocExample(exampleLines)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <example>
            /// var x = 1;
            /// var y = 2;
            /// </example>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExample_WithOnlyEmptyLines_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var exampleLines = new[] { "", "", "" };

        var result = builder.AppendXmlDocExample(exampleLines).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocExample_WithEmptyCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocExample(Array.Empty<string>()).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocSee_WithCref_Should_AppendSeeElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocSee("System.String");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("/// <see cref=\"System.String\"/>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocSee_WithNullCref_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendXmlDocSee(null);

        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocSee_WithIsHrefTrue_Should_UseHrefAttribute()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendXmlDocSee("https://example.com", isHref: true);

        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <see href=\"https://example.com\"/>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocSeeAlso_WithCref_Should_AppendSeealsoElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocSeeAlso("System.String");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <seealso cref=\"System.String\"/>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocSeeAlso_WithNullCref_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendXmlDocSeeAlso(null);

        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocSeeAlso_WithEmptyCref_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendXmlDocSeeAlso(string.Empty);

        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocSeeAlso_WithIsHrefTrue_Should_UseHrefAttribute()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendXmlDocSeeAlso("https://example.com", isHref: true);

        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <seealso href=\"https://example.com\"/>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocSeeAlso_WithIsHrefFalse_Should_UseCrefAttribute()
    {
        var builder = new CSharpCodeBuilder();

        _ = builder.AppendXmlDocSeeAlso("MyNamespace.MyClass", isHref: false);

        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <seealso cref=\"MyNamespace.MyClass\"/>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocSeeAlso_AfterContent_Should_StartOnNewLine()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocSeeAlso("RelatedClass")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <summary>
            /// Method summary
            /// </summary>
            /// <seealso cref="RelatedClass"/>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocValue_WithDescription_Should_AppendValueElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocValue("The value of the property");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <value>The value of the property</value>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocTypeParam_WithValidParameters_Should_AppendTypeParamElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocTypeParam("T", "The generic type parameter");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <typeparam name=\"T\">The generic type parameter</typeparam>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocInheritDoc_WithoutCref_Should_AppendInheritDocElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocInheritDoc().ToString();

        _ = await Assert.That(result).IsEqualTo("/// <inheritdoc />" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocInheritDoc_WithCref_Should_AppendInheritDocElementWithCref()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocInheritDoc("BaseClass.Method").ToString();

        _ = await Assert.That(result).IsEqualTo("/// <inheritdoc cref=\"BaseClass.Method\" />" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocCustomElement_WithContentAndAttributes_Should_AppendCustomElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocCustomElement("custom", "Custom content", "attr=\"value\"").ToString();

        _ = await Assert
            .That(result)
            .IsEqualTo("/// <custom attr=\"value\">Custom content</custom>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocCustomElement_WithoutContent_Should_AppendSelfClosingElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocCustomElement("custom", null, "attr=\"value\"").ToString();

        _ = await Assert.That(result).IsEqualTo("/// <custom attr=\"value\" />" + Environment.NewLine);
    }

    [Test]
    public async Task XmlDocumentationMethods_Should_SupportMethodChaining()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocParam("param1", "First parameter")
            .AppendXmlDocParam("param2", "Second parameter")
            .AppendXmlDocReturns("Return value description")
            .AppendXmlDocRemarks("Additional remarks")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <summary>
            /// Method summary
            /// </summary>
            /// <param name="param1">First parameter</param>
            /// <param name="param2">Second parameter</param>
            /// <returns>Return value description</returns>
            /// <remarks>
            /// Additional remarks
            /// </remarks>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task XmlDocumentationMethods_WithIndentation_Should_RespectIndentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();

        var result = builder
            .AppendXmlDocSummary("Indented summary")
            .AppendXmlDocParam("param", "Parameter description")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
                /// <summary>
                /// Indented summary
                /// </summary>
                /// <param name="param">Parameter description</param>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task XmlDocumentationMethods_AfterContent_Should_StartOnNewLine()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .Append("public class MyClass")
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocParam("param", "Parameter description")
            .Append("public void MyMethod(string param) { }")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            public class MyClass
            /// <summary>
            /// Method summary
            /// </summary>
            /// <param name="param">Parameter description</param>
            public void MyMethod(string param) { }
            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task XmlDocumentationMethods_AtStartOfBuilder_Should_NotAddExtraNewLine()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocParam("param", "Parameter description")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <summary>
            /// Method summary
            /// </summary>
            /// <param name="param">Parameter description</param>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task XmlDocumentationMethods_AfterNewLine_Should_NotAddExtraNewLine()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendLine("public class MyClass")
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocParam("param", "Parameter description")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            public class MyClass
            /// <summary>
            /// Method summary
            /// </summary>
            /// <param name="param">Parameter description</param>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocException_WithValidParameters_Should_AppendExceptionElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("ArgumentNullException", "Thrown when argument is null").ToString();

        _ = await Assert
            .That(result)
            .IsEqualTo(
                "/// <exception cref=\"ArgumentNullException\">Thrown when argument is null</exception>"
                    + Environment.NewLine
            );
    }

    [Test]
    public async Task AppendXmlDocException_WithNullExceptionType_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException(null, "Exception description").ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithEmptyExceptionType_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("", "Exception description").ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithNullDescription_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("ArgumentNullException", null).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithEmptyDescription_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("ArgumentNullException", "").ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithGenericExceptionType_Should_AppendCorrectly()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocException<InvalidOperationException>("Thrown when the operation is invalid")
            .ToString();

        _ = await Assert
            .That(result)
            .IsEqualTo(
                "/// <exception cref=\"InvalidOperationException\">Thrown when the operation is invalid</exception>"
                    + Environment.NewLine
            );
    }

    [Test]
    public async Task AppendXmlDocException_WithFullyQualifiedType_Should_AppendCorrectly()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocException("System.ArgumentOutOfRangeException", "Thrown when the value is out of range")
            .ToString();

        _ = await Assert
            .That(result)
            .IsEqualTo(
                "/// <exception cref=\"System.ArgumentOutOfRangeException\">Thrown when the value is out of range</exception>"
                    + Environment.NewLine
            );
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithValidExceptions_Should_AppendAllExceptionElements()
    {
        var builder = new CSharpCodeBuilder();
        var exceptions = new List<(string Type, string Description)>
        {
            ("ArgumentNullException", "Thrown when argument is null"),
            ("InvalidOperationException", "Thrown when operation is invalid"),
            ("ArgumentOutOfRangeException", "Thrown when value is out of range"),
        };

        var result = builder
            .AppendXmlDocExceptions(exceptions)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <exception cref="ArgumentNullException">Thrown when argument is null</exception>
            /// <exception cref="InvalidOperationException">Thrown when operation is invalid</exception>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when value is out of range</exception>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithSingleException_Should_AppendSingleExceptionElement()
    {
        var builder = new CSharpCodeBuilder();
        var exceptions = new List<(string Type, string Description)>
        {
            ("ArgumentNullException", "Thrown when argument is null"),
        };

        var result = builder.AppendXmlDocExceptions(exceptions).ToString();

        var expected =
            "/// <exception cref=\"ArgumentNullException\">Thrown when argument is null</exception>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithNullCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocExceptions(null).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithEmptyCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var exceptions = Array.Empty<(string Type, string Description)>();

        var result = builder.AppendXmlDocExceptions(exceptions).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithInvalidExceptions_Should_SkipInvalidEntries()
    {
        var builder = new CSharpCodeBuilder();
        var exceptions = new List<(string Type, string Description)>
        {
            ("ArgumentNullException", "Valid exception"),
            (null!, "Invalid - null type"),
            ("", "Invalid - empty type"),
            ("InvalidOperationException", null!),
            ("ArgumentException", ""),
            ("ValidException", "Another valid exception"),
        };

        var result = builder
            .AppendXmlDocExceptions(exceptions)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <exception cref="ArgumentNullException">Valid exception</exception>
            /// <exception cref="ValidException">Another valid exception</exception>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExceptions_Should_SupportMethodChaining()
    {
        var builder = new CSharpCodeBuilder();
        var exceptions = new List<(string Type, string Description)>
        {
            ("ArgumentNullException", "Thrown when argument is null"),
            ("InvalidOperationException", "Thrown when operation is invalid"),
        };

        var result = builder
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocExceptions(exceptions)
            .AppendXmlDocReturns("Return value")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <summary>
            /// Method summary
            /// </summary>
            /// <exception cref="ArgumentNullException">Thrown when argument is null</exception>
            /// <exception cref="InvalidOperationException">Thrown when operation is invalid</exception>
            /// <returns>Return value</returns>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithIndentation_Should_RespectIndentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();
        var exceptions = new List<(string Type, string Description)>
        {
            ("ArgumentNullException", "Thrown when argument is null"),
            ("InvalidOperationException", "Thrown when operation is invalid"),
        };

        var result = builder
            .AppendXmlDocExceptions(exceptions)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
                /// <exception cref="ArgumentNullException">Thrown when argument is null</exception>
                /// <exception cref="InvalidOperationException">Thrown when operation is invalid</exception>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocException_WithSpecialCharactersInDescription_Should_AppendCorrectly()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendXmlDocException("ArgumentException", "Thrown when value contains <, >, &, or \" characters")
            .ToString();

        _ = await Assert
            .That(result)
            .IsEqualTo(
                "/// <exception cref=\"ArgumentException\">Thrown when value contains <, >, &, or \" characters</exception>"
                    + Environment.NewLine
            );
    }

    [Test]
    public async Task AppendXmlDocTypeParams_WithMultipleParameters_Should_AppendAllTypeParamElements()
    {
        var builder = new CSharpCodeBuilder();
        var typeParameters = new List<(string, string)>
        {
            ("T", "First type parameter"),
            ("U", "Second type parameter"),
        };

        var result = builder
            .AppendXmlDocTypeParams(typeParameters)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <typeparam name="T">First type parameter</typeparam>
            /// <typeparam name="U">Second type parameter</typeparam>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocTypeParams_WithSingleParameter_Should_AppendSingleTypeParamElement()
    {
        var builder = new CSharpCodeBuilder();
        var typeParameters = new List<(string, string)> { ("T", "Generic type parameter") };

        var result = builder.AppendXmlDocTypeParams(typeParameters).ToString();

        var expected = "/// <typeparam name=\"T\">Generic type parameter</typeparam>" + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocTypeParams_WithNullCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocTypeParams(null).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocTypeParams_WithEmptyCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var typeParameters = Array.Empty<(string, string)>();

        var result = builder.AppendXmlDocTypeParams(typeParameters).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocTypeParams_WithInvalidEntries_Should_SkipInvalidEntries()
    {
        var builder = new CSharpCodeBuilder();
        var typeParameters = new List<(string, string)>
        {
            ("T", "Valid type parameter"),
            (null!, "Invalid - null name"),
            ("", "Invalid - empty name"),
            ("U", null!),
            ("V", ""),
            ("W", "Another valid type parameter"),
        };

        var result = builder
            .AppendXmlDocTypeParams(typeParameters)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <typeparam name="T">Valid type parameter</typeparam>
            /// <typeparam name="W">Another valid type parameter</typeparam>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocTypeParams_WithAllInvalidEntries_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var typeParameters = new List<(string, string)>
        {
            (null!, "Invalid - null name"),
            ("", "Invalid - empty name"),
            ("T", null!),
            ("U", ""),
        };

        var result = builder.AppendXmlDocTypeParams(typeParameters).ToString();

        _ = await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocTypeParams_Should_SupportMethodChaining()
    {
        var builder = new CSharpCodeBuilder();
        var typeParameters = new List<(string, string)>
        {
            ("T", "First type parameter"),
            ("U", "Second type parameter"),
        };

        var result = builder
            .AppendXmlDocSummary("Generic class")
            .AppendXmlDocTypeParams(typeParameters)
            .AppendXmlDocReturns("Return value")
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
            /// <summary>
            /// Generic class
            /// </summary>
            /// <typeparam name="T">First type parameter</typeparam>
            /// <typeparam name="U">Second type parameter</typeparam>
            /// <returns>Return value</returns>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocTypeParams_WithIndentation_Should_RespectIndentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();
        var typeParameters = new List<(string, string)>
        {
            ("T", "First type parameter"),
            ("U", "Second type parameter"),
        };

        var result = builder
            .AppendXmlDocTypeParams(typeParameters)
            .ToString()
            .Replace("\r\n", "\n", StringComparison.Ordinal);

        var expected = """
                /// <typeparam name="T">First type parameter</typeparam>
                /// <typeparam name="U">Second type parameter</typeparam>

            """.Replace("\r\n", "\n", StringComparison.Ordinal);

        _ = await Assert.That(result).IsEqualTo(expected);
    }
}
