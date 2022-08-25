using System.Diagnostics;

namespace Tuyin.IR.Analysis.Data
{
    [DebuggerDisplay("{ToString()}")]
    public struct Message
    {
        public Message(MessageInfo info, SourceSpan position, string content)
        {
            Info = info;
            SourceSpan = position;
            Content = content;
        }

        public MessageInfo Info { get; private set; }

        public string Content { get; private set; }

        public SourceSpan SourceSpan { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}  Index {2}", Info.Id, Content, SourceSpan.StartIndex);
        }
    }

    public class MessageInfo
    {
        public string Class { get; }

        public int Id { get; }

        public MessageLevel Level { get; }

        public MessageStage Stage { get; }

        public string MessageTemplate { get; }

        public MessageInfo(string @class, int id, MessageLevel level, MessageStage stage, string messageTemplate)
        {
            Class = @class;
            Id = id;
            Level = level;
            Stage = stage;
            MessageTemplate = messageTemplate;
        }
    }

    public enum MessageLevel
    {
        Info,
        Error,
        Warning,
        WarningStrict
    }

    public enum MessageStage
    {
        None,
        PreProcessing,
        Scanning,
        Parsing,
        SemanticAnalysis,
        CodeGeneration,
        PostProcessing,
        Other
    }
}
