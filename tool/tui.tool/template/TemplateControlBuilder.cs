using System.Text;

namespace tui.tool.template
{
    internal class TemplateControlBuilder
    {
        private LayoutType mLayoutType;
        private int mDefintCount;
        private StringBuilder mFieldBuilder;
        private StringBuilder mPropertyBuilder;
        private StringBuilder mPaintBuilder;

        private Dictionary<EventType, StringBuilder> mEventBuilders;

        public string ControlName { get; }

        public TemplateControlBuilder(string controlName) 
        {
            ControlName = controlName;
            mFieldBuilder = new StringBuilder();
            mPropertyBuilder = new StringBuilder();
            mPaintBuilder = new StringBuilder();
            mEventBuilders = new Dictionary<EventType, StringBuilder>();
        }

        internal void Layout(LayoutType layoutType)
        {
            mLayoutType = layoutType;
        }

        internal void Paint(string v)
        {
            mPaintBuilder.AppendLine("\t\t" + v);
        }

        internal void Property(string propertyName, float value)
        {
            mPropertyBuilder.AppendLine($"  private float {propertyName}={value};" +
                $"  public float {propertyName} " +
                $"  {{ " +
                $"      get {{ return {propertyName}; }} " +
                $"      set {{ {propertyName}=value; Invalidate(); }}" +
                $"  }}");
        }

        internal void Define(string name, string type, string v)
        {
            mDefintCount++;
            mFieldBuilder.AppendLine($" {type} {name}={v};");
        }

        internal void Event(EventType eventType, int value, string v)
        {
            if (!mEventBuilders.ContainsKey(eventType))
                mEventBuilders.Add(eventType, new StringBuilder());

            var builder = mEventBuilders[eventType];
            builder.AppendLine("\t\t" + v);
        }

        private string GetEventAnimateTimer(EventType et) 
        {
            return et switch
            {
                EventType.KeyDown => "mKeyDownAnimateTimer",
                EventType.KeyUp => "mKeyUpAnimateTimer",
                EventType.MouseDown => "mMouseDownAnimateTimer",
                EventType.MouseUp => "mMouseUpAnimateTimer",
                EventType.MouseMove => "mMouseMoveAnimateTimer",
                EventType.MouseEnter => "mMosueEnterAnimateTimer",
                EventType.MouseLeave => "mMouseLeaveAnimateTimer",
                _ => throw new NotImplementedException()
            };
        }

        public override string ToString()
        {
            foreach (var eb in mEventBuilders)
                mFieldBuilder.AppendLine($"{GetEventAnimateTimer(eb.Key)}=new AnimateTimer();");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using libtui.controls;");
            sb.AppendLine("using libtui.drawing;");
            sb.AppendLine();
            sb.AppendLine($"namespace tui.tool;");
            sb.AppendLine();
            sb.AppendLine($"public class {ControlName} : ControlBase\n{{");
            if (mDefintCount > 0) 
            {
                var type = mLayoutType switch
                {
                    LayoutType.Canvas => "Canvas",
                    LayoutType.Grid => "Grid",
                    _ => throw new NotSupportedException()
                };

                sb.AppendLine($"    {type} mLayout=new {type}();");
                sb.AppendLine();
            }
            if (mFieldBuilder.Length > 0)
            {
                sb.AppendLine(mFieldBuilder.ToString());
                sb.AppendLine();
            }
            if (mPropertyBuilder.Length > 0)
            {
                sb.AppendLine(mPropertyBuilder.ToString());
                sb.AppendLine();
            }
            foreach (var eb in mEventBuilders) 
            {
                var header = eb.Key switch
                {
                    EventType.KeyDown => "OnKeyDown(KeyEventArgs e)",
                    EventType.KeyUp => "OnKeyUp(KeyEventArgs e)",
                    EventType.MouseDown => "OnMouseDown(MouseEventArgs e)",
                    EventType.MouseUp => "OnMouseUp(MouseEventArgs e)",
                    EventType.MouseMove => "OnMouseMove(MouseEventArgs e)",
                    EventType.MouseEnter => "OnMosueEnter(MouseEventArgs e)",
                    EventType.MouseLeave => "OnMouseLeave(MouseEventArgs e)",
                    _ => throw new NotImplementedException()
                };

                sb.AppendLine($"    public override void {header}\n{{");
                sb.AppendLine($"        var timer={GetEventAnimateTimer(eb.Key)};");
                sb.AppendLine(eb.Value.ToString());
                sb.AppendLine("}");
                sb.AppendLine();
            }

            sb.AppendLine(" public override void Paint(PaintEventArgs e)\n{");
            sb.AppendLine("     var g = e.Graphics;");
            sb.AppendLine(mPaintBuilder.ToString());
            sb.AppendLine(" }");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
