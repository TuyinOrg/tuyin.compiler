namespace libtui.drawing
{
    public abstract class Image
    {
        public int Width => Size.Width;

        public int Height => Size.Height;

        public abstract Size Size { get; }

        public abstract PixelFormat PixelFormat { get; }
    }
}
