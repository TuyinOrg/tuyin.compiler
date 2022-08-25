#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

using LLParserLexerLib;
using System.Text;
using System.Collections.Generic;

namespace tui.tool.template
{
	partial class TemplateControlParser : ParserBase
	{
		public const int STRING = -2;
		public const int DEFINE = -3;
		public const int ID = -4;
		public const int INTEGER = -5;
		public const int LINE = -6;
		public const int HEXCOLOR = -7;
		public const int RECT = -8;
		public const int ELLIPSE = -9;
		public const int ELEMENT = -10;
		public const int NEWLINE = -11;
		public const int FROM = -12;
		public const int TO = -13;
		public const int IN = -14;
		public const int WHEN = -15;
		public const int KEYDOWN = -16;
		public const int KEYUP = -17;
		public const int MOUSEDOWN = -18;
		public const int MOUSEUP = -19;
		public const int MOUSEMOVE = -20;
		public const int MOUSEENTER = -21;
		public const int MOUSELEAVE = -22;
		public const int LEFT = -23;
		public const int RIGHT = -24;
		public const int MIDDLE = -25;
		
		Dictionary<int, string> _token;
		public override Dictionary<int, string> Token
		{
			get
			{
				if (_token == null)
				{
					_token = new Dictionary<int, string>();
					_token.Add(-1, "EOF");
					_token.Add(-2, "STRING");
					_token.Add(-3, "DEFINE");
					_token.Add(-4, "ID");
					_token.Add(-5, "INTEGER");
					_token.Add(-6, "LINE");
					_token.Add(-7, "HEXCOLOR");
					_token.Add(-8, "RECT");
					_token.Add(-9, "ELLIPSE");
					_token.Add(-10, "ELEMENT");
					_token.Add(-11, "NEWLINE");
					_token.Add(-12, "FROM");
					_token.Add(-13, "TO");
					_token.Add(-14, "IN");
					_token.Add(-15, "WHEN");
					_token.Add(-16, "KEYDOWN");
					_token.Add(-17, "KEYUP");
					_token.Add(-18, "MOUSEDOWN");
					_token.Add(-19, "MOUSEUP");
					_token.Add(-20, "MOUSEMOVE");
					_token.Add(-21, "MOUSEENTER");
					_token.Add(-22, "MOUSELEAVE");
					_token.Add(-23, "LEFT");
					_token.Add(-24, "RIGHT");
					_token.Add(-25, "MIDDLE");
				}
				return _token;
			}
		}
		
		Template template(IAST template_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			Template template_s = default(Template);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = element_list(nt1_i);
					template_s = new Template(nt1_s, null);
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
			return template_s;
		}
		
		ElementList element_list(IAST element_list_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case -1:
				alt = 0;
				break;
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ElementList element_list_s = default(ElementList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					element_list_s = new ElementList();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = tmp_1(nt1_i);
					element_list_s = nt1_s;
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
			return element_list_s;
		}
		
		ElementList tmp_1(IAST tmp_1_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			ElementList tmp_1_s = default(ElementList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = element(nt1_i);
					nt2_i = (new ElementList()).Add(nt1_s); 
					var nt2_s = tmp_4(nt2_i);
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
		
		ElementList tmp_4(IAST tmp_4_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case -1:
				alt = 0;
				break;
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			ElementList tmp_4_s = default(ElementList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_4_s = (ElementList)tmp_4_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = element(nt1_i);
					nt2_i = ((ElementList)tmp_4_i).Add(nt1_s); 
					var nt2_s = tmp_4(nt2_i);
					tmp_4_s = nt2_s; 
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
		
		Element element(IAST element_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case STRING:
				alt = 0;
				break;
			case DEFINE:
				alt = 1;
				break;
			case LINE:
				alt = 2;
				break;
			case RECT:
				alt = 3;
				break;
			case ELLIPSE:
				alt = 4;
				break;
			case ELEMENT:
				alt = 5;
				break;
			default:
				Error();
				break;
			}
			
			Element element_s = default(Element);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(STRING, nt1_i);
					element_s = new ElementLabel(nt1_s);
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					
					TokenAST nt1_s = Match(DEFINE, nt1_i);
					TokenAST nt2_s = Match(ID, nt2_i);
					TokenAST nt3_s = Match('=', nt3_i);
					TokenAST nt4_s = Match(INTEGER, nt4_i);
					element_s = new Property(nt1_s, nt3_s);
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					var nt6_i = default(IAST);
					
					TokenAST nt1_s = Match(LINE, nt1_i);
					TokenAST nt2_s = Match(HEXCOLOR, nt2_i);
					TokenAST nt3_s = Match(INTEGER, nt3_i);
					TokenAST nt4_s = Match(INTEGER, nt4_i);
					TokenAST nt5_s = Match(INTEGER, nt5_i);
					TokenAST nt6_s = Match(INTEGER, nt6_i);
					element_s = new ElementLine(nt2_s, nt3_s, nt4_s, nt5_s, nt6_s);
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					var nt6_i = default(IAST);
					
					TokenAST nt1_s = Match(RECT, nt1_i);
					TokenAST nt2_s = Match(HEXCOLOR, nt2_i);
					TokenAST nt3_s = Match(INTEGER, nt3_i);
					TokenAST nt4_s = Match(INTEGER, nt4_i);
					TokenAST nt5_s = Match(INTEGER, nt5_i);
					TokenAST nt6_s = Match(INTEGER, nt6_i);
					element_s = new ElementRect(nt2_s, nt3_s, nt4_s, nt5_s, nt6_s);
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					var nt6_i = default(IAST);
					
					TokenAST nt1_s = Match(ELLIPSE, nt1_i);
					TokenAST nt2_s = Match(HEXCOLOR, nt2_i);
					TokenAST nt3_s = Match(INTEGER, nt3_i);
					TokenAST nt4_s = Match(INTEGER, nt4_i);
					TokenAST nt5_s = Match(INTEGER, nt5_i);
					TokenAST nt6_s = Match(INTEGER, nt6_i);
					element_s = new ElementEllipse(nt2_s, nt3_s, nt4_s, nt5_s, nt6_s);
				}
				break;
			case 5:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					var nt4_i = default(IAST);
					var nt5_i = default(IAST);
					var nt6_i = default(IAST);
					
					TokenAST nt1_s = Match(ELEMENT, nt1_i);
					TokenAST nt2_s = Match(INTEGER, nt2_i);
					TokenAST nt3_s = Match('=', nt3_i);
					TokenAST nt4_s = Match(ID, nt4_i);
					var nt5_s = property_list(nt5_i);
					var nt6_s = animate_list(nt6_i);
					element_s = new ElementControl(nt2_s, nt4_s, nt5_s, nt6_s)	;
				}
				break;
			}
			
			switch (Next.token)
			{
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return element_s;
		}
		
		PropertyList property_list(IAST property_list_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				alt = 0;
				break;
			case ',':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			PropertyList property_list_s = default(PropertyList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					property_list_s = new PropertyList();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = tmp_2(nt1_i);
					property_list_s = nt1_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return property_list_s;
		}
		
		PropertyList tmp_2(IAST tmp_2_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case ',':
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			PropertyList tmp_2_s = default(PropertyList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(',', nt1_i);
					var nt2_s = property(nt2_i);
					nt3_i = (new PropertyList()).Add(nt2_s); 
					var nt3_s = tmp_5(nt3_i);
					tmp_2_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_2_s;
		}
		
		PropertyList tmp_5(IAST tmp_5_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				alt = 0;
				break;
			case ',':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			PropertyList tmp_5_s = default(PropertyList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_5_s = (PropertyList)tmp_5_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(',', nt1_i);
					var nt2_s = property(nt2_i);
					nt3_i = ((PropertyList)tmp_5_i).Add(nt2_s); 
					var nt3_s = tmp_5(nt3_i);
					tmp_5_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_5_s;
		}
		
		Property property(IAST property_i)
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
			
			Property property_s = default(Property);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					TokenAST nt2_s = Match(':', nt2_i);
					TokenAST nt3_s = Match(INTEGER, nt3_i);
					property_s = new Property(nt1_s, nt3_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case ',':
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return property_s;
		}
		
		AnimateList animate_list(IAST animate_list_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				alt = 0;
				break;
			case NEWLINE:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			AnimateList animate_list_s = default(AnimateList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					animate_list_s = new AnimateList();
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					var nt1_s = tmp_3(nt1_i);
					animate_list_s = nt1_s;
				}
				break;
			}
			
			switch (Next.token)
			{
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return animate_list_s;
		}
		
		AnimateList tmp_3(IAST tmp_3_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case NEWLINE:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			AnimateList tmp_3_s = default(AnimateList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(NEWLINE, nt1_i);
					var nt2_s = animate(nt2_i);
					nt3_i = (new AnimateList()).Add(nt2_s); 
					var nt3_s = tmp_6(nt3_i);
					tmp_3_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_3_s;
		}
		
		AnimateList tmp_6(IAST tmp_6_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				alt = 0;
				break;
			case NEWLINE:
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			AnimateList tmp_6_s = default(AnimateList);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_6_s = (AnimateList)tmp_6_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match(NEWLINE, nt1_i);
					var nt2_s = animate(nt2_i);
					nt3_i = ((AnimateList)tmp_6_i).Add(nt2_s); 
					var nt3_s = tmp_6(nt3_i);
					tmp_6_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return tmp_6_s;
		}
		
		Animate animate(IAST animate_i)
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
			
			Animate animate_s = default(Animate);
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
					var nt7_i = default(IAST);
					var nt8_i = default(IAST);
					var nt9_i = default(IAST);
					
					var nt1_s = member(nt1_i);
					TokenAST nt2_s = Match(FROM, nt2_i);
					TokenAST nt3_s = Match(INTEGER, nt3_i);
					TokenAST nt4_s = Match(TO, nt4_i);
					TokenAST nt5_s = Match(INTEGER, nt5_i);
					TokenAST nt6_s = Match(IN, nt6_i);
					TokenAST nt7_s = Match(INTEGER, nt7_i);
					TokenAST nt8_s = Match(WHEN, nt8_i);
					var nt9_s = event_input(nt9_i);
					animate_s = new AnimateTo(nt1_s, nt3_s, nt5_s, nt7_s, nt9_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return animate_s;
		}
		
		Member member(IAST member_i)
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
			
			Member member_s = default(Member);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					TokenAST nt1_s = Match(ID, nt1_i);
					nt2_i = new Member(nt1_s); 
					var nt2_s = tmp_7(nt2_i);
					member_s = nt2_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case FROM:
				break;
			default:
				Error();
				break;
			}
			return member_s;
		}
		
		Member tmp_7(IAST tmp_7_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case FROM:
				alt = 0;
				break;
			case '.':
				alt = 1;
				break;
			default:
				Error();
				break;
			}
			
			Member tmp_7_s = default(Member);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					tmp_7_s = (Member)tmp_7_i; 
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					var nt3_i = default(IAST);
					
					TokenAST nt1_s = Match('.', nt1_i);
					TokenAST nt2_s = Match(ID, nt2_i);
					nt3_i = ((Member)tmp_7_i).Add(nt2_s); 
					var nt3_s = tmp_7(nt3_i);
					tmp_7_s = nt3_s; 
				}
				break;
			}
			
			switch (Next.token)
			{
			case FROM:
				break;
			default:
				Error();
				break;
			}
			return tmp_7_s;
		}
		
		InputEvent event_input(IAST event_input_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case KEYDOWN:
			case KEYUP:
			case MOUSEDOWN:
			case MOUSEUP:
			case MOUSEMOVE:
			case MOUSEENTER:
			case MOUSELEAVE:
				alt = 0;
				break;
			default:
				Error();
				break;
			}
			
			InputEvent event_input_s = default(InputEvent);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					var nt2_i = default(IAST);
					
					var nt1_s = event_type(nt1_i);
					var nt2_s = event_value(nt2_i);
					event_input_s = new InputEvent(nt1_s, nt2_s);
				}
				break;
			}
			
			switch (Next.token)
			{
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return event_input_s;
		}
		
		EventType event_type(IAST event_type_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case KEYDOWN:
				alt = 0;
				break;
			case KEYUP:
				alt = 1;
				break;
			case MOUSEDOWN:
				alt = 2;
				break;
			case MOUSEUP:
				alt = 3;
				break;
			case MOUSEMOVE:
				alt = 4;
				break;
			case MOUSEENTER:
				alt = 5;
				break;
			case MOUSELEAVE:
				alt = 6;
				break;
			default:
				Error();
				break;
			}
			
			EventType event_type_s = default(EventType);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(KEYDOWN, nt1_i);
					event_type_s = EventType.KeyDown;
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(KEYUP, nt1_i);
					event_type_s = EventType.KeyUp;
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(MOUSEDOWN, nt1_i);
					event_type_s = EventType.MouseDown;
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(MOUSEUP, nt1_i);
					event_type_s = EventType.MouseUp;
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(MOUSEMOVE, nt1_i);
					event_type_s = EventType.MouseMove;
				}
				break;
			case 5:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(MOUSEENTER, nt1_i);
					event_type_s = EventType.MouseEnter;
				}
				break;
			case 6:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(MOUSELEAVE, nt1_i);
					event_type_s = EventType.MouseLeave;
				}
				break;
			}
			
			switch (Next.token)
			{
			case LEFT:
			case RIGHT:
			case MIDDLE:
			case STRING:
			case INTEGER:
				break;
			default:
				Error();
				break;
			}
			return event_type_s;
		}
		
		int event_value(IAST event_value_i)
		{
			int alt = 0;
			switch (Next.token)
			{
			case LEFT:
				alt = 0;
				break;
			case RIGHT:
				alt = 1;
				break;
			case MIDDLE:
				alt = 2;
				break;
			case STRING:
				alt = 3;
				break;
			case INTEGER:
				alt = 4;
				break;
			default:
				Error();
				break;
			}
			
			int event_value_s = default(int);
			switch (alt)
			{
			case 0:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(LEFT, nt1_i);
					event_value_s = 0;
				}
				break;
			case 1:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(RIGHT, nt1_i);
					event_value_s = 1;
				}
				break;
			case 2:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(MIDDLE, nt1_i);
					event_value_s = 2;
				}
				break;
			case 3:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(STRING, nt1_i);
					event_value_s = CharToKeys(nt1_s.strRead[0]);
				}
				break;
			case 4:
				{
					var nt1_i = default(IAST);
					
					TokenAST nt1_s = Match(INTEGER, nt1_i);
					event_value_s = int.Parse(nt1_s.strRead);
				}
				break;
			}
			
			switch (Next.token)
			{
			case NEWLINE:
			case STRING:
			case DEFINE:
			case LINE:
			case RECT:
			case ELLIPSE:
			case ELEMENT:
			case -1:
				break;
			default:
				Error();
				break;
			}
			return event_value_s;
		}
		
		protected override RegAcceptList CreateRegAcceptList()
		{
			var acts = new RegAcceptList();
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('e'), new RegToken('l')), new RegToken('e')), new RegToken('m')), new RegToken('e')), new RegToken('n')), new RegToken('t')), ELEMENT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('l'), new RegToken('i')), new RegToken('n')), new RegToken('e')), LINE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('r'), new RegToken('e')), new RegToken('c')), new RegToken('t')), RECT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('e'), new RegToken('l')), new RegToken('l')), new RegToken('i')), new RegToken('p')), new RegToken('s')), new RegToken('e')), ELLIPSE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('w'), new RegToken('h')), new RegToken('e')), new RegToken('n')), WHEN);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('f'), new RegToken('r')), new RegToken('o')), new RegToken('m')), FROM);
			acts.Add(new RegAnd(new RegToken('t'), new RegToken('o')), TO);
			acts.Add(new RegAnd(new RegToken('i'), new RegToken('n')), IN);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('k'), new RegToken('e')), new RegToken('y')), new RegToken('d')), new RegToken('o')), new RegToken('w')), new RegToken('n')), KEYDOWN);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('k'), new RegToken('e')), new RegToken('y')), new RegToken('u')), new RegToken('p')), KEYUP);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('m'), new RegToken('o')), new RegToken('u')), new RegToken('s')), new RegToken('e')), new RegToken('d')), new RegToken('o')), new RegToken('w')), new RegToken('n')), MOUSEDOWN);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('m'), new RegToken('o')), new RegToken('u')), new RegToken('s')), new RegToken('e')), new RegToken('u')), new RegToken('p')), MOUSEUP);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('m'), new RegToken('o')), new RegToken('u')), new RegToken('s')), new RegToken('e')), new RegToken('m')), new RegToken('o')), new RegToken('v')), new RegToken('e')), MOUSEMOVE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('m'), new RegToken('o')), new RegToken('u')), new RegToken('s')), new RegToken('e')), new RegToken('e')), new RegToken('n')), new RegToken('t')), new RegToken('e')), new RegToken('r')), MOUSEENTER);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('m'), new RegToken('o')), new RegToken('u')), new RegToken('s')), new RegToken('e')), new RegToken('l')), new RegToken('e')), new RegToken('a')), new RegToken('v')), new RegToken('e')), MOUSELEAVE);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegToken('l'), new RegToken('e')), new RegToken('f')), new RegToken('t')), LEFT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('r'), new RegToken('i')), new RegToken('g')), new RegToken('h')), new RegToken('t')), RIGHT);
			acts.Add(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegAnd(new RegToken('m'), new RegToken('i')), new RegToken('d')), new RegToken('d')), new RegToken('l')), new RegToken('e')), MIDDLE);
			acts.Add(new RegToken('='), '=');
			acts.Add(new RegToken('<'), '<');
			acts.Add(new RegToken('>'), '>');
			acts.Add(new RegToken('('), '(');
			acts.Add(new RegToken(')'), ')');
			acts.Add(new RegToken('{'), '{');
			acts.Add(new RegToken('}'), '}');
			acts.Add(new RegToken('['), '[');
			acts.Add(new RegToken(']'), ']');
			acts.Add(new RegToken('-'), '-');
			acts.Add(new RegToken('|'), '|');
			acts.Add(new RegToken(10), NEWLINE);
			acts.Add(new RegAnd(new RegToken('#'), new RegOneOrMore(new RegOr(new RegOr(new RegTokenRange(97, 122), new RegTokenRange(65, 90)), new RegTokenRange(48, 57)))), HEXCOLOR);
			acts.Add(new RegAnd(new RegOr(new RegOr(new RegToken('_'), new RegTokenRange(97, 122)), new RegTokenRange(65, 90)), new RegZeroOrMore(new RegOr(new RegOr(new RegOr(new RegToken('_'), new RegTokenRange(97, 122)), new RegTokenRange(65, 90)), new RegTokenRange(48, 57)))), ID);
			acts.Add(new RegOr(new RegAnd(new RegToken('-'), new RegOneOrMore(new RegTokenRange(48, 57))), new RegOneOrMore(new RegTokenRange(48, 57))), INTEGER);
			acts.Add(new RegAnd(new RegToken('/'), new RegToken('*')), (ref NFA.Token tk, LexReader rd, NFA nfa) => {
				ReadComment(rd);
				rd.SetMatch();
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				return false;
			});
			acts.Add(new RegToken(39), (ref NFA.Token tk, LexReader rd, NFA nfa) => {
				var sb = new StringBuilder();
				ReadString(rd, sb);
				rd.SetMatch();
				rd.EndToken(out tk.strRead, out tk.fileName, out tk.line, out tk.startIndex, out tk.endIndex);
				tk.strRead = sb.ToString();
				tk.token = STRING;
				return true;
			});
			acts.Add(new RegAnd(new RegAnd(new RegToken('/'), new RegToken('/')), new RegZeroOrMore(new RegTokenOutsideRange(10, 10))));
			acts.Add(new RegOneOrMore(new RegOr(new RegOr(new RegOr(new RegToken(' '), new RegToken(10)), new RegToken(13)), new RegToken(9))));
			return acts;
		}
	}
}
