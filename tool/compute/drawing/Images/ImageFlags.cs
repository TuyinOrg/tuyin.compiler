namespace compute.drawing
{
    using System;

    [Flags]
    public enum ImageFlags
    {
        None = 0,
        Scalable = 0x0001,
        HasAlpha = 0x0002,
        HasTranslucent = 0x0004,
        PartiallyScalable = 0x0008,
        ColorSpaceRgb = 0x0010,
        ColorSpaceCmyk = 0x0020,
        ColorSpaceGray = 0x0040,
        ColorSpaceYcbcr = 0x0080,
        ColorSpaceYcck = 0x0100,
        HasRealDpi = 0x1000,
        HasRealPixelSize = 0x2000,
        ReadOnly = 0x00010000,
        Caching = 0x00020000
    }
}
