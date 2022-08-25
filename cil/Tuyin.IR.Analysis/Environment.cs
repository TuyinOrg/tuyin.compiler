using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Analysis.Passes;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis
{
    public partial class Environment : AnalysisGraphBase<ObjectFile>
    {
        private const string DLL_EXTENSION = "tb";
        private List<ComputeUnit> mComputeUnits;
        private ModuleTree mModuleTree;

        /// <summary>
        /// 获取环境设置
        /// </summary>
        public EnvironmentSettings Settings 
        { 
            get; 
        }

        /// <summary>
        /// 获取创建的计算单元
        /// </summary>
        public IReadOnlyList<ComputeUnit> ComputeUnits => mComputeUnits;

        public override IReadOnlyList<AnalysisEdge> Edges { get; }

        public override IReadOnlyList<ObjectFile> Vertices { get; }

        /// <summary>
        /// 内部构造
        /// </summary>
        internal Environment(EnvironmentSettings settings) 
        {
            Settings = settings;
            mModuleTree = new ModuleTree();
            mComputeUnits = new List<ComputeUnit>();
        }

        /// <summary>
        /// 生产微码
        /// </summary>
        public IReadOnlyArray<Microcode> GenerateMicrocodes() 
        {
            foreach (var unit in mComputeUnits) 
            {

            }

            return null;
        }

        /// <summary>
        /// 向环境中添加模块
        /// </summary>
        public void AddModule(Module module) 
        {
            // 为函数构建控制流
            for (int i = 0; i < module.Functions.Count; i++) 
            {
                var func = module.Functions[i];
                var stmts = func.Statments;
                var ssa = new SSAAnalysis().Run(new SSAAnalysisOpation(stmts));
                var cfg = new CFGAnalysis().Run(new CFGAnalysisOpation(ssa));
                var dag = new DAGAnalysis().Run(new DAGAnalysisOpation(cfg));
                var vet = new VectorAnalysis().Run(new VectorAnalysisOpation(dag, cfg));

                mComputeUnits.Add(new ComputeUnit((ushort)i, func.Identifier.Value, cfg, dag, vet));
            }

            // 整理引用层级
            mModuleTree.Add(module);
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        public Module LoadModule(string path) 
        {
            return LoadModule(path, new HashSet<string>());
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        private Module LoadModule(string path, HashSet<string> loop) 
        {
            var module = Module.Load(path);
            if (!mModuleTree.Contains(module))
            {
                // 查找gac和同级目录中的imports
                var imports = new HashSet<string>();
                for (int i = 0; i < module.Imports.Count; i++)
                {
                    var fullPath = module.Imports[i].GetFullPath();
                    if (!loop.Contains(fullPath))
                    {
                        imports.Add(fullPath);
                    }
                }

                foreach (var item in imports) loop.Add(item);
                foreach (var file in Directory.GetFiles(Path.GetDirectoryName(path), $"*.{DLL_EXTENSION}"))
                {
                    var @namespace = Module.ReadNamespace(file);
                    var fullPath = string.Join(".", @namespace.Select(x => x.Value));
                    if (imports.Contains(fullPath))
                        LoadModule(file);
                }

                AddModule(module);
            }

            return module;
        }

        /// <summary>
        /// 创建程序环境
        /// </summary>
        public static Environment Create(IEnumerable<Module> modules) 
        {
            return Create(modules, EnvironmentSettings.Default);
        }

        /// <summary>
        /// 创建程序环境
        /// </summary>
        public static Environment Create(params Module[] modules) 
        {
            return Create(modules, EnvironmentSettings.Default);
        }

        /// <summary>
        /// 创建程序环境
        /// </summary>
        public static Environment Create(EnvironmentSettings settings) 
        {
            return Create(new Module[0], settings);
        }

        /// <summary>
        /// 创建程序环境
        /// </summary>
        public static Environment Create(IEnumerable<Module> modules, EnvironmentSettings settings) 
        {
            var env = new Environment(settings);
            foreach (var module in modules)
                env.AddModule(module);

            return env;
        }
    }
}
