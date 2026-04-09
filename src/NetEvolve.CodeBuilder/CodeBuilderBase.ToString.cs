namespace NetEvolve.CodeBuilder;

public partial class CodeBuilderBase
{
    /// <summary>
    /// Returns the string that has been built by this <see cref="CSharpCodeBuilder"/>.
    /// </summary>
    /// <returns>The string representation of the current instance.</returns>
    /// <remarks>
    /// This method returns the content of the internal <see cref="System.Text.StringBuilder"/>.
    /// </remarks>
    public sealed override string ToString() => _builder.ToString();
}
