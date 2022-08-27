using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Tuyin.IR.Analysis;
using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Target;
using Tuyin.IR.Compiler.Uitls;
using Path = System.IO.Path;

[DllImport("shell32.dll")]
static extern int FindExecutable(string lpFile, string lpDirectory, [Out] StringBuilder lpResult);

static void OpenImage(string imagePath)
{
    var exePathReturnValue = new StringBuilder();
    FindExecutable(Path.GetFileName(imagePath), Path.GetDirectoryName(imagePath), exePathReturnValue);
    var exePath = exePathReturnValue.ToString();
    var arguments = "\"" + imagePath + "\"";

    // Handle cases where the default application is photoviewer.dll.
    if (Path.GetFileName(exePath).Equals("photoviewer.dll", StringComparison.InvariantCultureIgnoreCase))
    {
        arguments = "\"" + exePath + "\", ImageView_Fullscreen " + imagePath;
        exePath = "rundll32";
    }

    var process = new Process();
    process.StartInfo.FileName = exePath;
    process.StartInfo.Arguments = arguments;

    process.Start();
}

string CreateParser(string fileName) 
{
    // 转换为dfa
    var file = Target.Parse(fileName);
    var parser = new FileParser(file);
    var table = parser.CreateGraph().Tabulate();

    // 储存parser状态位

    // 翻译目标基础parser
    using var tuyinStream = new StreamReader(typeof(Program).Assembly.GetManifestResourceStream($"Tuyin.IR.Compiler.Target.Templates.ParserTemplate.txt"));
    var tuyinTemplate = tuyinStream.ReadToEnd();
    var fr = new FastReplacer("{", "}");
    fr.Append(tuyinTemplate);
    fr.Replace("{PARSER_TABLE}", "");
    fr.Replace("{PARSER_SIMD}", "");
    fr.Replace("{SIMD_READER}", "");
    fr.Replace("{STATE_ACTIONS}", "");
    // 返回结果
    return fr.ToString();
}

void CreateDotGraph<TVertex>(IAnalysisGraph<TVertex> graph, string fileName)
    where TVertex : IAnalysisNode<AnalysisEdge>
{
    var workroot = Path.GetDirectoryName(fileName);
    var output = $"{Path.GetFileNameWithoutExtension(fileName)}.png";

    graph.SaveToFile(fileName);

    Process create = new Process();
    create.StartInfo.FileName = "dot";
    create.StartInfo.Arguments = $"-Tpng {System.IO.Path.GetFileName(fileName)} -o {output}";
    create.StartInfo.WorkingDirectory = workroot;
    create.Start();
    create.WaitForExit();

    OpenImage($"{workroot}\\{output}");
}

const bool output = false;

if (output)
{
    foreach (var file in Directory.EnumerateFiles(@"E:\bigbuns\cil\test", "*.txt"))
    {
        var tb = file + ".tb";
        var ib = file + ".png";
        var module = Target.Parse(file);
        var env = Tuyin.IR.Analysis.Environment.Create(module.ToIR());
    }
}
else 
{
    var file = @"E:\bigbuns\cil\test\invaild.txt";
    var name = $"{Path.GetDirectoryName(file)}\\{Path.GetFileNameWithoutExtension(file)}";
    var module = Target.Parse(file);
    var env = Tuyin.IR.Analysis.Environment.Create(module.ToIR());

    CreateDotGraph(env.ComputeUnits[0].CFG, name + ".cfg.gv");
    CreateDotGraph(env.ComputeUnits[0].DAG, name + ".dag.gv");
}

/*
var p = new Recursion();
var l = new List<DebugGraph>();
l.AddRange(DebugHelper.FromGraphBox(p.Graph));

try
{
    var table = p.Graph.Tabulate();
    table.Build();
    l.Insert(0, table.CreateDebugGraph("DFA"));
}
catch (ConflictException<ushort> ex)
{
    l.Insert(0, DebugHelper.FromTransitions("冲突", p.Graph, ex.Transitions, ex.Actions));
}

DebugHelper.SaveDebugGraphs(@"D:\Desktop\dfa.tb", l.ToArray());
Process.Start(@"E:\bigbuns\tool\BigBuns.Graph.Viewer\bin\Debug\net6.0-windows\BigBuns.Graph.Viewer.exe", @"D:\Desktop\dfa.tb");
*/