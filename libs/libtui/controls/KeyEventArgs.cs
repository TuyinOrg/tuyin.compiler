namespace libtui.controls
{
    public class KeyEventArgs
    {
        public KeyEventArgs(Keys key, int scanCode, InputState state, ModifierKeys mods)
        {
            KeyCode = key;
            ScanCode = scanCode;
            State = state;
            Modifiers = mods;
        }

        public int ScanCode { get; }

        public Keys KeyCode { get; }

        public ModifierKeys Modifiers { get; }

        public InputState State { get; }
    }
}