using System;
using System.Collections.Generic;

namespace libfsm
{
    public struct FASymbol
    {
        public FASymbolType Type { get; }

        public ushort Value { get; }
       
        public FASymbol(FASymbolType type, ushort symbol) 
        {
            Type = type;
            Value = symbol;
        }

        public override bool Equals(object obj)
        {
            return obj is FASymbol symbol &&
                   Type == symbol.Type &&
                   Value == symbol.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }

        public static FASymbol Combine(params FASymbol[] symbols) 
        {
            return Combine(symbols);
        }

        public static FASymbol Combine(IEnumerable<FASymbol> symbols)
        {
            bool setValue = false;
            ushort value = 0;
            FASymbolType type = FASymbolType.None;
            foreach (var symbol in symbols)
            {
                if(type != FASymbolType.Action && type != FASymbolType.None && symbol.Type == FASymbolType.Action)
                    throw new Exception("Action symbol cannot be merged with other types of symbols.");

                type = type | symbol.Type;

                if (symbol.Value != 0)
                {
                    if (!setValue)
                    {

                        value = symbol.Value;
                        setValue = true;
                    }
                    else
                    {
                        if (value != symbol.Value)
                        {
                            throw new Exception("Can not combine diffect value symbols.");
                        }
                    }
                }
            }

            return new FASymbol(type, value);
        }
    }
}
