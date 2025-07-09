namespace NetEvolve.CodeBuilder;

using System;
using System.Runtime.CompilerServices;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Appends a line terminator to the current builder.
    /// </summary>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>
    /// This method appends the default line terminator for the current environment and sets the internal state
    /// to indicate that the next character will be at the beginning of a new line.
    /// </remarks>
    public CSharpCodeBuilder AppendLine()
    {
        EnsureIndented(true);
        _ = _builder.AppendLine();
        _isNewline = true;
        return this;
    }

    /// <summary>
    /// Appends a string followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the string is null or empty, only the line terminator is appended.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendLine(string? value) => Append(value).AppendLine();

    /// <summary>
    /// Appends a read-only memory of characters followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">The read-only memory containing the characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the memory is empty, only the line terminator is appended.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendLine(ReadOnlyMemory<char> value) => Append(value).AppendLine();

    /// <summary>
    /// Appends a subset of a read-only memory of characters followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">The read-only memory containing the characters to append.</param>
    /// <param name="startIndex">The starting position in the memory.</param>
    /// <param name="count">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the memory is empty, only the line terminator is appended.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendLine(ReadOnlyMemory<char> value, int startIndex, int count) =>
        Append(value, startIndex, count).AppendLine();

    /// <summary>
    /// Appends an array of characters followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">The character array to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the array is null or empty, only the line terminator is appended.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendLine(char[]? value) => Append(value).AppendLine();

    /// <summary>
    /// Appends a subset of an array of characters followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">The character array to append.</param>
    /// <param name="startIndex">The starting position in the character array.</param>
    /// <param name="charCount">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the array is null or empty, only the line terminator is appended.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendLine(char[]? value, int startIndex, int charCount) =>
        Append(value, startIndex, charCount).AppendLine();

    /// <summary>
    /// Appends characters from a pointer followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">A pointer to a character array.</param>
    /// <param name="length">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the pointer is null or length is negative, only the line terminator is appended.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe CSharpCodeBuilder AppendLine(char* value, int length) =>
        Append(value, length).AppendLine();

    /// <summary>
    /// Appends a character followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">The character to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendLine(char value) => Append(value).AppendLine();

    /// <summary>
    /// Appends a character repeated a specified number of times followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">The character to append.</param>
    /// <param name="repeatCount">The number of times to repeat the character.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="repeatCount"/> is less than zero.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendLine(char value, int repeatCount) =>
        Append(value, repeatCount).AppendLine();

    /// <summary>
    /// Appends a boolean value followed by a line terminator to the current builder.
    /// </summary>
    /// <param name="value">The boolean value to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>Appends either "true" or "false" based on the value, followed by a line terminator.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendLine(bool value) => Append(value).AppendLine();
}
