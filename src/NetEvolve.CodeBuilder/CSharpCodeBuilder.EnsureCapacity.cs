namespace NetEvolve.CodeBuilder;

public partial record CSharpCodeBuilder
{
    /// <summary>
    /// Ensures that the capacity of the internal <see cref="System.Text.StringBuilder"/> is at least the specified value.
    /// </summary>
    /// <param name="capacity">The minimum capacity to ensure.</param>
    /// <remarks>
    /// If the current capacity is less than the value specified, the capacity is increased to the specified value.
    /// </remarks>
    public CSharpCodeBuilder EnsureCapacity(int capacity)
    {
        _ = _builder.EnsureCapacity(capacity);

        return this;
    }
}
