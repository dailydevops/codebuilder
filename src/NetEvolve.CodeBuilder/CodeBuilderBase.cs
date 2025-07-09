namespace NetEvolve.CodeBuilder;

using System.Text;

/// <summary>
/// Provides the base functionality for building code strings with proper indentation and formatting.
/// </summary>
public abstract partial record CodeBuilderBase
{
    private protected readonly StringBuilder _builder;
    private protected int _indentLevel;
    private protected bool _isNewline = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeBuilderBase"/> class.
    /// </summary>
    private protected CodeBuilderBase() => _builder = new StringBuilder();

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeBuilderBase"/> class with the specified initial capacity.
    /// </summary>
    /// <param name="initialCapacity">The initial capacity of the internal <see cref="StringBuilder"/>.</param>
    private protected CodeBuilderBase(int initialCapacity) => _builder = new StringBuilder(initialCapacity);

    /// <summary>
    /// Gets the current capacity of the internal <see cref="StringBuilder"/>.
    /// </summary>
    /// <value>The capacity of the internal string builder.</value>
    public int Capacity => _builder.Capacity;

    /// <summary>
    /// Gets the current length of the internal <see cref="StringBuilder"/>.
    /// </summary>
    /// <value>The length of the content that has been built so far.</value>
    public int Length => _builder.Length;

    /// <summary>
    /// Gets or sets a value indicating whether to use tabs instead of spaces for indentation.
    /// </summary>
    /// <value><c>true</c> to use tabs for indentation; <c>false</c> to use spaces.</value>
    public bool UseTabs { get; set; }
}
