using System.Collections.Generic;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis
{
    class ModuleTree : Dictionary<string, ModuleTree>
    {
        private Dictionary<ISignature, Module> mModules;

        internal ModuleTree() 
        {
            mModules = new Dictionary<ISignature, Module>();
        }

        public IEnumerable<Module> Models => mModules.Values;

        public bool Contains(Module module) 
        {
            var curr = this;
            for (var i = 0; i < module.Namespace.Length; i++)
            {
                var part = module.Namespace[i];
                if (!curr.ContainsKey(part.Value))
                    curr.Add(part.Value, curr = new ModuleTree());
            }

            return curr.mModules.ContainsKey(module.Signature);
        }

        public void Remove(Module module) 
        {
            var curr = this;
            for (var i = 0; i < module.Namespace.Length; i++)
            {
                var part = module.Namespace[i];
                if (!curr.ContainsKey(part.Value))
                    curr.Add(part.Value, curr = new ModuleTree());
            }

            curr.mModules.Remove(module.Signature);
        }

        public void Add(Module module) 
        {
            var curr = this;
            for (var i = 0; i < module.Namespace.Length; i++) 
            {
                var part = module.Namespace[i];
                if (!curr.ContainsKey(part.Value))
                    curr.Add(part.Value, curr = new ModuleTree());
            }

            curr.mModules.Add(module.Signature, module);
        }

        public IEnumerable<Module> GetAllModules() 
        {
            foreach (var module in mModules)
                yield return module.Value;

            foreach (var sub in this)
                foreach (var module in sub.Value.GetAllModules())
                    yield return module;
        }
    }
}
