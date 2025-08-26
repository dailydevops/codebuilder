namespace NetEvolve.CodeBuilder;

using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Appends a single-line XML documentation comment.
    /// </summary>
    /// <param name="content">The content for the documentation comment.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the content is null or empty, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendXmlDoc(string? content) =>
        string.IsNullOrEmpty(content) ? this : EnsureNewLineForXmlDoc().AppendLine($"/// {content}");

    /// <summary>
    /// Appends an XML documentation summary element.
    /// </summary>
    /// <param name="summary">The summary text to include in the documentation.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the summary is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocSummary(string? summary)
    {
        if (string.IsNullOrEmpty(summary))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc()
            .AppendLine("/// <summary>")
            .AppendLine($"/// {summary}")
            .AppendLine("/// </summary>");
    }

    /// <summary>
    /// Appends an XML documentation summary element with multiple lines.
    /// </summary>
    /// <param name="summaryLines">The summary lines to include in the documentation.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the summary lines collection is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocSummary(IEnumerable<string>? summaryLines)
    {
        if (summaryLines is null)
        {
            return this;
        }

        var hasContent = false;
        var builder = EnsureNewLineForXmlDoc().AppendLine("/// <summary>");

        foreach (var line in summaryLines.Where(l => !string.IsNullOrEmpty(l)))
        {
            builder = builder.AppendLine($"/// {line}");
            hasContent = true;
        }

        return hasContent ? builder.AppendLine("/// </summary>") : this;
    }

    /// <summary>
    /// Appends an XML documentation param element.
    /// </summary>
    /// <param name="paramName">The name of the parameter.</param>
    /// <param name="description">The description of the parameter.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the parameter name or description is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocParam(string? paramName, string? description)
    {
        if (string.IsNullOrEmpty(paramName) || string.IsNullOrEmpty(description))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc().AppendLine($"/// <param name=\"{paramName}\">{description}</param>");
    }

    /// <summary>
    /// Appends multiple XML documentation param elements.
    /// </summary>
    /// <param name="parameters">A collection of parameter name and description pairs.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the parameters collection is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocParams(IEnumerable<(string Name, string Description)>? parameters)
    {
        if (parameters is null)
        {
            return this;
        }

        var builder = this;
        foreach (var (name, description) in parameters)
        {
            builder = builder.AppendXmlDocParam(name, description);
        }

        return builder;
    }

    /// <summary>
    /// Appends an XML documentation returns element.
    /// </summary>
    /// <param name="description">The description of the return value.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the description is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocReturns(string? description)
    {
        if (string.IsNullOrEmpty(description))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc().AppendLine($"/// <returns>{description}</returns>");
    }

    /// <summary>
    /// Appends an XML documentation remarks element.
    /// </summary>
    /// <param name="remarks">The remarks text to include in the documentation.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the remarks text is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocRemarks(string? remarks)
    {
        if (string.IsNullOrEmpty(remarks))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc()
            .AppendLine("/// <remarks>")
            .Append("/// ")
            .AppendLine(remarks)
            .AppendLine("/// </remarks>");
    }

    /// <summary>
    /// Appends an XML documentation remarks element with multiple lines.
    /// </summary>
    /// <param name="remarksLines">The remarks lines to include in the documentation.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the remarks lines collection is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocRemarks(IEnumerable<string>? remarksLines)
    {
        if (remarksLines is null)
        {
            return this;
        }

        var hasContent = false;
        var builder = EnsureNewLineForXmlDoc().AppendLine("/// <remarks>");

        foreach (var line in remarksLines.Where(l => !string.IsNullOrEmpty(l)))
        {
            builder = builder.AppendLine($"/// {line}");
            hasContent = true;
        }

        return hasContent ? builder.AppendLine("/// </remarks>") : this;
    }

    /// <summary>
    /// Appends an XML documentation exception element.
    /// </summary>
    /// <param name="exceptionType">The type of exception that can be thrown.</param>
    /// <param name="description">The description of when the exception is thrown.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the exception type or description is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocException(string? exceptionType, string? description)
    {
        if (string.IsNullOrEmpty(exceptionType) || string.IsNullOrEmpty(description))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc()
            .AppendLine($"/// <exception cref=\"{exceptionType}\">{description}</exception>");
    }

    /// <summary>
    /// Appends an XML documentation exception element.
    /// </summary>
    /// <param name="description">The description of when the exception is thrown.</param>
    /// <typeparam name="TException">The type of exception that can be thrown. Must be a subclass of <see cref="Exception"/>.</typeparam>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the exception type or description is null or empty, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendXmlDocException<TException>(string? description)
        where TException : Exception => AppendXmlDocException(typeof(TException).Name, description);

    /// <summary>
    /// Appends multiple XML documentation exception elements.
    /// </summary>
    /// <param name="exceptions">A collection of exception type and description pairs.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the exceptions collection is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocExceptions(IEnumerable<(string Type, string Description)>? exceptions)
    {
        if (exceptions is null)
        {
            return this;
        }

        var builder = this;
        foreach (var (type, description) in exceptions)
        {
            builder = builder.AppendXmlDocException(type, description);
        }

        return builder;
    }

    /// <summary>
    /// Appends an XML documentation example element.
    /// </summary>
    /// <param name="example">The example text or code to include in the documentation.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the example is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocExample(string? example)
    {
        if (string.IsNullOrEmpty(example))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc()
            .AppendLine("/// <example>")
            .AppendLine($"/// {example}")
            .AppendLine("/// </example>");
    }

    /// <summary>
    /// Appends an XML documentation example element with multiple lines.
    /// </summary>
    /// <param name="exampleLines">The example lines to include in the documentation.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the example lines collection is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocExample(IEnumerable<string>? exampleLines)
    {
        if (exampleLines is null)
        {
            return this;
        }

        var hasContent = false;
        var builder = EnsureNewLineForXmlDoc().AppendLine("/// <example>");

        foreach (var line in exampleLines.Where(l => !string.IsNullOrEmpty(l)))
        {
            builder = builder.AppendLine($"/// {line}");
            hasContent = true;
        }

        return hasContent ? builder.AppendLine("/// </example>") : this;
    }

    /// <summary>
    /// Appends an XML documentation see element for cross-references.
    /// </summary>
    /// <param name="cref">The cross-reference to another member or type.</param>
    /// <param name="isHRef">If set to <c>true</c>, uses 'href' instead of 'cref' for external links.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the cref is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocSee(string? cref, bool isHRef = false)
    {
        if (string.IsNullOrEmpty(cref))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc().AppendLine($"/// <see {(isHRef ? "href" : "cref")}=\"{cref}\"/>");
    }

    /// <summary>
    /// Appends an XML documentation seealso element for see-also references.
    /// </summary>
    /// <param name="cref">The cross-reference to another member or type.</param>
    /// <param name="isHRef">If set to <c>true</c>, uses 'href' instead of 'cref' for external links.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the cref is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocSeeAlso(string? cref, bool isHRef = false)
    {
        if (string.IsNullOrEmpty(cref))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc().AppendLine($"/// <seealso {(isHRef ? "href" : "cref")}=\"{cref}\"/>");
    }

    /// <summary>
    /// Appends an XML documentation value element for property documentation.
    /// </summary>
    /// <param name="description">The description of the property value.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the description is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocValue(string? description)
    {
        if (string.IsNullOrEmpty(description))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc().AppendLine($"/// <value>{description}</value>");
    }

    /// <summary>
    /// Appends an XML documentation typeparam element for generic type parameters.
    /// </summary>
    /// <param name="paramName">The name of the type parameter.</param>
    /// <param name="description">The description of the type parameter.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the parameter name or description is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocTypeParam(string? paramName, string? description)
    {
        if (string.IsNullOrEmpty(paramName) || string.IsNullOrEmpty(description))
        {
            return this;
        }

        return EnsureNewLineForXmlDoc().AppendLine($"/// <typeparam name=\"{paramName}\">{description}</typeparam>");
    }

    /// <summary>
    /// Appends multiple XML documentation typeparam elements.
    /// </summary>
    /// <param name="typeParameters">A collection of type parameter name and description pairs.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the type parameters collection is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocTypeParams(IEnumerable<(string, string)>? typeParameters)
    {
        if (typeParameters is null)
        {
            return this;
        }

        var builder = this;
        foreach (var (name, description) in typeParameters)
        {
            builder = builder.AppendXmlDocTypeParam(name, description);
        }

        return builder;
    }

    /// <summary>
    /// Appends an inheritdoc XML documentation element.
    /// </summary>
    /// <param name="cref">Optional cross-reference to specify which member to inherit documentation from.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If no cref is provided, inherits documentation from the base or interface member.</remarks>
    public CSharpCodeBuilder AppendXmlDocInheritDoc(string? cref = null) =>
        string.IsNullOrEmpty(cref)
            ? EnsureNewLineForXmlDoc().AppendLine("/// <inheritdoc />")
            : EnsureNewLineForXmlDoc().AppendLine($"/// <inheritdoc cref=\"{cref}\" />");

    /// <summary>
    /// Appends a custom XML documentation element.
    /// </summary>
    /// <param name="elementName">The name of the XML element.</param>
    /// <param name="content">The content of the XML element.</param>
    /// <param name="attributes">Optional attributes for the XML element.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the element name is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder AppendXmlDocCustomElement(
        string? elementName,
        string? content = null,
        string? attributes = null
    )
    {
        if (string.IsNullOrEmpty(elementName))
        {
            return this;
        }

        var attributesPart = string.IsNullOrEmpty(attributes) ? string.Empty : $" {attributes}";

        if (string.IsNullOrEmpty(content))
        {
            return EnsureNewLineForXmlDoc().AppendLine($"/// <{elementName}{attributesPart} />");
        }

        return EnsureNewLineForXmlDoc().AppendLine($"/// <{elementName}{attributesPart}>{content}</{elementName}>");
    }

    /// <summary>
    /// Ensures that XML documentation comments start on a new line if there's already content in the builder.
    /// </summary>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>
    /// This method checks if the builder already has content and if we're not already on a new line,
    /// it appends a line break to ensure XML documentation starts on a fresh line.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CSharpCodeBuilder EnsureNewLineForXmlDoc() => AppendLineIf(Length > 0 && !_isNewline);
}
