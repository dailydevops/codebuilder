namespace NetEvolve.CodeBuilder.Tests.Integration;

using System;
using System.Collections.Generic;
using System.Threading;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task GenerateFullyDocumentedMethod_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder()
            .AppendLine("namespace MyApplication.Documentation;")
            .AppendLine()
            .AppendXmlDoc("A raw single-line documentation remark placed above the type.")
            .AppendXmlDocSummary(["Represents a fully documented service.", "Every XML doc tag is exercised."])
            .AppendXmlDocTypeParams([("TRequest", "The request type."), ("TResponse", "The response type.")])
            .AppendXmlDocRemarks(["This type demonstrates every supported XML documentation helper.", "Line two."])
            .AppendXmlDocExample([
                "var service = new DocumentedService<Request, Response>();",
                "service.Process(request);",
            ])
            .AppendXmlDocSeeAlso("DocumentedServiceBase")
            .AppendXmlDocSeeAlso("https://example.invalid/docs", isHref: true)
            .AppendLine("public sealed class DocumentedService<TRequest, TResponse>")
            .Append("{")
            .AppendXmlDocSummary("Processes the specified request.")
            .AppendXmlDocParams([
                ("request", "The request to process."),
                ("cancellationToken", "A cancellation token."),
            ])
            .AppendXmlDocReturns("The response produced for the request.")
            .AppendXmlDocExceptions([
                ("ArgumentNullException", "Thrown when request is null."),
                ("OperationCanceledException", "Thrown when the operation is cancelled."),
            ])
            .AppendXmlDocException<InvalidOperationException>("Thrown when the service has not been initialized.")
            .AppendXmlDocSee("DocumentedServiceBase.Process")
            .AppendXmlDocExample("var response = service.Process(request, default);")
            .AppendLine("public TResponse Process(TRequest request, CancellationToken cancellationToken)")
            .Append("{")
            .AppendLine("throw new NotImplementedException();")
            .Append("}")
            .AppendLine()
            .AppendXmlDocValue("Gets the number of processed requests.")
            .AppendLine("public int ProcessedCount { get; }")
            .AppendLine()
            .AppendXmlDocInheritDoc()
            .AppendLine("public override string? ToString() => base.ToString();")
            .AppendLine()
            .AppendXmlDocInheritDoc("DocumentedServiceBase.Dispose")
            .AppendLine("public void Dispose() { }")
            .AppendLine()
            .AppendXmlDocCustomElement("note", "This is a custom documentation element.", "type=\"important\"")
            .AppendXmlDocCustomElement("preliminary")
            .Append("}");

        var result = builder.ToString();

        _ = await Verify(result).ConfigureAwait(false);
    }

    [Test]
    public async Task GenerateDocumentation_WithEmptyOrNullInputs_Should_BeIgnored()
    {
        var builder = new CSharpCodeBuilder()
            .AppendXmlDoc(null)
            .AppendXmlDoc(string.Empty)
            .AppendXmlDocSummary(default(string))
            .AppendXmlDocSummary(default(IEnumerable<string>))
            .AppendXmlDocSummary(Array.Empty<string>())
            .AppendXmlDocParam(null, "description")
            .AppendXmlDocParam("name", null)
            .AppendXmlDocParams(null)
            .AppendXmlDocReturns(null)
            .AppendXmlDocRemarks(default(string))
            .AppendXmlDocRemarks(default(IEnumerable<string>))
            .AppendXmlDocException(null, "description")
            .AppendXmlDocException("Type", null)
            .AppendXmlDocExceptions(null)
            .AppendXmlDocExample(default(string))
            .AppendXmlDocExample(default(IEnumerable<string>))
            .AppendXmlDocSee(null)
            .AppendXmlDocSeeAlso(null)
            .AppendXmlDocValue(null)
            .AppendXmlDocTypeParam(null, "description")
            .AppendXmlDocTypeParam("name", null)
            .AppendXmlDocTypeParams(null)
            .AppendXmlDocCustomElement(null);

        _ = await Assert.That(builder.ToString()).IsEqualTo(string.Empty);
    }
}
