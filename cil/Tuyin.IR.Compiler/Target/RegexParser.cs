#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

using LLParserLexerLib;
using System.Text;
using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Target
{
    partial class RegexParser : ParserBase
	{
		public const int LITERAL = -2;
		public const int UNTIL = -3;
		public const int INGETER = -4;
		
		Dictionary<int, string> _token;
		public override Dictionary<int, string> Token
		{
			get
			{
				if (_token == null)
				{
					_token = new Dictionary<int, string>();
					_token.Add(-1, "EOF");
					_token.Add(-2, "LITERAL");
					_token.Add(-3, "UNTIL");
					_token.Add(-4, "INGETER");
				}
				return _token;
			}
		}
		
		Regex regex(IAST regex_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			Regex regex_s = default(Regex);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = or(nt1_i);
					regex_s = new Regex(nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case -1:
				break;
			default:
				Error();
				break;
			}
			return regex_s;
		}
		
		SourceExpression or(IAST or_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression or_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = next(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_1(nt2_i);
					or_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return or_s;
		}
		
		SourceExpression tmp_1(IAST tmp_1_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ',':
			case ')':
			case -1:
				alt = 0;
				break;
			case '|':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression tmp_1_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_1_s = (SourceExpression)tmp_1_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('|', nt1_i);
					var nt2_s = next(nt2_i);
					nt3_i = (((SourceExpression)tmp_1_i) | nt2_s).SetSpan(StartIndex, ((SourceExpression)tmp_1_i), nt2_s); 
					var nt3_s = tmp_1(nt3_i);
					tmp_1_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_1_s;
		}
		
		SourceExpression next(IAST next_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression next_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = empty(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_2(nt2_i);
					next_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return next_s;
		}
		
		SourceExpression tmp_2(IAST tmp_2_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '|':
			case ',':
			case ')':
			case -1:
				alt = 0;
				break;
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression tmp_2_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_2_s = (SourceExpression)tmp_2_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = empty(nt1_i);
					nt2_i = (((SourceExpression)tmp_2_i) > nt1_s).SetSpan(StartIndex, ((SourceExpression)tmp_2_i), nt1_s); 
					var nt2_s = tmp_2(nt2_i);
					tmp_2_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_2_s;
		}
		
		SourceExpression empty(IAST empty_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression empty_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = many(nt1_i);
					var nt2_s = tmp_3(nt1_s, nt2_i);
					empty_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return empty_s;
		}
		
		SourceExpression tmp_3(IAST nt1_s, IAST tmp_3_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '?':
				alt = 0;
				break;
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression tmp_3_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					
					TokenAST nt2_s = Match('?', nt2_i);
					tmp_3_s = ((SourceExpression)nt1_s).Optional().SetSpan(StartIndex, ((SourceExpression)nt1_s), nt2_s);
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					
					tmp_3_s = ((SourceExpression)nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_3_s;
		}
		
		SourceExpression many(IAST many_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression many_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = many1(nt1_i);
					var nt2_s = tmp_4(nt1_s, nt2_i);
					many_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return many_s;
		}
		
		SourceExpression tmp_4(IAST nt1_s, IAST tmp_4_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '*':
				alt = 0;
				break;
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression tmp_4_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					
					TokenAST nt2_s = Match('*', nt2_i);
					tmp_4_s = ((SourceExpression)nt1_s).Many().SetSpan(StartIndex, ((SourceExpression)nt1_s), nt2_s);
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					
					tmp_4_s = ((SourceExpression)nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_4_s;
		}
		
		SourceExpression many1(IAST many1_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression many1_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = literal(nt1_i);
					var nt2_s = tmp_5(nt1_s, nt2_i);
					many1_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '*':
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return many1_s;
		}
		
		SourceExpression tmp_5(IAST nt1_s, IAST tmp_5_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '+':
				alt = 0;
				break;
			case '*':
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression tmp_5_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					
					TokenAST nt2_s = Match('+', nt2_i);
					tmp_5_s = ((SourceExpression)nt1_s).Many1().SetSpan(StartIndex, ((SourceExpression)nt1_s), nt2_s);
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					
					tmp_5_s = ((SourceExpression)nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '*':
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_5_s;
		}
		
		SourceExpression literal(IAST literal_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LITERAL:
				alt = 0;
				break;
			case '[':
				alt = 1;
				break;
			case UNTIL:
				alt = 2;
				break;
			case '{':
				alt = 3;
				break;
			case '.':
				alt = 4;
				break;
			case '(':
				alt = 5;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression literal_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(LITERAL, nt1_i);
					literal_s = Literal(nt1_s).SetSpan(StartIndex, nt1_s);
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match('[', nt1_i);
					var nt2_s = tmp_6(nt1_s, nt2_i);
					literal_s = nt2_s;
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(UNTIL, nt1_i);
					TokenAST nt2_s = Match(LITERAL, nt2_i);
					literal_s = Until(nt2_s).SetSpan(StartIndex, nt1_s, nt2_s);
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt1_s = Match('{', nt1_i);
					var nt2_s = or(nt2_i);
					TokenAST nt3_s = Match(',', nt3_i);
					TokenAST nt4_s = Match(INGETER, nt4_i);
					TokenAST nt5_s = Match('}', nt5_i);
					literal_s = Repeat(nt2_s, nt4_s).SetSpan(StartIndex, nt1_s, nt5_s);
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match('.', nt1_i);
					literal_s = RegularExpression.Any().SetSpan(StartIndex, nt1_s);
				}
				break;
			case 5:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('(', nt1_i);
					var nt2_s = or(nt2_i);
					TokenAST nt3_s = Match(')', nt3_i);
					literal_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '+':
			case '*':
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return literal_s;
		}
		
		SourceExpression tmp_6(IAST nt1_s, IAST tmp_6_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LITERAL:
				alt = 0;
				break;
			case '^':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression tmp_6_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt2_s = Match(LITERAL, nt2_i);
					var nt3_s = tmp_7(nt1_s, nt2_s, nt3_i);
					tmp_6_s = nt3_s;
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt2_s = Match('^', nt2_i);
					TokenAST nt3_s = Match(LITERAL, nt3_i);
					TokenAST nt4_s = Match(']', nt4_i);
					tmp_6_s = Except(nt3_s).SetSpan(StartIndex, ((TokenAST)nt1_s), nt4_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '+':
			case '*':
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_6_s;
		}
		
		SourceExpression tmp_7(IAST nt1_s, IAST nt2_s, IAST tmp_7_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ']':
				alt = 0;
				break;
			case '-':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceExpression tmp_7_s = default(SourceExpression);
			switch (alt)
			{
			case 0:
				{
					var nt3_i = default(IAST);
					
					TokenAST nt3_s = Match(']', nt3_i);
					tmp_7_s = CharSet(((TokenAST)nt2_s)).SetSpan(StartIndex, ((TokenAST)nt1_s), nt3_s);
				}
				break;
			case 1:
				{
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt3_s = Match('-', nt3_i);
					TokenAST nt4_s = Match(LITERAL, nt4_i);
					TokenAST nt5_s = Match(']', nt5_i);
					tmp_7_s = Range(((TokenAST)nt1_s), nt5_s).SetSpan(StartIndex, ((TokenAST)nt2_s), nt4_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '+':
			case '*':
			case '?':
			case LITERAL:
			case '[':
			case UNTIL:
			case '{':
			case '.':
			case '(':
			case '|':
			case ',':
			case ')':
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_7_s;
		}
		
		protected override RegAcceptList CreateRegAcceptList()
		{
			var acts = new RegAcceptList();
			acts.Add(new RegToken('|'), '|');
			acts.Add(new RegToken('('), '(');
			acts.Add(new RegToken(')'), ')');
			acts.Add(new RegToken('.'), '.');
			acts.Add(new RegToken(':'), ':');
			acts.Add(new RegToken(';'), ';');
			acts.Add(new RegToken('+'), '+');
			acts.Add(new RegToken('*'), '*');
			acts.Add(new RegToken('?'), '?');
			acts.Add(new RegToken('.'), '.');
			acts.Add(new RegToken('^'), '^');
			acts.Add(new RegToken('['), '[');
			acts.Add(new RegToken(']'), ']');
			acts.Add(new RegAnd(new RegToken('.'), new RegToken('.')), UNTIL);
			acts.Add(new RegOneOrMore(new RegTokenRange(48, 57)), INGETER);
			acts.Add(new RegTokenOutsideRange(10, 10), (ref NFA.Token tk, LexReader rd, NFA nfa) => {
				var sb = new StringBuilder();
				ReadLiteral(rd, sb);
				rd.SetMatch();
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				tk.strRead = sb.ToString();
				tk.token = LITERAL;
				return true;
			});
			acts.Add(new RegAnd(new RegToken('/'), new RegToken('*')), (ref NFA.Token tk, LexReader rd, NFA nfa) => {
				ReadComment(rd);
				rd.SetMatch();
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				return false;
			});
			acts.Add(new RegAnd(new RegAnd(new RegToken('/'), new RegToken('/')), new RegZeroOrMore(new RegTokenOutsideRange(10, 10))));
			acts.Add(new RegOneOrMore(new RegOr(new RegOr(new RegOr(new RegToken(' '), new RegToken(10)), new RegToken(13)), new RegToken(9))));
			return acts;
		}
	}
}
