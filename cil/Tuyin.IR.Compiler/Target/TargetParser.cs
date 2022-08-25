#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

using LLParserLexerLib;
using System.Text;
using Tuyin.IR.Compiler.Parser.Productions;
using Tuyin.IR.Compiler.Target.Translates;
using Tuyin.IR.Reflection.Types;

namespace Tuyin.IR.Compiler.Target
{
    partial class TargetParser : ParserBase
	{
		public const int NAMESPACE = -2;
		public const int IMPORT = -3;
		public const int MATCH = -4;
		public const int ID = -5;
		public const int ARROW = -6;
		public const int CHS = -7;
		public const int REGEX = -8;
		public const int STRUCT = -9;
		public const int FUNC = -10;
		public const int BREAK = -11;
		public const int CONTINUE = -12;
		public const int RETURN = -13;
		public const int VAR = -14;
		public const int WHILE = -15;
		public const int SWITCH = -16;
		public const int IF = -17;
		public const int ELSE = -18;
		public const int CASE = -19;
		public const int DEFAULT = -20;
		public const int OROR = -21;
		public const int ANDAND = -22;
		public const int EQ = -23;
		public const int NE = -24;
		public const int LT = -25;
		public const int LE = -26;
		public const int GT = -27;
		public const int GE = -28;
		public const int SHL = -29;
		public const int SHR = -30;
		public const int INC = -31;
		public const int DEC = -32;
		public const int INTEGER = -33;
		public const int FLOAT = -34;
		public const int UNICODE = -35;
		public const int TRUE = -36;
		public const int FALSE = -37;
		public const int NEW = -38;
		public const int BOOL = -39;
		public const int BYTE = -40;
		public const int SBYTE = -41;
		public const int SHORT = -42;
		public const int USHORT = -43;
		public const int INT = -44;
		public const int UINT = -45;
		public const int LONG = -46;
		public const int ULONG = -47;
		public const int DOUBLE = -48;
		public const int CHAR = -49;
		public const int STRING = -50;
		public const int PUBLIC = -51;
		public const int PRIVATE = -52;
		public const int PROTECTED = -53;
		public const int EXTERN = -54;
		
		Dictionary<int, string> _token;
		public override Dictionary<int, string> Token
		{
			get
			{
				if (_token == null)
				{
					_token = new Dictionary<int, string>();
					_token.Add(-1, "EOF");
					_token.Add(-2, "NAMESPACE");
					_token.Add(-3, "IMPORT");
					_token.Add(-4, "MATCH");
					_token.Add(-5, "ID");
					_token.Add(-6, "ARROW");
					_token.Add(-7, "CHS");
					_token.Add(-8, "REGEX");
					_token.Add(-9, "STRUCT");
					_token.Add(-10, "FUNC");
					_token.Add(-11, "BREAK");
					_token.Add(-12, "CONTINUE");
					_token.Add(-13, "RETURN");
					_token.Add(-14, "VAR");
					_token.Add(-15, "WHILE");
					_token.Add(-16, "SWITCH");
					_token.Add(-17, "IF");
					_token.Add(-18, "ELSE");
					_token.Add(-19, "CASE");
					_token.Add(-20, "DEFAULT");
					_token.Add(-21, "OROR");
					_token.Add(-22, "ANDAND");
					_token.Add(-23, "EQ");
					_token.Add(-24, "NE");
					_token.Add(-25, "LT");
					_token.Add(-26, "LE");
					_token.Add(-27, "GT");
					_token.Add(-28, "GE");
					_token.Add(-29, "SHL");
					_token.Add(-30, "SHR");
					_token.Add(-31, "INC");
					_token.Add(-32, "DEC");
					_token.Add(-33, "INTEGER");
					_token.Add(-34, "FLOAT");
					_token.Add(-35, "UNICODE");
					_token.Add(-36, "TRUE");
					_token.Add(-37, "FALSE");
					_token.Add(-38, "NEW");
					_token.Add(-39, "BOOL");
					_token.Add(-40, "BYTE");
					_token.Add(-41, "SBYTE");
					_token.Add(-42, "SHORT");
					_token.Add(-43, "USHORT");
					_token.Add(-44, "INT");
					_token.Add(-45, "UINT");
					_token.Add(-46, "LONG");
					_token.Add(-47, "ULONG");
					_token.Add(-48, "DOUBLE");
					_token.Add(-49, "CHAR");
					_token.Add(-50, "STRING");
					_token.Add(-51, "PUBLIC");
					_token.Add(-52, "PRIVATE");
					_token.Add(-53, "PROTECTED");
					_token.Add(-54, "EXTERN");
				}
				return _token;
			}
		}
		
		Target target(IAST target_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case IMPORT:
			case NAMESPACE:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			Target target_s = default(Target);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					var nt1_s = import_list(nt1_i);
					TokenAST nt2_s = Match(NAMESPACE, nt2_i);
					var nt3_s = path(nt3_i);
					TokenAST nt4_s = Match(';', nt4_i);
					var nt5_s = decl_list(nt5_i);
					target_s = new Target(FileName, TargetLanguage, Lexicon, nt3_s, nt1_s, nt5_s);
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
			return target_s;
		}
		
		ImportList import_list(IAST import_list_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case NAMESPACE:
				alt = 0;
				break;
			case IMPORT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ImportList import_list_s = default(ImportList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					import_list_s = new ImportList();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = imports(nt1_i);
					import_list_s = nt1_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case NAMESPACE:
				break;
			default:
				Error();
				break;
			}
			return import_list_s;
		}
		
		ImportList imports(IAST imports_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case IMPORT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ImportList imports_s = default(ImportList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = import(nt1_i);
					nt2_i = new ImportList(nt1_s); 
					var nt2_s = tmp_3(nt2_i);
					imports_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case NAMESPACE:
				break;
			default:
				Error();
				break;
			}
			return imports_s;
		}
		
		ImportList tmp_3(IAST tmp_3_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case NAMESPACE:
				alt = 0;
				break;
			case IMPORT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ImportList tmp_3_s = default(ImportList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_3_s = (ImportList)tmp_3_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = import(nt1_i);
					nt2_i = ((ImportList)tmp_3_i).Add(nt1_s); 
					var nt2_s = tmp_3(nt2_i);
					tmp_3_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case NAMESPACE:
				break;
			default:
				Error();
				break;
			}
			return tmp_3_s;
		}
		
		Import import(IAST import_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case IMPORT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			Import import_s = default(Import);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(IMPORT, nt1_i);
					var nt2_s = path(nt2_i);
					TokenAST nt3_s = Match(';', nt3_i);
					import_s = new Import(GetSpan(nt1_s, nt3_s), nt2_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case IMPORT:
			case NAMESPACE:
				break;
			default:
				Error();
				break;
			}
			return import_s;
		}
		
		DeclareList decl_list(IAST decl_list_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case -1:
				alt = 0;
				break;
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			DeclareList decl_list_s = default(DeclareList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					decl_list_s = new DeclareList();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = tmp_1(nt1_i);
					decl_list_s = nt1_s;
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
			return decl_list_s;
		}
		
		DeclareList tmp_1(IAST tmp_1_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			DeclareList tmp_1_s = default(DeclareList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					var nt1_s = modifiter(nt1_i);
					var nt2_s = decl(nt2_i);
					nt3_i = (new DeclareList()).Add(nt2_s.SetModifiter(nt1_s)); 
					var nt3_s = tmp_4(nt3_i);
					tmp_1_s = nt3_s; 
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
		
		DeclareList tmp_4(IAST tmp_4_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case -1:
				alt = 0;
				break;
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			DeclareList tmp_4_s = default(DeclareList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_4_s = (DeclareList)tmp_4_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					var nt1_s = modifiter(nt1_i);
					var nt2_s = decl(nt2_i);
					nt3_i = ((DeclareList)tmp_4_i).Add(nt2_s.SetModifiter(nt1_s)); 
					var nt3_s = tmp_4(nt3_i);
					tmp_4_s = nt3_s; 
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
			return tmp_4_s;
		}
		
		Declare decl(IAST decl_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case FUNC:
				alt = 0;
				break;
			case STRUCT:
				alt = 1;
				break;
			case MATCH:
				alt = 2;
				break;
			default:
				Error();
				break;
			}
			
			Declare decl_s = default(Declare);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = func_decl(nt1_i);
					decl_s = nt1_s;
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = strcut_decl(nt1_i);
					decl_s = nt1_s;
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = match_decl(nt1_i);
					decl_s = nt1_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return decl_s;
		}
		
		MatchDecl match_decl(IAST match_decl_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case MATCH:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			MatchDecl match_decl_s = default(MatchDecl);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt1_s = Match(MATCH, nt1_i);
					TokenAST nt2_s = Match(ID, nt2_i);
					TokenAST nt3_s = Match('=', nt3_i);
					var nt4_s = or(nt4_i);
					TokenAST nt5_s = Match(';', nt5_i);
					match_decl_s = new MatchDecl(GetSpan(nt1_s, nt5_s), nt2_s, nt4_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return match_decl_s;
		}
		
		SourceProduction or(IAST or_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CHS:
			case REGEX:
			case ID:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction or_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = mapping(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_5(nt2_i);
					or_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ';':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return or_s;
		}
		
		SourceProduction tmp_5(IAST tmp_5_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ';':
			case ')':
				alt = 0;
				break;
			case '|':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction tmp_5_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_5_s = (SourceProduction)tmp_5_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('|', nt1_i);
					var nt2_s = mapping(nt2_i);
					nt3_i = new OrProduction(((SourceProduction)tmp_5_i), nt2_s).SetSpan(((SourceProduction)tmp_5_i), nt2_s); 
					var nt3_s = tmp_5(nt3_i);
					tmp_5_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ';':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return tmp_5_s;
		}
		
		SourceProduction mapping(IAST mapping_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CHS:
			case REGEX:
			case ID:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction mapping_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = next(nt1_i);
					var nt2_s = tmp_28(nt1_s, nt2_i);
					mapping_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '|':
			case ';':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return mapping_s;
		}
		
		SourceProduction tmp_28(IAST nt1_s, IAST tmp_28_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ARROW:
				alt = 0;
				break;
			case '|':
			case ';':
			case ')':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction tmp_28_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt2_s = Match(ARROW, nt2_i);
					var nt3_s = stmt(nt3_i);
					tmp_28_s = new MappingProduction(((SourceProduction)nt1_s), Translater.Convert(nt3_s, TargetLanguage)).SetSpan(((SourceProduction)nt1_s), nt3_s);
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					
					tmp_28_s = ((SourceProduction)nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '|':
			case ';':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return tmp_28_s;
		}
		
		SourceProduction next(IAST next_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CHS:
			case REGEX:
			case ID:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction next_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = empty(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_6(nt2_i);
					next_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ARROW:
			case '|':
			case ';':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return next_s;
		}
		
		SourceProduction tmp_6(IAST tmp_6_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ARROW:
			case '|':
			case ';':
			case ')':
				alt = 0;
				break;
			case CHS:
			case REGEX:
			case ID:
			case '(':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction tmp_6_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_6_s = (SourceProduction)tmp_6_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = empty(nt1_i);
					nt2_i = new ConcatenationProduction(((SourceProduction)tmp_6_i), nt1_s).SetSpan(((SourceProduction)tmp_6_i), nt1_s); 
					var nt2_s = tmp_6(nt2_i);
					tmp_6_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ARROW:
			case '|':
			case ';':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return tmp_6_s;
		}
		
		SourceProduction empty(IAST empty_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CHS:
			case REGEX:
			case ID:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction empty_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = many(nt1_i);
					TokenAST nt2_s = Match('?', nt2_i);
					empty_s = new OrProduction(nt1_s, new EmptyProduction().SetSpan(nt2_s)).SetSpan(nt1_s, nt2_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case CHS:
			case REGEX:
			case ID:
			case '(':
			case ARROW:
			case '|':
			case ';':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return empty_s;
		}
		
		SourceProduction many(IAST many_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CHS:
			case REGEX:
			case ID:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction many_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = many1(nt1_i);
					var nt2_s = tmp_29(nt1_s, nt2_i);
					many_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '?':
				break;
			default:
				Error();
				break;
			}
			return many_s;
		}
		
		SourceProduction tmp_29(IAST nt1_s, IAST tmp_29_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '*':
				alt = 0;
				break;
			case '?':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction tmp_29_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					
					TokenAST nt2_s = Match('*', nt2_i);
					tmp_29_s = new RepeatProduction(((SourceProduction)nt1_s), null).SetSpan(((SourceProduction)nt1_s), nt2_s);
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					
					tmp_29_s = ((SourceProduction)nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '?':
				break;
			default:
				Error();
				break;
			}
			return tmp_29_s;
		}
		
		SourceProduction many1(IAST many1_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CHS:
			case REGEX:
			case ID:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction many1_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = prim(nt1_i);
					var nt2_s = tmp_30(nt1_s, nt2_i);
					many1_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '*':
			case '?':
				break;
			default:
				Error();
				break;
			}
			return many1_s;
		}
		
		SourceProduction tmp_30(IAST nt1_s, IAST tmp_30_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '+':
				alt = 0;
				break;
			case '*':
			case '?':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction tmp_30_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					
					TokenAST nt2_s = Match('+', nt2_i);
					tmp_30_s = new ConcatenationProduction(((SourceProduction)nt1_s), new RepeatProduction(((SourceProduction)nt1_s), null).SetSpan(((SourceProduction)nt1_s), nt2_s)).SetSpan(((SourceProduction)nt1_s), nt2_s);
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					
					tmp_30_s = ((SourceProduction)nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '*':
			case '?':
				break;
			default:
				Error();
				break;
			}
			return tmp_30_s;
		}
		
		SourceProduction prim(IAST prim_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CHS:
				alt = 0;
				break;
			case REGEX:
				alt = 1;
				break;
			case ID:
				alt = 2;
				break;
			case '(':
				alt = 3;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction prim_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(CHS, nt1_i);
					prim_s = new Terminal(GetLiterallToken(nt1_s)).SetSpan(nt1_s);
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(REGEX, nt1_i);
					prim_s = new Terminal(GetRegexToken(nt1_s)).SetSpan(nt1_s);
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					var nt2_s = tmp_31(nt1_s, nt2_i);
					prim_s = nt2_s;
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('(', nt1_i);
					var nt2_s = or(nt2_i);
					TokenAST nt3_s = Match(')', nt3_i);
					prim_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '+':
			case '*':
			case '?':
				break;
			default:
				Error();
				break;
			}
			return prim_s;
		}
		
		SourceProduction tmp_31(IAST nt1_s, IAST tmp_31_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '(':
				alt = 0;
				break;
			case '+':
			case '*':
			case '?':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			SourceProduction tmp_31_s = default(SourceProduction);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt2_s = Match('(', nt2_i);
					var nt3_s = or(nt3_i);
					TokenAST nt4_s = Match(')', nt4_i);
					tmp_31_s = new DefineFragment(GetSpan(((TokenAST)nt1_s), nt4_s), nt3_s, ((TokenAST)nt1_s));
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					
					tmp_31_s = new ReferenceMatch(((TokenAST)nt1_s)).SetSpan(((TokenAST)nt1_s));
				}
				break;
			}
			
			switch (Next.token)
			{
			case '+':
			case '*':
			case '?':
				break;
			default:
				Error();
				break;
			}
			return tmp_31_s;
		}
		
		StructDecl strcut_decl(IAST strcut_decl_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case STRUCT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			StructDecl strcut_decl_s = default(StructDecl);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt1_s = Match(STRUCT, nt1_i);
					var nt2_s = typedName(nt2_i);
					TokenAST nt3_s = Match('{', nt3_i);
					var nt4_s = struct_member_list(nt4_i);
					TokenAST nt5_s = Match('}', nt5_i);
					strcut_decl_s = new StructDecl(GetSpan(nt2_s), nt2_s, nt4_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return strcut_decl_s;
		}
		
		StructDeclMemberList struct_member_list(IAST struct_member_list_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '}':
				alt = 0;
				break;
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case ID:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			StructDeclMemberList struct_member_list_s = default(StructDeclMemberList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					struct_member_list_s = new StructDeclMemberList();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = tmp_2(nt1_i);
					struct_member_list_s = nt1_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
				break;
			default:
				Error();
				break;
			}
			return struct_member_list_s;
		}
		
		StructDeclMemberList tmp_2(IAST tmp_2_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case ID:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			StructDeclMemberList tmp_2_s = default(StructDeclMemberList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = struct_member(nt1_i);
					nt2_i = (new StructDeclMemberList()).Add(nt1_s); 
					var nt2_s = tmp_7(nt2_i);
					tmp_2_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_2_s;
		}
		
		StructDeclMemberList tmp_7(IAST tmp_7_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '}':
				alt = 0;
				break;
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case ID:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			StructDeclMemberList tmp_7_s = default(StructDeclMemberList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_7_s = (StructDeclMemberList)tmp_7_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = struct_member(nt1_i);
					nt2_i = ((StructDeclMemberList)tmp_7_i).Add(nt1_s); 
					var nt2_s = tmp_7(nt2_i);
					tmp_7_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_7_s;
		}
		
		StructDeclMember struct_member(IAST struct_member_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case ID:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			StructDeclMember struct_member_s = default(StructDeclMember);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					var nt1_s = modifiter(nt1_i);
					var nt2_s = typedName(nt2_i);
					TokenAST nt3_s = Match(';', nt3_i);
					struct_member_s = new StructDeclMember(GetSpan(nt1_s, nt3_s), nt1_s, nt2_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case ID:
			case '}':
				break;
			default:
				Error();
				break;
			}
			return struct_member_s;
		}
		
		FuncDecl func_decl(IAST func_decl_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case FUNC:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			FuncDecl func_decl_s = default(FuncDecl);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					var nt6_i = default(IAST);
					
					TokenAST nt1_s = Match(FUNC, nt1_i);
					var nt2_s = typedName(nt2_i);
					TokenAST nt3_s = Match('(', nt3_i);
					var nt4_s = argse(nt4_i);
					TokenAST nt5_s = Match(')', nt5_i);
					var nt6_s = stmt(nt6_i);
					func_decl_s = new FuncDecl(GetSpan(nt2_s), nt2_s, nt4_s, nt6_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return func_decl_s;
		}
		
		Args argse(IAST argse_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ')':
				alt = 0;
				break;
			case ID:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			Args argse_s = default(Args);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					argse_s = new Args();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = args(nt1_i);
					argse_s = nt1_s;
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
			return argse_s;
		}
		
		Args args(IAST args_i)
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
			
			Args args_s = default(Args);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = typedName(nt1_i);
					nt2_i = new Args(nt1_s); 
					var nt2_s = tmp_8(nt2_i);
					args_s = nt2_s; 
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
			return args_s;
		}
		
		Args tmp_8(IAST tmp_8_i)
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
			
			Args tmp_8_s = default(Args);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_8_s = (Args)tmp_8_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(',', nt1_i);
					var nt2_s = typedName(nt2_i);
					nt3_i = ((Args)tmp_8_i).Add(nt2_s); 
					var nt3_s = tmp_8(nt3_i);
					tmp_8_s = nt3_s; 
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
			return tmp_8_s;
		}
		
		StmtRoot stmt(IAST stmt_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			case BREAK:
				alt = 1;
				break;
			case CONTINUE:
				alt = 2;
				break;
			case RETURN:
				alt = 3;
				break;
			case VAR:
				alt = 4;
				break;
			case WHILE:
				alt = 5;
				break;
			case SWITCH:
				alt = 6;
				break;
			case IF:
				alt = 7;
				break;
			case '{':
				alt = 8;
				break;
			default:
				Error();
				break;
			}
			
			StmtRoot stmt_s = default(StmtRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e(nt1_i);
					TokenAST nt2_s = Match(';', nt2_i);
					stmt_s = new StmtExpression(GetSpan(nt1_s, nt2_s), nt1_s);
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(BREAK, nt1_i);
					TokenAST nt2_s = Match(';', nt2_i);
					stmt_s = new StmtBreak(GetSpan(nt1_s, nt2_s));
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(CONTINUE, nt1_i);
					TokenAST nt2_s = Match(';', nt2_i);
					stmt_s = new StmtContinue(GetSpan(nt1_s, nt2_s));
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(RETURN, nt1_i);
					var nt2_s = e(nt2_i);
					TokenAST nt3_s = Match(';', nt3_i);
					stmt_s = new StmtReturn(GetSpan(nt1_s, nt3_s), nt2_s);
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt1_s = Match(VAR, nt1_i);
					TokenAST nt2_s = Match(ID, nt2_i);
					TokenAST nt3_s = Match('=', nt3_i);
					var nt4_s = e(nt4_i);
					TokenAST nt5_s = Match(';', nt5_i);
					stmt_s = new StmtVarDecl(GetSpan(nt1_s, nt5_s), nt2_s, nt4_s);
				}
				break;
			case 5:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt1_s = Match(WHILE, nt1_i);
					TokenAST nt2_s = Match('(', nt2_i);
					var nt3_s = e(nt3_i);
					TokenAST nt4_s = Match(')', nt4_i);
					var nt5_s = stmt(nt5_i);
					stmt_s = new StmtWhile(GetSpan(nt1_s, nt5_s), nt3_s, nt5_s);
				}
				break;
			case 6:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					var nt6_i = default(IAST);
					var nt7_i = default(IAST);
					
					TokenAST nt1_s = Match(SWITCH, nt1_i);
					TokenAST nt2_s = Match('(', nt2_i);
					var nt3_s = e(nt3_i);
					TokenAST nt4_s = Match(')', nt4_i);
					TokenAST nt5_s = Match('{', nt5_i);
					var nt6_s = caselist(nt6_i);
					TokenAST nt7_s = Match('}', nt7_i);
					stmt_s = new StmtSwitch(GetSpan(nt1_s, nt7_s), nt3_s, nt6_s);
				}
				break;
			case 7:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = ifstmt(nt1_i);
					stmt_s = nt1_s;
				}
				break;
			case 8:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('{', nt1_i);
					var nt2_s = stmtlist(nt2_i);
					TokenAST nt3_s = Match('}', nt3_i);
					stmt_s = new StmtBlock(GetSpan(nt1_s, nt3_s), nt2_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case ELSE:
			case BREAK:
			case CONTINUE:
			case RETURN:
			case VAR:
			case WHILE:
			case SWITCH:
			case '{':
			case IF:
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
			case '|':
			case ';':
			case ')':
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
			case -1:
			case '}':
			case CASE:
			case DEFAULT:
				break;
			default:
				Error();
				break;
			}
			return stmt_s;
		}
		
		StmtRoot ifstmt(IAST ifstmt_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case IF:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			StmtRoot ifstmt_s = default(StmtRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					var nt6_i = default(IAST);
					
					TokenAST nt1_s = Match(IF, nt1_i);
					TokenAST nt2_s = Match('(', nt2_i);
					var nt3_s = e(nt3_i);
					TokenAST nt4_s = Match(')', nt4_i);
					var nt5_s = stmt(nt5_i);
					var nt6_s = tmp_32(nt1_s, nt2_s, nt3_s, nt4_s, nt5_s, nt6_i);
					ifstmt_s = nt6_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case ELSE:
			case BREAK:
			case CONTINUE:
			case RETURN:
			case VAR:
			case WHILE:
			case SWITCH:
			case '{':
			case IF:
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
			case '|':
			case ';':
			case ')':
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
			case -1:
			case '}':
			case CASE:
			case DEFAULT:
				break;
			default:
				Error();
				break;
			}
			return ifstmt_s;
		}
		
		StmtRoot tmp_32(IAST nt1_s, IAST nt2_s, IAST nt3_s, IAST nt4_s, IAST nt5_s, IAST tmp_32_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case BREAK:
			case CONTINUE:
			case RETURN:
			case VAR:
			case WHILE:
			case SWITCH:
			case '{':
			case IF:
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
			case '|':
			case ';':
			case ')':
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
			case -1:
			case '}':
			case CASE:
			case DEFAULT:
				alt = 0;
				break;
			case ELSE:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			StmtRoot tmp_32_s = default(StmtRoot);
			switch (alt)
			{
			case 0:
				{
					var nt6_i = default(IAST);
					
					tmp_32_s = new StmtIf(GetSpan(((TokenAST)nt1_s), ((StmtRoot)nt5_s)), ((ExprRoot)nt3_s), ((StmtRoot)nt5_s));
				}
				break;
			case 1:
				{
					var nt6_i = default(IAST);
					var nt7_i = default(IAST);
					
					TokenAST nt6_s = Match(ELSE, nt6_i);
					var nt7_s = stmt(nt7_i);
					tmp_32_s = new StmtIf(GetSpan(((TokenAST)nt1_s), nt7_s), ((ExprRoot)nt3_s), ((StmtRoot)nt5_s), nt7_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case ELSE:
			case BREAK:
			case CONTINUE:
			case RETURN:
			case VAR:
			case WHILE:
			case SWITCH:
			case '{':
			case IF:
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
			case '|':
			case ';':
			case ')':
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case EXTERN:
			case FUNC:
			case STRUCT:
			case MATCH:
			case -1:
			case '}':
			case CASE:
			case DEFAULT:
				break;
			default:
				Error();
				break;
			}
			return tmp_32_s;
		}
		
		StmtCase casestmt(IAST casestmt_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CASE:
				alt = 0;
				break;
			case DEFAULT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			StmtCase casestmt_s = default(StmtCase);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt1_s = Match(CASE, nt1_i);
					var nt2_s = e(nt2_i);
					TokenAST nt3_s = Match(':', nt3_i);
					var nt4_s = stmtlist(nt4_i);
				casestmt_s = new StmtCase(GetSpan(nt1_s, nt4_s), nt2_s, nt4_s);
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(DEFAULT, nt1_i);
					TokenAST nt2_s = Match(':', nt2_i);
					var nt3_s = stmtlist(nt3_i);
				casestmt_s = new StmtDefaultCase(GetSpan(nt1_s, nt3_s), nt3_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case CASE:
			case DEFAULT:
			case '}':
				break;
			default:
				Error();
				break;
			}
			return casestmt_s;
		}
		
		CasesBlock caselist(IAST caselist_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case CASE:
			case DEFAULT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			CasesBlock caselist_s = default(CasesBlock);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = casestmt(nt1_i);
					nt2_i = new CasesBlock(nt1_s); 
					var nt2_s = tmp_9(nt2_i);
				caselist_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
				break;
			default:
				Error();
				break;
			}
			return caselist_s;
		}
		
		CasesBlock tmp_9(IAST tmp_9_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '}':
				alt = 0;
				break;
			case CASE:
			case DEFAULT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			CasesBlock tmp_9_s = default(CasesBlock);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_9_s = (CasesBlock)tmp_9_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = casestmt(nt1_i);
					nt2_i = ((CasesBlock)tmp_9_i).Add(nt1_s); 
					var nt2_s = tmp_9(nt2_i);
					tmp_9_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_9_s;
		}
		
		StmtList stmtlist(IAST stmtlist_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case BREAK:
			case CONTINUE:
			case RETURN:
			case VAR:
			case WHILE:
			case SWITCH:
			case '{':
			case IF:
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			StmtList stmtlist_s = default(StmtList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = stmt(nt1_i);
					nt2_i = new StmtList(nt1_s); 
					var nt2_s = tmp_10(nt2_i);
					stmtlist_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
			case CASE:
			case DEFAULT:
				break;
			default:
				Error();
				break;
			}
			return stmtlist_s;
		}
		
		StmtList tmp_10(IAST tmp_10_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '}':
			case CASE:
			case DEFAULT:
				alt = 0;
				break;
			case BREAK:
			case CONTINUE:
			case RETURN:
			case VAR:
			case WHILE:
			case SWITCH:
			case '{':
			case IF:
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			StmtList tmp_10_s = default(StmtList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_10_s = (StmtList)tmp_10_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = stmt(nt1_i);
					nt2_i = ((StmtList)tmp_10_i).Add(nt1_s); 
					var nt2_s = tmp_10(nt2_i);
					tmp_10_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
			case CASE:
			case DEFAULT:
				break;
			default:
				Error();
				break;
			}
			return tmp_10_s;
		}
		
		ExprRoot e(IAST e_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = e_assignment(nt1_i);
					e_s = nt1_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case ';':
			case ')':
			case ':':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_s;
		}
		
		ExprRoot e_assignment(IAST e_assignment_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_assignment_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_cond(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_11(nt2_i);
					e_assignment_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ';':
			case ')':
			case ':':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_assignment_s;
		}
		
		ExprRoot tmp_11(IAST tmp_11_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ';':
			case ')':
			case ':':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case '=':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_11_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_11_s = (ExprRoot)tmp_11_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('=', nt1_i);
					var nt2_s = e_cond(nt2_i);
					nt3_i = new ExprAssignment(GetSpan(((ExprRoot)tmp_11_i), nt2_s), ((ExprRoot)tmp_11_i), nt2_s); 
					var nt3_s = tmp_11(nt3_i);
					tmp_11_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ';':
			case ')':
			case ':':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_11_s;
		}
		
		ExprRoot e_cond(IAST e_cond_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_cond_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_oror(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_12(nt2_i);
					e_cond_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '=':
			case ';':
			case ')':
			case ':':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_cond_s;
		}
		
		ExprRoot tmp_12(IAST tmp_12_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '=':
			case ';':
			case ')':
			case ':':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case '?':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_12_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_12_s = (ExprRoot)tmp_12_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt1_s = Match('?', nt1_i);
					var nt2_s = e_oror(nt2_i);
					TokenAST nt3_s = Match(':', nt3_i);
					var nt4_s = e_oror(nt4_i);
					nt5_i = new ExprConditional(GetSpan(((ExprRoot)tmp_12_i), nt4_s), ((ExprRoot)tmp_12_i), nt2_s, nt4_s); 
					var nt5_s = tmp_12(nt5_i);
					tmp_12_s = nt5_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '=':
			case ';':
			case ')':
			case ':':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_12_s;
		}
		
		ExprRoot e_oror(IAST e_oror_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_oror_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_andand(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_13(nt2_i);
					e_oror_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_oror_s;
		}
		
		ExprRoot tmp_13(IAST tmp_13_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case OROR:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_13_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_13_s = (ExprRoot)tmp_13_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(OROR, nt1_i);
					var nt2_s = e_andand(nt2_i);
					nt3_i = new ExprOrOr(GetSpan(((ExprRoot)tmp_13_i), nt2_s), ((ExprRoot)tmp_13_i), nt2_s); 
					var nt3_s = tmp_13(nt3_i);
					tmp_13_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_13_s;
		}
		
		ExprRoot e_andand(IAST e_andand_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_andand_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_or(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_14(nt2_i);
					e_andand_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_andand_s;
		}
		
		ExprRoot tmp_14(IAST tmp_14_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case ANDAND:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_14_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_14_s = (ExprRoot)tmp_14_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(ANDAND, nt1_i);
					var nt2_s = e_or(nt2_i);
					nt3_i = new ExprAndAnd(GetSpan(((ExprRoot)tmp_14_i), nt2_s), ((ExprRoot)tmp_14_i), nt2_s); 
					var nt3_s = tmp_14(nt3_i);
					tmp_14_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_14_s;
		}
		
		ExprRoot e_or(IAST e_or_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_or_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_xor(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_15(nt2_i);
					e_or_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_or_s;
		}
		
		ExprRoot tmp_15(IAST tmp_15_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case '|':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_15_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_15_s = (ExprRoot)tmp_15_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('|', nt1_i);
					var nt2_s = e_xor(nt2_i);
					nt3_i = new ExprOr(GetSpan(((ExprRoot)tmp_15_i), nt2_s), ((ExprRoot)tmp_15_i), nt2_s); 
					var nt3_s = tmp_15(nt3_i);
					tmp_15_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_15_s;
		}
		
		ExprRoot e_xor(IAST e_xor_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_xor_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_and(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_16(nt2_i);
					e_xor_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_xor_s;
		}
		
		ExprRoot tmp_16(IAST tmp_16_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case '^':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_16_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_16_s = (ExprRoot)tmp_16_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('^', nt1_i);
					var nt2_s = e_and(nt2_i);
					nt3_i = new ExprXor(GetSpan(((ExprRoot)tmp_16_i), nt2_s), ((ExprRoot)tmp_16_i), nt2_s); 
					var nt3_s = tmp_16(nt3_i);
					tmp_16_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_16_s;
		}
		
		ExprRoot e_and(IAST e_and_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_and_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_eq(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_17(nt2_i);
					e_and_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_and_s;
		}
		
		ExprRoot tmp_17(IAST tmp_17_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case '&':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_17_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_17_s = (ExprRoot)tmp_17_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('&', nt1_i);
					var nt2_s = e_eq(nt2_i);
					nt3_i = new ExprAnd(GetSpan(((ExprRoot)tmp_17_i), nt2_s), ((ExprRoot)tmp_17_i), nt2_s); 
					var nt3_s = tmp_17(nt3_i);
					tmp_17_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_17_s;
		}
		
		ExprRoot e_eq(IAST e_eq_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_eq_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_cmp(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_18(nt2_i);
					e_eq_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_eq_s;
		}
		
		ExprRoot tmp_18(IAST tmp_18_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case EQ:
				alt = 1;
				break;
			case NE:
				alt = 2;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_18_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_18_s = (ExprRoot)tmp_18_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(EQ, nt1_i);
					var nt2_s = e_cmp(nt2_i);
					nt3_i = new ExprEqual(GetSpan(((ExprRoot)tmp_18_i), nt2_s), ((ExprRoot)tmp_18_i), nt2_s); 
					var nt3_s = tmp_18(nt3_i);
					tmp_18_s = nt3_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(NE, nt1_i);
					var nt2_s = e_cmp(nt2_i);
					nt3_i = new ExprNotEqual(GetSpan(((ExprRoot)tmp_18_i), nt2_s), ((ExprRoot)tmp_18_i), nt2_s); 
					var nt3_s = tmp_18(nt3_i);
					tmp_18_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_18_s;
		}
		
		ExprRoot e_cmp(IAST e_cmp_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_cmp_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_shift(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_19(nt2_i);
					e_cmp_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_cmp_s;
		}
		
		ExprRoot tmp_19(IAST tmp_19_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case LT:
				alt = 1;
				break;
			case LE:
				alt = 2;
				break;
			case GT:
				alt = 3;
				break;
			case GE:
				alt = 4;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_19_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_19_s = (ExprRoot)tmp_19_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(LT, nt1_i);
					var nt2_s = e_shift(nt2_i);
					nt3_i = new ExprLessThen(GetSpan(((ExprRoot)tmp_19_i), nt2_s), ((ExprRoot)tmp_19_i), nt2_s); 
					var nt3_s = tmp_19(nt3_i);
					tmp_19_s = nt3_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(LE, nt1_i);
					var nt2_s = e_shift(nt2_i);
					nt3_i = new ExprLessEqual(GetSpan(((ExprRoot)tmp_19_i), nt2_s), ((ExprRoot)tmp_19_i), nt2_s); 
					var nt3_s = tmp_19(nt3_i);
					tmp_19_s = nt3_s; 
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(GT, nt1_i);
					var nt2_s = e_shift(nt2_i);
					nt3_i = new ExprGreaterThen(GetSpan(((ExprRoot)tmp_19_i), nt2_s), ((ExprRoot)tmp_19_i), nt2_s); 
					var nt3_s = tmp_19(nt3_i);
					tmp_19_s = nt3_s; 
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(GE, nt1_i);
					var nt2_s = e_shift(nt2_i);
					nt3_i = new ExprGreaterEqual(GetSpan(((ExprRoot)tmp_19_i), nt2_s), ((ExprRoot)tmp_19_i), nt2_s); 
					var nt3_s = tmp_19(nt3_i);
					tmp_19_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_19_s;
		}
		
		ExprRoot e_shift(IAST e_shift_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_shift_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_add(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_20(nt2_i);
					e_shift_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_shift_s;
		}
		
		ExprRoot tmp_20(IAST tmp_20_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case SHL:
				alt = 1;
				break;
			case SHR:
				alt = 2;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_20_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_20_s = (ExprRoot)tmp_20_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(SHL, nt1_i);
					var nt2_s = e_add(nt2_i);
					nt3_i = new ExprLeftShift(GetSpan(((ExprRoot)tmp_20_i), nt2_s), ((ExprRoot)tmp_20_i), nt2_s); 
					var nt3_s = tmp_20(nt3_i);
					tmp_20_s = nt3_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(SHR, nt1_i);
					var nt2_s = e_add(nt2_i);
					nt3_i = new ExprRightShift(GetSpan(((ExprRoot)tmp_20_i), nt2_s), ((ExprRoot)tmp_20_i), nt2_s); 
					var nt3_s = tmp_20(nt3_i);
					tmp_20_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_20_s;
		}
		
		ExprRoot e_add(IAST e_add_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_add_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_mul(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_21(nt2_i);
					e_add_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_add_s;
		}
		
		ExprRoot tmp_21(IAST tmp_21_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case '+':
				alt = 1;
				break;
			case '-':
				alt = 2;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_21_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_21_s = (ExprRoot)tmp_21_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('+', nt1_i);
					var nt2_s = e_mul(nt2_i);
					nt3_i = new ExprAdd(GetSpan(((ExprRoot)tmp_21_i), nt2_s), ((ExprRoot)tmp_21_i), nt2_s); 
					var nt3_s = tmp_21(nt3_i);
					tmp_21_s = nt3_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('-', nt1_i);
					var nt2_s = e_mul(nt2_i);
					nt3_i = new ExprSub(GetSpan(((ExprRoot)tmp_21_i), nt2_s), ((ExprRoot)tmp_21_i), nt2_s); 
					var nt3_s = tmp_21(nt3_i);
					tmp_21_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_21_s;
		}
		
		ExprRoot e_mul(IAST e_mul_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_mul_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_una(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_22(nt2_i);
					e_mul_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '+':
			case '-':
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_mul_s;
		}
		
		ExprRoot tmp_22(IAST tmp_22_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '+':
			case '-':
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case '*':
				alt = 1;
				break;
			case '/':
				alt = 2;
				break;
			case '%':
				alt = 3;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_22_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_22_s = (ExprRoot)tmp_22_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('*', nt1_i);
					var nt2_s = e_una(nt2_i);
					nt3_i = new ExprMul(GetSpan(((ExprRoot)tmp_22_i), nt2_s), ((ExprRoot)tmp_22_i), nt2_s); 
					var nt3_s = tmp_22(nt3_i);
					tmp_22_s = nt3_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('/', nt1_i);
					var nt2_s = e_una(nt2_i);
					nt3_i = new ExprDiv(GetSpan(((ExprRoot)tmp_22_i), nt2_s), ((ExprRoot)tmp_22_i), nt2_s); 
					var nt3_s = tmp_22(nt3_i);
					tmp_22_s = nt3_s; 
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('%', nt1_i);
					var nt2_s = e_una(nt2_i);
					nt3_i = new ExprRem(GetSpan(((ExprRoot)tmp_22_i), nt2_s), ((ExprRoot)tmp_22_i), nt2_s); 
					var nt3_s = tmp_22(nt3_i);
					tmp_22_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '+':
			case '-':
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_22_s;
		}
		
		ExprRoot e_una(IAST e_una_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			case '-':
				alt = 1;
				break;
			case '+':
				alt = 2;
				break;
			case INC:
				alt = 3;
				break;
			case DEC:
				alt = 4;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_una_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = e_prim(nt1_i);
					e_una_s = nt1_s;
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match('-', nt1_i);
					var nt2_s = e_una(nt2_i);
					e_una_s = new ExprNeg(GetSpan(nt1_s, nt2_s), nt2_s);
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match('+', nt1_i);
					var nt2_s = e_una(nt2_i);
					e_una_s = new ExprPlus(GetSpan(nt1_s, nt2_s), nt2_s);
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(INC, nt1_i);
					var nt2_s = e_una(nt2_i);
					e_una_s = new ExprPreInc(GetSpan(nt1_s, nt2_s), nt2_s);
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(DEC, nt1_i);
					var nt2_s = e_una(nt2_i);
					e_una_s = new ExprPreDec(GetSpan(nt1_s, nt2_s), nt2_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '*':
			case '/':
			case '%':
			case '+':
			case '-':
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_una_s;
		}
		
		ExprNum e_num(IAST e_num_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case INTEGER:
				alt = 0;
				break;
			case FLOAT:
				alt = 1;
				break;
			case UNICODE:
				alt = 2;
				break;
			default:
				Error();
				break;
			}
			
			ExprNum e_num_s = default(ExprNum);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(INTEGER, nt1_i);
					e_num_s = new ExprInteger(GetSpan(nt1_s), nt1_s);
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(FLOAT, nt1_i);
					e_num_s = new ExprFloat(GetSpan(nt1_s), nt1_s);
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(UNICODE, nt1_i);
					e_num_s = new ExprUnicode(GetSpan(nt1_s), nt1_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '.':
			case '(':
			case '[':
			case INC:
			case DEC:
			case '*':
			case '/':
			case '%':
			case '+':
			case '-':
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_num_s;
		}
		
		ExprRoot e_prim(IAST e_prim_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case INTEGER:
			case FLOAT:
			case UNICODE:
				alt = 0;
				break;
			case TRUE:
				alt = 1;
				break;
			case FALSE:
				alt = 2;
				break;
			case CHS:
				alt = 3;
				break;
			case ID:
				alt = 4;
				break;
			case '[':
				alt = 5;
				break;
			case NEW:
				alt = 6;
				break;
			case '(':
				alt = 7;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot e_prim_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e_num(nt1_i);
					nt2_i = nt1_s; 
					var nt2_s = tmp_23(nt2_i);
					e_prim_s = nt2_s; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(TRUE, nt1_i);
					nt2_i = new ExprTrue(GetSpan(nt1_s)); 
					var nt2_s = tmp_23(nt2_i);
					e_prim_s = nt2_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(FALSE, nt1_i);
					nt2_i = new ExprFalse(GetSpan(nt1_s)); 
					var nt2_s = tmp_23(nt2_i);
					e_prim_s = nt2_s; 
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(CHS, nt1_i);
					nt2_i = new ExprString(GetSpan(nt1_s), nt1_s); 
					var nt2_s = tmp_23(nt2_i);
					e_prim_s = nt2_s; 
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					nt2_i = new ExprIdentifier(GetSpan(nt1_s), nt1_s); 
					var nt2_s = tmp_23(nt2_i);
					e_prim_s = nt2_s; 
				}
				break;
			case 5:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt1_s = Match('[', nt1_i);
					var nt2_s = elist(nt2_i);
					TokenAST nt3_s = Match(']', nt3_i);
					nt4_i = new ExprArray(GetSpan(nt1_s, nt3_s), nt2_s); 
					var nt4_s = tmp_23(nt4_i);
					e_prim_s = nt4_s; 
				}
				break;
			case 6:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt1_s = Match(NEW, nt1_i);
					TokenAST nt2_s = Match('{', nt2_i);
					var nt3_s = elist_named(nt3_i);
					TokenAST nt4_s = Match('}', nt4_i);
					nt5_i = new ExprLiteral(GetSpan(nt1_s, nt4_s), nt3_s); 
					var nt5_s = tmp_23(nt5_i);
					e_prim_s = nt5_s; 
				}
				break;
			case 7:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt1_s = Match('(', nt1_i);
					var nt2_s = e(nt2_i);
					TokenAST nt3_s = Match(')', nt3_i);
					nt4_i = nt2_s; 
					var nt4_s = tmp_23(nt4_i);
					e_prim_s = nt4_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '*':
			case '/':
			case '%':
			case '+':
			case '-':
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return e_prim_s;
		}
		
		ExprRoot tmp_23(IAST tmp_23_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '*':
			case '/':
			case '%':
			case '+':
			case '-':
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				alt = 0;
				break;
			case '.':
				alt = 1;
				break;
			case '(':
				alt = 2;
				break;
			case '[':
				alt = 3;
				break;
			case INC:
				alt = 4;
				break;
			case DEC:
				alt = 5;
				break;
			default:
				Error();
				break;
			}
			
			ExprRoot tmp_23_s = default(ExprRoot);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_23_s = (ExprRoot)tmp_23_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('.', nt1_i);
					TokenAST nt2_s = Match(ID, nt2_i);
					nt3_i = new ExprMember(GetSpan(((ExprRoot)tmp_23_i), nt2_s), ((ExprRoot)tmp_23_i), nt2_s); 
					var nt3_s = tmp_23(nt3_i);
					tmp_23_s = nt3_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt1_s = Match('(', nt1_i);
					var nt2_s = eliste(nt2_i);
					TokenAST nt3_s = Match(')', nt3_i);
					nt4_i = new ExprCall(GetSpan(((ExprRoot)tmp_23_i), nt3_s), ((ExprRoot)tmp_23_i), nt2_s); 
					var nt4_s = tmp_23(nt4_i);
					tmp_23_s = nt4_s; 
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt1_s = Match('[', nt1_i);
					var nt2_s = elist(nt2_i);
					TokenAST nt3_s = Match(']', nt3_i);
					nt4_i = new ExprIndex(GetSpan(((ExprRoot)tmp_23_i), nt3_s), ((ExprRoot)tmp_23_i), nt2_s); 
					var nt4_s = tmp_23(nt4_i);
					tmp_23_s = nt4_s; 
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(INC, nt1_i);
					nt2_i = new ExprPostInc(GetSpan(((ExprRoot)tmp_23_i), nt1_s), ((ExprRoot)tmp_23_i)); 
					var nt2_s = tmp_23(nt2_i);
					tmp_23_s = nt2_s; 
				}
				break;
			case 5:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(DEC, nt1_i);
					nt2_i = new ExprPostDec(GetSpan(((ExprRoot)tmp_23_i), nt1_s), ((ExprRoot)tmp_23_i)); 
					var nt2_s = tmp_23(nt2_i);
					tmp_23_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '*':
			case '/':
			case '%':
			case '+':
			case '-':
			case SHL:
			case SHR:
			case LT:
			case LE:
			case GT:
			case GE:
			case EQ:
			case NE:
			case '&':
			case '^':
			case '|':
			case ANDAND:
			case OROR:
			case '?':
			case ':':
			case '=':
			case ';':
			case ')':
			case ',':
			case ']':
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_23_s;
		}
		
		ExprList eliste(IAST eliste_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ')':
				alt = 0;
				break;
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprList eliste_s = default(ExprList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					eliste_s = new ExprList();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = elist(nt1_i);
					eliste_s = nt1_s;
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
			return eliste_s;
		}
		
		ExprList elist(IAST elist_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '-':
			case '+':
			case INC:
			case DEC:
			case INTEGER:
			case FLOAT:
			case UNICODE:
			case TRUE:
			case FALSE:
			case CHS:
			case ID:
			case '[':
			case NEW:
			case '(':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ExprList elist_s = default(ExprList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = e(nt1_i);
					nt2_i = new ExprList(nt1_s); 
					var nt2_s = tmp_24(nt2_i);
					elist_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ']':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return elist_s;
		}
		
		ExprList tmp_24(IAST tmp_24_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ']':
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
			
			ExprList tmp_24_s = default(ExprList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_24_s = (ExprList)tmp_24_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(',', nt1_i);
					var nt2_s = e(nt2_i);
					nt3_i = ((ExprList)tmp_24_i).Add(nt2_s); 
					var nt3_s = tmp_24(nt3_i);
					tmp_24_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ']':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return tmp_24_s;
		}
		
		ExprNamedList elist_named(IAST elist_named_i)
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
			
			ExprNamedList elist_named_s = default(ExprNamedList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					TokenAST nt2_s = Match('=', nt2_i);
					var nt3_s = e(nt3_i);
					nt4_i = new ExprNamedList(new ExprNamed(nt1_s, nt3_s)); 
					var nt4_s = tmp_25(nt4_i);
					elist_named_s = nt4_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
				break;
			default:
				Error();
				break;
			}
			return elist_named_s;
		}
		
		ExprNamedList tmp_25(IAST tmp_25_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '}':
				alt = 0;
				break;
			case ',':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ExprNamedList tmp_25_s = default(ExprNamedList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_25_s = (ExprNamedList)tmp_25_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					
					TokenAST nt1_s = Match(',', nt1_i);
					TokenAST nt2_s = Match(ID, nt2_i);
					TokenAST nt3_s = Match('=', nt3_i);
					var nt4_s = e(nt4_i);
					nt5_i = ((ExprNamedList)tmp_25_i).Add(new ExprNamed(nt2_s, nt4_s)); 
					var nt5_s = tmp_25(nt5_i);
					tmp_25_s = nt5_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '}':
				break;
			default:
				Error();
				break;
			}
			return tmp_25_s;
		}
		
		TypeNamed typedName(IAST typedName_i)
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
			
			TypeNamed typedName_s = default(TypeNamed);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					var nt2_s = tmp_33(nt1_s, nt2_i);
					typedName_s = nt2_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case '{':
			case ';':
			case '(':
			case ',':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return typedName_s;
		}
		
		TypeNamed tmp_33(IAST nt1_s, IAST tmp_33_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '{':
			case ';':
			case '(':
			case ',':
			case ')':
				alt = 0;
				break;
			case ':':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			TypeNamed tmp_33_s = default(TypeNamed);
			switch (alt)
			{
			case 0:
				{
					var nt2_i = default(IAST);
					
					tmp_33_s = new TypeNamed(((TokenAST)nt1_s), new AutoType().SetSpan());
				}
				break;
			case 1:
				{
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt2_s = Match(':', nt2_i);
					var nt3_s = type(nt3_i);
					tmp_33_s = new TypeNamed(((TokenAST)nt1_s), nt3_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case '{':
			case ';':
			case '(':
			case ',':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return tmp_33_s;
		}
		
		SourceType type(IAST type_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case BOOL:
				alt = 0;
				break;
			case BYTE:
				alt = 1;
				break;
			case SBYTE:
				alt = 2;
				break;
			case SHORT:
				alt = 3;
				break;
			case USHORT:
				alt = 4;
				break;
			case INT:
				alt = 5;
				break;
			case UINT:
				alt = 6;
				break;
			case LONG:
				alt = 7;
				break;
			case ULONG:
				alt = 8;
				break;
			case FLOAT:
				alt = 9;
				break;
			case DOUBLE:
				alt = 10;
				break;
			case CHAR:
				alt = 11;
				break;
			case STRING:
				alt = 12;
				break;
			case ID:
				alt = 13;
				break;
			default:
				Error();
				break;
			}
			
			SourceType type_s = default(SourceType);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(BOOL, nt1_i);
					nt2_i = new IntegerType(1, false).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(BYTE, nt1_i);
					nt2_i = new IntegerType(8, false).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(SBYTE, nt1_i);
					nt2_i = new IntegerType(8, true).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(SHORT, nt1_i);
					nt2_i = new IntegerType(16, true).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(USHORT, nt1_i);
					nt2_i = new IntegerType(16, false).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 5:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(INT, nt1_i);
					nt2_i = new IntegerType(32, true).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 6:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(UINT, nt1_i);
					nt2_i = new IntegerType(32, false).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 7:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(LONG, nt1_i);
					nt2_i = new IntegerType(64, true).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 8:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(ULONG, nt1_i);
					nt2_i = new IntegerType(64, false).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 9:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(FLOAT, nt1_i);
					nt2_i = new FloatType(28, 6).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 10:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(DOUBLE, nt1_i);
					nt2_i = new FloatType(49, 15).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 11:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(CHAR, nt1_i);
					nt2_i = new IntegerType(8, true).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 12:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(STRING, nt1_i);
					nt2_i = new MutableType("string").SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			case 13:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					nt2_i = new IdentifierType(nt1_s).SetSpan(nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					type_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '{':
			case ';':
			case '(':
			case ',':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return type_s;
		}
		
		SourceType tmp_26(IAST tmp_26_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case '{':
			case ';':
			case '(':
			case ',':
			case ')':
				alt = 0;
				break;
			case '[':
				alt = 1;
				break;
			case '*':
				alt = 2;
				break;
			default:
				Error();
				break;
			}
			
			SourceType tmp_26_s = default(SourceType);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_26_s = (SourceType)tmp_26_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('[', nt1_i);
					TokenAST nt2_s = Match(']', nt2_i);
					nt3_i = new ArrayType(((SourceType)tmp_26_i)).SetSpan(((SourceType)tmp_26_i), nt2_s); 
					var nt3_s = tmp_26(nt3_i);
					tmp_26_s = nt3_s; 
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match('*', nt1_i);
					nt2_i = new PointerType(((SourceType)tmp_26_i)).SetSpan(((SourceType)tmp_26_i), nt1_s); 
					var nt2_s = tmp_26(nt2_i);
					tmp_26_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case '{':
			case ';':
			case '(':
			case ',':
			case ')':
				break;
			default:
				Error();
				break;
			}
			return tmp_26_s;
		}
		
		SourceModifiter modifiter(IAST modifiter_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case FUNC:
			case STRUCT:
			case MATCH:
			case ID:
				alt = 0;
				break;
			case PUBLIC:
				alt = 1;
				break;
			case PRIVATE:
				alt = 2;
				break;
			case PROTECTED:
				alt = 3;
				break;
			case EXTERN:
				alt = 4;
				break;
			default:
				Error();
				break;
			}
			
			SourceModifiter modifiter_s = default(SourceModifiter);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					modifiter_s = new SourceModifiter(null, Modifiter.Private);
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(PUBLIC, nt1_i);
					modifiter_s = new SourceModifiter(nt1_s, Modifiter.Public);
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(PRIVATE, nt1_i);
					modifiter_s = new SourceModifiter(nt1_s, Modifiter.Private);
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(PROTECTED, nt1_i);
					modifiter_s = new SourceModifiter(nt1_s, Modifiter.Protected);
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(EXTERN, nt1_i);
					modifiter_s = new SourceModifiter(nt1_s, Modifiter.Extern);
				}
				break;
			}
			
			switch (Next.token)
			{
			case FUNC:
			case STRUCT:
			case MATCH:
			case ID:
				break;
			default:
				Error();
				break;
			}
			return modifiter_s;
		}
		
		Path path(IAST path_i)
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
			
			Path path_s = default(Path);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					nt2_i = new Path(nt1_s); 
					var nt2_s = tmp_27(nt2_i);
					path_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ';':
				break;
			default:
				Error();
				break;
			}
			return path_s;
		}
		
		Path tmp_27(IAST tmp_27_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ';':
				alt = 0;
				break;
			case '.':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			Path tmp_27_s = default(Path);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_27_s = (Path)tmp_27_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('.', nt1_i);
					TokenAST nt2_s = Match(ID, nt2_i);
					nt3_i = ((Path)tmp_27_i).Add(nt2_s); 
					var nt3_s = tmp_27(nt3_i);
					tmp_27_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case ';':
				break;
			default:
				Error();
				break;
			}
			return tmp_27_s;
		}
		
		protected override RegAcceptList CreateRegAcceptList()
		{
			var acts = new RegAcceptList();
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('b'), new RegToken('o')), new RegToken('o')), new RegToken('l')), BOOL);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('b'), new RegToken('y')), new RegToken('t')), new RegToken('e')), BYTE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('s'), new RegToken('b')), new RegToken('y')), new RegToken('t')), new RegToken('e')), SBYTE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('s'), new RegToken('h')), new RegToken('o')), new RegToken('r')), new RegToken('t')), SHORT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('u'), new RegToken('s')), new RegToken('h')), new RegToken('o')), new RegToken('r')), new RegToken('t')), USHORT);
			acts.Add(new RegAnd(new RegAnd(new RegToken('i'), new RegToken('n')), new RegToken('t')), INT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('u'), new RegToken('i')), new RegToken('n')), new RegToken('t')), UINT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('l'), new RegToken('o')), new RegToken('n')), new RegToken('g')), LONG);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('u'), new RegToken('l')), new RegToken('o')), new RegToken('n')), new RegToken('g')), ULONG);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('f'), new RegToken('l')), new RegToken('o')), new RegToken('a')), new RegToken('t')), FLOAT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('d'), new RegToken('o')), new RegToken('u')), new RegToken('b')), new RegToken('l')), new RegToken('e')), DOUBLE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('c'), new RegToken('h')), new RegToken('a')), new RegToken('r')), CHAR);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('s'), new RegToken('t')), new RegToken('r')), new RegToken('i')), new RegToken('n')), new RegToken('g')), STRING);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('n'), new RegToken('a')), new RegToken('m')), new RegToken('e')), new RegToken('s')), new RegToken('p')), new RegToken('a')), new RegToken('c')), new RegToken('e')), NAMESPACE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('m'), new RegToken('a')), new RegToken('t')), new RegToken('c')), new RegToken('h')), MATCH);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('s'), new RegToken('t')), new RegToken('r')), new RegToken('u')), new RegToken('c')), new RegToken('t')), STRUCT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('f'), new RegToken('u')), new RegToken('n')), new RegToken('c')), FUNC);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('i'), new RegToken('m')), new RegToken('p')), new RegToken('o')), new RegToken('r')), new RegToken('t')), IMPORT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('p'), new RegToken('u')), new RegToken('b')), new RegToken('l')), new RegToken('i')), new RegToken('c')), PUBLIC);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('p'), new RegToken('r')), new RegToken('i')), new RegToken('v')), new RegToken('a')), new RegToken('t')), new RegToken('e')), PRIVATE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('p'), new RegToken('r')), new RegToken('o')), new RegToken('t')), new RegToken('e')), new RegToken('c')), new RegToken('t')), new RegToken('e')), new RegToken('d')), PROTECTED);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('e'), new RegToken('x')), new RegToken('t')), new RegToken('e')), new RegToken('r')), new RegToken('n')), EXTERN);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('s'), new RegToken('w')), new RegToken('i')), new RegToken('t')), new RegToken('c')), new RegToken('h')), SWITCH);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('c'), new RegToken('a')), new RegToken('s')), new RegToken('e')), CASE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('w'), new RegToken('h')), new RegToken('i')), new RegToken('l')), new RegToken('e')), WHILE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('b'), new RegToken('r')), new RegToken('e')), new RegToken('a')), new RegToken('k')), BREAK);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('c'), new RegToken('o')), new RegToken('n')), new RegToken('t')), new RegToken('i')), new RegToken('n')), new RegToken('u')), new RegToken('e')), CONTINUE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('r'), new RegToken('e')), new RegToken('t')), new RegToken('u')), new RegToken('r')), new RegToken('n')), RETURN);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('d'), new RegToken('e')), new RegToken('f')), new RegToken('a')), new RegToken('u')), new RegToken('l')), new RegToken('t')), DEFAULT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('t'), new RegToken('r')), new RegToken('u')), new RegToken('e')), TRUE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('f'), new RegToken('a')), new RegToken('l')), new RegToken('s')), new RegToken('e')), FALSE);
			acts.Add(new RegAnd(new RegAnd(new RegToken('v'), new RegToken('a')), new RegToken('r')), VAR);
			acts.Add(new RegAnd(new RegAnd(new RegToken('n'), new RegToken('e')), new RegToken('w')), NEW);
			acts.Add(new RegAnd(new RegToken('i'), new RegToken('f')), IF);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('e'), new RegToken('l')), new RegToken('s')), new RegToken('e')), ELSE);
			acts.Add(new RegAnd(new RegToken('='), new RegToken('>')), ARROW);
			acts.Add(new RegAnd(new RegToken('&'), new RegToken('&')), ANDAND);
			acts.Add(new RegAnd(new RegToken('|'), new RegToken('|')), OROR);
			acts.Add(new RegAnd(new RegToken('+'), new RegToken('+')), INC);
			acts.Add(new RegAnd(new RegToken('-'), new RegToken('-')), DEC);
			acts.Add(new RegToken('^'), '^');
			acts.Add(new RegToken('|'), '|');
			acts.Add(new RegToken('+'), '+');
			acts.Add(new RegToken('-'), '-');
			acts.Add(new RegToken('='), '=');
			acts.Add(new RegToken('*'), '*');
			acts.Add(new RegToken('/'), '/');
			acts.Add(new RegToken('%'), '%');
			acts.Add(new RegToken(';'), ';');
			acts.Add(new RegToken('('), '(');
			acts.Add(new RegToken(')'), ')');
			acts.Add(new RegToken('{'), '{');
			acts.Add(new RegToken('}'), '}');
			acts.Add(new RegToken('['), '[');
			acts.Add(new RegToken(']'), ']');
			acts.Add(new RegToken(','), ',');
			acts.Add(new RegToken('.'), '.');
			acts.Add(new RegToken(':'), ':');
			acts.Add(new RegToken('>'), GT);
			acts.Add(new RegAnd(new RegToken('>'), new RegToken('=')), GE);
			acts.Add(new RegToken('<'), LT);
			acts.Add(new RegAnd(new RegToken('<'), new RegToken('=')), LE);
			acts.Add(new RegAnd(new RegToken('='), new RegToken('=')), EQ);
			acts.Add(new RegAnd(new RegToken('!'), new RegToken('=')), NE);
			acts.Add(new RegAnd(new RegToken('>'), new RegToken('>')), SHR);
			acts.Add(new RegAnd(new RegToken('<'), new RegToken('<')), SHL);
			acts.Add(new RegAnd(new RegOr(new RegOr(new RegToken('_'), new RegTokenRange(97, 122)), new RegTokenRange(65, 90)), new RegZeroOrMore(new RegOr(new RegOr(new RegOr(new RegToken('_'), new RegTokenRange(97, 122)), new RegTokenRange(65, 90)), new RegTokenRange(48, 57)))), ID);
			acts.Add(new RegOr(new RegAnd(new RegToken('-'), new RegOneOrMore(new RegTokenRange(48, 57))), new RegOneOrMore(new RegTokenRange(48, 57))), INTEGER);
			acts.Add(new RegOr(new RegAnd(new RegToken('-'), new RegOneOrMore(new RegTokenRange(48, 57))), new RegAnd(new RegAnd(new RegOneOrMore(new RegTokenRange(48, 57)), new RegTokenOutsideRange(10, 10)), new RegOneOrMore(new RegTokenRange(48, 57)))), FLOAT);
			acts.Add(new RegAnd(new RegOr(new RegAnd(new RegToken('\\'), new RegToken('u')), new RegAnd(new RegToken('0'), new RegToken('x'))), new RegOneOrMore(new RegOr(new RegOr(new RegTokenRange(48, 57), new RegTokenRange(97, 102)), new RegTokenRange(65, 70)))), UNICODE);
			acts.Add(new RegToken('$'), (ref NFA.Token tk, LexReader rd, NFA nfa) => {
				var sb = new StringBuilder();
				ReadRegex(rd, sb);
				rd.SetMatch();
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				tk.strRead = sb.ToString();
				tk.token = REGEX;
				return true;
			});
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
			acts.Add(new RegOneOrMore(new RegOr(new RegOr(new RegOr(new RegToken(' '), new RegToken(10)), new RegToken(13)), new RegToken(9))));
			return acts;
		}
	}
}
