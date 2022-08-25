using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tuyin.IR.Reflection.IO;
using Tuyin.IR.Reflection.Symbols;
using String = Tuyin.IR.Reflection.Instructions.String;

namespace Tuyin.IR.Reflection
{
    /// <summary>
    /// tuyin ir 模块
    /// </summary>
    public sealed class Module
    {
        private List<Import> mImports;
        private List<DIMetadata> mAttributes;

        /// <summary>
        /// 模块签名
        /// </summary>
        public ISignature Signature { get; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public String[] Namespace { get; }

        /// <summary>
        /// 导入
        /// </summary>
        public IReadOnlyList<Import> Imports                                    
        {
            get { return mImports; }
        }

        /// <summary>
        /// 函数
        /// </summary>
        public IReadOnlyList<Function> Functions                                
        { 
            get;
        }

        /// <summary>
        /// 属性组
        /// </summary>
        public IReadOnlyList<DIMetadata> Metadatas                              
        {
            get { return mAttributes; }
        }

        /// <summary>
        /// 创建一个新模块
        /// </summary>
        /// <param name="funs">模块中包含的函数</param>
        public Module(string fileName, String[] @namespace, IEnumerable<Function> funs)                               
        {
            Namespace = @namespace;
            Functions = funs.ToArray();
            mImports = new List<Import>();
            mAttributes = new List<DIMetadata>();
            Signature = new ModuleSignature(fileName);
        }

        public Module(ISignature signature, String[] @namespace, List<Import> imports, List<DIMetadata> attributes, IReadOnlyList<Function> functions)
        {
            mImports = imports;
            mAttributes = attributes;
            Signature = signature;
            Namespace = @namespace;
            Functions = functions;
        }

        /// <summary>
        /// 在该模块内添加导入
        /// </summary>
        public Module Import(IEnumerable<Import> imports)                       
        {
            mImports.AddRange(imports);
            return this;
        }

        /// <summary>
        /// 在该模块内添加属性
        /// </summary>
        public Module Attribute(IEnumerable<DIMetadata> attributes)              
        {
            mAttributes.AddRange(attributes);
            return this;
        }

        /// <summary>
        /// 保存模块到指定路径
        /// </summary>
        public void Save(DIMetadataManager metadata, string fileName, ModuleTarget target)                                       
        {
            Stream stream = new FileStream(fileName, FileMode.Create);

            // 保存模块数据
            ModuleWriter writer = target switch
            {
                ModuleTarget.Binary => new BinaryModuleWriter(metadata, stream),
                ModuleTarget.Text => new TextModuleWriter(metadata, stream),
                _ => throw new NotImplementedException()
            };

            writer.WriteNamespace(Namespace);
            writer.WriteSignature(Signature);
            for (var i = 0; i < Imports.Count; i++)
                writer.WriteImport(Imports[i].Path, Imports[i].Identifier);

            for (var i = 0; i < Functions.Count; i++)
                writer.WriteFunction(Functions[i]);

            for (var i = 0; i < Metadatas.Count; i++)
                writer.WriteMetadata(Metadatas[i]);

            stream.Flush();
            stream.Close();
        }

        /// <summary>
        /// 从指定文件加载模块
        /// </summary>
        public static Module Load(string fileName)                              
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
    }
}
