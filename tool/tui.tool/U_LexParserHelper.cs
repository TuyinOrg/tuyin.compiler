using LLParserLexerLib;

partial class RegexprParser : ParserBase
{
	public const int DIRECTIVE = 1000;

	public RegexprParser()
	{
	}

	public IAST start(NFA net, LexReader lrd)
	{
		base.init(net, lrd);
		var r = LexParser(null);
		return r;
	}

	private RegRoot CreateAny()
	{
		return new RegTokenOutsideRange('\n', '\n');
	}
	private RegRoot CreateString(TokenAST ast)
	{
		string v = ast.strRead.Substring(1);
		v = v.Remove(v.Length - 1);
		return RegRoot.R(v);
	}
	private IAST CreateRange(TokenAST a, TokenAST b)
	{
		int pa = -1;
		int pb = -1;

		if (a.strRead.Length == 1) pa = (char)a.strRead[0];
		if (b.strRead.Length == 1) pb = (char)b.strRead[0];

		return new RegTokenRange(pa, pb);
	}
	private RegToken CreateToken(TokenAST ast)
	{
		if (ast.strRead.Length == 1)
			return new RegToken(ast.strRead[0]);
		else
		{
			string r = ast.strRead;
			switch (r)
			{
			case @"\n": return new RegToken('\n');
			case @"\r": return new RegToken('\r');
			case @"\t": return new RegToken('\t');
			case @"\a_i": return new RegToken('\a');
			default:
				return new RegToken(r[1]);

			}
		}
	}

	public List<U.Tuple<RegRoot, string>> LexerActions = new List<U.Tuple<RegRoot, string>>();

	private void AddRole(RegRoot r, string id)
	{
		LexerActions.Add(U.Tuple<RegRoot, string>.Create(r, id));
	}
}

/*

public abstract class ParserBase
{
	LexReader _rd;
	NFA _nfa;
	TokenAST _next;

	protected abstract RegAcceptList CreateRegAcceptList();

	protected ParserBase(int state)
	{
		this._nfa = new NFA();
		var acts = CreateRegAcceptList();
		this._nfa.Add(state, acts);
	}
	protected ParserBase()
	{
		this._nfa = null;
		this._rd = null;
	}

	protected virtual TokenAST Next
	{
		get
		{
			if (_next == null)
			{
				var t = _nfa.ReadToken(_rd);
				_next = new TokenAST(t.fileName, t.line, t.token, t.value);
			}
			return _next;
		}
	}
	protected virtual TokenAST Match(int ch, AST v)
	{
		if (Next.ch != ch)
			throw new SyntaxError(_next.fileName, _next.lineNu, "{0}({1}): expected char '{2}' got ", (char)ch, Next.v);

		var ret = _next;
		_next = null;
		return ret;
	}

	public void init(NFA nfa, LexReader rd)
	{
		this._nfa = nfa;
		this._rd = rd;
	}

}


public class SyntaxError : Exception
{
	public SyntaxError(string fn, int line, string fmt, params object[] args)
		: base(U.F("{0}({1}): {2}", fn, line, U.F(fmt, args)))
	{
	}
}

public abstract class AST
{
}

public class TokenAST : AST
{
	public TokenAST(string fileName, int lineNu, int ch, string v) { this.fileName= fileName; this.lineNu = lineNu; this.ch = ch; this.v = v; }

	public readonly int ch;
	public readonly string v;
	public readonly string fileName;
	public readonly int lineNu;

	public override string ToString()
	{
		return U.F("{0}({1}): {2} - \"{3}\"", fileName, lineNu, ch, v);
	}

	public string Header
	{
		get { return U.F("{0}({1}): ", fileName, lineNu); }
	}
}

*/
