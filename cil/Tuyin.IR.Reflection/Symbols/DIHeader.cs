using System;

namespace Tuyin.IR.Reflection.Symbols
{
    public class DIHeader
    {
        public DIHeader(DIToken name) 
        {
            Name = name;
        }

        public DIToken Name { get; }

        public DIModifiter Modifiter { get; private set; }

        public DIHeader SetModifiter(DIModifiter nt1_s)
        {
            Modifiter = nt1_s;
            return this;
        }
    }
}
