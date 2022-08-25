using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LLParserLexerLib
{
	static class U
	{
		public static string F(string fmt, params object[] args) => string.Format(System.Globalization.CultureInfo.InvariantCulture, fmt, args);
	}

	class RegAcceptList : IEnumerable<RegAccept>
	{
		private readonly List<RegAccept> _a = new List<RegAccept>();

		public void Add(RegRoot rg) { _a.Add(new RegAccept(rg)); }
		public void Add(RegRoot rg, int token) { _a.Add(new RegAccept(rg, token)); }
		public void Add(RegRoot rg, NFA.ActionDelegate action) { _a.Add(new RegAccept(rg, action)); }

		public void Add(string str, int token) { _a.Add(new RegAccept(RegString(str), token)); }
		public void Add(string str) { _a.Add(new RegAccept(RegString(str))); }
		public void Add(string str, NFA.ActionDelegate action) { _a.Add(new RegAccept(RegString(str), action)); }

		public void Add(char ch, int token) { _a.Add(new RegAccept(new RegToken(ch), token)); }
		public void Add(char ch) { _a.Add(new RegAccept(new RegToken(ch))); }
		public void Add(char ch, NFA.ActionDelegate action) { _a.Add(new RegAccept(new RegToken(ch), action)); }

		public IEnumerator<RegAccept> GetEnumerator() { return _a.GetEnumerator(); }
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return _a.GetEnumerator(); }

		private RegRoot RegString(string str) { return RegRoot.R(str); }
	}

	abstract class RegRoot : IAST
	{
		public abstract void CreateNFA(out NFA.Node a, out NFA.Node b);

		public static RegRoot operator |(RegRoot a, RegRoot b) { return new RegOr(a, b); }
		public static RegRoot operator |(RegRoot a, char b) { return new RegOr(a, new RegToken(b)); }
		public static RegRoot operator |(RegRoot a, string b) { return new RegOr(a, R(b)); }
		public static RegRoot operator |(char a, RegRoot b) { return new RegOr(new RegToken(a), b); }
		public static RegRoot operator |(string a, RegRoot b) { return new RegOr(R(a), b); }

		public static RegRoot operator &(RegRoot a, RegRoot b) { return new RegAnd(a, b); }
		public static RegRoot operator &(RegRoot a, char b) { return new RegAnd(a, new RegToken(b)); }
		public static RegRoot operator &(RegRoot a, string b) { return new RegAnd(a, R(b)); }
		public static RegRoot operator &(char a, RegRoot b) { return new RegAnd(new RegToken(a), b); }
		public static RegRoot operator &(string a, RegRoot b) { return new RegAnd(R(a), b); }

		public static RegRoot R(string s)
		{
			RegRoot a = new RegToken(s[0]);
			foreach (var c in s.Substring(1))
				a = new RegAnd(a, new RegToken(c));
			return a;
		}
	}

	class RegToken : RegRoot
	{
		public RegToken(int token)
		{
			_token = token;
		}
		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			a = new NFA.Node();
			b = new NFA.Node();
			a.Add(new NFA.EdgeToken(b, _token));
		}
		public readonly int _token;

		public override string ToString()
		{
			if (_token != '\'' && _token >= 32 && _token < 128)
				return U.F("new RegToken('{0}')", (char)_token);
			else
				return U.F("new RegToken({0})", _token);
		}
	}

	class RegTokenRange : RegRoot
	{
		public RegTokenRange(int sToken, int eToken)
		{
			_sToken = sToken;
			_eToken = eToken;
		}
		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			a = new NFA.Node();
			b = new NFA.Node();
			a.Add(new NFA.EdgeRange(b, _sToken, _eToken));
		}
		readonly int _sToken;
		readonly int _eToken;

		public override string ToString()
		{
			return U.F("new RegTokenRange({0}, {1})", _sToken, _eToken);
		}

	}

	class RegTokenOutsideRange : RegRoot
	{
		public RegTokenOutsideRange(int sToken, int eToken)
		{
			_sToken = sToken;
			_eToken = eToken;
		}
		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			a = new NFA.Node();
			b = new NFA.Node();
			a.Add(new NFA.EdgeOutsideRange(b, _sToken, _eToken));
		}
		public override string ToString()
		{
			return U.F("new RegTokenOutsideRange({0}, {1})", _sToken, _eToken);
		}

		readonly int _sToken;
		readonly int _eToken;
	}

	class RegOr : RegRoot
	{
		public RegOr(RegRoot a, RegRoot b)
		{
			_a = a;
			_b = b;
		}
		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			a = new NFA.Node();
			b = new NFA.Node();

			_a.CreateNFA(out NFA.Node a1, out NFA.Node a2);
			_b.CreateNFA(out NFA.Node b1, out NFA.Node b2);

			a.Add(new NFA.EdgeEmpty(a1));
			a.Add(new NFA.EdgeEmpty(b1));

			a2.Add(new NFA.EdgeEmpty(b));
			b2.Add(new NFA.EdgeEmpty(b));
		}

		public override string ToString() => U.F("new RegOr({0}, {1})", _a.ToString(), _b.ToString());

		readonly RegRoot _a, _b;
	}

	class RegAnd : RegRoot
	{
		public RegAnd(RegRoot a, RegRoot b)
		{
			_a = a;
			_b = b;
		}
		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			_a.CreateNFA(out a, out var a1);
			_b.CreateNFA(out var b1, out b);
			a1.Add(new NFA.EdgeEmpty(b1));
		}
		public override string ToString() => U.F("new RegAnd({0}, {1})", _a.ToString(), _b.ToString());
		private readonly RegRoot _a, _b;
	}

	class RegZeroOrOne : RegRoot
	{
		private readonly RegRoot _a;
		public RegZeroOrOne(RegRoot a) => _a = a;

		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			_a.CreateNFA(out a, out b);
			a.Add(new NFA.EdgeEmpty(b));
		}

		public override string ToString() => U.F("new RegZeroOrOne({0})", _a.ToString());
	}

	class RegZeroOrMore : RegRoot
	{
		readonly RegRoot _a;
		public RegZeroOrMore(RegRoot a) => _a = a;

		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			_a.CreateNFA(out a, out b);
			a.Add(new NFA.EdgeEmpty(b));
			b.Add(new NFA.EdgeEmpty(a));
		}
		public override string ToString() => U.F("new RegZeroOrMore({0})", _a.ToString());

	}

	class RegOneOrMore : RegRoot
	{
		readonly RegRoot _a;

		public RegOneOrMore(RegRoot a) => _a = a;

		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			_a.CreateNFA(out a, out b);
			b.Add(new NFA.EdgeEmpty(a));
		}
		public override string ToString() => U.F("new RegOneOrMore({0})", _a.ToString());
	}

	class RegStartLine : RegRoot
	{
		readonly RegRoot _a;

		public RegStartLine(RegRoot a) => _a = a;

		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			a = new NFA.Node();
			_a.CreateNFA(out var r, out b);
			a.Add(new NFA.EdgeStartLine(r));
		}
		public override string ToString() => U.F("new RegStartLine({0})", _a.ToString());
	}

	class RegEndLine : RegRoot
	{
		readonly RegRoot _a;

		public RegEndLine(RegRoot a) => _a = a;

		public override void CreateNFA(out NFA.Node a, out NFA.Node b)
		{
			b = new NFA.Node();
			_a.CreateNFA(out a, out var r);
			r.Add(new NFA.EdgeEndLine(b));
		}
		public override string ToString() => U.F("new RegEndLine({0})", _a.ToString());
	}

	class RegAccept
	{
		public RegAccept(RegRoot a)
		{
			this._a = a;
			this._action = (ref NFA.Token tk, LexReader rd, NFA nfa) =>
			{
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				return false;
			};
		}
		public RegAccept(RegRoot a, int token)
		{
			this._a = a;
			this._action = (ref NFA.Token tk, LexReader rd, NFA nfa) =>
			{
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				tk.token = token;
				return true;
			};
		}
		public RegAccept(RegRoot a, NFA.ActionDelegate action)
		{
			this._a = a;
			this._action = action;
		}

		public void CreateNFA(out NFA.Node a, out NFA.AcceptNode b)
		{
			NFA.Node r;
			_a.CreateNFA(out a, out r);
			b = new NFA.AcceptNode(this._action);
			r.Add(new NFA.EdgeEmpty(b));
		}
		readonly RegRoot _a;
		readonly NFA.ActionDelegate _action;
	}

	class NFA
	{
		public abstract class Edge
		{
			public Edge(Node node) => this.Node = node;
			public abstract Node Move(int token);
			public readonly Node Node;
		}

		public class EdgeRange : Edge
		{
			public EdgeRange(Node node, int tokenStart, int tokenEnd)
				: base(node)
			{
				this._tokenStart = tokenStart;
				this._tokenEnd = tokenEnd;
			}

			public override Node Move(int token)
			{
				return (token >= this._tokenStart && token <= this._tokenEnd) ? this.Node : null;
			}

			private readonly int _tokenStart;
			private readonly int _tokenEnd;
		}

		public class EdgeOutsideRange : Edge
		{
			public EdgeOutsideRange(Node node, int tokenStart, int tokenEnd)
				: base(node)
			{
				this._tokenStart = tokenStart;
				this._tokenEnd = tokenEnd;
			}

			public override Node Move(int token)
			{
				return !(token >= this._tokenStart && token <= this._tokenEnd) ? this.Node : null;
			}

			int _tokenStart;
			int _tokenEnd;
		}

		public class EdgeToken : Edge
		{
			public EdgeToken(Node node, int token)
				: base(node)
			{
				this._token = token;
			}

			public override Node Move(int token)
			{
				return (token == this._token) ? this.Node : null;
			}

			int _token;
		}

		public class EdgeStart : EdgeEmpty
		{
			public EdgeStart(int state, Node node) : base(node) => State = state;
			public readonly int State;
		}

		public class EdgeEmpty : Edge
		{
			public EdgeEmpty(Node node) : base(node) { }
			public override Node Move(int token) => null;
		}
		public class EdgeStartLine : EdgeEmpty
		{
			public EdgeStartLine(Node node) : base(node) { }
		}
		public class EdgeEndLine : EdgeEmpty
		{
			public EdgeEndLine(Node node) : base(node) { }
		}


		public class Node
		{
			public Node()
			{
			}

			public IEnumerable<Edge> Edge
			{
				get { return _edges; }
			}

			public void Add(Edge e)
			{
				this._edges.Add(e);
			}

			public void Move(List<Node> r, int a)
			{
				foreach (var e in _edges)
				{
					var t = e.Move(a);
					if (t != null && r.Contains(t) == false)
						r.Add(t);
				}
			}

			List<Edge> _edges = new List<Edge>();
		}

		public class AcceptNode : Node
		{

			public AcceptNode(ActionDelegate action)
			{
				this.Action = action;
			}

			public readonly ActionDelegate Action;
		}

		public NFA()
		{
			this.start = new NFA.Node();
			this.end = new List<NFA.AcceptNode>();
		}

		public NFA(int state, IEnumerable<RegAccept> acts)
			: this()
		{
			Add(state, acts);
		}
		public void Add(int state, IEnumerable<RegAccept> acts)
		{
			foreach (RegAccept act in acts)
			{
				NFA.Node s;
				NFA.AcceptNode e;
				act.CreateNFA(out s, out e);
				this.start.Add(new NFA.EdgeStart(state, s));
				this.end.Add(e);
			}
		}

		Node start;
		List<AcceptNode> end;
		public int State = 0;

		/*
		private static List<Node> Closure(List<Node> s)
		{
			var ret = new List<Node>();
			var k = new Stack<Node>();
			foreach (var n in s)
			{
				ret.Add(n);
				k.Push(n);
			}

			while (k.Count > 0)
			{
				var n = k.Pop();
				foreach (var e in n.Edge)
				{
					EdgeEmpty ee = e as EdgeEmpty;
					if (ee != null && ret.Contains(ee.Node) == false)
					{
						k.Push(ee.Node);
						ret.Add(ee.Node);
					}
				}
			}
			return ret;
		}
		*/
		private static List<Node> Closure(List<Node> s, bool startOfLine, bool endOfLine, int State)
		{
			var ret = new List<Node>();
			var k = new Stack<Node>();
			foreach (var n in s)
			{
				ret.Add(n);
				k.Push(n);
			}

			while (k.Count > 0)
			{
				var n = k.Pop();
				foreach (var e in n.Edge)
				{
					EdgeEmpty ee = e as EdgeEmpty;
					if (ee == null) continue;

					bool ok = true;
					EdgeStartLine ea = e as EdgeStartLine;
					if (ea != null)
						ok = startOfLine;
					EdgeEndLine eb = e as EdgeEndLine;
					if (eb != null)
						ok = endOfLine;
					EdgeStart ec = e as EdgeStart;
					if (ec != null)
						ok = State == ec.State;

					if (ok && ret.Contains(ee.Node) == false)
					{
						k.Push(ee.Node);
						ret.Add(ee.Node);
					}
				}
			}
			return ret;
		}


		private static List<Node> Move(List<Node> S, int a)
		{
			var ret = new List<Node>();
			foreach (var s in S)
				s.Move(ret, a);
			return ret;
		}

		public struct Token
		{
			public int token;
			public string strRead;
			public string fileName;
			public int line;

			public int startIndex;
			public int endIndex;

			public override string ToString()
			{
				return U.F("{0}({1}): token={2} value={3} start={4} end={5}", this.fileName, this.line, token, strRead, startIndex, endIndex);
			}
		}

		public delegate bool ActionDelegate(ref Token a, LexReader rd, NFA nfa);

		public Token ReadToken(LexReader rd)
		{
			if (_pb.Count > 0)
			{
				Token tk = _pb[0];
				_pb.RemoveAt(0);
				return tk;
			}

			// \r ==> 13
			// \n ==> 10

			for (; ; )
			{
				var matchNodes = new List<AcceptNode>();
				var b = rd.Peek();

				var S = new List<Node>() { this.start };
				S = Closure(S, b.startLine, b.endLine, this.State);

				while (b.ch != -1)
				{
					var Sn = Closure(Move(S, b.ch), b.startLine, b.endLine, this.State);
					if (Sn.Count() == 0)
						break; // il char <a> non è riconosciuto in nessuna sequenza.
					rd.Read();

					var m = end.FindAll(n => Sn.Contains(n));
					if (m.Count > 0)
					{
						matchNodes = m;
						rd.SetMatch();
					}
					S = Sn;
					b = rd.Peek();
				}

				var match = end.Find(n => matchNodes.Contains(n));
				if (match != null)
				{
					Token ret;
					ret.token = 0;
					ret.fileName = null;
					ret.line = 0;
					ret.strRead = null;
					ret.startIndex = 0;
					ret.endIndex = 0;

					if (match.Action(ref ret, rd, this))
						return ret;
				}
				else if (b.ch == -1)
				{
					Token ret;
					ret.token = -1;
					rd.EndToken(out ret.strRead, out ret.fileName, out ret.line, out ret.startIndex, out ret.endIndex);
					if (ret.strRead == "")
						return ret;
				}
				else
					throw new SyntaxError(rd.FileName, b.line, "Unrecognized char '{0}'", (char)b.ch);
			}
		}

		public void PushBack(Token tk)
		{
			_pb.Add(tk);
		}

		List<Token> _pb = new List<Token>();
	}

	class LexReader : IDisposable
	{
		List<StartEndChar> _ch;
		int _idxMatch;
		int _idxNext;
		string _fileName;
		StartEndTextReader _rd;

		public struct StartEndChar
		{
			public int index;
			public bool startLine;
			public int ch;
			public bool endLine;

			public int line;
			public int col;

			public override string ToString()
			{
				if (ch >= 32)
					return U.F("index='{5}' ch='{0}' startLine={1} endLine={2} line={3} col={4}", (char)ch, startLine, endLine, line, col, index);
				else
					return U.F("index='{5}' ch={0} startLine={1} endLine={2} line={3} col={4}", ch, startLine, endLine, line, col, index);
			}
		}
		class StartEndTextReader : IDisposable
		{
			private int _index;

			public StartEndTextReader(TextReader rd)
			{
				_rd = rd;
				_a = '\n';
				_b = ReadChar();
				_c = _b != -1 ? ReadChar() : -1;
				_d = _c != -1 ? ReadChar() : -1;

				// Sono del carattere di cui si e` appena fatto read.
				// siccome non ho fatto ancora ReadChar la linea e` zero.
				// E siccome il precedente char e` \n la prossima linea sara` 1
				_line = 0;
				_col = 0;
			}

			private int ReadChar()
			{
				int ch = _rd.Read();
				if (ch == '\r' && _rd.Peek() == '\n')
					ch = _rd.Read();

				return ch;
			}

			public StartEndChar Read()
			{
				if (_a == '\n')
				{
					_line += 1;
					_col = 1;
				}
				else
					_col += 1;

				StartEndChar r;
				r.index = _index++;
				r.startLine = _a == '\n';
				r.ch = _b;
				r.endLine = _c == '\n' || _c == -1;

				r.line = _line;
				r.col = _col;

				_a = _b;
				_b = _c;
				_c = _d;
				_d = (_c != -1) ? ReadChar() : -1;

				return r;
			}
			public StartEndChar Peek()
			{
				StartEndChar r;
				r.index = _index;
				r.startLine = _b == '\n';
				r.ch = _c;
				r.endLine = _d == '\n' || _d == -1;

				if (_b == '\n')
				{
					r.line = _line + 1;
					r.col = 1;
				}
				else
				{
					r.line = _line;
					r.col = _col + 1;
				}

				return r;
			}

			TextReader _rd;
			int _a;
			int _b;
			int _c;
			int _d;

			int _line;
			int _col;

			public void Dispose()
			{
				_rd.Dispose();
			}
		}

		public LexReader(string fileName)
		{
			_rd = new StartEndTextReader(File.OpenText(fileName));
			_idxNext = 0;
			_idxMatch = 0;
			_fileName = fileName;
			_ch = new List<StartEndChar>();
		}

		public LexReader(TextReader rd, string fileName)
		{
			_rd = new StartEndTextReader(rd);
			_fileName = fileName;
			_idxNext = 0;
			_idxMatch = 0;

			_ch = new List<StartEndChar>();
		}

		public string FileName { get { return _fileName; } }

		public void Dispose()
		{
			if (_rd != null)
			{
				_rd.Dispose();
				_rd = null;
			}
		}

		public void EndToken(out string s, out string file, out int line, out int startIndex, out int endIndex)
		{
			StringBuilder sb = new StringBuilder();
			line = _ch[0].line;
			file = _fileName;
			startIndex = _ch[0].index;
			endIndex = _ch[^1].index;
			for (int i = 0; i < this._idxMatch; ++i)
				sb.Append((char)this._ch[i].ch);

			s = sb.ToString();

			_ch.RemoveRange(0, _idxMatch);
			_idxMatch = 0;
			_idxNext = 0;
		}

		public StartEndChar Peek()
		{
			if (this._idxNext >= _ch.Count)
				_ch.Add(_rd.Read());

			return _ch[_idxNext];
		}

		public StartEndChar Read()
		{
			if (this._idxNext >= _ch.Count)
				_ch.Add(_rd.Read());

			return _ch[_idxNext++];
		}

		/// <summary>
		/// Imposta l'ultimo carattere letto con Read come match
		/// </summary>
		public void SetMatch()
		{
			_idxMatch = _idxNext;
		}

		/// <summary>
		/// Serve per rimettere i caratteri letti ma non confermati con SetMatch
		/// in modo che al prossima lettura si ri-inizi da capo.
		/// </summary>
		public void ForgetAll()
		{
			_idxMatch = 0;
			_idxNext = 0;
		}
	}

	static class LexerTest
	{
		public static void Test()
		{
			int state = 0;

			var acts = new RegAcceptList();
			acts.Add("leo", 1);
			acts.Add("leon", 2);
			acts.Add(new RegOneOrMore(new RegTokenRange('a', 'z')), 3);
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
			acts.Add(' ');
			acts.Add('\n');

			NFA net = new NFA(state, acts);

			using (var rd = new LexReader("leo.txt"))
			{
				try
				{
					for (; ; )
					{
						NFA.Token ret = net.ReadToken(rd);

						if (ret.strRead != "\n")
							Console.WriteLine("token={0} value=\"{1}\" line={2}", ret.token, ret.strRead, ret.line);
						else
							Console.WriteLine("token={0} value=\"\\n\" line={1}", ret.token, ret.line);

						if (ret.token == -1)
							break;
					}
				}
				catch (Exception ex)
				{
					Console.Write(ex.Message);
				}
			}
		}
	}
}
