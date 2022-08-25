#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

using System;
using System.Text;
using System.Collections.Generic;
using LLParserLexerLib;

partial class RegexprParser : ParserBase
{
	public const int ID = -2;
	public const int CODE = -3;
	public const int CH_LIT = -4;
	public const int CH_NOLIT = -5;
	public const int STRING = -6;


	Dictionary<int, string> _token;
	public override Dictionary<int, string> Token
	{
		get
		{
			if (_token == null)
			{
				_token = new Dictionary<int, string>();
				_token.Add(-1, "EOF");
				_token.Add(-2, "ID");
				_token.Add(-3, "CODE");
				_token.Add(-4, "CH_LIT");
				_token.Add(-5, "CH_NOLIT");
				_token.Add(-6, "STRING");
			}
			return _token;
		}
	}
	
	IAST LexParser(IAST LexParser_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '^':
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
			alt = 0;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST LexParser_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				
				IAST nt1_s = r(nt1_i);
			}
			break;
		}
		
		switch (Next.token)
		{
		case -1:
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return LexParser_s;
	}
	
	IAST r(IAST r_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '^':
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
			alt = 0;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST r_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				IAST nt1_s = e(nt1_i);
				IAST nt2_s = tmp_1(nt2_i);
			}
			break;
		}
		
		switch (Next.token)
		{
		case -1:
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return r_s;
	}
	
	IAST tmp_1(IAST tmp_1_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case -1:
			alt = 0;
			break;
		case '^':
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
			alt = 1;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST tmp_1_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				
				tmp_1_s = tmp_1_i; 
			}
			break;
		case 1:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				IAST nt1_s = e(nt1_i);
				IAST nt2_s = tmp_1(nt2_i);
			}
			break;
		}
		
		switch (Next.token)
		{
		case -1:
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return tmp_1_s;
	}
	
	IAST e(IAST e_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '^':
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
			alt = 0;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST e_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				IAST nt1_s = c(nt1_i);
				var nt2_s = tmp_5(nt1_s, nt2_i);
				e_s = nt2_s;
			}
			break;
		}
		
		switch (Next.token)
		{
		case '^':
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
		case -1:
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return e_s;
	}
	
	IAST tmp_5(IAST nt1_s, IAST tmp_5_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case ID:
			alt = 0;
			break;
		case CODE:
			alt = 1;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST tmp_5_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt2_i = default(IAST);
				
				TokenAST nt2_s = Match(ID, nt2_i);
				AddRole((RegRoot)nt1_s, nt2_s.strRead); 
			}
			break;
		case 1:
			{
				var nt2_i = default(IAST);
				
				TokenAST nt2_s = Match(CODE, nt2_i);
				AddRole((RegRoot)nt1_s, nt2_s.strRead); 
			}
			break;
		}
		
		switch (Next.token)
		{
		case '^':
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
		case -1:
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return tmp_5_s;
	}
	
	IAST c(IAST c_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
			alt = 0;
			break;
		case '^':
			alt = 1;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST c_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				IAST nt1_s = c1(nt1_i);
				var nt2_s = tmp_6(nt1_s, nt2_i);
				c_s = nt2_s;
			}
			break;
		case 1:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				var nt3_i = default(IAST);
				
				TokenAST nt1_s = Match('^', nt1_i);
				IAST nt2_s = c1(nt2_i);
				var nt3_s = tmp_7(nt1_s, nt2_s, nt3_i);
				c_s = nt3_s;
			}
			break;
		}
		
		switch (Next.token)
		{
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return c_s;
	}
	
	IAST tmp_6(IAST nt1_s, IAST tmp_6_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case ID:
		case CODE:
		case ')':
			alt = 0;
			break;
		case '$':
			alt = 1;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST tmp_6_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt2_i = default(IAST);
				
				tmp_6_s = nt1_s;
			}
			break;
		case 1:
			{
				var nt2_i = default(IAST);
				
				TokenAST nt2_s = Match('$', nt2_i);
				tmp_6_s = new RegEndLine((RegRoot)nt1_s);
			}
			break;
		}
		
		switch (Next.token)
		{
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return tmp_6_s;
	}
	
	IAST tmp_7(IAST nt1_s, IAST nt2_s, IAST tmp_7_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case ID:
		case CODE:
		case ')':
			alt = 0;
			break;
		case '$':
			alt = 1;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST tmp_7_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt3_i = default(IAST);
				
				tmp_7_s = new RegStartLine((RegRoot)nt2_s);
			}
			break;
		case 1:
			{
				var nt3_i = default(IAST);
				
				TokenAST nt3_s = Match('$', nt3_i);
				tmp_7_s = new RegStartLine(new RegEndLine((RegRoot)nt2_s));
			}
			break;
		}
		
		switch (Next.token)
		{
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return tmp_7_s;
	}
	
	IAST c1(IAST c1_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
			alt = 0;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST c1_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				IAST nt1_s = t(nt1_i);
				nt2_i = nt1_s; 
				IAST nt2_s = tmp_2(nt2_i);
				c1_s = nt2_s; 
			}
			break;
		}
		
		switch (Next.token)
		{
		case '$':
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return c1_s;
	}
	
	IAST tmp_2(IAST tmp_2_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '$':
		case ID:
		case CODE:
		case ')':
			alt = 0;
			break;
		case '|':
			alt = 1;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST tmp_2_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				
				tmp_2_s = tmp_2_i; 
			}
			break;
		case 1:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				var nt3_i = default(IAST);
				
				TokenAST nt1_s = Match('|', nt1_i);
				IAST nt2_s = t(nt2_i);
				nt3_i = new RegOr((RegRoot)tmp_2_i, (RegRoot)nt2_s); 
				IAST nt3_s = tmp_2(nt3_i);
				tmp_2_s = nt3_s; 
			}
			break;
		}
		
		switch (Next.token)
		{
		case '$':
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return tmp_2_s;
	}
	
	IAST t(IAST t_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
			alt = 0;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST t_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				IAST nt1_s = se(nt1_i);
				nt2_i = nt1_s; 
				IAST nt2_s = tmp_3(nt2_i);
				t_s = nt2_s; 
			}
			break;
		}
		
		switch (Next.token)
		{
		case '|':
		case '$':
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return t_s;
	}
	
	IAST tmp_3(IAST tmp_3_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '|':
		case '$':
		case ID:
		case CODE:
		case ')':
			alt = 0;
			break;
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
			alt = 1;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST tmp_3_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				
				tmp_3_s = tmp_3_i; 
			}
			break;
		case 1:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				IAST nt1_s = se(nt1_i);
				nt2_i = new RegAnd((RegRoot)tmp_3_i, (RegRoot)nt1_s); 
				IAST nt2_s = tmp_3(nt2_i);
				tmp_3_s = nt2_s; 
			}
			break;
		}
		
		switch (Next.token)
		{
		case '|':
		case '$':
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return tmp_3_s;
	}
	
	IAST se(IAST se_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '(':
			alt = 0;
			break;
		case '.':
			alt = 1;
			break;
		case '[':
			alt = 2;
			break;
		case CH_LIT:
			alt = 3;
			break;
		case CH_NOLIT:
			alt = 4;
			break;
		case STRING:
			alt = 5;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST se_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				var nt3_i = default(IAST);
				var nt4_i = default(IAST);
				
				TokenAST nt1_s = Match('(', nt1_i);
				IAST nt2_s = c(nt2_i);
				TokenAST nt3_s = Match(')', nt3_i);
				nt4_i = nt2_s; 
				IAST nt4_s = tmp_4(nt4_i);
				se_s = nt4_s; 
			}
			break;
		case 1:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				TokenAST nt1_s = Match('.', nt1_i);
				nt2_i = CreateAny(); 
				IAST nt2_s = tmp_4(nt2_i);
				se_s = nt2_s; 
			}
			break;
		case 2:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				var nt3_i = default(IAST);
				var nt4_i = default(IAST);
				
				TokenAST nt1_s = Match('[', nt1_i);
				IAST nt2_s = rg(nt2_i);
				TokenAST nt3_s = Match(']', nt3_i);
				nt4_i = nt2_s; 
				IAST nt4_s = tmp_4(nt4_i);
				se_s = nt4_s; 
			}
			break;
		case 3:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				TokenAST nt1_s = Match(CH_LIT, nt1_i);
				nt2_i = CreateToken(nt1_s); 
				IAST nt2_s = tmp_4(nt2_i);
				se_s = nt2_s; 
			}
			break;
		case 4:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				TokenAST nt1_s = Match(CH_NOLIT, nt1_i);
				nt2_i = CreateToken(nt1_s); 
				IAST nt2_s = tmp_4(nt2_i);
				se_s = nt2_s; 
			}
			break;
		case 5:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				TokenAST nt1_s = Match(STRING, nt1_i);
				nt2_i = CreateString(nt1_s); 
				IAST nt2_s = tmp_4(nt2_i);
				se_s = nt2_s; 
			}
			break;
		}
		
		switch (Next.token)
		{
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
		case '|':
		case '$':
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return se_s;
	}
	
	IAST tmp_4(IAST tmp_4_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
		case '|':
		case '$':
		case ID:
		case CODE:
		case ')':
			alt = 0;
			break;
		case '*':
			alt = 1;
			break;
		case '+':
			alt = 2;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST tmp_4_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				
				tmp_4_s = tmp_4_i; 
			}
			break;
		case 1:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				TokenAST nt1_s = Match('*', nt1_i);
				nt2_i = new RegZeroOrMore((RegRoot)tmp_4_i); 
				IAST nt2_s = tmp_4(nt2_i);
				tmp_4_s = nt2_s; 
			}
			break;
		case 2:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				
				TokenAST nt1_s = Match('+', nt1_i);
				nt2_i = new RegOneOrMore((RegRoot)tmp_4_i); 
				IAST nt2_s = tmp_4(nt2_i);
				tmp_4_s = nt2_s; 
			}
			break;
		}
		
		switch (Next.token)
		{
		case '(':
		case '.':
		case '[':
		case CH_LIT:
		case CH_NOLIT:
		case STRING:
		case '|':
		case '$':
		case ID:
		case CODE:
		case ')':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return tmp_4_s;
	}
	
	IAST rg(IAST rg_i)
	{
		int alt = 0;
		switch (Next.token)
		{
		case CH_NOLIT:
			alt = 0;
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		
		IAST rg_s = default(IAST);
		switch (alt)
		{
		case 0:
			{
				var nt1_i = default(IAST);
				var nt2_i = default(IAST);
				var nt3_i = default(IAST);
				
				TokenAST nt1_s = Match(CH_NOLIT, nt1_i);
				TokenAST nt2_s = Match('-', nt2_i);
				TokenAST nt3_s = Match(CH_NOLIT, nt3_i);
				rg_s = CreateRange(nt1_s, nt3_s);
			}
			break;
		}
		
		switch (Next.token)
		{
		case ']':
			break;
		default:
			throw new SyntaxError(Next.fileName, Next.lineNu, "unexpected token '{0}'", Next.strRead);
		}
		return rg_s;
	}
	
	protected override RegAcceptList CreateRegAcceptList()
	{
		var acts = new RegAcceptList();
		acts.Add(new RegAnd(new RegOr(new RegOr(new RegToken('_'), new RegTokenRange(97, 122)), new RegTokenRange(65, 90)), new RegZeroOrMore(new RegOr(new RegOr(new RegOr(new RegToken('_'), new RegTokenRange(97, 122)), new RegTokenRange(65, 90)), new RegTokenRange(48, 57)))), ID);
		acts.Add(new RegAnd(new RegAnd(new RegToken('/'), new RegToken('/')), new RegZeroOrMore(new RegTokenOutsideRange(10, 10))));
		return acts;
	}
}
