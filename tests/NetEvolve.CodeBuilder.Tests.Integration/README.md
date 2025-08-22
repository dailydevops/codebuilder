# NetEvolve.CodeBuilder.Tests.Integration

This project contains integration tests for the NetEvolve.CodeBuilder library, focusing on end-to-end testing scenarios that validate the complete functionality of the code generation capabilities.

## Test Framework

- **TUnit**: Primary testing framework
- **Verify.TUnit**: For snapshot testing (to be enabled when environment supports it)
- **No Mocking**: Tests use real instances and validate actual output

## Test Organization

The integration tests are organized into partial classes for better maintainability:

### CSharpCodeBuilderIntegrationTests.ComplexGeneration.cs
- Complete class generation with documentation
- Interface generation with multiple methods
- Real-world code generation scenarios

### CSharpCodeBuilderIntegrationTests.ConditionalGeneration.cs
- Conditional content generation using AppendIf methods
- Reflection-based code generation patterns
- Dynamic property and method generation

### CSharpCodeBuilderIntegrationTests.Formatting.cs
- Indentation testing (spaces vs tabs)
- Code formatting validation
- Enum generation with attributes

### CSharpCodeBuilderIntegrationTests.SnapshotTests.cs
- Placeholder for snapshot tests using Verify.TUnit
- Will be enabled when framework compatibility is resolved

## Test Scenarios

The integration tests cover:

1. **End-to-End Class Generation**: Complete C# classes with proper indentation, documentation, and structure
2. **Interface Generation**: Service interfaces with async methods and documentation
3. **Conditional Logic**: Using AppendIf and AppendLineIf for dynamic content
4. **Formatting Options**: Testing both space and tab-based indentation
5. **Complex Structures**: Enums with attributes, nested code blocks, exception handling

## Running Tests

```bash
# Run all integration tests
dotnet test tests/NetEvolve.CodeBuilder.Tests.Integration/

# Run specific test class
dotnet test tests/NetEvolve.CodeBuilder.Tests.Integration/ --filter "ClassName=CSharpCodeBuilderIntegrationTests"
```

## Adding New Tests

When adding new integration tests:

1. Follow the existing naming convention: `MethodName_Condition_Should_ExpectedResult`
2. Use the [Test] attribute from TUnit
3. Use partial classes to organize related tests
4. Validate both content and formatting in assertions
5. Include real-world scenarios that developers would encounter

## Future Enhancements

- Snapshot testing with Verify.TUnit when framework support is available
- Performance benchmarking for large code generation scenarios
- Cross-platform indentation testing
- Template-based code generation tests