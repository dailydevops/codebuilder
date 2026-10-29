namespace NetEvolve.CodeBuilder;

using System;

public partial class CSharpCodeBuilder
{
    /// <summary>
    /// Creates a scope that automatically manages indentation levels.
    /// </summary>
    /// <returns>A <see cref="ScopeHandler"/> that increments indentation on creation and decrements on disposal.</returns>
    /// <remarks>
    /// The returned scope handler implements <see cref="IDisposable"/> and is designed for use with
    /// the 'using' statement. When the scope is created, indentation is incremented by one level.
    /// When the scope is disposed (at the end of the using block), indentation is decremented.
    /// </remarks>
    /// <example>
    /// <code>
    /// using (builder.Scope())
    /// {
    ///     builder.AppendLine("return true;");
    /// }
    /// </code>
    /// </example>
    public IDisposable Scope() => new ScopeHandler(this);

    /// <summary>
    /// Appends a line of text and creates a scope that automatically manages indentation levels with braces.
    /// </summary>
    /// <param name="value">The string value to append before creating the scope. Can be <see langword="null"/>.</param>
    /// <returns>A <see cref="ScopeHandler"/> that appends an opening brace, increments indentation on creation, and appends a closing brace with decremented indentation on disposal.</returns>
    /// <remarks>
    /// This method combines <see cref="AppendLine(string?)"/> with <see cref="Scope()"/> functionality.
    /// The returned scope handler implements <see cref="IDisposable"/> and is designed for use with the using statement.
    /// When the scope is created, an opening brace is appended and indentation is incremented by one level.
    /// When the scope is disposed, indentation is decremented and a closing brace is appended.
    /// </remarks>
    /// <example>
    /// <code>
    /// var builder = new CSharpCodeBuilder();
    /// using (builder.ScopeLine("public class MyClass"))
    /// {
    ///     builder.AppendLine("public string Name { get; set; }");
    /// }
    /// </code>
    /// </example>
    public IDisposable ScopeLine(string? value)
    {
        _ = AppendLine(value);
        return new ScopeHandler(this);
    }

    /// <summary>
    /// A disposable class that manages indentation scope for a <see cref="CSharpCodeBuilder"/>.
    /// </summary>
    /// <remarks>
    /// This class increments the indentation level when created and decrements it when disposed.
    /// It is designed to work with the 'using' statement to provide automatic indentation management.
    /// </remarks>
    private sealed class ScopeHandler : IDisposable
    {
        private readonly CSharpCodeBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeHandler"/> class and increments the indentation level.
        /// </summary>
        /// <param name="builder">The <see cref="CSharpCodeBuilder"/> instance to manage indentation for.</param>
        internal ScopeHandler(CSharpCodeBuilder builder)
        {
            _builder = builder;
            _ = _builder.Append('{');
        }

        /// <summary>
        /// Decrements the indentation level when the scope is disposed.
        /// </summary>
        /// <remarks>
        /// This method is called automatically when the 'using' statement block ends.
        /// </remarks>
        public void Dispose() => _ = _builder.Append('}');
    }
}
