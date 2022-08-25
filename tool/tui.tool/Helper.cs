using libtui.drawing;

namespace tui.tool
{
    static class Helper
    {
        internal static string CodeHexColor(string strRead)
        {
            var c = HexToColor(strRead);
            if(c.A == 255)
                return $"Color.FromArgb({c.R},{c.G},{c.B})";

            return $"Color.FromArgb({c.A},{c.R},{c.G},{c.B})";
        }

        /// <summary>
        /// hex转换到color
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static Color HexToColor(string hex)
        {
            byte br = byte.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            byte bg = byte.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            byte bb = byte.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
            float r = br / 255f;
            float g = bg / 255f;
            float b = bb / 255f;
            float a = hex.Length > 7 ? byte.Parse(hex.Substring(7, 2), System.Globalization.NumberStyles.HexNumber) / 255f : 255f;
            return new Color(r, g, b, a);
        }
    }
}
