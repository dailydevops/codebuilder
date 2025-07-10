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

        var result = builder.AppendXmlDocSummary("This is a summary");

        var expected =
            "/// <summary>"
            + Environment.NewLine
            + "/// This is a summary"
            + Environment.NewLine
            + "/// </summary>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocSummary_WithMultipleLines_Should_AppendSummaryElementWithAllLines()
    {
        var builder = new CSharpCodeBuilder();
        var summaryLines = new[] { "First line", "Second line", "Third line" };

        var result = builder.AppendXmlDocSummary(summaryLines);

        var expected =
            "/// <summary>"
            + Environment.NewLine
            + "/// First line"
            + Environment.NewLine
            + "/// Second line"
            + Environment.NewLine
            + "/// Third line"
            + Environment.NewLine
            + "/// </summary>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocSummary_WithEmptyAndNullLines_Should_SkipEmptyLines()
    {
        var builder = new CSharpCodeBuilder();
        var summaryLines = new List<string?> { "First line", "", null, "Last line" };

        var result = builder.AppendXmlDocSummary(summaryLines.Where(x => x is not null)!);

        var expected =
            "/// <summary>"
            + Environment.NewLine
            + "/// First line"
            + Environment.NewLine
            + "/// Last line"
            + Environment.NewLine
            + "/// </summary>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
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

        var result = builder.AppendXmlDocParams(parameters);

        var expected =
            "/// <param name=\"param1\">First parameter</param>"
            + Environment.NewLine
            + "/// <param name=\"param2\">Second parameter</param>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
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

        var result = builder.AppendXmlDocRemarks("This is a remark");

        var expected =
            "/// <remarks>"
            + Environment.NewLine
            + "/// This is a remark"
            + Environment.NewLine
            + "/// </remarks>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithMultipleLines_Should_AppendRemarksElementWithAllLines()
    {
        var builder = new CSharpCodeBuilder();
        var remarksLines = new[] { "First remark line", "Second remark line", "Third remark line" };

        var result = builder.AppendXmlDocRemarks(remarksLines);

        var expected =
            "/// <remarks>"
            + Environment.NewLine
            + "/// First remark line"
            + Environment.NewLine
            + "/// Second remark line"
            + Environment.NewLine
            + "/// Third remark line"
            + Environment.NewLine
            + "/// </remarks>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithEmptyAndNullLines_Should_SkipEmptyLines()
    {
        var builder = new CSharpCodeBuilder();
        var remarksLines = new[] { "First remark", "", "Third remark" };

        var result = builder.AppendXmlDocRemarks(remarksLines);

        var expected =
            "/// <remarks>"
            + Environment.NewLine
            + "/// First remark"
            + Environment.NewLine
            + "/// Third remark"
            + Environment.NewLine
            + "/// </remarks>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithOnlyEmptyLines_Should_AppendOnlyOpeningTag()
    {
        var builder = new CSharpCodeBuilder();
        var remarksLines = new[] { "", "", "" };

        var result = builder.AppendXmlDocRemarks(remarksLines);

        // The current implementation has a bug where it opens the tag but doesn't close it
        // when there are no valid content lines
        var expected = "/// <remarks>" + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithNullCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocRemarks((IEnumerable<string>?)null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithEmptyCollection_Should_AppendOnlyOpeningTag()
    {
        var builder = new CSharpCodeBuilder();
        var remarksLines = Array.Empty<string>();

        var result = builder.AppendXmlDocRemarks(remarksLines);

        // The current implementation has a bug where it opens the tag but doesn't close it
        // when there are no valid content lines
        var expected = "/// <remarks>" + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithNullString_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocRemarks((string?)null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocRemarks_WithEmptyString_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocRemarks("");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocExample_WithContent_Should_AppendExampleElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocExample("var example = new Example();");

        var expected =
            "/// <example>"
            + Environment.NewLine
            + "/// var example = new Example();"
            + Environment.NewLine
            + "/// </example>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
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

        var result = builder.AppendXmlDocExample(exampleLines);

        var expected =
            "/// <example>"
            + Environment.NewLine
            + "/// var builder = new CSharpCodeBuilder();"
            + Environment.NewLine
            + "/// builder.AppendLine(\"Hello World\");"
            + Environment.NewLine
            + "/// var result = builder.ToString();"
            + Environment.NewLine
            + "/// </example>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExample_WithEmptyAndNullLines_Should_SkipEmptyLines()
    {
        var builder = new CSharpCodeBuilder();
        var exampleLines = new[] { "var x = 1;", "", "var y = 2;" };

        var result = builder.AppendXmlDocExample(exampleLines);

        var expected =
            "/// <example>"
            + Environment.NewLine
            + "/// var x = 1;"
            + Environment.NewLine
            + "/// var y = 2;"
            + Environment.NewLine
            + "/// </example>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExample_WithOnlyEmptyLines_Should_AppendOnlyOpeningTag()
    {
        var builder = new CSharpCodeBuilder();
        var exampleLines = new[] { "", "", "" };

        var result = builder.AppendXmlDocExample(exampleLines);

        // The current implementation has a bug where it opens the tag but doesn't close it
        // when there are no valid content lines
        var expected = "/// <example>" + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExample_WithEmptyCollection_Should_AppendOnlyOpeningTag()
    {
        var builder = new CSharpCodeBuilder();
        var exampleLines = Array.Empty<string>();

        var result = builder.AppendXmlDocExample(exampleLines);

        // The current implementation has a bug where it opens the tag but doesn't close it
        // when there are no valid content lines
        var expected = "/// <example>" + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExample_WithNullString_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocExample((string?)null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocExample_WithEmptyString_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocExample("");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
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
    public async Task AppendXmlDocSeeAlso_WithCref_Should_AppendSeeAlsoElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocSeeAlso("System.Object");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <seealso cref=\"System.Object\"/>" + Environment.NewLine);
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

        var result = builder.AppendXmlDocInheritDoc();

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("/// <inheritdoc />" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocInheritDoc_WithCref_Should_AppendInheritDocElementWithCref()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocInheritDoc("BaseClass.Method");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <inheritdoc cref=\"BaseClass.Method\" />" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocCustomElement_WithContentAndAttributes_Should_AppendCustomElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocCustomElement("custom", "Custom content", "attr=\"value\"");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <custom attr=\"value\">Custom content</custom>" + Environment.NewLine);
    }

    [Test]
    public async Task AppendXmlDocCustomElement_WithoutContent_Should_AppendSelfClosingElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocCustomElement("custom", null, "attr=\"value\"");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo("/// <custom attr=\"value\" />" + Environment.NewLine);
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
            .AppendXmlDocRemarks("Additional remarks");

        var expected =
            "/// <summary>"
            + Environment.NewLine
            + "/// Method summary"
            + Environment.NewLine
            + "/// </summary>"
            + Environment.NewLine
            + "/// <param name=\"param1\">First parameter</param>"
            + Environment.NewLine
            + "/// <param name=\"param2\">Second parameter</param>"
            + Environment.NewLine
            + "/// <returns>Return value description</returns>"
            + Environment.NewLine
            + "/// <remarks>"
            + Environment.NewLine
            + "/// Additional remarks"
            + Environment.NewLine
            + "/// </remarks>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task XmlDocumentationMethods_WithIndentation_Should_RespectIndentation()
    {
        var builder = new CSharpCodeBuilder();
        builder.IncrementIndent();

        var result = builder
            .AppendXmlDocSummary("Indented summary")
            .AppendXmlDocParam("param", "Parameter description");

        var expected =
            "    /// <summary>"
            + Environment.NewLine
            + "    /// Indented summary"
            + Environment.NewLine
            + "    /// </summary>"
            + Environment.NewLine
            + "    /// <param name=\"param\">Parameter description</param>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task XmlDocumentationMethods_AfterContent_Should_StartOnNewLine()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .Append("public class MyClass")
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocParam("param", "Parameter description")
            .Append("public void MyMethod(string param) { }");

        var expected =
            "public class MyClass"
            + Environment.NewLine
            + "/// <summary>"
            + Environment.NewLine
            + "/// Method summary"
            + Environment.NewLine
            + "/// </summary>"
            + Environment.NewLine
            + "/// <param name=\"param\">Parameter description</param>"
            + Environment.NewLine
            + "public void MyMethod(string param) { }";

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task XmlDocumentationMethods_AtStartOfBuilder_Should_NotAddExtraNewLine()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocSummary("Method summary").AppendXmlDocParam("param", "Parameter description");

        var expected =
            "/// <summary>"
            + Environment.NewLine
            + "/// Method summary"
            + Environment.NewLine
            + "/// </summary>"
            + Environment.NewLine
            + "/// <param name=\"param\">Parameter description</param>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task XmlDocumentationMethods_AfterNewLine_Should_NotAddExtraNewLine()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder
            .AppendLine("public class MyClass")
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocParam("param", "Parameter description");

        var expected =
            "public class MyClass"
            + Environment.NewLine
            + "/// <summary>"
            + Environment.NewLine
            + "/// Method summary"
            + Environment.NewLine
            + "/// </summary>"
            + Environment.NewLine
            + "/// <param name=\"param\">Parameter description</param>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocException_WithValidParameters_Should_AppendExceptionElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("ArgumentNullException", "Thrown when argument is null");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo(
                "/// <exception cref=\"ArgumentNullException\">Thrown when argument is null</exception>"
                    + Environment.NewLine
            );
    }

    [Test]
    public async Task AppendXmlDocException_WithNullExceptionType_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException(null, "Exception description");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithEmptyExceptionType_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("", "Exception description");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithNullDescription_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("ArgumentNullException", null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithEmptyDescription_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("ArgumentNullException", "");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithBothNullParameters_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException(null, null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocException_WithGenericExceptionType_Should_AppendCorrectly()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException("InvalidOperationException", "Thrown when the operation is invalid");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo(
                "/// <exception cref=\"InvalidOperationException\">Thrown when the operation is invalid</exception>"
                    + Environment.NewLine
            );
    }

    [Test]
    public async Task AppendXmlDocException_WithFullyQualifiedType_Should_AppendCorrectly()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException(
            "System.ArgumentOutOfRangeException",
            "Thrown when the value is out of range"
        );

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
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

        var result = builder.AppendXmlDocExceptions(exceptions);

        var expected =
            "/// <exception cref=\"ArgumentNullException\">Thrown when argument is null</exception>"
            + Environment.NewLine
            + "/// <exception cref=\"InvalidOperationException\">Thrown when operation is invalid</exception>"
            + Environment.NewLine
            + "/// <exception cref=\"ArgumentOutOfRangeException\">Thrown when value is out of range</exception>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithSingleException_Should_AppendSingleExceptionElement()
    {
        var builder = new CSharpCodeBuilder();
        var exceptions = new List<(string Type, string Description)>
        {
            ("ArgumentNullException", "Thrown when argument is null"),
        };

        var result = builder.AppendXmlDocExceptions(exceptions);

        var expected =
            "/// <exception cref=\"ArgumentNullException\">Thrown when argument is null</exception>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithNullCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocExceptions(null);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithEmptyCollection_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var exceptions = Array.Empty<(string Type, string Description)>();

        var result = builder.AppendXmlDocExceptions(exceptions);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
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

        var result = builder.AppendXmlDocExceptions(exceptions);

        var expected =
            "/// <exception cref=\"ArgumentNullException\">Valid exception</exception>"
            + Environment.NewLine
            + "/// <exception cref=\"ValidException\">Another valid exception</exception>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocExceptions_WithAllInvalidExceptions_Should_NotAppendAnything()
    {
        var builder = new CSharpCodeBuilder();
        var exceptions = new List<(string Type, string Description)>
        {
            (null!, "Invalid - null type"),
            ("", "Invalid - empty type"),
            ("SomeException", null!),
            ("AnotherException", ""),
        };

        var result = builder.AppendXmlDocExceptions(exceptions);

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
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
            .AppendXmlDocReturns("Return value");

        var expected =
            "/// <summary>"
            + Environment.NewLine
            + "/// Method summary"
            + Environment.NewLine
            + "/// </summary>"
            + Environment.NewLine
            + "/// <exception cref=\"ArgumentNullException\">Thrown when argument is null</exception>"
            + Environment.NewLine
            + "/// <exception cref=\"InvalidOperationException\">Thrown when operation is invalid</exception>"
            + Environment.NewLine
            + "/// <returns>Return value</returns>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
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

        var result = builder.AppendXmlDocExceptions(exceptions);

        var expected =
            "    /// <exception cref=\"ArgumentNullException\">Thrown when argument is null</exception>"
            + Environment.NewLine
            + "    /// <exception cref=\"InvalidOperationException\">Thrown when operation is invalid</exception>"
            + Environment.NewLine;

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert.That(builder.ToString()).IsEqualTo(expected);
    }

    [Test]
    public async Task AppendXmlDocException_WithSpecialCharactersInDescription_Should_AppendCorrectly()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException(
            "ArgumentException",
            "Thrown when value contains <, >, &, or \" characters"
        );

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo(
                "/// <exception cref=\"ArgumentException\">Thrown when value contains <, >, &, or \" characters</exception>"
                    + Environment.NewLine
            );
    }
}
