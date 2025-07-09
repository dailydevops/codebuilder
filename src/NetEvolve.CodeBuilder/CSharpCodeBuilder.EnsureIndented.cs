namespace NetEvolve.CodeBuilder;

using System.Runtime.CompilerServices;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Ensures that indentation is applied if we are at the start of a new line.
    /// </summary>
    /// <remarks>
    /// This method adds the appropriate indentation (tabs or spaces) at the beginning of a line
    /// based on the current indentation level. It only applies indentation if the current position is
    /// at the start of a new line (when <see cref="_isNewline"/> is <see langword="true"/>).
    /// </remarks>
    private void EnsureIndented(bool deactivate = false)
    {
        if (!_isNewline || deactivate)
        {
            return;
        }

        var indentCount = Math.Max(0, _indentLevel * (UseTabs ? 1 : 4));
        _ = _builder.Append(UseTabs ? '\t' : ' ', indentCount);

        _isNewline = false;
    }

    /// <summary>
    /// Increments the indentation level by one.
    /// </summary>
    /// <remarks>
    /// This method increases the current indentation level, which affects subsequent lines
    /// that are appended to the builder. The operation is thread-safe.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void IncrementIndent() => Interlocked.Increment(ref _indentLevel);

    /// <summary>
    /// Decrements the indentation level by one.
    /// </summary>
    /// <remarks>
    /// This method decreases the current indentation level, which affects subsequent lines
    /// that are appended to the builder. The operation is thread-safe.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void DecrementIndent()
    {
        if (Interlocked.Decrement(ref _indentLevel) < 0)
        {
            _ = Interlocked.Exchange(ref _indentLevel, 0);
        }
    }
}
