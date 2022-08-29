using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Analysis.IO;
using Tuyin.IR.Analysis.Passes;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using Tuyin.IR.Reflection.Symbols;
using String = Tuyin.IR.Reflection.Instructions.String;

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
        /// 保存模块到指定路径
        /// </summary>
        public void SaveModule(Module module, DIMetadataManager metadata, IEnumerable<ComputeUnit> computeUnits, string fileName, ModuleTarget target)
        {
            Stream stream = new FileStream(fileName, FileMode.Create);

            // 保存模块数据
            ModuleWriter writer = target switch
            {
                ModuleTarget.Binary => new BinaryModuleWriter(metadata, stream),
                ModuleTarget.Text => new TextModuleWriter(metadata, stream),
                _ => throw new NotImplementedException()
            };

            writer.WriteNamespace(module.Namespace);
            writer.WriteSignature(module.Signature);

            for (var i = 0; i < module.Imports.Count; i++)
                writer.WriteImport(module.Imports[i].Path, module.Imports[i].Identifier);

            for (var i = 0; i < module.Metadatas.Count; i++)
                writer.WriteMetadata(module.Metadatas[i]);

            foreach (var unit in computeUnits)
                writer.WriteFunction(unit.Function, unit.DAG);

            writer.Flush();
            stream.Flush();
            stream.Close();
        }

        /// <summary>
        /// 快速读取模块namespace
        /// </summary>
        public static String[] ReadNamespace(string fileName)
        {
            return GetReader(File.OpenRead(fileName)).ReadNamespace();
        }

        private static ModuleReader GetReader(Stream stream)
        {
            const string HEADER = "string";

            var isText = true;
            var br = new BinaryReader(stream);
            for (var i = 0; i < HEADER.Length; i++)
            {
                if (HEADER[i] != br.ReadChar())
                {
                    isText = false;
                    br.BaseStream.Seek(0, SeekOrigin.Begin);
                }
            }

            return isText ?
                new TextModuleReader(stream) :
                new BinaryModuleReader(stream);
        }

        /// <summary>
        /// 向环境中添加模块
        /// </summary>
        public void AddModule(Module module) 
        {
            // 为函数构建控制流
            var index = mComputeUnits.Count;
            for (int i = 0; i < module.Functions.Count; i++) 
            {
                var func = module.Functions[i];
                var stmts = func.Statments;
                var ssa = new SSAAnalysis().Run(new SSAAnalysisOpation(new BranchAnalysis().Run(stmts), stmts, Settings));
                var cfg = new CFGAnalysis().Run(new CFGAnalysisOpation(new BranchAnalysis().Run(ssa), ssa, Settings));
                var dag = new DAGAnalysis().Run(new DAGAnalysisOpation(cfg, Settings));
                var vet = new VectorAnalysis().Run(new VectorAnalysisOpation(dag, cfg));

                mComputeUnits.Add(new ComputeUnit((ushort)i, func, cfg, dag, vet));
            }

            var dir = Path.GetDirectoryName(module.Signature.Content);
            var name = Path.GetFileNameWithoutExtension(module.Signature.Content) + ".obj";
            var obj = Path.Combine(dir, name);
            var metadatas = new DIMetadataManager();
            SaveModule(module, metadatas, mComputeUnits.GetRange(index, mComputeUnits.Count - index), obj, ModuleTarget.Binary);

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
            var module = LoadModuleInternal(path);
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
                    var @namespace = ReadNamespace(file);
                    var fullPath = string.Join(".", @namespace.Select(x => x.Value));
                    if (imports.Contains(fullPath))
                        LoadModule(file);
                }

                AddModule(module);
            }

            return module;
        }

        /// <summary>
        /// 从指定文件加载模块
        /// </summary>
        private static Module LoadModuleInternal(string fileName)
        {
            var reader = GetReader(File.OpenRead(fileName));
            var namesapce = reader.ReadNamespace();
            var signature = reader.ReadSignature();

            Import import = null;
            Function func = null;
            DIMetadata metadata = null;

            var imports = new List<Import>();
            while ((import = reader.ReadImport()) != null)
                imports.Add(import);

            var funcs = new List<Function>();
            while ((func = reader.ReadFunction()) != null)
                funcs.Add(func);

            var metadatas = new List<DIMetadata>();
            while ((metadata = reader.ReadMetadata()) != null)
                metadatas.Add(metadata);

            return new Module(signature, namesapce, imports, metadatas, funcs);
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
