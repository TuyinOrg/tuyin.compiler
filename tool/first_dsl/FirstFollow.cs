using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;


using LLParserLexerLib;

class ParserOptions
{
	public string Class;
	public string Namespace;
	public List<string> Using;
	public bool GenerateLexerFunctions;

	public ParserOptions()
	{
		Class = "Parser";
		Namespace = null;
		Using = new List<string>();
		GenerateLexerFunctions = true;
	}
}


class GrammarReader
{
	const int NODE = -100;
	const int CHAR = -101;
	const int RESOLVER = -102;
	const int TYPE = -103;
	const int CODE = -104;
	const int ID = -105;
	const int DIRECTIVE = -106;

	RegexprParser _regExprParser;

	public Grammar G { get; set; }
	public List<U.Tuple<RegRoot, string>> LexerActions { get; set; }

	public GrammarReader()
	{
		G = new Grammar();
		_regExprParser = new RegexprParser();
		LexerActions = new List<U.Tuple<RegRoot, string>>();
	}

	public void Read(string fileName, TextReader tr, ParserOptions po)
	{
		NFA nfa = new NFA();

		CreateLexerForStateNone((int)State.None, nfa);
		CreateLexerForStateParser((int)State.Parser, nfa, po);
		CreateLexerForStateLexer((int)State.Lexer, nfa);

		using (var rd = new LexReader(tr, fileName))
		{
			_regExprParser = new RegexprParser();
			this.ReadParserAndLexer(nfa, rd);
		}
	}

	enum State
	{
		None,
		Lexer,
		Parser
	}

	private void ReadParserAndLexer(NFA net, LexReader rd)
	{
		net.State = (int)State.None;

		for (; ; )
		{
			switch ((State)net.State)
			{
			case State.None:
				for (; ; )
				{
					NFA.Token ret = net.ReadToken(rd);
					if (ret.token == -1)
						return;

					if (ret.token == RegexprParser.DIRECTIVE)
					{
						var args = ret.strRead.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
						switch (args[0])
						{
						case "%parser":
							net.State = (int)State.Parser;
							break;

						case "%lexer":
							net.State = (int)State.Lexer;
							break;

						default:
							throw new SyntaxError(ret.fileName, ret.line, "unknown directive");
						}

						if ((State)net.State != State.None)
							break;
					}
					else
						throw new SyntaxError(ret.fileName, ret.line, "syntax error");
				}
				break;

			case State.Lexer:
				if (true)
				{
					_regExprParser.start(net, rd);
					this.LexerActions = _regExprParser.LexerActions;
				}
				break;

			case State.Parser:
				{
					NFA.Token ret = net.ReadToken(rd);
					if (ret.token == -1)
						return;

					if (ret.token != ID)
						throw new SyntaxError(ret.fileName, ret.line, "expected identifier");

					string role = ret.strRead;
					string type = null;
					List<string> right = new List<string>();

					ret = net.ReadToken(rd);
					if (ret.token == TYPE)
						type = ret.strRead;
					else
						net.PushBack(ret);

					ret = net.ReadToken(rd);
					if (ret.token != ':')
						throw new SyntaxError(ret.fileName, ret.line, "expected ':'");

					int posResolver = -1;
					for (; ; )
					{
						ret = net.ReadToken(rd);
						if (ret.token == '|')
						{
							if (right.Count == 1 && (right[0].StartsWith("^") || right[0].StartsWith("{")))
								right.Insert(0, "");
							var a = G.Add(role, right.ToArray());
							if (posResolver >= 0)
								a.AddResolver(posResolver + 1, right[posResolver]);
							if (type != null)
								G[role].Type = type;
							posResolver = -1;
							right.Clear();
						}
						else if (ret.token == ';')
						{
							if (right.Count == 1 && (right[0].StartsWith("^") || right[0].StartsWith("{")))
								right.Insert(0, "");
							var a = G.Add(role, right.ToArray());
							if (posResolver >= 0)
								a.AddResolver(posResolver + 1, right[posResolver]);
							if (type != null)
								G[role].Type = type;
							posResolver = -1;
							break;
						}
						else if (ret.token == ID)
						{
							right.Add(ret.strRead);
						}
						else if (ret.token == CHAR)
						{
							right.Add(ret.strRead);
						}
						else if (ret.token == CODE)
						{
							right.Add(ret.strRead);
						}
						else if (ret.token == NODE)
						{
							right.Add(ret.strRead);
						}
						else if (ret.token == RESOLVER)
						{
							posResolver = right.Count;
						}
						else
							throw new SyntaxError(ret.fileName, ret.line, "unexpected token {0}", ret.token);
					}
				}
				break;
			}
		}

	}

	private static void CreateLexerForStateNone(int state, NFA net)
	{
		var rWhites = new RegOneOrMore(new RegToken(' ') | '\t' | '\n' | '\r');
		var rDirective = new RegStartLine(new RegToken('%'));

		var acts = new List<RegAccept>();

		acts.Add(new RegAccept(rWhites));
		acts.Add(new RegAccept(rDirective, (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == '\n') break;
				if (rd.Peek().ch == -1) break;
				rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			tk.token = RegexprParser.DIRECTIVE;
			return true;
		}));

		acts.Add(new RegAccept(new RegToken('/') & '/', (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == '\n') break;
				if (rd.Peek().ch == -1) break;
				rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			return false;
		}));

		acts.Add(new RegAccept(new RegToken('/') & '*', (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
				if (rd.Read().ch == '*' && rd.Peek().ch == '/')
				{
					rd.Read();
					break;
				}
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			return false;
		}));

		net.Add(state, acts);
	}
	private static void CreateLexerForStateParser(int state, NFA net, ParserOptions po)
	{
		var rNum = new RegTokenRange('0', '9');
		var rAlpha = new RegTokenRange('a', 'z') | new RegTokenRange('A', 'Z') | '_';
		var rAlphaNum = rNum | rAlpha;
		var rId = rAlpha & new RegZeroOrMore(rAlphaNum);
		var rChar = new RegToken('\'') & new RegTokenRange(32, 127) & new RegToken('\'');
		var rWhites = new RegOneOrMore(new RegToken(' ') | '\t' | '\n' | '\r');
		var rDirective = new RegStartLine(new RegToken('%'));


		var acts = new List<RegAccept>();
		acts.Add(new RegAccept(rId, ID));
		acts.Add(new RegAccept(rChar, CHAR));
		acts.Add(new RegAccept(new RegToken(':'), ':'));
		acts.Add(new RegAccept(new RegToken('|'), '|'));
		acts.Add(new RegAccept(new RegToken(';'), ';'));
		acts.Add(new RegAccept(new RegToken('*'), RESOLVER));

		acts.Add(new RegAccept(rWhites));
		acts.Add(new RegAccept(rDirective, (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == '\n') break;
				if (rd.Peek().ch == -1) break;
				rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			tk.token = DIRECTIVE;

			var opts = tk.strRead.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
			if (opts.Length > 1)
			{
				switch (opts[0])
				{
				case "%parser.class": po.Class = opts[1]; break;
				case "%parser.namespace": po.Namespace = opts[1]; break;
				case "%parser.using": po.Using.Add(opts[1]); break;
				default: throw new ApplicationException(U.F("{0}({1}): unknonw option", tk.fileName, tk.line));
				}
			}
			else
			{
				switch (opts[0])
				{
				default: throw new ApplicationException(U.F("{0}({1}): unknonw option", tk.fileName, tk.line));
				}
			}


			return false;
		}));


		acts.Add(new RegAccept(new RegToken('{'), (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			int ngr = 1;
			for (; ; )
			{
				if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
				else if (rd.Peek().ch == '{') { rd.Read(); ngr += 1; }
				else if (rd.Peek().ch == '}')
				{
					rd.Read();

					ngr -= 1;
					if (ngr == 0)
						break;
				}
				else if (rd.Peek().ch == -1)
					new Exception("EOF in code");
				else
					rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			tk.token = CODE;
			return true;
		}));
		acts.Add(new RegAccept(new RegToken('^'), (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == '\r') break;
				if (rd.Peek().ch == '\n') break;
				if (rd.Peek().ch == -1) break;
				rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			tk.token = NODE;
			return true;
		}));
		acts.Add(new RegAccept(new RegToken('<'), (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
				if (rd.Peek().ch == '>')
				{
					rd.Read();
					break;
				}
				else if (rd.Peek().ch == -1)
					new Exception("EOF in code");
				else
					rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			tk.token = TYPE;
			tk.strRead = tk.strRead.Substring(1, tk.strRead.Length - 2);
			return true;
		}));


		acts.Add(new RegAccept(new RegToken('/') & '/', (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == '\n') break;
				if (rd.Peek().ch == -1) break;
				rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			return false;
		}));
		acts.Add(new RegAccept(new RegToken('/') & '*', (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
				if (rd.Read().ch == '*' && rd.Peek().ch == '/')
				{
					rd.Read();
					break;
				}
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			return false;
		}));



		net.Add(state, acts);
	}

	private static void CreateLexerForStateLexer(int state, NFA net)
	{
		var rNum = new RegTokenRange('0', '9');
		var rAlpha = new RegTokenRange('a', 'z') | new RegTokenRange('A', 'Z') | '_';
		var rAlphaNum = rNum | rAlpha;
		var rId = rAlpha & new RegZeroOrMore(rAlphaNum);
		var rWhites = new RegOneOrMore(new RegToken(' ') | '\t' | '\n' | '\r');

		var rDirective = new RegStartLine(new RegToken('%'));

		var rChNoLitCh = rAlphaNum;

		var rChLit = RegRoot.R("\\") & new RegTokenOutsideRange('\n', '\n');

		var acts = new RegAcceptList();
		acts.Add(rWhites);

		acts.Add(rChNoLitCh, RegexprParser.CH_NOLIT);
		acts.Add(rChLit, RegexprParser.CH_LIT);

		acts.Add("^" & rId, RegexprParser.ID);
		acts.Add("^()", RegexprParser.ID);
		acts.Add(RegRoot.R("^(") & new RegTokenOutsideRange('\n', '\n') & ")", RegexprParser.ID);
		acts.Add("*", '*');
		acts.Add("+", '+');
		acts.Add(".", '.');
		acts.Add("-", '-');
		acts.Add("?", '?');
		acts.Add("|", '|');
		acts.Add("[", '[');
		acts.Add("]", ']');
		acts.Add("(", '(');
		acts.Add(")", ')');
		acts.Add("^", '^');
		acts.Add("$", '$');
		acts.Add("{", (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			int ngr = 1;
			for (; ; )
			{
				if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
				else if (rd.Peek().ch == '{') { rd.Read(); ngr += 1; }
				else if (rd.Peek().ch == '}')
				{
					rd.Read();

					ngr -= 1;
					if (ngr == 0)
						break;
				}
				else if (rd.Peek().ch == -1)
					new Exception("EOF in code");
				else
					rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			tk.token = RegexprParser.CODE;
			return true;
		});
		acts.Add("\"", (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\"");
			for (; ; )
			{
				if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
				if (rd.Peek().ch == '\n' || rd.Peek().ch == '\r') throw new Exception("return in string");
				if (rd.Peek().ch == '"')
				{
					int ch = rd.Read().ch;
					sb.Append((char)ch);
					break;
				}
				else if (rd.Peek().ch == '\\') 
				{
					rd.Read();
					if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
					if (rd.Peek().ch == '\n' || rd.Peek().ch == '\r') throw new Exception("return in string");
					if (!(rd.Peek().ch == '"' || rd.Peek().ch == '\\'))
						throw new Exception("invalid character after \\");
					int ch = rd.Read().ch;
					sb.Append((char)ch);
				}
				else
				{
					int ch = rd.Read().ch;
					sb.Append((char)ch);
				}
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			tk.strRead = sb.ToString();
			tk.token = RegexprParser.STRING;
			return true;
		});

		acts.Add(rDirective, (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("%");
			for (; ; )
			{
				if (rd.Peek().ch == '\n') break;
				if (rd.Peek().ch == -1) break;
				sb.Append((char)rd.Read().ch);
			}

			var args = sb.ToString().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
			if (args[0].StartsWith("%parser"))
			{
				tk.token = -1;  // lo faccio uscire dal parser delle espressioni regolari.
				nfa.State = 0;  // continuera` a_i leggere come se fosse in stato None.
				// non consumo il token in modo che nelle prossime letture il tutto ri-inizi con %parser....
				rd.ForgetAll();
				return true;
			}


			// consumo qui la direttiva.
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);

			if (args[0].StartsWith("%lexer") == false)
				throw new ApplicationException(U.F("{0}({1}): unknonw directive", tk.fileName, tk.line));

			// leggo la direttiva....

			return false;
		});

		acts.Add("//", (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == '\n') break;
				if (rd.Peek().ch == -1) break;
				rd.Read();
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			return false;
		});

		acts.Add("/*", (ref NFA.Token tk, LexReader rd, NFA nfa) =>
		{
			for (; ; )
			{
				if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
				if (rd.Read().ch == '*' && rd.Peek().ch == '/')
				{
					rd.Read();
					break;
				}
			}
			rd.SetMatch();
			rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
			return false;
		});

		net.Add(state, acts);
	}
}


class Grammar
{
	private readonly List<Production> _production;
	private Dictionary<string, TerminalSet> _first;
	private Dictionary<string, TerminalSet> _follow;
	private int _tmp;

	public static String ReplaceWorld(String s, String from, String to)
	{
		int i = s.IndexOf(from);
		if (i < 0)
			return s;

		i += from.Length;
		if (i == s.Length)
			return s.Replace(from, to);

		char c = s[i];
		if ((c >= '0' && c <= '9') || 
				(c >= 'a' && c <= 'z') || 
				(c >= 'A' && c <= 'Z') || 
				c == '_')
			return s;

		return s.Replace(from, to);
	}

	public Grammar()
		: this(1)
	{
	}

	public Grammar(int t)
	{
		_tmp = t;
		_production = new List<Production>();
	}

	public override string ToString()
	{
		var s = new StringBuilder();
		foreach (Production p in _production)
			s.AppendFormat("{0}\n", p.ToString());
		return s.ToString();

	}

	public Alternative Add(string symbol, params string[] alt)
	{
		Production prod = this[symbol];
		Alternative a = prod.CreateAlternative();
		if (alt.Length == 0)
		{
			a.Add(new Empty(this));
		}
		else
		{
			foreach (string v in alt)
			{
				if (v == "ε" || v == "" || v == "<e>")
					a.Add(new Empty(this));
				else if (v[0] >= 'a' && v[0] <= 'z')
					a.Add(new NonTerminal(this, v));
				else if (v.StartsWith("{") && v.EndsWith("}"))
					a.Add(new Action(this, v));
				else if (v.StartsWith("^"))
					a.Add(new Action(this, v));
				else if (v[0] >= 'A' && v[0] <= 'Z')
					a.Add(new Token(this, v, false));
				else
					a.Add(new Token(this, v, true));
			}
		}
		return a;
	}

	public Production this[string symbol]
	{
		get
		{
			Production p = _production.Find(w => w.Symbol == symbol);
			if (p != null) return p;
			p = new Production(this, symbol, "IAST");
			_production.Add(p);
			return p;
		}
	}

	private Dictionary<string, TerminalSet> ComputeFirst()
	{
		Dictionary<string, TerminalSet> first = new Dictionary<string, TerminalSet>();

		// per ogni simbolo non terminal (ossia per ogni produzione) creo in first set
		foreach (var production in _production)
			first.Add(production.Symbol, new TerminalSet());

		// i simboli terminali hanno tutti First(a_i) = {a_i}
		foreach (var production in _production)
			foreach (Alternative alt in production.Alt)
				foreach (GrammarSymbol gs in alt.ListWithoutAction)
					if (gs is Terminal)
					{
						if (first.ContainsKey(gs.ToString()) == false)
						{
							first.Add(gs.ToString(), new TerminalSet());
							first[gs.ToString()].Add((Terminal)gs);
						}
					}

		// ora mi dedico alle produzioni

		bool changed = false;
		do
		{
			changed = false;

			foreach (var production in _production)
			{
				foreach (Alternative alt in production.Alt)
				{
					for (var gsi = 0; gsi < alt.ListWithoutAction.Count; gsi++)
					{
						GrammarSymbol gs = alt.ListWithoutAction[gsi];

						if (gs is Empty)
						{
							changed |= first[production.Symbol].Add((Empty)gs);
							Debug.Assert(alt.ListWithoutAction.Count == 1);
							break;  // empty e' l'ultimo e l'unico
						}
						else if (gs is Token)
						{
							changed |= first[production.Symbol].Add((Token)gs);
							break;
						}
						else if (gs is NonTerminal)
						{
							NonTerminal nt = (NonTerminal)gs;
							var first_gs = first[nt.Symbol];

							if (gsi == alt.ListWithoutAction.Count - 1) // se sono all'ultimo aggiungo eventualmente anche empty
								changed |= first[production.Symbol].Add(first_gs);
							else
								changed |= first[production.Symbol].AddExceptEmpty(first_gs);

							if (first_gs.ContainsEmpty() == false)
								break;
						}
						else
							Debug.Assert(false);
					}
				}
			}
		}
		while (changed);

		return first;
	}
	private Dictionary<string, TerminalSet> ComputeFollow(Dictionary<string, TerminalSet> first)
	{
		Dictionary<string, TerminalSet> follow = new Dictionary<string, TerminalSet>();
		foreach (var production in _production)
			follow[production.Symbol] = new TerminalSet();

		// regola 1
		follow[_production[0].Symbol].Add(new Eof(this));

		bool changed;
		do
		{
			changed = false;

			// regola 2
			foreach (var A in _production)
			{
				foreach (Alternative alt in A.Alt)
				{
					// se ho una  X : empty continuo
					if (alt.ListWithoutAction.Count == 1 && alt.ListWithoutAction[0] is Empty) continue;

					// se ho almeno 2 simboli, vado a_i ritroso.
					if (alt.ListWithoutAction.Count >= 2)
					{
						GrammarSymbol beta = alt.ListWithoutAction[alt.ListWithoutAction.Count - 1];
						TerminalSet betaSet = first[beta.ToString()].Clone();

						// per tutti i terminali/non terminali precedenti a_i quello corrente
						for (int i = alt.ListWithoutAction.Count - 2; i >= 0; --i)
						{
							var B = alt.ListWithoutAction[i];
							if (B is NonTerminal)
							{
								changed |= follow[B.ToString()].AddExceptEmpty(betaSet);
							}

							// calcolo il firstset del grammar symbol corrente.
							if (first[B.ToString()].ContainsEmpty() == true)
								betaSet.Add(first[B.ToString()]);
							else
								betaSet = first[B.ToString()].Clone();
						}
					}
				}
			}


			// regola 3
			foreach (var A in _production)
			{
				foreach (Alternative alt in A.Alt)
				{
					// se la produzione rule e' empty passa alla prossima rule
					if (alt.ListWithoutAction.Count == 1 && alt.ListWithoutAction[0] is Empty == true)
						continue;

					// visito tutti gli elementi a_i ritroso.
					for (int i = alt.ListWithoutAction.Count - 1; i >= 0; --i)
					{
						// smetto se l'elemento E e' un terminale
						if (alt.ListWithoutAction[i] is Terminal) break;

						NonTerminal B = (NonTerminal)alt.ListWithoutAction[i];

						if (B.Symbol != A.Symbol)
						{
							changed |= follow[B.Symbol].Add(follow[A.Symbol]);
						}

						if (first[B.Symbol].ContainsEmpty() == false)
							break;
					}

				}
			}
		}
		while (changed);

		return follow;
	}

	/// <summary>
	/// Calcola il First[a_i,b,c,d...] 
	/// Il calcolo del first follow della grammatica deve essere gia` stato fatto
	/// </summary>
	public TerminalSet ComputeFirst(List<GrammarSymbol> gsList, int gsStart)
	{
		// mi assicuro di avere first/follow calcolato
		if (_first == null) _first = ComputeFirst();
		if (_follow == null) _follow = ComputeFollow(_first);

		TerminalSet ret = new TerminalSet();

		for (int gsi = gsStart; gsi < gsList.Count; ++gsi)
		{
			GrammarSymbol gs = gsList[gsi];

			if (gs is Empty)
			{
				ret.Add((Empty)gs);
				break;
			}
			else if (gs is Token)
			{
				ret.Add((Token)gs);
				break;
			}
			else if (gs is NonTerminal)
			{
				var fgs = this.First[gs.ToString()];

				if (gsi == ret.Count - 1)
					ret.Add(fgs);		// se fgs ha empty nell'ultimo simbolo lo metto anche nel risultato.
				else
					ret.AddExceptEmpty(fgs);

				if (fgs.ContainsEmpty() == false)
					break;
			}
			else if (gs is Action)
			{
				//Debug.Assert(false);
			}
			else
				Debug.Assert(false);
		}

		return ret;
	}

	public Dictionary<string, TerminalSet> First
	{
		get
		{
			if (_first == null)
				_first = ComputeFirst();
			return _first;
		}
	}
	public Dictionary<string, TerminalSet> Follow
	{
		get
		{
			if (_follow == null)
				_follow = ComputeFollow(this.First);
			return _follow;
		}
	}

	public bool CheckGrammar(TextWriter tw)
	{
		bool ok = true;

		// controllo che le produzioni non abbiano simboli duplicati
		if (true)
		{
			foreach (var p in _production)
			{
				var rr = _production.FindAll(pp => pp.Symbol == p.Symbol);
				if (rr.Count > 1)
				{
					tw.WriteLine("Production '{0}' is multiple defined", p.Symbol);
					ok = false;
				}
			}
		}

		// controllo che i NonTerminal puntino ad una Production
		if (true)
		{
			foreach (var p in _production)
				foreach (var a in p.Alt)
					foreach (var gs in a.ListWithoutAction)
						if (gs is NonTerminal)
						{
							NonTerminal nt = (NonTerminal)gs;

							var rr = _production.FindAll(pp => pp.Symbol == nt.Symbol);
							if (rr.Count == 0)
							{
								tw.WriteLine("Non terminal '{0}' has no production", nt.Symbol);
								ok = false;
							}
						}
		}

		// controllo che ogni produzione sia raggiungibile dalla prima (start)
		if (true)
		{
			List<string> reachable = new List<string>();
			reachable.Add(_production[0].Symbol);

			AddReachable(_production[0].Symbol, reachable);

			foreach (var p in _production)
				if (reachable.Contains(p.Symbol) == false)
				{
					tw.WriteLine("Production '{0}' cannot be reached from '{1}'.", p.Symbol, _production[0].Symbol);
					ok = false;
				}
		}

		return ok;
	}

	public bool CheckConflicts(TextWriter tw)
	{
		bool ok = true;

		// calcolo di first/follow
		if (true)
		{
			_first = ComputeFirst();
			_follow = ComputeFollow(_first);
		}

		// controllo se la grammatica e' ricorsiva (immediata o ciclica)
		if (true)
		{
			foreach (var p in _production)
				if (CheckIsRecursive(tw, p.Symbol) != RecursiveType.None) ok = false;
		}

		// controllo se ci sono conflitti
		if (true)
		{
			// non piu' di una alt puo' avere in first empty
			foreach (var p in _production)
			{
				int nEmpty = p.Alt.Count(a => a.First.ContainsEmpty());
				if (nEmpty > 1)
				{
					tw.WriteLine("Conflict in '{0}' production", p.Symbol);
					foreach (var a in p.Alt)
						if (a.First.ContainsEmpty())
							tw.WriteLine("\talternative '{0}'", a);
					tw.WriteLine("have empty in First set.");
					tw.WriteLine();
					ok = false;
				}
			}

			// i Select set non possono avere elementi in comune
			foreach (var p in _production)
			{
				TerminalSet u = new TerminalSet();
				foreach (var a in p.Alt)
					u = TerminalSet.Union(u, a.Select);

				foreach (Terminal t in u)
				{
					var lt = new List<Alternative>();
					p.Alt.ForEach(alt => { if (alt.Select.Contains(t)) lt.Add(alt); });

					if (lt.Count >= 2)
					{
						// 2 o piu' alternative condividono lo stesso "t" ==> conflitto.

						int resolve = 0;
						lt.ForEach(aa => { if (aa.ListWithAction[0].ContainsResolver(t.ToString())) resolve += 1; });
						if (resolve == 0)
						{
							tw.WriteLine("Conflict in '{0}' production for token {1}", p.Symbol, t);
							tw.WriteLine("\t\tFollow  {0}", p.Follow);
							lt.ForEach(aa =>
							{
								tw.WriteLine("\t{0}", aa);
								tw.WriteLine("\t\tFirst  {0}", aa.First);
								tw.WriteLine("\t\tSelect {0}", aa.Select);
							});
							tw.WriteLine();
							ok = false;
						}
						else if (resolve != 1)
						{
							tw.WriteLine("Too many resolver in '{0}' production for token {1}", p.Symbol, t);
							ok = false;
						}
						else
						{
							// una sola alternativa ha il resolver
						}
					}
				}
			}

		}

		return ok;
	}

	public enum RecursiveType
	{
		/// <summary>
		/// Non è ricorsiva
		/// </summary>
		None,		
		/// <summary>
		/// E' ricorsiva immediata ossia ha una alternativa A : A
		/// oppuere A : B A dove First(B) contiene empty
		/// </summary>
		Immediate,
		/// <summary>
		/// E' ricorsiva ciclica ossia ci vogliono più non terminali per la ricorsione
		/// </summary>
		Cyclic
	}
	public RecursiveType CheckIsRecursive(TextWriter tw, string nonTerminal)
	{
		List<string> path = new List<string>();
		path.Add(nonTerminal);
		return CheckIsRecursive(tw, path);
	}
	private RecursiveType CheckIsRecursive(TextWriter tw, List<string> path)
	{
		RecursiveType ret = RecursiveType.None;

		Production p = this[path[path.Count-1]];
		foreach (var a in p.Alt)
		{
			foreach (var gs in a.ListWithoutAction)
			{
				if (gs is Empty) break;
				else if (gs is Terminal) break;
				else if (gs is NonTerminal)
				{
					NonTerminal nt = (NonTerminal)gs;

					if (path[0] == nt.Symbol)
					{
						var rr = (path.Count == 1) ? RecursiveType.Immediate : RecursiveType.Cyclic;
						if (tw != null)
						{
							tw.WriteLine("Production '{0}' is {1} left recursive:", path[0], rr == RecursiveType.Immediate ? "immediate" : "cyclic");
							foreach (var s in path)
								tw.WriteLine("\t{0}", s);
							tw.WriteLine("\t{0}", nt.Symbol);
							tw.WriteLine();
						}

						return rr;
					}

					if (path.Contains(nt.Symbol) == false)
					{
						path.Add(nt.Symbol);
						var r = CheckIsRecursive(tw, path);
						if (r != RecursiveType.None) ret = r;
						path.RemoveAt(path.Count - 1);
					}

					if (First[nt.Symbol].ContainsEmpty() == false)
						break;
				}
				else if (gs is Action) { Debug.Assert(false); }
				else Debug.Assert(false);
			}
		}

		return ret;
	}

	private void AddReachable(string symbol, List<string> rechable)
	{
		var p = this[symbol];
		foreach (var a in p.Alt)
		{
			foreach (var gs in a.ListWithoutAction)
			{
				if (gs is NonTerminal)
				{
					if (rechable.Contains(((NonTerminal)gs).Symbol) == false)
					{
						rechable.Add(((NonTerminal)gs).Symbol);
						AddReachable(((NonTerminal)gs).Symbol, rechable);
					}
				}
			}
		}
	}

	public void GenerateCode(List<U.Tuple<RegRoot, string>> lexerActions, ParserOptions po, U.CsStreamWriter tw)
	{
		tw.WriteLine("#pragma warning disable 0168 // variable declared but not used.");
		tw.WriteLine("#pragma warning disable 0219 // variable assigned but not used.");
		tw.WriteLine();

		foreach (var u in po.Using)
			tw.WriteLine("using {0};", u);

		tw.WriteLine("using System.Collections.Generic;");
		tw.WriteLine();
		if (po.Namespace != null)
		{
			tw.WriteLine("namespace {0}", po.Namespace);
			tw.WriteLine("{");
		}

		tw.WriteLine("partial class {0} : ParserBase", po.Class);
		tw.WriteLine("{");
		if (true)
		{
			TerminalSet token = new TerminalSet();
			foreach (var p in _production)
				foreach (var a in p.Alt)
					foreach (var t in a.ListWithoutAction)
						if (t is Token)
							if (((Token)t).IsSimpleChar == false)
								token.Add((Token)t);
			int n = -2;
			foreach (var tk in token)
				tw.WriteLine("public const int {0} = {1};", tk.ToString(), n--);
			if (token.Count > 0) tw.WriteLine();

			if (token.Count > 0)
			{
				tw.WriteLine("Dictionary<int, string> _token;");
				tw.WriteLine("public override Dictionary<int, string> Token");
				tw.WriteLine("{");
				tw.WriteLine("get");
				tw.WriteLine("{");
				tw.WriteLine("if (_token == null)");
				tw.WriteLine("{");
				tw.WriteLine("_token = new Dictionary<int, string>();");
				tw.WriteLine("_token.Add(-1, \"EOF\");");
				n = -2;
				foreach (var tk in token)
					tw.WriteLine("_token.Add({1}, \"{0}\");", tk.ToString(), n--);

				tw.WriteLine("}");
				tw.WriteLine("return _token;");
				tw.WriteLine("}");
				tw.WriteLine("}");
				tw.WriteLine();
			}

		}

		foreach (var p in _production)
		{
			if (p.FromLeftFactorize == 0)
				tw.WriteLine("{0} {1}(IAST {1}_i)", p.Type, p.Symbol);
			else
			{
				tw.Write("{0} {1}(", p.Type, p.Symbol);
				for (int kk = 0; kk < p.FromLeftFactorize; ++kk)
					tw.Write("IAST nt{0}_s, ", kk + 1);
				tw.WriteLine("IAST {0}_i)", p.Symbol, p.Type);
			}
			tw.WriteLine("{");
			tw.WriteLine("int alt = 0;");
			tw.WriteLine("switch (Next.token)");
			tw.WriteLine("{");
			for (var a = 0; a < p.Alt.Count; ++a)
			{
				var dc = p.ComputeSwitch(null);
				foreach (var dcv in dc)
				{
					if (dcv.Value == p.Alt[a])
					{
						Terminal t = dcv.Key;
						if (t is Eof) tw.WriteLine("case -1:");
						else if (t is Token) tw.WriteLine("case {0}:", t);
						else if (t is Empty) Debug.Assert(false);
						else Debug.Assert(false);
					}
				}
				tw.WriteLine("alt = {0};", a);
				tw.WriteLine("break;");
			}
			tw.WriteLine("default:");
			tw.WriteLine("Error();");
			tw.WriteLine("break;");
			tw.WriteLine("}");
			tw.WriteLine();

			tw.WriteLine("{1} {0}_s = default({1});", p.Symbol, p.Type);
			tw.WriteLine("switch (alt)");
			tw.WriteLine("{");

			for (var a = 0; a < p.Alt.Count; ++a)
			{
				// per ogni alternativa
				tw.WriteLine("case {0}:", a);
				tw.WriteLine("{");
				if (true)
				{
					int nnt = 0;
					foreach (var t in p.Alt[a].ListWithAction)
					{
						if (t is Empty)
							nnt += 1;
						else if (t is Token)
							nnt += 1;
						else if (t is NonTerminal)
							nnt += 1;
						else if (t is Action)
						{ }
						else
							Debug.Assert(false);
					}
					if (nnt > 0)
					{
						for (int i = 0; i < nnt; ++i)
							tw.WriteLine("var nt{0}_i = default(IAST);", i + 1 + p.FromLeftFactorize);
						tw.WriteLine();

						/*
						if (p.FromLeftFactorize == 0)
						{
							for (int i = 0; i < nnt; ++i)
								tw.WriteLine("var nt{0}_i = default(IAST);", i + 1);
							tw.WriteLine();
						}
						else
						{
							for (int i = 0; i < nnt; ++i)
								tw.WriteLine("var nt{0}_i = default(IAST);", i + 1 + p.FromLeftFactorize);
							tw.WriteLine();
						}
						*/
					}
				}


				int nti = p.FromLeftFactorize + 1;
				foreach (var t in p.Alt[a].ListWithAction)
				{
					if (t is Empty)
					{
						// non faccio niente
					}
					else if (t is Token)
					{
						tw.WriteLine("TokenAST nt{1}_s = Match({0}, nt{1}_i);", ((Token)t).Symbol, nti++);
					}
					else if (t is NonTerminal)
					{
						NonTerminal nt = (NonTerminal)t;
						if (nt.Production.FromLeftFactorize == 0)
						{
							tw.WriteLine("var nt{1}_s = {0}(nt{1}_i);", nt.Symbol, nti++);
						}
						else
						{
							tw.Write("var nt{1}_s = {0}(", nt.Symbol, nti);
							for (int kk = 1; kk < nti; kk++)
								tw.Write("nt{0}_s, ", kk);
							tw.WriteLine("nt{0}_i);", nti);
							nti += 1;
						}
					}
					else if (t is Action)
					{
						string code = ((Action)t).Code;

						if (code.StartsWith("^"))
						{
							code = "$$ = " + code.Substring(1) + ";";
						}
						else if (code.StartsWith("{") && code.EndsWith("}"))
						{
							code = code.Substring(1);
							while (code[0] == ' ') code = code.Substring(1);
							code = code.Remove(code.Length - 1);
						}
						else
							Debug.Assert(false);


						code = ReplaceWorld(code, "$$.i", U.F("{0}_i", p.Symbol));
						code = ReplaceWorld(code, "$$.s", U.F("{0}_s", p.Symbol));
						code = ReplaceWorld(code, "$$",   U.F("{0}_s", p.Symbol));
						for (int i = 0; i < 1000; ++i)
						{
							string k, v;

							k = U.F("${0}.i", i);
							v = U.F("nt{0}_i", i);
							code = ReplaceWorld(code, k, v);

							k = U.F("${0}.s", i);
							v = U.F("nt{0}_s", i);
							code = ReplaceWorld(code, k, v);
							
							k = U.F("${0}", i);
							v = U.F("nt{0}_s", i);
							code = ReplaceWorld(code, k, v);
						}

						tw.WriteLine("{0}", code);
					}
					else
						Debug.Assert(false);
				}
				tw.WriteLine("}");
				tw.WriteLine("break;");
			}

			tw.WriteLine("}");
			tw.WriteLine();

			tw.WriteLine("switch (Next.token)");
			tw.WriteLine("{");

			foreach (var f in Follow[p.Symbol])
				if (f is Eof == false)
					tw.WriteLine("case {0}:", f.ToString());
				else
					tw.WriteLine("case -1:");
			tw.WriteLine("break;");
			tw.WriteLine("default:");
			tw.WriteLine("Error();");
			tw.WriteLine("break;");

			tw.WriteLine("}");

			tw.WriteLine("return {0}_s;", p.Symbol);

			tw.WriteLine("}");
			tw.WriteLine();
		}

		if (lexerActions.Count > 0)
		{
			tw.WriteLine("protected override RegAcceptList CreateRegAcceptList()");
			tw.WriteLine("{");
			tw.WriteLine("var acts = new RegAcceptList();");
			foreach (var v in lexerActions)
			{
				if (v.Item2 == "^()")
				{
					tw.WriteLine("acts.Add({0});", v.Item1.ToString());
				}
				else if (v.Item2.StartsWith("^("))
				{
					string r = v.Item2.Substring(2);
					r = r.Remove(r.Length - 1);
					if (r == "'")
						tw.WriteLine("acts.Add({0}, '\'');", v.Item1.ToString());
					else
						tw.WriteLine("acts.Add({0}, '{1}');", v.Item1.ToString(), r);
				}
				else if (v.Item2.StartsWith("^"))
				{
					tw.WriteLine("acts.Add({0}, {1});", v.Item1.ToString(), v.Item2.Substring(1));
				}
				else
				{
					string code = v.Item2.Substring(1);
					code = code.Remove(code.Length - 1);

					if (v.Item1.ToString() == "'''")
						tw.WriteLine("acts.Add( '\'', (ref NFA.Token tk, LexReader rd, NFA nfa) => {1}", v.Item1.ToString(), "{");
					else
						tw.WriteLine("acts.Add({0}, (ref NFA.Token tk, LexReader rd, NFA nfa) => {1}", v.Item1.ToString(), "{");

					var cc = new List<string>(code.Split('\n'));
					if (cc.Count > 0 && cc[0] == "") cc.RemoveAt(0);
					if (cc.Count > 0 && cc[cc.Count - 1] == "") cc.RemoveAt(cc.Count - 1);

					tw.SetTab(+1);
					foreach (var s in cc)
						tw.WriteLine("{0}", s.Trim().Replace("\n", Environment.NewLine));
					tw.SetTab(-1);
					tw.WriteLine("});");
				}
			}
			tw.WriteLine("return acts;");
			tw.WriteLine("}");
			tw.WriteLine("}");
			if (po.Namespace != null)
			{
				tw.WriteLine("}");
			}
		}
	}

	public Grammar RemoveEmptyLeftRecursion()
	{
		Grammar s = this;
		for (; ; )
		{
			Grammar a = s.DoRemoveEmptyLeftRecursion();
			if (a == s)
				return a;
			s = a;
		}
	}
	private Grammar DoRemoveEmptyLeftRecursion()
	{
		Grammar g_new = new Grammar(_tmp);
		bool done = false;

		foreach (var p_i in _production)
		{
			bool isEmptyLR = false;

			if (true)
			{
				// trovo una produzione con un alternativa immediatamente left recursive
				// ossia A : A ...
				bool isLR = p_i.Alt.Exists(a => a.ListWithoutAction[0].ToString() == p_i.Symbol);
				isEmptyLR = isLR && this.First[p_i.Symbol].ContainsEmpty(); // e controllo se può generare <e>
			}
			if (isEmptyLR)
			{
				// controllo che la sola alternativa che può generare <e> sia A : <e>
				foreach (var a in p_i.Alt)
					if (a.First.ContainsEmpty() && a.ListWithoutAction[0] is Empty == false)
						throw new ApplicationException(U.F("Cannot remove empty production from '{0}' since alternative '{1}' is not directly <empty>", p_i.Symbol, a));

				// controllo che tutte le alternative abbiano l'azione solo in fondo e del tipo ^
				foreach (var a in p_i.Alt)
					foreach (var gs in U.FirstLast(a.ListWithAction))
					{
						if (gs.Current is Action)
						{
							if (gs.IsLast == false)
								throw new ApplicationException(U.F("Cannot remove empty production from '{0}' since alternative '{1}' contains inner actions", p_i.Symbol, a));
							if (gs.Current.ToString().StartsWith("^") == false)
								throw new ApplicationException(U.F("Cannot remove empty production from '{0}' since alternative '{1}' contains non ^ action", p_i.Symbol, a));
						}
					}
			}


			if (isEmptyLR == false)
			{
				Production p_s = g_new[p_i.Symbol];
				p_s.Type = p_i.Type;

				foreach (var a_i in p_i.Alt)
				{
					Alternative a_s = p_s.CreateAlternative();
					foreach (var g_i in a_i.ListWithAction)
						a_s.Add(g_i.Clone(g_new));
				}
			}
			else
			{
				done = true;

				string aEmptyAction = null;
				foreach (var a in p_i.Alt)
					if (a.ListWithoutAction[0] is Empty)
					{
						aEmptyAction = "^$1";
						if (a.ListWithAction.Count >= 2 && a.ListWithAction[1] is Action)
							aEmptyAction = ((Action)a.ListWithAction[1]).Code;
						break;
					}


				string A1 = U.F("tmp_{0}", g_new._tmp++);
				string A = p_i.Symbol;
				if (true)
				{
					var p_A = g_new[A];
					p_A.Type = p_i.Type;
					var a1 = p_A.CreateAlternative();
					a1.Add(new Empty(g_new));
					a1.Add(new Action(g_new, aEmptyAction));
					var a2 = p_A.CreateAlternative();
					a2.Add(new NonTerminal(g_new, A1));
					a2.Add(new Action(g_new, "^$1"));
				}

				if (true)
				{
					var p_A1 = g_new[A1];
					p_A1.Type = p_i.Type;

					foreach (var a_i in p_i.Alt)
					{
						if (a_i.ListWithoutAction[0] is Empty) continue;

						int nA = a_i.ListWithoutAction.Count(gr => gr.ToString() == A);
						if (nA == 0)
						{
							Alternative a_d = p_A1.CreateAlternative();
							foreach (var g_i in a_i.ListWithAction)
								if (g_i.ToString() != p_i.Symbol)
									a_d.Add(g_i.Clone(g_new));
								else
									a_d.Add(new NonTerminal(g_new, A1));
						}
						else
						{
							foreach (var c in U.Combine(nA, 2))
							{
								List<int> posA = new List<int>();
								int pa = 0;
								Alternative a_d = p_A1.CreateAlternative();
								for (int p = 0; p < a_i.ListWithAction.Count; ++p)
								{
									var g_i = a_i.ListWithAction[p];
									if (g_i.ToString() == A)
									{
										if (c[pa] == 0)
										{
											// da rimuovere
											posA.Add(p);
										}
										else
										{
											// da non rimuovere.
											a_d.Add(new NonTerminal(g_new, A1));
										}
										pa += 1;
									}
									else if (g_i is Action)
									{
										string s = g_i.ToString();
										foreach (var pp in posA)
											s = ReplaceWorld(s, U.F("${0}", pp + 1), "(" + aEmptyAction.Substring(1) + ")");
										foreach (var pp in posA)
										{
											for (int k = pp + 1; k < 100; ++k)
												s = ReplaceWorld(s, U.F("${0}", k + 1), U.F("${0}", k));
										}
										a_d.Add(new Action(g_new, s));
									}
									else
										a_d.Add(g_i.Clone(g_new));
								}
							}

							/*
							// devo creare 2^nA alternative.
							if (true)
							{
								Alternative a_d = p_A1.CreateAlternative();
								foreach (var g_i in a_i.ListWithAction)
									if (g_i.ToString() != A)
										a_d.Add(g_i.Clone(g_new));
									else
										a_d.Add(new NonTerminal(g_new, A1));
							}
							if (true)
							{
								Alternative a_d = p_A1.CreateAlternative();
								foreach (var g_i in a_i.ListWithAction)
								{
									if (g_i.ToString() == A) continue; 
									if (g_i is Action == false)
										a_d.Add(g_i.Clone(g_new));
									else
									{
										string s = g_i.ToString();
										s = s.Replace("$1", "(" + aEmptyAction.Substring(1) + ")");
										s = s.Replace("$2", "$1");
										a_d.Add(new Action(g_new, s));
									}
								}
							}
							*/
						}
					}

				}

			}
		}

		return done ? g_new : this;
	}
	public Grammar RemoveLeftRecursion()
	{
		Grammar s = this;
		for (; ; )
		{
			Grammar a = s.DoRemoveLeftRecursion();
			if (a == s)
				return a;
			s = a;
		}
	}
	private Grammar DoRemoveLeftRecursion()
	{
		Grammar g_new = new Grammar(_tmp);
		bool done = false;

		foreach (var p_i in _production)
		{
			bool isLR = false;
			foreach (var a in p_i.Alt)
			{
				if (a.ListWithoutAction[0].ToString() == p_i.Symbol)
				{
					isLR = true;
					break;
				}
			}

			if (isLR)
			{
				// controllo che non produca <e>
				if (this.First[p_i.Symbol].ContainsEmpty())
					throw new ApplicationException(U.F("Cannot remove recursion from production '{0}' because {0} => <e>", p_i.Symbol));

				// controllo che non abbia cicli ossia che A => A (oltre che A : A naturalmente)
				if (this.CheckIsRecursive(null, p_i.Symbol) == RecursiveType.Cyclic)
					throw new ApplicationException(U.F("Cannot remove recursion from production '{0}' because is cyclic recursive", p_i.Symbol));

				// controllo che sia nella forma
				// A : a_i b c ^Action    (non sono ammesse azioni interne, solo l'ultima azione che deve essere nel formato ^)
				foreach (var a in p_i.Alt)
				{
					if (a.ListWithoutAction.Count == a.ListWithAction.Count) continue;   // lista senza azioni = list con azioni ==> non ci sono azioni
					if (a.ListWithoutAction.Count + 1 != a.ListWithAction.Count)
						throw new ApplicationException(U.F("Cannot remove recursion from production {0} because inner actions", p_i.Symbol));

					// controllo che l'ultima sia ^
					var gs = a.ListWithAction[a.ListWithAction.Count - 1];
					if (gs is Action == false)
						throw new ApplicationException(U.F("Cannot remove recursion from production {0} because inner actions", p_i.Symbol));

					var action = gs as Action;
					if (action.Code.StartsWith("^") == false)
						throw new ApplicationException(U.F("Cannot remove recursion from production {0} because last action must be of ^ type", p_i.Symbol));
				}
			}

			if (isLR == false)
			{
				Production p_s = g_new[p_i.Symbol];
				p_s.Type = p_i.Type;

				foreach (var a_i in p_i.Alt)
				{
					Alternative a_s = p_s.CreateAlternative();
					foreach (var g_i in a_i.ListWithAction)
						a_s.Add(g_i.Clone(g_new));
				}
			}
			else
			{
				done = true;

				// A : A g1 { $$ = g1($1, $2..) } ..
				//   | A gm { $$ = gm($1, $2..) }
				//   | f1   { $$ = f1($1); } ..
				//   | fn   { $$ = fn($1); } ..
				//   ;
				// diventa
				// A : f1 { $2.i = f1($1.s) } B { $$ = $2.s } ..
				//   | fn { $2.i = fn($1.s) } B { $$ = $2.s } ..
				//   ;
				// B : g1  { $2.i = g1($$.i, $1.s) B { $$ = $2.s } ..
				//   | gm  { $2.i = gm($$.i, $1.s) B { $$ = $2.s }
				//   | <e> { $$ = $$.i }
				//   ;
				string B = U.F("tmp_{0}", g_new._tmp++);

				if (true)
				{
					Production p_s = g_new[p_i.Symbol];
					p_s.Type = p_i.Type;

					// ricerco A : f1..n
					foreach (var a_i in p_i.Alt)
					{
						if (a_i.ListWithoutAction[0].ToString() != p_i.Symbol)
						{
							// A : fn
							// diventa
							//   | fn { $2.i = fn($1.s) } B { $$ = $2.s } ..

							string action_f = null;
							if (a_i.ListWithoutAction.Count != a_i.ListWithAction.Count)
							{
								Action a = (Action)a_i.ListWithAction[a_i.ListWithAction.Count - 1];
								if (a.Code.StartsWith("^"))
									action_f = a.Code.Substring(1);
								else
									Debug.Assert(false);
							}

							var a_s = p_s.CreateAlternative();
							foreach (var g in a_i.ListWithoutAction)
								a_s.Add(g.Clone(g_new));

							if (action_f != null)
								a_s.Add(new Action(g_new, U.F("{{ ${0}.i = {1}; }}", a_i.ListWithoutAction.Count + 1, action_f)));

							a_s.Add(new NonTerminal(g_new, B));

							if (action_f != null)
								a_s.Add(new Action(g_new, U.F("{{ $$ = ${0}.s; }}", a_i.ListWithoutAction.Count + 1)));
						}
					}
				}

				if (true)
				{
					// ricerco A : A g1..m
					var p_s = g_new[B];
					p_s.Type = p_i.Type;

					if (true)
					{
						//   | <e> { $$ = $$.i }
						var a_s = p_s.CreateAlternative();
						a_s.Add(new Empty(g_new));
						a_s.Add(new Action(g_new, "{" + U.F(" $$ = ({0})$$.i; ", p_s.Type) + "}"));
					}

					foreach (var a in p_i.Alt)
					{
						string action_g = null;
						if (a.ListWithoutAction.Count != a.ListWithAction.Count)
							action_g = a.ListWithAction[a.ListWithAction.Count - 1].ToString();

						if (a.ListWithoutAction[0].ToString() == p_i.Symbol)
						{
							Alternative a_s = p_s.CreateAlternative();

							// A : A gm
							// diventa
							//   | gm  { $2.i = gm($$.i, $1.s) } B { $$ = $2.s }
							//   | <e> { $$ = $$.i }  (gia` messo)
							for (var gi = 1; gi < a.ListWithoutAction.Count; gi++)
								a_s.Add(a.ListWithoutAction[gi].Clone(g_new));

							if (action_g != null)
							{
								action_g = ReplaceWorld(action_g, "$1", U.F("(({0})$$.i)", p_i.Type));

								for (int ii = 0; ii <100; ++ii)
									action_g = ReplaceWorld(action_g, U.F("${0}", ii + 2), U.F("${0}.s", ii + 1));

								if (action_g.StartsWith("^"))
									action_g = action_g.Substring(1);

								a_s.Add(new Action(g_new, U.F("{{ ${0}.i = {1}; }}", a.ListWithoutAction.Count, action_g)));
							}

							a_s.Add(new NonTerminal(g_new, B));

							if (action_g != null)
								a_s.Add(new Action(g_new, U.F("{{ $$ = ${0}.s; }}", a.ListWithoutAction.Count)));
						}
					}
				}
			}
		}

		if (done)
			return g_new;
		else
			return this;
	}
	public Grammar LeftFactorize()
	{
		Grammar s = this;
		for (; ; )
		{
			Grammar a = s.DoLeftFactorize();
			if (a == s)
				return a;
			s = a;
		}
	}
	private void GetLeftTypesFromLeftFactorize(Production p, List<string> prod, List<string> types)
	{
		Alternative ap = null;
		var a = this[p.FactorizedSymbol];

		foreach (var aa in a.Alt)
		{
			foreach (var bb in aa.ListWithoutAction)
			{
				if ((bb is NonTerminal) && ((NonTerminal)bb).Symbol == p.Symbol)
				{
					ap = aa;
					break;
				}
			}
			if (ap != null) break;
		}

		if (a.FromLeftFactorize > 0)
			GetLeftTypesFromLeftFactorize(a, prod, types);

	}
	private Grammar DoLeftFactorize()
	{
		bool doLeftFactorize = false;

		// cerco una produzione da left-fattorizzare
		foreach (var p_src in this._production)
		{
			Dictionary<GrammarSymbol, List<Alternative>> alt_src_Dic = new Dictionary<GrammarSymbol, List<Alternative>>();
			foreach (var alt in p_src.Alt)
			{
				GrammarSymbol first_gs = alt.ListWithAction[0];
				if (alt_src_Dic.ContainsKey(first_gs) == false)
					alt_src_Dic[first_gs] = new List<Alternative>();

				alt_src_Dic[first_gs].Add(alt);
			}

			foreach (var d in alt_src_Dic)
				if (d.Value.Count > 1)
					doLeftFactorize = true;

			if (doLeftFactorize)
				break;
		}
		if (doLeftFactorize == false)
			return this;

		Grammar ret = new Grammar(_tmp);

		foreach (var p_src in this._production)
		{
			Dictionary<GrammarSymbol, List<Alternative>> alt_src_Dic = new Dictionary<GrammarSymbol, List<Alternative>>();
			foreach (var alt in p_src.Alt)
			{
				GrammarSymbol first_gs = alt.ListWithAction[0];
				if (alt_src_Dic.ContainsKey(first_gs) == false)
					alt_src_Dic[first_gs] = new List<Alternative>();

				alt_src_Dic[first_gs].Add(alt);
			}

			Production p_new = ret[p_src.Symbol];
			p_new.Type = p_src.Type;
			p_new.FromLeftFactorize = p_src.FromLeftFactorize;
			p_new.FactorizedSymbol = p_src.FactorizedSymbol;

			foreach (var alt_src in alt_src_Dic)
			{
				if (alt_src.Value.Count == 1)
				{
					// questi sono i gamma
					var aa = p_new.CreateAlternative();
					alt_src.Value[0].ListWithAction.ForEach(gs => aa.Add(gs.Clone(ret)));
				}
				else
				{
					// questi sono da fattorizzare.

					// cerco la parte in comune.
					int ig = 0;
					if (true)
					{
						bool eq = false;
						do
						{
							eq = true;
							for (int i = 1; i < alt_src.Value.Count; ++i)
							{
								if (ig == alt_src.Value[0].ListWithAction.Count) { eq = false; break; }
								if (ig == alt_src.Value[i].ListWithAction.Count) { eq = false; break; }
								if (alt_src.Value[i].ListWithAction[ig] != alt_src.Value[0].ListWithAction[ig]) { eq = false; break; }
							}
							if (eq) ig += 1;
						}
						while (eq);

						// da [0..ig) sono uguali
					}

					// questa e` la parte in comune
					string A1 = U.F("tmp_{0}", ret._tmp++);
					if (true)
					{
						var aa = p_new.CreateAlternative();
						for (int ii = 0; ii < ig; ++ii)
							aa.Add(alt_src.Value[0].ListWithAction[ii].Clone(ret));

						aa.Add(new NonTerminal(ret, A1));
						aa.Add(new Action(ret, U.F("^${0}", p_src.FromLeftFactorize + ig + 1)));
					}

					// questa e` la parte da fattorizzare
					Production a1 = ret[A1];
					a1.Type = p_src.Type;
					a1.FromLeftFactorize = p_src.FromLeftFactorize + ig;
					a1.FactorizedSymbol = p_src.Symbol;

					foreach (var b_src in alt_src.Value)
					{
						var b_dest = a1.CreateAlternative();

						bool onlyAction = true;
						for (int g = ig; g < b_src.ListWithAction.Count; ++g)
							if (b_src.ListWithAction[g] is Action == false)
								onlyAction = false;

						if (onlyAction)
							b_dest.Add(new Empty(ret));

						//if (a1.Symbol == "tmp_10")
						//{
						//	int rrr = 3;
						//	GetLeftTypesFromLeftFactorize(p_src, null, null);
						//}

						Dictionary<int, string> ty = new Dictionary<int, string>();

						//if (p_src.FromLeftFactorize > 0)
						//{
						//	// aggiungere i tipi di p_src
						//	int ddddd = 3;
						//}

						for (int g = 0; g < ig; ++g)
						{
							var aa = b_src.ListWithAction[g];
							if (aa is NonTerminal)
							{
								ty[g + 1 + p_src.FromLeftFactorize] = this[((NonTerminal)aa).Symbol].Type;
							}
							else if (aa is Token)
							{
								ty[g + 1 + p_src.FromLeftFactorize] = "TokenAST";
							}
							else if (aa is Empty)
							{
							}
						}

						for (int g = ig; g < b_src.ListWithAction.Count; ++g)
						{
							var r = b_src.ListWithAction[g].Clone(ret);
							if (r is Action)
							{
								var aa = r as Action;

								foreach (var dd in ty)
								{
									aa.Code = ReplaceWorld(aa.Code, U.F("${0}", dd.Key), U.F("(({1})${0})", dd.Key, dd.Value));
								}
							}
							b_dest.Add(r);
						}
					}
				}
			}
		}

		return ret;
	}
}

abstract class GrammarSymbol : IEquatable<GrammarSymbol>
{
	protected GrammarSymbol(Grammar g) { _g = g; _resolver = new List<string>(); }
	protected GrammarSymbol(Grammar g, GrammarSymbol gs) : this(g) { gs._resolver.ForEach(a => _resolver.Add(a)); }

	public static bool operator ==(GrammarSymbol a, GrammarSymbol b) { return a.Equals(b) == true; }
	public static bool operator !=(GrammarSymbol a, GrammarSymbol b) { return a.Equals(b) == false; }

	public override bool Equals(Object v) { Debug.Assert(false); return false; }
	public override int GetHashCode() { return base.GetHashCode(); }

	public bool Equals(GrammarSymbol v) { return this.Equals((Object)v); }

	public virtual void AddResolver(string v) { _resolver.Add(v); }
	public bool ContainsResolver(string v) { return _resolver.Contains(v); }

	abstract public GrammarSymbol Clone(Grammar g);

	protected List<string> _resolver;
	protected Grammar _g;
}
abstract class Terminal : GrammarSymbol
{
	protected Terminal(Grammar g) : base(g) { }
	protected Terminal(Grammar g, Terminal gs) : base(g, gs) { }
}
class Empty : Terminal
{
	public Empty(Grammar g) : base(g) { }
	public Empty(Grammar g, Empty gs) : base(g, gs) { }
	public override string ToString() { return /*"ε"*/"<e>"; }

	public override bool Equals(Object obj)
	{
		if (obj == null) return false;
		return obj.GetType() == typeof(Empty);
	}
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
	public override GrammarSymbol Clone(Grammar g)
	{
		return new Empty(g, this);
	}
}
class Eof : Terminal
{
	public Eof(Grammar g) : base(g) { }
	public Eof(Grammar g, Eof gs) : base(g, gs) { }
	public override string ToString() { return "$"; }

	public override bool Equals(Object obj)
	{
		if (obj == null) return false;
		return obj.GetType() == typeof(Eof);
	}
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
	public override GrammarSymbol Clone(Grammar g)
	{
		return new Eof(g, this);
	}
}
class Token : Terminal
{
	public Token(Grammar g, string val, bool simpleChar)
		: base(g)
	{
		_simpleChar = simpleChar;
		_val = val;
	}
	public Token(Grammar g, Token gs)
		: base(g, gs)
	{
		_simpleChar = gs._simpleChar;
		_val = gs._val;
	}
	public override string ToString() { return _val; }

	public string Symbol { get { return _val; } }

	protected readonly string _val;
	protected readonly bool _simpleChar;

	public bool IsSimpleChar { get { return _simpleChar; } }

	public override bool Equals(Object obj)
	{
		if (obj == null) return false;
		if (obj.GetType() != typeof(Token)) return false;
		return ((Token)obj)._val == this._val;
	}
	public override int GetHashCode()
	{
		return _val.GetHashCode();
	}
	public override GrammarSymbol Clone(Grammar g)
	{
		return new Token(g, this);
	}
}
class NonTerminal : GrammarSymbol
{
	public NonTerminal(Grammar g, string symbol) : base(g) { _symbol = symbol; }
	public NonTerminal(Grammar g, NonTerminal gs) : base(g, gs) { _symbol = gs._symbol; }
	public override string ToString() { return _symbol; }
	public string Symbol { get { return _symbol; } }
	public override bool Equals(Object obj)
	{
		if (obj == null) return false;
		if (obj.GetType() != typeof(NonTerminal)) return false;
		return ((NonTerminal)obj)._symbol == this._symbol;
	}
	public override int GetHashCode()
	{
		return _symbol.GetHashCode();
	}

	public Production Production { get { return _g[_symbol]; } }


	public override GrammarSymbol Clone(Grammar g)
	{
		return new NonTerminal(g, this._symbol);
	}

	private string _symbol;
}
class Action : GrammarSymbol
{
	public Action(Grammar g, string code) : base(g) { _code = code; }
	public Action(Grammar g, Action gs) : base(g, gs) { _code = gs._code; }

	public override string ToString() { return _code; }
	public string Code { get { return _code; } set { _code = value; } }
	public override bool Equals(Object obj)
	{
		if (obj == null) return false;
		if (obj.GetType() != typeof(Action)) return false;
		return ((Action)obj)._code == this._code;
	}
	public override int GetHashCode()
	{
		return _code.GetHashCode();
	}

	public override GrammarSymbol Clone(Grammar g)
	{
		return new Action(g, this._code);
	}

	public override void AddResolver(string v)
	{
		Debug.Assert(false);
		throw new ApplicationException("AddResolver cannot be done on Action grammar symbol");
	}

	private string _code;
}

class Alternative
{
	public Alternative(Production p)
	{
		_production = p;
		_alt = new List<GrammarSymbol>();
		_gs = new List<GrammarSymbol>();
	}
	public void Add(GrammarSymbol gs)
	{
		_alt.Add(gs);
		if (gs is Action == false)
			_gs.Add(gs);
	}
	public override string ToString()
	{
		StringBuilder s = new StringBuilder();

		foreach (var a in U.FirstLast(_alt))
		{
			s.AppendFormat("{0}{1}", a.Current, (a.IsLast ? "" : " "));
		}

		return s.ToString();
	}

	public override bool Equals(Object obj)
	{
		if (obj == null) return false;
		if (obj is Alternative == false) return false;
		Alternative b = (Alternative)obj;
		if (this._alt.Count != b._alt.Count) return false;
		for (int i = 0; i < this._alt.Count; ++i)
			if (this._alt[i].Equals(b._alt[i]) == false) return false;
		return true;
	}
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public void AddResolver(int pos, string tk)
	{
		_alt[pos - 1].AddResolver(tk);
	}


	public List<GrammarSymbol> ListWithAction { get { return _alt; } }
	public List<GrammarSymbol> ListWithoutAction { get { return _gs; } }

	public TerminalSet First
	{
		get
		{
			if (_first == null)
				_first = this._production.G.ComputeFirst(_alt, 0);
			return _first;
		}
	}
	public TerminalSet Select
	{
		get
		{
			if (_select != null)
				return _select;

			TerminalSet select = new TerminalSet();

			if (First.ContainsEmpty() == false)
			{
				select.Add(First);
			}
			else
			{
				select.AddExceptEmpty(First);
				select.Add(this._production.Follow);
			}

			_select = select;
			return _select;
		}
	}

	private List<GrammarSymbol> _alt;
	private List<GrammarSymbol> _gs;
	private Production _production;
	private TerminalSet _first;
	private TerminalSet _select;
}

class Production
{
	public Production(Grammar g, string symbol, string type)
	{
		_g = g;
		_symbol = symbol;
		_type = type;
		_alt = new List<Alternative>();
		_fromLeftFactorize = 0;
	}

	public string Symbol { get { return _symbol; } }
	public int FromLeftFactorize
	{
		get { return _fromLeftFactorize; }
		set { _fromLeftFactorize = value; }
	}
	public string FactorizedSymbol { get; set; }
	public Alternative CreateAlternative() { var a = new Alternative(this); _alt.Add(a); return a; }

	public override string ToString()
	{
		StringBuilder s = new StringBuilder();
		s.AppendFormat("{0}<{1}>\n", _symbol, _type);
		foreach (var p in U.FirstLast(_alt))
			if (p.IsFirst)
				s.AppendFormat("\t: {0}\n", p.Current.ToString());
			else
				s.AppendFormat("\t| {0}\n", p.Current.ToString());
		s.Append("\t;\n");
		return s.ToString();
	}

	public List<Alternative> Alt { get { return _alt; } }
	public Grammar G { get { return _g; } }

	public TerminalSet Follow { get { return G.Follow[_symbol]; } }

	public string Type { get { return _type; } set { _type = value; } }

	public Dictionary<Terminal, Alternative> ComputeSwitch(TextWriter tw)
	{
		var ret = new Dictionary<Terminal, Alternative>();

		TerminalSet u = new TerminalSet();
		foreach (var a in this.Alt)
			u = TerminalSet.Union(u, a.Select);

		foreach (Terminal t in u)
		{
			var lt = new List<Alternative>();
			this.Alt.ForEach(alt => { if (alt.Select.Contains(t)) lt.Add(alt); });

			if (lt.Count == 1)
			{
				ret.Add(t, lt[0]);
			}
			else if (lt.Count >= 2)
			{
				// 2 o piu' alternative condividono lo stesso "t" ==> conflitto.
				Alternative altResolve = null;
				int resolve = 0;
				lt.ForEach(aa => { if (aa.ListWithAction[0].ContainsResolver(t.ToString())) { resolve += 1; altResolve = aa; } });
				if (resolve == 0)
				{
					if (tw != null)
					{
						tw.WriteLine("Conflict in '{0}' production for token {1}", this.Symbol, t);
						lt.ForEach(aa => tw.WriteLine("\t{0}", aa));
						tw.WriteLine();
					}
					return null;
				}
				else if (resolve != 1)
				{
					if (tw != null)
						tw.WriteLine("Too many resolver in '{0}' production for token {1}", this.Symbol, t);

					return null;
				}
				else
				{
					// una sola alternativa ha il resolver
					ret.Add(t, altResolve);
				}
			}
			else
			{
				Debug.Assert(false);
			}
		}

		return ret;
	}


	private Grammar _g;
	private string _symbol;
	private List<Alternative> _alt;
	int _fromLeftFactorize;
	string _type = "IAST";

}


class TerminalSet : IEnumerable<Terminal>
{
	List<Terminal> _terminals = new List<Terminal>();

	public bool Add(Terminal t)
	{
		foreach (var r in _terminals)
			if (r.Equals(t))
				return false;
		_terminals.Add(t);
		return true;
	}

	public bool Add(TerminalSet ts)
	{
		bool b = false;
		foreach (var r in ts)
			if (Add(r))
				b = true;
		return b;
	}

	public bool AddExceptEmpty(TerminalSet ts)
	{
		bool b = false;
		foreach (var r in ts)
		{
			if (r is Empty) continue;
			b |= Add(r);
		}
		return b;
	}

	public bool ContainsEmpty()
	{
		foreach (var r in _terminals)
			if (r is Empty)
				return true;
		return false;
	}
	public bool Contains(Terminal t)
	{
		foreach (var r in _terminals)
			if (r.Equals(t))
				return true;
		return false;
	}

	#region IEnumerable<Terminal> Members

	public IEnumerator<Terminal> GetEnumerator()
	{
		return _terminals.GetEnumerator();
	}

	#endregion

	#region IEnumerable Members

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return _terminals.GetEnumerator();
	}

	#endregion

	public override string ToString()
	{
		StringBuilder s = new StringBuilder();
		foreach (var t in U.FirstLast(_terminals))
			if (t.IsFirst)
				s.AppendFormat("{0}", t.Current.ToString());
			else
				s.AppendFormat(" {0}", t.Current.ToString());
		return s.ToString();
	}

	public TerminalSet Clone()
	{
		TerminalSet ret = new TerminalSet();
		ret._terminals = new List<Terminal>(this._terminals.Count);
		foreach (var t in this._terminals)
			ret._terminals.Add(t);
		return ret;
	}

	public int Count { get { return _terminals.Count; } }

	public static TerminalSet Union(TerminalSet a, TerminalSet b)
	{
		TerminalSet c = new TerminalSet();
		c.Add(a);
		c.Add(b);
		return c;
	}

	public static TerminalSet Intersect(TerminalSet a, TerminalSet b)
	{
		TerminalSet c = new TerminalSet();

		foreach (var ta in a)
		{
			bool trovato = false;
			foreach (var tb in b)
			{
				if (tb.Equals(ta))
				{
					trovato = true;
					break;
				}
			}
			if (trovato)
				c.Add(ta);
		}

		return c;
	}
}

