# NetEvolve.CodeBuilder

[![NuGet](https://img.shields.io/nuget/v/NetEvolve.CodeBuilder.svg)](https://www.nuget.org/packages/NetEvolve.CodeBuilder/)
[![Build Status](https://github.com/dailydevops/codebuilder/workflows/CI/badge.svg)](https://github.com/dailydevops/codebuilder/actions)

A high-performance, memory-efficient builder for creating C# code with proper indentation and formatting. Designed specifically for code generation scenarios, it provides an intuitive API with automatic indentation management and thread-safe operations.

## Features

- **Automatic Indentation**: Intelligent handling of code blocks with `{` and `}` characters
- **High Performance**: Built on top of `StringBuilder` with optimized memory usage
- **Flexible Formatting**: Support for both spaces and tabs indentation
- **Thread-Safe**: Safe indentation operations for multi-threaded scenarios
- **Conditional Appending**: `AppendIf` and `AppendLineIf` methods for conditional code generation
- **Format Support**: `AppendFormat` methods with culture-specific formatting
- **Memory Efficient**: Support for `ReadOnlyMemory<char>` and unsafe pointer operations
- **Multi-Target Framework**: Supports .NET Standard 2.0, 2.1, .NET 8, 9, and 10

## Installation

### Package Manager Console
```powershell
Install-Package NetEvolve.CodeBuilder
```

### .NET CLI
```bash
dotnet add package NetEvolve.CodeBuilder
```

### PackageReference
```xml
<PackageReference Include="NetEvolve.CodeBuilder" Version="1.0.0" />
```

## Quick Start

```csharp
using NetEvolve.CodeBuilder;

var builder = new CSharpCodeBuilder();

builder.AppendLine("public class HelloWorld")
       .Append("{")
       .AppendLine("public void SayHello()")
       .Append("{")
       .AppendLine("Console.WriteLine(\"Hello, World!\");")
       .Append("}")
       .Append("}");

Console.WriteLine(builder.ToString());
```

**Output:**
```csharp
public class HelloWorld
{
    public void SayHello()
    {
        Console.WriteLine("Hello, World!");
    }
}
```

## Basic Usage

### Creating a Builder

```csharp
// Default constructor
var builder = new CSharpCodeBuilder();

// With initial capacity for better performance
var builder = new CSharpCodeBuilder(1024);
```

### Appending Content

```csharp
var builder = new CSharpCodeBuilder();

// Append strings
builder.Append("public class ")
       .Append("MyClass");

// Append with automatic line breaks
builder.AppendLine("public class MyClass");

// Append characters
builder.Append('{');  // Automatically increments indentation
builder.Append('}');  // Automatically decrements indentation
```

### Working with Indentation

```csharp
var builder = new CSharpCodeBuilder();

// Manual indentation control
builder.IncrementIndent();
builder.AppendLine("// This line is indented");
builder.DecrementIndent();

// Automatic indentation with braces
builder.AppendLine("if (condition)")
       .Append("{")           // Automatically increments indent
       .AppendLine("DoSomething();")
       .Append("}");          // Automatically decrements indent
```

### Using Tabs Instead of Spaces

```csharp
var builder = new CSharpCodeBuilder { UseTabs = true };

builder.AppendLine("public class MyClass")
       .Append("{")
       .AppendLine("// This uses tab indentation")
       .Append("}");
```

## Advanced Features

### Conditional Appending

```csharp
var builder = new CSharpCodeBuilder();
bool includeComments = true;
bool isPublic = true;

builder.AppendLineIf(includeComments, "// This is a comment")
       .AppendIf(isPublic, "public ")
       .AppendLine("class MyClass")
       .Append("{")
       .Append("}");
```

### Format Support

```csharp
var builder = new CSharpCodeBuilder();

// Basic formatting
builder.AppendFormat(CultureInfo.InvariantCulture, "public {0} {1}", "class", "MyClass");

// Multiple arguments
builder.AppendFormat(CultureInfo.InvariantCulture, 
    "public {0} {1}({2} {3})", 
    "void", "SetValue", "string", "value");

// With culture-specific formatting
var culture = new CultureInfo("de-DE");
builder.AppendFormat(culture, "// Price: {0:C}", 123.45m);
```

### Memory-Efficient Operations

```csharp
var builder = new CSharpCodeBuilder();

// Using ReadOnlyMemory<char>
ReadOnlyMemory<char> codeFragment = "public void Method()".AsMemory();
builder.Append(codeFragment);

// Using character arrays
char[] chars = ['p', 'u', 'b', 'l', 'i', 'c'];
builder.Append(chars, 0, 6);

// Unsafe operations for maximum performance
unsafe
{
    fixed (char* ptr = "unsafe code")
    {
        builder.Append(ptr, 11);
    }
}
```

### Capacity Management

```csharp
var builder = new CSharpCodeBuilder();

// Check current capacity and length
Console.WriteLine($"Capacity: {builder.Capacity}");
Console.WriteLine($"Length: {builder.Length}");

// Ensure sufficient capacity for better performance
builder.EnsureCapacity(2048);

// Clear content but keep capacity
builder.Clear();
```

## Real-World Examples

### Class Generation

```csharp
public string GenerateClass(string className, List<Property> properties)
{
    var builder = new CSharpCodeBuilder();
    
    builder.AppendLine("using System;")
           .AppendLine()
           .AppendFormat(CultureInfo.InvariantCulture, "public class {0}", className)
           .AppendLine()
           .Append("{");
    
    foreach (var property in properties)
    {
        builder.AppendFormat(CultureInfo.InvariantCulture, 
                "public {0} {1} {{ get; set; }}", 
                property.Type, property.Name)
               .AppendLine();
    }
    
    builder.Append("}");
    
    return builder.ToString();
}
```

### Method Generation with Conditions

```csharp
public string GenerateMethod(string methodName, bool isAsync, bool isPublic, List<Parameter> parameters)
{
    var builder = new CSharpCodeBuilder();
    
    builder.AppendIf(isPublic, "public ")
           .AppendIf(isAsync, "async ")
           .AppendIf(isAsync, "Task", "void")
           .Append(" ")
           .Append(methodName)
           .Append("(");
    
    for (int i = 0; i < parameters.Count; i++)
    {
        if (i > 0) builder.Append(", ");
        builder.AppendFormat(CultureInfo.InvariantCulture, 
            "{0} {1}", parameters[i].Type, parameters[i].Name);
    }
    
    builder.AppendLine(")")
           .Append("{")
           .AppendLineIf(isAsync, "await Task.CompletedTask;")
           .Append("}");
    
    return builder.ToString();
}
```

### Interface Generation

```csharp
public string GenerateInterface(string interfaceName, List<MethodSignature> methods)
{
    var builder = new CSharpCodeBuilder();
    
    builder.AppendFormat(CultureInfo.InvariantCulture, "public interface {0}", interfaceName)
           .AppendLine()
           .Append("{");
    
    foreach (var method in methods)
    {
        builder.AppendFormat(CultureInfo.InvariantCulture, 
                "{0} {1}({2});", 
                method.ReturnType, 
                method.Name, 
                string.Join(", ", method.Parameters.Select(p => $"{p.Type} {p.Name}")))
               .AppendLine();
    }
    
    builder.Append("}");
    
    return builder.ToString();
}
```

## API Reference

### Core Methods

| Method | Description |
|--------|-------------|
| `Append(string)` | Appends a string to the builder |
| `AppendLine()` | Appends a line terminator |
| `AppendLine(string)` | Appends a string followed by a line terminator |
| `AppendIf(bool, string)` | Conditionally appends a string |
| `AppendLineIf(bool, string)` | Conditionally appends a string with line terminator |
| `AppendFormat(IFormatProvider, string, ...)` | Appends formatted string |
| `Clear()` | Removes all content from the builder |
| `EnsureCapacity(int)` | Ensures the builder has at least the specified capacity |
| `ToString()` | Returns the built string |

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Capacity` | `int` | Gets the current capacity of the internal StringBuilder |
| `Length` | `int` | Gets the current length of the content |
| `UseTabs` | `bool` | Gets or sets whether to use tabs instead of spaces for indentation |

### Indentation Methods

| Method | Description |
|--------|-------------|
| `IncrementIndent()` | Increases indentation level by one |
| `DecrementIndent()` | Decreases indentation level by one |

## Performance Considerations

- **Initial Capacity**: Specify an appropriate initial capacity to minimize memory allocations
- **Memory Types**: Use `ReadOnlyMemory<char>` overloads for better memory efficiency
- **Unsafe Operations**: Use unsafe pointer methods for maximum performance in critical scenarios
- **Capacity Management**: Use `EnsureCapacity()` when you know the approximate final size

```csharp
// Good: Pre-allocate capacity
var builder = new CSharpCodeBuilder(4096);

// Good: Ensure capacity before large operations
builder.EnsureCapacity(estimatedSize);

// Good: Use memory-efficient overloads
builder.Append(text.AsMemory());
```

## Thread Safety

The `CSharpCodeBuilder` uses thread-safe operations for indentation management through `Interlocked` operations. However, the underlying `StringBuilder` operations are not thread-safe, so avoid concurrent access to the same instance from multiple threads.

## Target Frameworks

- .NET Standard 2.0
- .NET Standard 2.1
- .NET 8.0
- .NET 9.0
- .NET 10.0

## Dependencies

- **.NET Standard 2.0/2.1**: `System.Memory` package for `ReadOnlyMemory<T>` support
- **.NET 8+**: No additional dependencies

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Changelog

See [RELEASES](https://github.com/dailydevops/codebuilder/releases) for a detailed changelog.
