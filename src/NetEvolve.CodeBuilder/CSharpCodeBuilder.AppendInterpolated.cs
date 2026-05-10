#if NET6_0_OR_GREATER
namespace NetEvolve.CodeBuilder;

using System.Globalization;
using System.Runtime.CompilerServices;

public partial class CSharpCodeBuilder
{
    /// <summary>
    /// Appends an interpolated string to the current builder.
    /// </summary>
    /// <param name="handler">The interpolated string handler built by the compiler.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <example>
    /// <code>
    /// builder.AppendInterpolated($"public {typeName} {memberName}");
    /// </code>
    /// </example>
    public CSharpCodeBuilder AppendInterpolated(
#pragma warning disable IDE0060 // Remove unused parameter
        [InterpolatedStringHandlerArgument("")] ref CSharpInterpolatedStringHandler handler
#pragma warning restore IDE0060 // Remove unused parameter
    ) => this;

    /// <summary>
    /// Appends an interpolated string followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="handler">The interpolated string handler built by the compiler.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <example>
    /// <code>
    /// builder.AppendLineInterpolated($"public class {className}");
    /// </code>
    /// </example>
    public CSharpCodeBuilder AppendLineInterpolated(
#pragma warning disable IDE0060 // Remove unused parameter
        [InterpolatedStringHandlerArgument("")] ref CSharpInterpolatedStringHandler handler
#pragma warning restore IDE0060 // Remove unused parameter
    ) => AppendLine();

    internal void HandlerEnsureIndented() => EnsureIndented();

    internal void HandlerRawAppend(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _ = _builder.Append(value);
        }
    }

    internal void HandlerRawAppend(ReadOnlySpan<char> value)
    {
        if (!value.IsEmpty)
        {
            _ = _builder.Append(value);
        }
    }
}
#endif
