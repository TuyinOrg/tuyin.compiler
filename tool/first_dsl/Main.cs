class _
{
	public static int Main(string[] args)
	{
		try
		{
			if (args.Length == 0)
				args = new string[1] 
				{
					@"../../../../../cil/Tuyin.IR.Reflection/Symbols/DIMetadataParser.gram"
				};

			var debug = false;
			var check = false;

			string grammarFileName = null;
			string codeFileName = null;

			var cp = new U.CommandProcessor("d|debug|c|check", args) { Program = "LLParserGen" };
			while (cp.Read())
			{
				switch (cp.Option)
				{
				case "d":
				case "debug":
					debug = true;
					break;

				case "c":
				case "check":
					check = true;
					break;

				case "":
					if (grammarFileName == null) grammarFileName = cp.Argument;
					else if (codeFileName == null) codeFileName = cp.Argument;
					break;
				}
			}

			if (grammarFileName == null)
			{
				Console.Error.WriteLine("Grammar filename required.");
				return 1;
			}

			var gr = new GrammarReader();
			var po = new ParserOptions();
			using (var tr = File.OpenText(grammarFileName))
			{
				gr.Read(grammarFileName, tr, po);
			}

			var G = gr.G;
			var lexerActions = gr.LexerActions;

			if (G.CheckGrammar(Console.Out) == false)
				return 1;

			if (check)
			{
				if (G.CheckConflicts(Console.Out) == false)
					return 1;
			}

			G = G.RemoveEmptyLeftRecursion();
			if (debug)
			{
				Console.WriteLine(G);
				Console.WriteLine();
			}
			G = G.RemoveLeftRecursion();
			G = G.LeftFactorize();

			if (debug)
			{
				Console.WriteLine(G);
				Console.WriteLine();
			}

			if (G.CheckConflicts(Console.Out) == false)
				return 1;

			if (codeFileName == null)
				codeFileName = Path.ChangeExtension(grammarFileName, ".cs");

			using (var wr = File.CreateText(codeFileName))
			{
				var csw = new U.CsStreamWriter(wr);
				G.GenerateCode(lexerActions, po, csw);
			}
		}
		catch (ApplicationException ex)
		{
			Console.Error.WriteLine(ex.Message);
			return 1;
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex.Message);
			Console.Error.WriteLine();
			Console.Error.WriteLine(ex.StackTrace);
			return 1;
		}

		return 0;
	}
}

