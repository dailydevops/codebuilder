namespace NetEvolve.CodeBuilder.Tests.Integration;

using System;
using System.Globalization;
using System.Linq;

public partial class CSharpCodeBuilderIntegrationTests
{
    [Test]
    public async Task GenerateMethodWithConditionalContent_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder();

        var includeLogging = true;
        var includeValidation = false;
        var isAsync = true;

        builder.AppendLine("public class ServiceClass").Append("{");

        if (isAsync)
        {
            builder.Append("public async Task");
        }
        else
        {
            builder.Append("public void");
        }

        builder
            .Append(" ProcessDataAsync(string input)")
            .Append("{")
            .AppendLineIf(includeValidation, "if (string.IsNullOrEmpty(input))")
            .AppendLineIf(includeValidation, "{")
            .AppendLineIf(
                includeValidation,
                "    throw new ArgumentException(\"Input cannot be null or empty\", nameof(input));"
            )
            .AppendLineIf(includeValidation, "}")
            .AppendLineIf(includeValidation, "")
            .AppendLineIf(includeLogging, "Console.WriteLine($\"Processing input: {input}\");")
            .AppendLine("var result = input.ToUpperInvariant();")
            .AppendLineIf(includeLogging, "Console.WriteLine($\"Processing complete: {result}\");")
            .AppendLineIf(isAsync, "await Task.CompletedTask;")
            .AppendLine("return result;")
            .Append("}")
            .Append("}");

        var result = builder.ToString();

        // Verify conditional content was applied correctly
        _ = await Assert.That(result).Contains("public async Task ProcessDataAsync(string input)"); // async version
        _ = await Assert.That(result).Contains("Console.WriteLine($\"Processing input: {input}\");"); // logging included
        _ = await Assert.That(result).Contains("await Task.CompletedTask;"); // async included
        _ = await Assert.That(result).DoesNotContain("if (string.IsNullOrEmpty(input))"); // validation excluded
        _ = await Assert.That(result).Contains("input.ToUpperInvariant()"); // proper string case conversion
    }

    [Test]
    public async Task GenerateReflectionBasedCode_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder();

        var properties = new[]
        {
            new
            {
                Name = "Id",
                Type = "int",
                HasGetter = true,
                HasSetter = false,
            },
            new
            {
                Name = "Name",
                Type = "string?",
                HasGetter = true,
                HasSetter = true,
            },
            new
            {
                Name = "Email",
                Type = "string?",
                HasGetter = true,
                HasSetter = true,
            },
            new
            {
                Name = "CreatedAt",
                Type = "DateTime",
                HasGetter = true,
                HasSetter = false,
            },
        };

        builder.AppendLine("using System;").AppendLine().AppendLine("public class GeneratedEntity").Append("{");

        // Generate backing fields for properties without setters
        foreach (var prop in properties.Where(p => !p.HasSetter))
        {
            builder
                .AppendFormat(
                    CultureInfo.InvariantCulture,
                    "private readonly {0} _{1};",
                    prop.Type,
                    prop.Name.ToUpperInvariant()
                )
                .AppendLine();
        }

        if (properties.Any(p => !p.HasSetter))
        {
            builder.AppendLine();
        }

        // Generate constructor
        var readOnlyProps = properties.Where(p => !p.HasSetter).ToArray();
        if (readOnlyProps.Length > 0)
        {
            builder.Append("public GeneratedEntity(");
            for (int i = 0; i < readOnlyProps.Length; i++)
            {
                if (i > 0)
                    builder.Append(", ");
                builder.AppendFormat(
                    CultureInfo.InvariantCulture,
                    "{0} {1}",
                    readOnlyProps[i].Type,
                    readOnlyProps[i].Name.ToUpperInvariant()
                );
            }
            builder.AppendLine(")").Append("{");

            foreach (var prop in readOnlyProps)
            {
                builder
                    .AppendFormat(
                        CultureInfo.InvariantCulture,
                        "_{0} = {1};",
                        prop.Name.ToUpperInvariant(),
                        prop.Name.ToUpperInvariant()
                    )
                    .AppendLine();
            }

            builder.Append("}").AppendLine();
        }

        // Generate properties
        foreach (var prop in properties)
        {
            builder.AppendFormat(CultureInfo.InvariantCulture, "public {0} {1}", prop.Type, prop.Name);

            if (prop.HasGetter && prop.HasSetter)
            {
                builder.AppendLine(" { get; set; }");
            }
            else if (prop.HasGetter && !prop.HasSetter)
            {
                builder
                    .AppendFormat(CultureInfo.InvariantCulture, " => _{0};", prop.Name.ToUpperInvariant())
                    .AppendLine();
            }

            builder.AppendLine();
        }

        builder.Append("}");

        var result = builder.ToString();

        // Basic verification for reflection-based code generation
        _ = await Assert.That(result).Contains("public class GeneratedEntity");
        _ = await Assert.That(result).Contains("private readonly int _ID;");
        _ = await Assert.That(result).Contains("private readonly DateTime _CREATEDAT;");
        _ = await Assert.That(result).Contains("public GeneratedEntity(int ID, DateTime CREATEDAT)");
        _ = await Assert.That(result).Contains("public int Id => _ID;");
        _ = await Assert.That(result).Contains("public string? Name { get; set; }");
        _ = await Assert.That(result).Contains("public string? Email { get; set; }");
        _ = await Assert.That(result).Contains("public DateTime CreatedAt => _CREATEDAT;");
    }
}
