using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;


public partial class U
{
	public class CsStreamWriter
	{
		public CsStreamWriter(TextWriter sw)
		{
			_sw = sw;
			_tab = 0;
			_last = "";
			_sb = new StringBuilder();

			_stmt = new List<Regex>
				{
					new Regex(@"^if \(.*\)$"),
					new Regex(@"^else$"),
					new Regex(@"^else if \(.*\)$"),
					new Regex(@"^for \(.*\)$"),
					new Regex(@"^foreach \(.*\)$"),
					new Regex(@"^while \(.*\)$"),
					new Regex(@"^do$")
				};
		}

		public void WriteLine()
		{
			WriteLine("");
		}
		public void WriteLine(string s)
		{
			_sb.Append(s);
			s = _sb.ToString(); 
			_sb = new StringBuilder();

			if (s == "{")
			{
				Tab();
				_sw.WriteLine("{");
				_tab += 1;
			}
			else if (s == "}")
			{
				_tab -= 1;
				Tab();
				_sw.WriteLine("}");
			}
			else if (s.StartsWith("case") || s.StartsWith("default"))
			{
				_tab -= 1;
				Tab();
				_sw.WriteLine(s);
				_tab += 1;
			}
			else if (MatchLast())
			{
				_tab += 1;
				Tab();
				_sw.WriteLine(s);
				_tab -= 1;
			}
			else
			{
				Tab();
				_sw.WriteLine(s);
			}

			_last = s;
		}
		public void WriteLine(string s, params object[] a)
		{
			string v = string.Format(CultureInfo.InvariantCulture, s, a);
			WriteLine(v);
		}

		public void Write(string s)
		{
			_sb.Append(s);
		}
		public void Write(string s, params object [] a)
		{
			_sb.AppendFormat(CultureInfo.InvariantCulture, s, a);
		}

		public void SetTab(int i)
		{
			_tab += i;
		}

		private readonly TextWriter _sw;
		private int _tab;
		private readonly List<Regex> _stmt;
		private string _last;
		private StringBuilder _sb;

		private void Tab()
		{
			for (int i = 0; i < _tab; ++i)
				_sw.Write("\t");
		}
		private bool MatchLast()
		{
			// il primo della lista _stmt che ha .Match().success esce con true, altrimenti si esce con false.
			return _stmt.Any(m => m.Match(_last).Success);
		}
	}
}
