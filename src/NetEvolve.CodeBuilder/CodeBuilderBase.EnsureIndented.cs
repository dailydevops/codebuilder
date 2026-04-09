namespace NetEvolve.CodeBuilder;

using System.Runtime.CompilerServices;

public partial class CodeBuilderBase
{
    /// <summary>
    /// Ensures that indentation is applied if we are at the start of a new line.
    /// </summary>
    /// <remarks>
    /// This method adds the appropriate indentation (tabs or spaces) at the beginning of a line
    /// based on the current indentation level. It only applies indentation if the current position is
    /// at the start of a new line (when <see cref="_isNewline"/> is <see langword="true"/>).
    /// </remarks>
    private protected void EnsureIndented(bool deactivate = false)
    {
        if (!_isNewline || deactivate)
        {
            return;
        }

        if (UseTabs)
        {
            _ = _builder.Append('\t', _indentLevel);
        }
        else
        {
            _ = _builder.Append(' ', _indentLevel * 4);
        }

        _isNewline = false;
    }

    /// <summary>
    /// Increments the indentation level by one.
    /// </summary>
    /// <remarks>
    /// This method increases the current indentation level, which affects subsequent lines
    /// that are appended to the builder.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void IncrementIndent() => _indentLevel++;

    /// <summary>
    /// Decrements the indentation level by one.
    /// </summary>
    /// <remarks>
    /// This method decreases the current indentation level, which affects subsequent lines
    /// that are appended to the builder. The indentation level will not go below zero.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void DecrementIndent()
    {
        if (--_indentLevel < 0)
        {
            _indentLevel = 0;
        }
    }
}
