using System.Collections.Specialized;

public partial class U
{
	public class CommandProcessor
	{
		public delegate void ErrorDelegate(string pgm, string fmt, params object[] a);

		/// <summary>
		/// Il formato è "abc:|e+|fg"
		/// "fg" non richiede argomento aggiuntivo
		/// "abc" richiede un argomento
		/// "e"   può specificare + o -
		/// "*"   indica che sono ammesse anche altre opzioni non comprese nel formato
		/// </summary>
		/// <param name="fmt"></param>
		/// <param name="args"></param>
		public CommandProcessor(string fmt, string[] args)
			: this(fmt, 0, args)
		{
		}

		public CommandProcessor(string fmt, int argc, string[] args)
		{
			_argc = argc;
			_args = args;

			if (fmt != null)
			{
				_fmt = new StringCollection();
				foreach (string f in fmt.Split('|'))
					_fmt.Add(f);
			}
		}


		public CommandProcessor(string[] args)
			: this(null, args)
		{
		}

		/// <summary>
		/// ritorna true se c'e' un opzione o un argomento da leggere.
		/// Option e' "" se non ci sono piu' opzioni (ossia se iniziano i files)
		/// altrimenti Option e' valorizzato alla stringa di opzione in ingresso.
		/// Argument punta all'eventuale argomento presente dopo il : dell'opzione.
		/// o al file se Option=""
		/// Switch punta a '+' o a '-' dopo un opzione.
		/// 
		/// Specificando il formato si può indicare quali opzioni hano un argomento (:)
		/// e quali possono avere uno switch (+ o -)
		/// </summary>
		/// <returns></returns>
		public bool Read()
		{
			_option = string.Empty;
			_argument = string.Empty;
			_switch = string.Empty;

			if (_argc >= _args.Length)
				return false;

			string ag = _args[_argc];
			int agLen = ag.Length;

			if (ag[0] != '-' && ag[0] != '/')
			{
				// nessuna opzione
				_argument = ag;
				_argc++;
				return true;
			}

			int agIndex = 1;
			while (agIndex < agLen && Char.IsLetterOrDigit(ag, agIndex))
				agIndex++;

			_option = ag.Substring(1, agIndex - 1);

			if (agIndex < agLen)
			{
				switch (ag[agIndex])
				{
				case ':':
					// opzione con argomento
					agIndex++;
					if (agIndex < agLen)
						_argument = ag.Substring(agIndex);
					else if (++_argc < _args.Length)
						_argument = _args[_argc];
					else
						Error(Program, "Invalid option '{0}': argument required.", Option);
					break;

				case '+':
				case '-':
					// opzione con switch +/-
					_switch = ag.Substring(agIndex, 1);
					agIndex++;
					if (agIndex < agLen)
						Error(Program, "Invalid option '{0}': argument '+' or '-' required.", Option);
					break;

				default:
					Error(Program, "Invalid option '{0}'", ag);
					break;
				}
			}

			CheckOption();

			_argc++;
			return true;
		}

		/// <summary>
		/// l'opzione o "" se è un argomento senza opzione
		/// </summary>
		public string Option { get { return _option; } }
		/// <summary>
		/// lo switch specificato o "" se non è stato specificato lo switch
		/// </summary>
		public string Switch { get { return _switch; } }

		/// <summary>
		/// L'argomento dopo un opzione con :
		/// </summary>
		public string Argument { get { return _argument; } }

		public int Argc { get { return _argc; } }
		public ErrorDelegate Error = new ErrorDelegate(DefaultError);

		public string Program { get; set; }

		private int _argc = 0;
		private string[] _args;
		private StringCollection _fmt;

		public string _option = null;
		public string _switch = null;
		public string _argument = null;

		private static void DefaultError(string pgm, string fmt, params object[] a)
		{
			if (pgm != null) Console.Error.Write("{0}: ", pgm);
			Console.Error.WriteLine(fmt, a);
			Environment.Exit(1);
		}

		private void CheckOption()
		{
			if (_fmt == null) return;

			foreach (string s in _fmt)
			{
				string requiredOption = s;
				bool requireArgument = s.EndsWith(":");
				bool acceptSwitch = s.EndsWith("+") || s.EndsWith("-");

				if (acceptSwitch || requireArgument)
					requiredOption = s.Substring(0, s.Length - 1);

				if (string.Compare(requiredOption, Option, true) == 0)
				{
					if (requireArgument && Argument.Length == 0)
						Error(Program, "Option '{0}' require an argument.", Option);
					else if (acceptSwitch == false && Switch.Length > 0)
						Error(Program, "Option '{0}' doesn't require switch +/-.", Option);

					return;
				}
			}

			if (!_fmt.Contains("*"))
				Error(Program, "Invalid option '{0}'.", Option);
		}
	}
}
