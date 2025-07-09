namespace NetEvolve.CodeBuilder;

using System;
using System.Runtime.CompilerServices;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Appends a boolean value to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The boolean value to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>Appends either "true" or "false" based on the value if the condition is true.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(bool condition, bool value) =>
        condition ? Append(value) : this;

    /// <summary>
    /// Appends a character repeated a specified number of times to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The character to append.</param>
    /// <param name="repeatCount">The number of times to repeat the character.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="repeatCount"/> is less than zero.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(bool condition, char value, int repeatCount) =>
        condition ? Append(value, repeatCount) : this;

    /// <summary>
    /// Appends a character to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The character to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(bool condition, char value) =>
        condition ? Append(value) : this;

    /// <summary>
    /// Appends a subset of an array of characters to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The character array to append.</param>
    /// <param name="startIndex">The starting position in the character array.</param>
    /// <param name="charCount">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the array is null or empty, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(
        bool condition,
        char[]? value,
        int startIndex,
        int charCount
    ) => condition ? Append(value, startIndex, charCount) : this;

    /// <summary>
    /// Appends an array of characters to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The character array to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the array is null or empty, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(bool condition, char[]? value) =>
        condition ? Append(value) : this;

    /// <summary>
    /// Appends characters from a pointer to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">A pointer to a character array.</param>
    /// <param name="length">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the pointer is null or length is negative, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe CSharpCodeBuilder AppendIf(bool condition, char* value, int length) =>
        condition ? Append(value, length) : this;

    /// <summary>
    /// Appends a read-only memory of characters to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The read-only memory containing the characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the memory is empty, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(bool condition, ReadOnlyMemory<char> value) =>
        condition ? Append(value) : this;

    /// <summary>
    /// Appends a subset of a read-only memory of characters to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The read-only memory containing the characters to append.</param>
    /// <param name="startIndex">The starting position in the memory.</param>
    /// <param name="count">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the memory is empty, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(
        bool condition,
        ReadOnlyMemory<char> value,
        int startIndex,
        int count
    ) => condition ? Append(value, startIndex, count) : this;

    /// <summary>
    /// Appends a subset of a string to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The string to append.</param>
    /// <param name="startIndex">The starting position in the string.</param>
    /// <param name="count">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the string is null or empty, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(bool condition, string? value, int startIndex, int count) =>
        condition ? Append(value, startIndex, count) : this;

    /// <summary>
    /// Appends a string to the current builder if the specified condition is true.
    /// </summary>
    /// <param name="condition">The condition that determines whether to append the value.</param>
    /// <param name="value">The string to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the string is null or empty, the method returns without appending anything.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CSharpCodeBuilder AppendIf(bool condition, string? value) =>
        condition ? Append(value) : this;
}
