namespace NetEvolve.CodeBuilder;

using System;
using System.Globalization;
using System.Runtime.CompilerServices;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Appends a formatted string to the current builder using invariant culture.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="arg0">The object to format.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="format"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException">Thrown when <paramref name="format"/> is invalid or the index of a format item is greater than zero.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendFormat(string format, object? arg0) =>
        AppendFormat(CultureInfo.InvariantCulture, format, arg0);

    /// <summary>
    /// Appends a formatted string to the current builder using invariant culture.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to format.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="format"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException">Thrown when <paramref name="format"/> is invalid or the index of a format item is greater than the number of elements in <paramref name="args"/> minus 1.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendFormat(string format, params object?[] args) =>
        AppendFormat(CultureInfo.InvariantCulture, format, args);

    /// <summary>
    /// Appends a formatted string to the current builder using the specified format provider.
    /// </summary>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to format.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="format"/> is <see langword="null"/>.</exception>
    /// <exception cref="FormatException">Thrown when <paramref name="format"/> is invalid or the index of a format item is greater than the number of elements in <paramref name="args"/> minus 1.</exception>
    public CSharpCodeBuilder AppendFormat(
        IFormatProvider? provider,
        string format,
        params object?[] args
    )
    {
        EnsureIndented();
        _ = _builder.AppendFormat(provider, format, args);
        return this;
    }
}
