using System.Diagnostics;

namespace Tuyin.IR.Compiler.Parser
{
    struct Feature
    {
        public bool Break                                                               
        {
            get;
        }

        public bool Stack                                                               
        {
            get;
        }

        public byte Priority                                                            
        {
            get;
        }

        public bool IsTransitive                                                        
        {
            get 
            {
                return Priority != 0;
            }
        }

        public bool IsDefault                                                           
        {
            get
            {
                return !IsTransitive && !Stack;
            }
        }

        public Feature(bool @break, bool @stack, byte priority)   
        {
            Break = @break;
            Stack = @stack;
            Priority = priority;
        }

        public Feature Combine(Feature right)                                           
        {
            System.Diagnostics.Debug.Assert(!(right.Priority != 0 && Priority != 0), "无法确定标点符号");

            return new Feature(
                Break || right.Break,
                Stack || right.Stack,
                Priority);
        }
    }
}
