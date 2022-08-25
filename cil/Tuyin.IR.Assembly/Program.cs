using Tuyin.IR.Analysis;
using Tuyin.IR.Reflection;
using Env = Tuyin.IR.Analysis.Environment;

string cmdLine = "?";

Console.WriteLine($"Tuyin Assembly 1.0 you can input '?' to query all commands.");

#if DEBUG
Solve(@"E:\bigbuns\cil\test\member.trv -o E:\bigbuns\cil\test\member.wasm");
#else
while (!string.IsNullOrEmpty(cmdLine = Console.ReadLine()) && Solve(cmdLine));
#endif

bool Solve(string cmdLine) 
{
    var cmd = new CmdLineHelper(cmdLine);
    if (cmd.WasArgSupplied("-e", "-exit"))
        return false;

    if (cmd.Args.Count > 0 && cmd.Args[0].Value == "?")
        ShowHelper();
    else
        Pack(cmd.GetArgValue("-i", "-input") ?? (cmd.Args.Count > 0 ? (cmd.Args[0]?.Value ?? String.Empty) : String.Empty),
            cmd.GetArgValue("-o", "-output"));

    return true;
}

void Pack(string input, string output) 
{
    // 读取obj文件
    if (!File.Exists(input))
        throw new IOException($"未能查找到输入文件，路径'{input}'一个或多个地址填写错误。");

    // 初始化环境设置
    var settings = new EnvironmentSettings();

    // 加载入口文件
    var module = Module.Load(input);

    // 生成程序环境
    var env = Env.Create(settings);
    env.AddModule(module);

    // 加载微码并转码成对应执行文件
    var atoms = env.GenerateMicrocodes();

    // 打包程序集

}

void ShowHelper() 
{
}