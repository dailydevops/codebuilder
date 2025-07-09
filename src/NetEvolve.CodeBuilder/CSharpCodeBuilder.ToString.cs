namespace NetEvolve.CodeBuilder;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Returns the string that has been built by this <see cref="CSharpCodeBuilder"/>.
    /// </summary>
    /// <returns>The string representation of the current instance.</returns>
    /// <remarks>
    /// This method returns the content of the internal <see cref="System.Text.StringBuilder"/>.
    /// </remarks>
    public override string ToString() => _builder.ToString();
}
