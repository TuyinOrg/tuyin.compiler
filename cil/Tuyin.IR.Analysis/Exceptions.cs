using System;
using Tuyin.IR.Analysis.Utils;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis
{
    public class IRException : Exception
    {
        public Errors Error { get; }

        public ISourceSpan SourceSpan { get; }

        public IRException(Errors error, ISourceSpan sourceSpan, params object[] args)
            : base(string.Format(error.GetHelperDescrption(), args))
        {
            Error = error;
            SourceSpan = sourceSpan;
        }
    }
}
