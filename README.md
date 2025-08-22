# NetEvolve.CodeBuilder

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](../../LICENSE)
[![NuGet](https://img.shields.io/nuget/v/NetEvolve.CodeBuilder.svg)](https://www.nuget.org/packages/NetEvolve.CodeBuilder/)

**NetEvolve.CodeBuilder** is a high-performance, memory-efficient builder for creating C# code with proper indentation and formatting. Designed specifically for code generation scenarios, it provides an intuitive API with automatic indentation management and thread-safe operations.

## 🎯 Overview

This package provides a powerful code building solution that simplifies the creation of well-formatted C# code through programmatic means. The main goal is to provide developers with a time-saving tool that handles the complexity of proper code formatting, indentation, and structure, with a focus on:

- **Automatic indentation management** for code blocks with `{` and `}` characters
- **High-performance operations** built on top of `StringBuilder` with optimized memory usage
- **Flexible formatting options** supporting both spaces and tabs for indentation
- **Conditional code generation** with `AppendIf` and `AppendLineIf` methods
- **Culture-aware formatting** with `AppendFormat` methods
- **XML documentation support** for generating properly formatted code comments
- **Memory-efficient operations** with support for `ReadOnlyMemory<char>` and unsafe pointer operations

## 🚀 Features

### Automatic Indentation Management
- Smart handling of opening and closing braces (`{`, `}`) and brackets (`[`, `]`)
- Configurable indentation using spaces or tabs
- Thread-safe indentation operations for multi-threaded scenarios

### High-Performance Architecture
- Built on top of `StringBuilder` for optimal memory usage
- Support for pre-allocated capacity to minimize memory allocations
- Efficient string building with minimal overhead

### Conditional Code Generation
- `AppendIf()` and `AppendLineIf()` methods for conditional content
- Clean, readable code generation logic without complex if-else blocks

### Culture-Aware Formatting
- `AppendFormat()` methods with `IFormatProvider` support
- Proper handling of culture-specific formatting requirements
- Support for multiple format arguments

### XML Documentation Support
- Built-in methods for generating XML documentation comments
- Support for summary, param, returns, exception, and custom elements
- Proper indentation and formatting of documentation blocks

### Memory-Efficient Operations
- Support for `ReadOnlyMemory<char>` for zero-allocation string operations
- Unsafe pointer operations for maximum performance scenarios
- Efficient handling of character arrays and subsets

## 📦 Installation

### .NET CLI

```bash
dotnet add package NetEvolve.CodeBuilder
```

### Package Manager Console

```powershell
Install-Package NetEvolve.CodeBuilder
```

### PackageReference

```xml
<PackageReference Include="NetEvolve.CodeBuilder" Version="1.0.0" />
```

## 🛠️ Requirements

- **.NET Standard 2.0/2.1**: `System.Memory` package for `ReadOnlyMemory<T>` support
- **.NET 8+**: No additional dependencies
- **Multi-target support**: .NET Standard 2.0, 2.1, .NET 8, 9, and 10

## 📖 Usage

All code building functionality is available through the `CSharpCodeBuilder` class in the `NetEvolve.CodeBuilder` namespace.

```csharp
using NetEvolve.CodeBuilder;
```

### Basic Usage

#### Creating a Builder

```csharp
// Default constructor
var builder = new CSharpCodeBuilder();

// With initial capacity for better performance
var builder = new CSharpCodeBuilder(1024);
```

#### Simple Code Generation

```csharp
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

#### Working with Indentation

```csharp
var builder = new CSharpCodeBuilder();

builder.AppendLine("namespace MyNamespace")
       .Append("{")
       .AppendLine("public class MyClass")
       .Append("{")
       .AppendLine("private int _field;")
       .AppendLine()
       .AppendLine("public int Property { get; set; }")
       .Append("}")
       .Append("}");
```

#### Using Tabs Instead of Spaces

```csharp
var builder = new CSharpCodeBuilder();
builder.UseTabs = true; // Use tabs for indentation instead of spaces

builder.AppendLine("public class TabIndentedClass")
       .Append("{")
       .AppendLine("public void Method() { }")
       .Append("}");
```

### Advanced Features

#### Conditional Code Generation

```csharp
var builder = new CSharpCodeBuilder();
bool includeComments = true;
bool isPublic = true;
bool isStatic = false;

builder.AppendLineIf(includeComments, "// This is a dynamically generated class")
       .AppendIf(isPublic, "public ")
       .AppendIf(isStatic, "static ")
       .AppendLine("class MyClass")
       .Append("{")
       .AppendLineIf(includeComments, "// Class implementation here")
       .Append("}");
```

#### Format Support

```csharp
var builder = new CSharpCodeBuilder();

// Basic formatting
builder.AppendFormat(CultureInfo.InvariantCulture, "public {0} {1}", "class", "MyClass");

// Multiple arguments with proper formatting
builder.AppendFormat(CultureInfo.InvariantCulture, 
    "public {0} {1}({2} {3})", 
    "void", "SetValue", "string", "value");

// Culture-specific formatting
var culture = new CultureInfo("de-DE");
builder.AppendFormat(culture, "// Price: {0:C}", 123.45m);
```

#### XML Documentation Generation

```csharp
var builder = new CSharpCodeBuilder();

builder.AppendXmlDocSummary("Calculates the sum of two numbers.")
       .AppendXmlDocParam("a", "The first number to add.")
       .AppendXmlDocParam("b", "The second number to add.")
       .AppendXmlDocReturns("The sum of the two numbers.")
       .AppendLine("public int Add(int a, int b)")
       .Append("{")
       .AppendLine("return a + b;")
       .Append("}");
```

**Output:**
```csharp
/// <summary>
/// Calculates the sum of two numbers.
/// </summary>
/// <param name="a">The first number to add.</param>
/// <param name="b">The second number to add.</param>
/// <returns>The sum of the two numbers.</returns>
public int Add(int a, int b)
{
    return a + b;
}
```

#### Memory-Efficient Operations

```csharp
var builder = new CSharpCodeBuilder();

// Using ReadOnlyMemory<char>
ReadOnlyMemory<char> methodName = "GetValue".AsMemory();
builder.AppendLine("public string ")
       .Append(methodName)
       .AppendLine("()");

// Pre-allocating capacity
var largeBuilder = new CSharpCodeBuilder(4096); // Pre-allocate 4KB
```

#### Capacity Management

```csharp
var builder = new CSharpCodeBuilder();

// Check current capacity and length
Console.WriteLine($"Capacity: {builder.Capacity}, Length: {builder.Length}");

// Ensure minimum capacity for performance
builder.EnsureCapacity(2048);

// Clear content while keeping capacity
builder.Clear();
```

## 🏗️ Real-World Examples

### Class Generation

```csharp
public string GenerateClass(string className, List<Property> properties)
{
    var builder = new CSharpCodeBuilder();
    
    builder.AppendLine("using System;")
           .AppendLine()
           .AppendXmlDocSummary($"Represents a {className} entity.")
           .AppendFormat(CultureInfo.InvariantCulture, "public class {0}", className)
           .AppendLine()
           .Append("{");
    
    foreach (var property in properties)
    {
        builder.AppendXmlDocSummary($"Gets or sets the {property.Name}.")
               .AppendFormat(CultureInfo.InvariantCulture, 
                    "public {0} {1} {{ get; set; }}", 
                    property.Type, property.Name)
               .AppendLine()
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
    
    builder.AppendXmlDocSummary($"Executes the {methodName} operation.")
           .AppendIf(isPublic, "public ")
           .AppendIf(isAsync, "async Task")
           .AppendIf(!isAsync, "void")
           .Append($" {methodName}(");
    
    for (int i = 0; i < parameters.Count; i++)
    {
        var param = parameters[i];
        builder.Append($"{param.Type} {param.Name}");
        
        if (i < parameters.Count - 1)
            builder.Append(", ");
    }
    
    builder.AppendLine(")")
           .Append("{")
           .AppendLineIf(isAsync, "await Task.CompletedTask;")
           .AppendLine("// Implementation here")
           .Append("}");
    
    return builder.ToString();
}
```

### Interface Generation

```csharp
public string GenerateInterface(string interfaceName, List<MethodSignature> methods)
{
    var builder = new CSharpCodeBuilder();
    
    builder.AppendXmlDocSummary($"Defines the contract for {interfaceName}.")
           .AppendFormat(CultureInfo.InvariantCulture, "public interface {0}", interfaceName)
           .AppendLine()
           .Append("{");
    
    foreach (var method in methods)
    {
        builder.AppendXmlDocSummary(method.Description);
        
        foreach (var param in method.Parameters)
        {
            builder.AppendXmlDocParam(param.Name, param.Description);
        }
        
        if (!string.IsNullOrEmpty(method.ReturnDescription))
        {
            builder.AppendXmlDocReturns(method.ReturnDescription);
        }
        
        builder.AppendFormat(CultureInfo.InvariantCulture,
                "{0} {1}({2});",
                method.ReturnType,
                method.Name,
                string.Join(", ", method.Parameters.Select(p => $"{p.Type} {p.Name}")))
               .AppendLine()
               .AppendLine();
    }
    
    builder.Append("}");
    
    return builder.ToString();
}
```

## 📚 API Reference

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

### XML Documentation Methods

| Method | Description |
|--------|-------------|
| `AppendXmlDoc(string)` | Appends a single-line XML documentation comment |
| `AppendXmlDocSummary(string)` | Appends an XML summary element |
| `AppendXmlDocSummary(IEnumerable<string>)` | Appends an XML summary element with multiple lines |
| `AppendXmlDocParam(string, string)` | Appends an XML param element |
| `AppendXmlDocReturns(string)` | Appends an XML returns element |
| `AppendXmlDocException(string, string)` | Appends an XML exception element |
| `AppendXmlDocRemarks(string)` | Appends an XML remarks element |
| `AppendXmlDocExample(string)` | Appends an XML example element |
| `AppendXmlDocCustomElement(string, string, string)` | Appends a custom XML documentation element |

### Format Methods

| Method | Description |
|--------|-------------|
| `AppendFormat(IFormatProvider, string, object)` | Appends formatted string with single argument |
| `AppendFormat(IFormatProvider, string, object, object)` | Appends formatted string with two arguments |
| `AppendFormat(IFormatProvider, string, object, object, object)` | Appends formatted string with three arguments |
| `AppendFormat(IFormatProvider, string, params object[])` | Appends formatted string with multiple arguments |

### Memory-Efficient Methods

| Method | Description |
|--------|-------------|
| `Append(ReadOnlyMemory<char>)` | Appends read-only memory of characters |
| `Append(ReadOnlyMemory<char>, int, int)` | Appends subset of read-only memory |
| `Append(char*, int)` | Appends characters from unsafe pointer (unsafe context required) |
| `Append(char[], int, int)` | Appends subset of character array |

## 🤝 Contributing

Contributions are welcome! Please feel free to submit issues, feature requests, or pull requests.

## 🔗 Related Packages

This package is part of the **NetEvolve** ecosystem of .NET extensions and utilities. Check out other packages in the NetEvolve family for additional functionality.

---

**Made with ❤️ by the NetEvolve Team**