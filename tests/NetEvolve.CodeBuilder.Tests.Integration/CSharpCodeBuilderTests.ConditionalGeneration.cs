namespace NetEvolve.CodeBuilder.Tests.Integration;

using System.Globalization;
using System.Linq;

public partial class CSharpCodeBuilderTests
{
    [Test]
    public async Task GenerateMethodWithConditionalContent_Should_ProduceCorrectOutput()
    {
        var builder = new CSharpCodeBuilder();

        var includeLogging = true;
        var includeValidation = false;
        var isAsync = true;

        _ = builder.AppendLine("public class ServiceClass").Append("{");

        if (isAsync)
        {
            _ = builder.Append("public async Task");
        }
        else
        {
            _ = builder.Append("public void");
        }

        _ = builder
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

        _ = await Verify(result);
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

        _ = builder.AppendLine("using System;").AppendLine().AppendLine("public class GeneratedEntity").Append("{");

        // Generate backing fields for properties without setters
        foreach (var prop in properties.Where(p => !p.HasSetter))
        {
            _ = builder
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
            _ = builder.AppendLine();
        }

        // Generate constructor
        var readOnlyProps = properties.Where(p => !p.HasSetter).ToArray();
        if (readOnlyProps.Length > 0)
        {
            _ = builder.Append("public GeneratedEntity(");
            for (var i = 0; i < readOnlyProps.Length; i++)
            {
                if (i > 0)
                {
                    _ = builder.Append(", ");
                }

                _ = builder.AppendFormat(
                    CultureInfo.InvariantCulture,
                    "{0} {1}",
                    readOnlyProps[i].Type,
                    readOnlyProps[i].Name.ToUpperInvariant()
                );
            }
            _ = builder.AppendLine(")").Append("{");

            foreach (var propertyName in readOnlyProps.Select(x => x.Name.ToUpperInvariant()))
            {
                _ = builder
                    .AppendFormat(CultureInfo.InvariantCulture, "_{0} = {1};", propertyName, propertyName)
                    .AppendLine();
            }

            _ = builder.Append("}").AppendLine();
        }

        // Generate properties
        foreach (var prop in properties)
        {
            _ = builder.AppendFormat(CultureInfo.InvariantCulture, "public {0} {1}", prop.Type, prop.Name);

            if (prop.HasGetter && prop.HasSetter)
            {
                _ = builder.AppendLine(" { get; set; }");
            }
            else if (prop.HasGetter && !prop.HasSetter)
            {
                _ = builder
                    .AppendFormat(CultureInfo.InvariantCulture, " => _{0};", prop.Name.ToUpperInvariant())
                    .AppendLine();
            }

            _ = builder.AppendLine();
        }

        _ = builder.Append("}");

        var result = builder.ToString();

        _ = await Verify(result);
    }
}
