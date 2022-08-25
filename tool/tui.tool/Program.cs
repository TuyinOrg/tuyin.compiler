// See https://aka.ms/new-console-template for more information
using libtui;
using libtui.controls;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Reflection;
using tui.tool.template;

if (args.Length > 0)
{
    IEnumerable<MetadataReference> AddAssemblies(params string[] files)
    {
        for (var i = 0; i < files.Length; i++)
            yield return MetadataReference.CreateFromFile(files[i]);
    }

    IEnumerable<MetadataReference> AddNetCoreDefaultReferences()
    {
        var rtPath = Path.GetDirectoryName(typeof(object).Assembly.Location) +
                     Path.DirectorySeparatorChar;

        return AddAssemblies(
            rtPath + "System.Private.CoreLib.dll",
            rtPath + "System.Runtime.dll",
            rtPath + "System.Console.dll",
            rtPath + "netstandard.dll",
            rtPath + "System.Linq.dll",
            rtPath + "System.IO.dll",
            rtPath + "System.Net.Primitives.dll",
            rtPath + "System.Net.Http.dll",
            rtPath + "System.Private.Uri.dll",
            rtPath + "System.ComponentModel.Primitives.dll",
            rtPath + "System.Globalization.dll",
            rtPath + "Microsoft.CSharp.dll"
        );
    }

    string templateName = Path.GetFileNameWithoutExtension(args[0]);
    SourceText st = SourceText.From(TemplateControlParser.Parse(templateName, File.ReadAllText(args[0])));
    CSharpParseOptions option = new CSharpParseOptions(LanguageVersion.CSharp10, preprocessorSymbols: new List<string>() { "Debug" });
    SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(st, option);
    Debug.Assert(syntaxTree.GetDiagnostics().ToList().Count == 0);

    //var ns = Assembly.Load("netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51");
    string assemblyName = Path.GetRandomFileName();
    var references = AddNetCoreDefaultReferences().Concat(new MetadataReference[]
    {
        //MetadataReference.CreateFromFile(ns.Location),
        MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
        MetadataReference.CreateFromFile(typeof(IControl).Assembly.Location)
    });

    CSharpCompilation compilation = CSharpCompilation.Create(
        assemblyName,
        syntaxTrees: new[] { syntaxTree },
        references: references,
        options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

    using (var ms = new MemoryStream())
    {
        EmitResult result = compilation.Emit(ms);

        if (!result.Success)
        {
            IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                diagnostic.IsWarningAsError ||
                diagnostic.Severity == DiagnosticSeverity.Error);

            foreach (Diagnostic diagnostic in failures)
                Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());

            Console.ReadLine();
        }
        else
        {
            ms.Seek(0, SeekOrigin.Begin);
            Assembly assembly = Assembly.Load(ms.ToArray());

            Type type = assembly.GetType($"tui.tool.{templateName}");
            IControl obj = Activator.CreateInstance(type) as IControl;
            App.Lanuch(type.Name, obj);
        }
    }
}
else
{
    App.PerformanceTest();
}