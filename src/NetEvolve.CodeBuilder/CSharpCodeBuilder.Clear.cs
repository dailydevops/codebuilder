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

    /// <summary>
    /// Appends a single indentation unit to the current builder without affecting the indentation level.
    /// </summary>
    /// <returns>The current <see cref="CSharpCodeBuilder"/> instance to allow for method chaining.</returns>
    /// <remarks>
    /// This method adds one indentation unit directly to the builder based on the <see cref="CodeBuilderBase.UseTabs"/> setting.
    /// If <see cref="CodeBuilderBase.UseTabs"/> is <see langword="true"/>, a tab character is appended.
    /// If <see cref="CodeBuilderBase.UseTabs"/> is <see langword="false"/>, four space characters are appended.
    /// Unlike automatic indentation that occurs at the start of new lines, this method provides manual control
    /// over indentation placement and does not modify the current indentation level.
    /// </remarks>
    public CSharpCodeBuilder Intend()
    {
        _ = _builder.Append(UseTabs ? '\t' : ' ', UseTabs ? 1 : 4);

        return this;
    }
}
