namespace NetEvolve.CodeBuilder.Tests.Integration;

using System.Runtime.CompilerServices;
using VerifyTUnit;

internal static class Startup
{
    [ModuleInitializer]
    public static void Initialize()
    {
        Verifier.DerivePathInfo(
            (sourceFile, projectDirectory, type, method) =>
            {
                var directory = Path.Combine(projectDirectory, "_snapshots");
                _ = Directory.CreateDirectory(directory);
                return new(directory, type.Name, method.Name);
            }
        );

        VerifierSettings.AutoVerify(includeBuildServer: false);
    }
}
