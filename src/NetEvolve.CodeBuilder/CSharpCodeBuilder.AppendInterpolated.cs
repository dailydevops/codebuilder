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
        [InterpolatedStringHandlerArgument("")] ref CSharpInterpolatedStringHandler handler
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
        [InterpolatedStringHandlerArgument("")] ref CSharpInterpolatedStringHandler handler
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

/// <summary>
/// Custom interpolated string handler for <see cref="CSharpCodeBuilder"/>.
/// </summary>
/// <remarks>
/// This handler is instantiated by the compiler when an interpolated string is passed to
/// <see cref="CSharpCodeBuilder.AppendInterpolated"/> or <see cref="CSharpCodeBuilder.AppendLineInterpolated"/>.
/// It appends each literal and formatted part directly to the builder, applying indentation before
/// the first non-empty part on a new line.
/// </remarks>
[InterpolatedStringHandler]
public ref struct CSharpInterpolatedStringHandler
{
    private readonly CSharpCodeBuilder _owner;
    private bool _indentEnsured;

    /// <summary>
    /// Initializes a new instance of the <see cref="CSharpInterpolatedStringHandler"/> struct.
    /// </summary>
    /// <param name="literalLength">The total length of all literal parts (hint for capacity).</param>
    /// <param name="formattedCount">The number of formatted holes in the interpolated string.</param>
    /// <param name="builder">The <see cref="CSharpCodeBuilder"/> to append to.</param>
    public CSharpInterpolatedStringHandler(int literalLength, int formattedCount, CSharpCodeBuilder builder)
    {
        _owner = builder;
        _indentEnsured = false;
    }

    private void EnsureIndented()
    {
        if (!_indentEnsured)
        {
            _owner.HandlerEnsureIndented();
            _indentEnsured = true;
        }
    }

    /// <summary>Appends a literal string part of the interpolated string.</summary>
    /// <param name="value">The literal string to append.</param>
    public void AppendLiteral(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        EnsureIndented();
        _owner.HandlerRawAppend(value);
    }

    /// <summary>Appends a formatted value from the interpolated string.</summary>
    /// <typeparam name="T">The type of the value to format.</typeparam>
    /// <param name="value">The value to append.</param>
    public void AppendFormatted<T>(T value)
    {
        var str = value?.ToString();
        if (string.IsNullOrEmpty(str))
        {
            return;
        }

        EnsureIndented();
        _owner.HandlerRawAppend(str);
    }

    /// <summary>Appends a formatted value with a format string from the interpolated string.</summary>
    /// <typeparam name="T">The type of the value to format. Must implement <see cref="System.IFormattable"/>.</typeparam>
    /// <param name="value">The value to append.</param>
    /// <param name="format">The format string.</param>
    public void AppendFormatted<T>(T value, string? format)
        where T : System.IFormattable
    {
        var str = value?.ToString(format, CultureInfo.InvariantCulture);
        if (string.IsNullOrEmpty(str))
        {
            return;
        }

        EnsureIndented();
        _owner.HandlerRawAppend(str);
    }

    /// <summary>Appends a formatted value with alignment from the interpolated string.</summary>
    /// <typeparam name="T">The type of the value to format.</typeparam>
    /// <param name="value">The value to append.</param>
    /// <param name="alignment">Minimum width; negative values left-align.</param>
    public void AppendFormatted<T>(T value, int alignment)
    {
        var str = value?.ToString();
        if (str is null)
        {
            return;
        }

        str = alignment >= 0 ? str.PadLeft(alignment) : str.PadRight(-alignment);
        EnsureIndented();
        _owner.HandlerRawAppend(str);
    }

    /// <summary>Appends a formatted value with alignment and format string from the interpolated string.</summary>
    /// <typeparam name="T">The type of the value to format. Must implement <see cref="System.IFormattable"/>.</typeparam>
    /// <param name="value">The value to append.</param>
    /// <param name="alignment">Minimum width; negative values left-align.</param>
    /// <param name="format">The format string.</param>
    public void AppendFormatted<T>(T value, int alignment, string? format)
        where T : System.IFormattable
    {
        var str = value?.ToString(format, CultureInfo.InvariantCulture) ?? string.Empty;
        str = alignment >= 0 ? str.PadLeft(alignment) : str.PadRight(-alignment);
        EnsureIndented();
        _owner.HandlerRawAppend(str);
    }

    /// <summary>Appends a <see cref="ReadOnlySpan{T}"/> value from the interpolated string.</summary>
    /// <param name="value">The span to append.</param>
    public void AppendFormatted(ReadOnlySpan<char> value)
    {
        if (value.IsEmpty)
        {
            return;
        }

        EnsureIndented();
        _owner.HandlerRawAppend(value);
    }
}
#endif
