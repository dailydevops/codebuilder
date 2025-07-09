namespace NetEvolve.CodeBuilder;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Clears the content of the current builder and resets the indentation level to zero.
    /// </summary>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>
    /// This method removes all characters from the internal <see cref="System.Text.StringBuilder"/> and
    /// resets the indentation level to zero, effectively providing a clean slate for building new content.
    /// </remarks>
    public CSharpCodeBuilder Clear()
    {
        _ = _builder.Clear();
        _ = Interlocked.Exchange(ref _indentLevel, 0);

        return this;
    }
}
