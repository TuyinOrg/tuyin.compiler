using Tuyin.IR.Compiler.Uitls;

namespace Tuyin.IR.Compiler.Target.Translates
{
    abstract class Translater
    {
        public abstract TargetLanguage Language { get; }

        internal unsafe string Translate(object cfg)
        {
            throw new NotImplementedException();

            var chars = new CharSource(2048);


            return new string(chars.Handle);
        }

        internal static string Convert(string source, TargetLanguage targetLanguage) 
        {
            return Convert(Target.ParseSource(source), targetLanguage);
        }

        internal static string Convert(Target model, TargetLanguage targetLanguage) 
        {
            /*
            var modelTemplateName = targetLanguage switch
            {
                TargetLanguage.CSharp => "Model_CSharp",
                TargetLanguage.C => "Model_C",
                TargetLanguage.Javascript => "Model_Javascript",
                TargetLanguage.Python => "Model_Python",
                TargetLanguage.Tuyin => "Model_Tuyin",
                _ => throw new NotImplementedException()
            };

            using var tuyinStream = new StreamReader(typeof(Program).Assembly.GetManifestResourceStream($"Tuyin.IR.Compiler.Target.Templates.{modelTemplateName}.txt"));
            var tuyinTemplate = tuyinStream.ReadToEnd();
            var fr = new FastReplacer("{", "}");
            fr.Append(tuyinTemplate);
            fr.Replace("{MODEL_NAME}", string.Join(".", model.Namespace.Select(x => x.strRead)));


            var references = new List<ExternalReference>();
            foreach (var func in model.Declares.Where(x => x.DeclareType == DeclareType.Function).Cast<FuncDecl>())
            {
                references.AddRange(
                    Convert(func, targetLanguage, out string source));

                fr.Append(source);
            }

            var importSource = string.Empty;
            foreach (var import in model.Imports)
            {
                // 得到import所使用模块

            }

            fr.Replace("{IMPORT_SOURCE}", importSource);
            return fr.ToString();
            */
            throw new NotImplementedException();
        }

        internal static IEnumerable<object> Convert(FuncDecl func, TargetLanguage targetLanguage, out string source) 
        {
            /*
            var funcBodyTemplateName = targetLanguage switch
            {
                TargetLanguage.CSharp => "Function_CSharp",
                TargetLanguage.C => "Function_C",
                TargetLanguage.Javascript => "Function_Javascript",
                TargetLanguage.Python => "Function_Python",
                TargetLanguage.Tuyin => "Function_Tuyin",
                _ => throw new NotImplementedException()
            };

            var references = Convert(func.Body, targetLanguage, out string bodySource);

            using var tuyinStream = new StreamReader(typeof(Program).Assembly.GetManifestResourceStream($"Tuyin.IR.Compiler.Target.Templates.{funcBodyTemplateName}.txt"));
            var tuyinTemplate = tuyinStream.ReadToEnd();
            var fr = new FastReplacer("{", "}");
            fr.Append(tuyinTemplate);
            fr.Replace("{BODY_SOURCE}", bodySource);
            source = fr.ToString();

            return references;
            */
            throw new NotImplementedException();
        }

        internal static string Convert(StmtRoot stmt, TargetLanguage targetLanguage) 
        {
            Convert(stmt, targetLanguage, out string source);
            return source;
        }

        internal static IEnumerable<object> Convert(StmtRoot stmt, TargetLanguage targetLanguage, out string source) 
        {
            /*
            // 首先转成cfg后在进行翻译
            var builder = new StatmentBuilder();
            stmt.Write(builder);
            var cfg = Analysis.Environment.CreateCFG(builder);

            source = null;
            switch (targetLanguage)
            {
                case TargetLanguage.C: source = new CTranslater().Translate(cfg); break;
                case TargetLanguage.CSharp: source = new CSharpTranslater().Translate(cfg); break;
                case TargetLanguage.Python: source = new PythonTranslater().Translate(cfg); break;
                case TargetLanguage.Javascript: source = new JavascriptTranslater().Translate(cfg); break;
            }

            return cfg.ExternalReferences;
            */
            throw new NotImplementedException();
        }
    }
}
