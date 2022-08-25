using System;

namespace libtui.controls
{
    public class CharEventArgs : EventArgs
    {
        public CharEventArgs(uint codePoint, ModifierKeys mods)
        {
            CodePoint = codePoint;
            ModifierKeys = mods;
        }

        public string Char => char.ConvertFromUtf32(unchecked((int)CodePoint));

        public uint CodePoint { get; }

        public ModifierKeys ModifierKeys { get; }
    }
}