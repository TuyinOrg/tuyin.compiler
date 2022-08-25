#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

using System;
using System.Text;
using LLParserLexerLib;
using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Symbols
{
	partial class DIMetadataParser : ParserBase
	{
		public const int ID = -2;
		public const int DISSTINCT = -3;
		public const int CHS = -4;
		public const int INTEGER = -5;
		public const int FLOAT = -6;
		
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
					_token.Add(-3, "DISSTINCT");
					_token.Add(-4, "CHS");
					_token.Add(-5, "INTEGER");
					_token.Add(-6, "FLOAT");
				}
				return _token;
			}
		}
		
		Metadatas metadatas(IAST metadatas_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case -1:
				alt = 0;
				break;
			case ID:
			case DISSTINCT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			Metadatas metadatas_s = default(Metadatas);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					metadatas_s = new Metadatas();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = tmp_1(nt1_i);
					metadatas_s = nt1_s;
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
			return metadatas_s;
		}
		
		Metadatas tmp_1(IAST tmp_1_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ID:
			case DISSTINCT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			Metadatas tmp_1_s = default(Metadatas);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = metadata(nt1_i);
					nt2_i = (new Metadatas()).Add(nt1_s); 
					var nt2_s = tmp_3(nt2_i);
					tmp_1_s = nt2_s; 
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
			return tmp_1_s;
		}
		
		Metadatas tmp_3(IAST tmp_3_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case -1:
				alt = 0;
				break;
			case ID:
			case DISSTINCT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			Metadatas tmp_3_s = default(Metadatas);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_3_s = (Metadatas)tmp_3_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = metadata(nt1_i);
					nt2_i = ((Metadatas)tmp_3_i).Add(nt1_s); 
					var nt2_s = tmp_3(nt2_i);
					tmp_3_s = nt2_s; 
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
			return tmp_3_s;
		}
		
		DIMetadata metadata(IAST metadata_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ID:
			case DISSTINCT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			DIMetadata metadata_s = default(DIMetadata);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					var nt1_s = header(nt1_i);
					TokenAST nt2_s = Match('(', nt2_i);
					var nt3_s = properties(nt3_i);
					TokenAST nt4_s = Match(')', nt4_i);
					metadata_s = new DIMetadata(nt1_s, nt3_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case ID:
			case DISSTINCT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return metadata_s;
		}
		
		DIHeader header(IAST header_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ID:
				alt = 0;
				break;
			case DISSTINCT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			DIHeader header_s = default(DIHeader);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					header_s = new DIHeader(new DIToken(nt1_s.strRead, nt1_s.StartIndex, nt1_s.EndIndex - nt1_s.StartIndex));
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = modifiter(nt1_i);
					var nt2_s = header(nt2_i);
					header_s = nt2_s.SetModifiter(nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '(':
				break;
			default:
				Error();
				break;
			}
			return header_s;
		}
		
		DIModifiter modifiter(IAST modifiter_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case DISSTINCT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			DIModifiter modifiter_s = default(DIModifiter);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(DISSTINCT, nt1_i);
					modifiter_s = DIModifiter.Disstinct;
				}
				break;
			}
			
			switch (Next.token)
			{
			case ID:
			case DISSTINCT:
				break;
			default:
				Error();
				break;
			}
			return modifiter_s;
		}
		
		DIProperties properties(IAST properties_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ')':
				alt = 0;
				break;
			case ',':
			case ID:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			DIProperties properties_s = default(DIProperties);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					properties_s = new DIProperties();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = tmp_2(nt1_i);
					properties_s = nt1_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case ')':
				break;
			default:
				Error();
				break;
			}
			return properties_s;
		}
		
		DIProperties tmp_2(IAST tmp_2_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ID:
				alt = 0;
				break;
			case ',':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			DIProperties tmp_2_s = default(DIProperties);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = property(nt1_i);
					nt2_i = new DIProperties(nt1_s); 
					var nt2_s = tmp_4(nt2_i);
					tmp_2_s = nt2_s; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(',', nt1_i);
					var nt2_s = property(nt2_i);
					nt3_i = (new DIProperties()).Add(nt2_s); 
					var nt3_s = tmp_4(nt3_i);
					tmp_2_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ')':
				break;
			default:
				Error();
				break;
			}
			return tmp_2_s;
		}
		
		DIProperties tmp_4(IAST tmp_4_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ')':
				alt = 0;
				break;
			case ',':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			DIProperties tmp_4_s = default(DIProperties);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_4_s = (DIProperties)tmp_4_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(',', nt1_i);
					var nt2_s = property(nt2_i);
					nt3_i = ((DIProperties)tmp_4_i).Add(nt2_s); 
					var nt3_s = tmp_4(nt3_i);
					tmp_4_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ')':
				break;
			default:
				Error();
				break;
			}
			return tmp_4_s;
		}
		
		DIProperty property(IAST property_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ID:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			DIProperty property_s = default(DIProperty);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					TokenAST nt2_s = Match(':', nt2_i);
					var nt3_s = value(nt3_i);
					property_s = new DIProperty(new DIToken(nt1_s.strRead, nt1_s.StartIndex, nt1_s.EndIndex - nt1_s.StartIndex), nt3_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case ',':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return property_s;
		}
		
		DIExpression value(IAST value_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CHS:
				alt = 0;
				break;
			case INTEGER:
				alt = 1;
				break;
			case FLOAT:
				alt = 2;
				break;
			default:
				Error();
				break;
			}
			
			DIExpression value_s = default(DIExpression);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(CHS, nt1_i);
					value_s = new DIExpression<string>(nt1_s.strRead, new DIToken(nt1_s.strRead, nt1_s.StartIndex, nt1_s.EndIndex - nt1_s.StartIndex));
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(INTEGER, nt1_i);
					value_s = new DIExpression<int>(int.Parse(nt1_s.strRead), new DIToken(nt1_s.strRead, nt1_s.StartIndex, nt1_s.EndIndex - nt1_s.StartIndex));
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(FLOAT, nt1_i);
					value_s = new DIExpression<float>(float.Parse(nt1_s.strRead), new DIToken(nt1_s.strRead, nt1_s.StartIndex, nt1_s.EndIndex - nt1_s.StartIndex));
				}
				break;
			}
			
			switch (Next.token)
			{
			case ',':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return value_s;
		}
		
		protected override RegAcceptList CreateRegAcceptList()
		{
			var acts = new RegAcceptList();
			acts.Add(new RegToken('.'), '.');
			acts.Add(new RegToken('('), '(');
			acts.Add(new RegToken(')'), ')');
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('d'), new RegToken('i')), new RegToken('s')), new RegToken('t')), new RegToken('i')), new RegToken('n')), new RegToken('c')), new RegToken('t')), DISSTINCT);
			acts.Add(new RegAnd(new RegOr(new RegOr(new RegToken('_'), new RegTokenRange(97, 122)), new RegTokenRange(65, 90)), new RegZeroOrMore(new RegOr(new RegOr(new RegOr(new RegToken('_'), new RegTokenRange(97, 122)), new RegTokenRange(65, 90)), new RegTokenRange(48, 57)))), ID);
			acts.Add(new RegOr(new RegAnd(new RegToken('-'), new RegOneOrMore(new RegTokenRange(48, 57))), new RegOneOrMore(new RegTokenRange(48, 57))), INTEGER);
			acts.Add(new RegOr(new RegAnd(new RegToken('-'), new RegOneOrMore(new RegTokenRange(48, 57))), new RegAnd(new RegAnd(new RegOneOrMore(new RegTokenRange(48, 57)), new RegTokenOutsideRange(10, 10)), new RegOneOrMore(new RegTokenRange(48, 57)))), FLOAT);
			acts.Add(new RegToken(39), (ref NFA.Token tk, LexReader rd, NFA nfa) => {
				var sb = new StringBuilder();
				ReadString(rd, sb);
				rd.SetMatch();
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				tk.strRead = sb.ToString();
				tk.token = CHS;
				return true;
			});
			acts.Add(new RegAnd(new RegToken('/'), new RegToken('*')), (ref NFA.Token tk, LexReader rd, NFA nfa) => {
				ReadComment(rd);
				rd.SetMatch();
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				return false;
			});
			acts.Add(new RegAnd(new RegAnd(new RegToken('/'), new RegToken('/')), new RegZeroOrMore(new RegTokenOutsideRange(10, 10))));
			return acts;
		}
	}
}
