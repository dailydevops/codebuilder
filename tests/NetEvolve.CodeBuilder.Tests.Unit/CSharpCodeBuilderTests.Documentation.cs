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
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// This is a comment" + Environment.NewLine);
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
            .IsEqualTo(
                "/// <param name=\"paramName\">Parameter description</param>" + Environment.NewLine
            );
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
    public async Task AppendXmlDocException_WithValidParameters_Should_AppendExceptionElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocException(
            "ArgumentNullException",
            "Thrown when argument is null"
        );

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo(
                "/// <exception cref=\"ArgumentNullException\">Thrown when argument is null</exception>"
                    + Environment.NewLine
            );
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
    public async Task AppendXmlDocSee_WithCref_Should_AppendSeeElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocSee("System.String");

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <see cref=\"System.String\"/>" + Environment.NewLine);
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
            .IsEqualTo(
                "/// <typeparam name=\"T\">The generic type parameter</typeparam>"
                    + Environment.NewLine
            );
    }

    [Test]
    public async Task AppendXmlDocInheritDoc_WithoutCref_Should_AppendInheritDocElement()
    {
        var builder = new CSharpCodeBuilder();

        var result = builder.AppendXmlDocInheritDoc();

        _ = await Assert.That(result).IsEqualTo(builder);
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <inheritdoc />" + Environment.NewLine);
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

        var result = builder.AppendXmlDocCustomElement(
            "custom",
            "Custom content",
            "attr=\"value\""
        );

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
        _ = await Assert
            .That(builder.ToString())
            .IsEqualTo("/// <custom attr=\"value\" />" + Environment.NewLine);
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

        var result = builder
            .AppendXmlDocSummary("Method summary")
            .AppendXmlDocParam("param", "Parameter description");

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
}
