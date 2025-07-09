namespace NetEvolve.CodeBuilder;

using System;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Appends a boolean value to the current builder.
    /// </summary>
    /// <param name="value">The boolean value to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>Appends either "True" or "False" based on the value.</remarks>
    public CSharpCodeBuilder Append(bool value)
    {
        EnsureIndented();
        _ = _builder.Append(value ? "true" : "false");
        return this;
    }

    /// <summary>
    /// Appends a character repeated a specified number of times to the current builder.
    /// </summary>
    /// <param name="value">The character to append.</param>
    /// <param name="repeatCount">The number of times to repeat the character.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="repeatCount"/> is less than zero.</exception>
    public CSharpCodeBuilder Append(char value, int repeatCount)
    {
        EnsureIndented();
        _ = _builder.Append(value, repeatCount);
        return this;
    }

    /// <summary>
    /// Appends a character to the current builder.
    /// </summary>
    /// <param name="value">The character to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    public CSharpCodeBuilder Append(char value)
    {
        if (value is '\0')
        {
            return this; // No need to append null character
        }

        if (value is '\n' or '\r')
        {
            return AppendLine(); // Handle new line characters
        }

        if (value is '}' or ']')
        {
            DecrementIndent();
        }

        EnsureIndented();
        _ = _builder.Append(value);

        if (value is '{' or '[')
        {
            IncrementIndent();
            return AppendLine();
        }
        else if (value is '}' or ']')
        {
            return AppendLine();
        }

        return this;
    }

    /// <summary>
    /// Appends a subset of an array of characters to the current builder.
    /// </summary>
    /// <param name="value">The character array to append.</param>
    /// <param name="startIndex">The starting position in the character array.</param>
    /// <param name="charCount">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the array is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder Append(char[]? value, int startIndex, int charCount)
    {
        if (value is null || value.Length == 0)
        {
            return this;
        }

        EnsureIndented();
        _ = _builder.Append(value, startIndex, charCount);
        return this;
    }

    /// <summary>
    /// Appends an array of characters to the current builder.
    /// </summary>
    /// <param name="value">The character array to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the array is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder Append(char[]? value)
    {
        if (value is null || value.Length == 0)
        {
            return this;
        }

        EnsureIndented();
        _ = _builder.Append(value);
        return this;
    }

    /// <summary>
    /// Appends characters from a pointer to the current builder.
    /// </summary>
    /// <param name="value">A pointer to a character array.</param>
    /// <param name="length">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the pointer is null or length is negative, the method returns without appending anything.</remarks>
    public unsafe CSharpCodeBuilder Append(char* value, int length)
    {
        if (value == null || length < 0)
        {
            return this;
        }

        EnsureIndented();
        _ = _builder.Append(value, length);
        return this;
    }

    /// <summary>
    /// Appends a read-only memory of characters to the current builder.
    /// </summary>
    /// <param name="value">The read-only memory containing the characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the memory is empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder Append(ReadOnlyMemory<char> value)
    {
        if (value.IsEmpty)
        {
            return this;
        }

        EnsureIndented();
        _ = _builder.Append(value);
        return this;
    }

    /// <summary>
    /// Appends a subset of a read-only memory of characters to the current builder.
    /// </summary>
    /// <param name="value">The read-only memory containing the characters to append.</param>
    /// <param name="startIndex">The starting position in the memory.</param>
    /// <param name="count">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the memory is empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder Append(ReadOnlyMemory<char> value, int startIndex, int count)
    {
        if (value.IsEmpty)
        {
            return this;
        }

        EnsureIndented();
        _ = _builder.Append(value.Slice(startIndex, count));
        return this;
    }

    /// <summary>
    /// Appends a subset of a string to the current builder.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <param name="startIndex">The starting position in the string.</param>
    /// <param name="count">The number of characters to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the string is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder Append(string? value, int startIndex, int count)
    {
        if (string.IsNullOrEmpty(value))
        {
            return this;
        }

        EnsureIndented();
        _ = _builder.Append(value, startIndex, count);
        return this;
    }

    /// <summary>
    /// Appends a string to the current builder.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>If the string is null or empty, the method returns without appending anything.</remarks>
    public CSharpCodeBuilder Append(string? value)
    {
        if (string.IsNullOrEmpty(value) || value is "\0")
        {
            return this;
        }

        if (value is "\n" or "\r")
        {
            return AppendLine(); // Handle new line characters
        }

        if (value is "}" or "]")
        {
            DecrementIndent();
            _ = AppendLine();
        }

        EnsureIndented();
        _ = _builder.Append(value);

        if (value is "{" or "[")
        {
            IncrementIndent();
            _ = AppendLine();
        }

        return this;
    }
}
