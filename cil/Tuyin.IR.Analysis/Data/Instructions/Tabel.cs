using System;
using System.Collections.Generic;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis.Data.Instructions
{
    internal class Tabel : Statment
    {
        public override AstNodeType NodeType => throw new NotImplementedException();

        public override IEnumerable<AstNode> GetNodes()
        {
            throw new NotImplementedException();
        }

        public class Row 
        {

        }

        public class Column 
        {

        }
    }
}
