namespace NetEvolve.CodeBuilder;

using System.Text;

/// <summary>
/// Provides functionality for building C# code strings with proper indentation.
/// </summary>
public partial record CSharpCodeBuilder : CodeBuilderBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CSharpCodeBuilder"/> class.
    /// </summary>
    public CSharpCodeBuilder()
        : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CSharpCodeBuilder"/> class with the specified initial capacity.
    /// </summary>
    /// <param name="initialCapacity">The initial capacity of the internal <see cref="StringBuilder"/>.</param>
    public CSharpCodeBuilder(int initialCapacity)
        : base(initialCapacity) { }
}
