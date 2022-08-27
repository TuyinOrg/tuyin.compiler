using System.Collections.Generic;
using System.Linq;
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
    }
}
