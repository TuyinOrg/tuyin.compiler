using Tuyin.IR.Compiler.Parser.Productions;

namespace Tuyin.IR.Compiler.Parser
{
    static class Grammer
    {
        public static ProductionBase AsTerminal(this Token token)
        {
            return new Terminal(token);
        }

        public static ProductionBase Many(this Token token)
        {
            return Many(token, default(ProductionBase));
        }

        public static ProductionBase Many(this ProductionBase production)
        {
            return Many(production, default(ProductionBase));
        }

        public static ProductionBase Many(this Token token, Token seperator)
        {
            return Many(token, seperator?.AsTerminal());
        }

        public static ProductionBase Many(this Token token, ProductionBase separator)
        {
            return token.AsTerminal().Many(separator);
        }

        public static ProductionBase Many(this ProductionBase production, Token seperator)
        {
            return Many(production, seperator?.AsTerminal());
        }

        public static ProductionBase Many(this ProductionBase production, ProductionBase separator)
        {
            return new RepeatProduction(production, separator);
        }

        public static ProductionBase Many1(this Token token)
        {
            return Many1(token.AsTerminal(), default(ProductionBase));
        }

        public static ProductionBase Many1(this Token token, Token seperator)
        {
            return Many1(token.AsTerminal(), seperator?.AsTerminal());
        }

        public static ProductionBase Many1(this Token token, ProductionBase separator)
        {
            return Many1(token.AsTerminal(), separator);
        }

        public static ProductionBase Many1(this ProductionBase production)
        {
            return Many1(production, default(ProductionBase));
        }

        public static ProductionBase Many1(this ProductionBase production, Token seperator)
        {
            return Many1(production, seperator?.AsTerminal());
        }

        public static ProductionBase Many1(this ProductionBase production, ProductionBase separator)
        {
            if (separator != null)
                return production & production.PrefixedBy(separator).Many();
            else
                return production & production.Many();
        }

        public static ProductionBase Close<T>(this Token token, ProductionBase right)
        {
            return token.AsTerminal().Close(right);
        }

        public static ProductionBase Close(this ProductionBase left, ProductionBase right)
        {
            return new ConcatenationProduction(left, right);
        }

        public static ProductionBase Close(this ProductionBase left, Token right)
        {
            return new ConcatenationProduction(left, right.AsTerminal());
        }

        public static ProductionBase Optional(this Token token)
        {
            return token.AsTerminal().Optional();
        }

        public static ProductionBase Optional(this ProductionBase production)
        {
            if (production.ProductionType == ProductionType.Empty)
                return production;

            return new EmptyProduction() | production;
        }

        public static ProductionBase PrefixedBy(this Token token, ProductionBase prefix)
        {
            return PrefixedBy(token.AsTerminal(), prefix);
        }

        public static ProductionBase PrefixedBy(this ProductionBase production, ProductionBase prefix)
        {
            return new ConcatenationProduction(prefix, production);
        }

    }
}
