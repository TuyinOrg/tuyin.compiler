using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Analysis.Utils.Colors;

/// <summary>
/// Color Library.
/// </summary>
class ColorLibrary
{
    private readonly Dictionary<EColorScheme, KnownColor> _knownColors;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ColorLibrary()
    {
        _knownColors = new Dictionary<EColorScheme, KnownColor>()
        {
            {
                EColorScheme.Monochrome,
                new KnownColor(new Range(0, 0), new Range[]
                {
                    new(0, 0),
                    new(100, 0)
                })
            },
            {
                EColorScheme.Red,
                new KnownColor(new Range(-26, 18), new Range[]
                {
                    new(20, 100),
                    new(30, 92),
                    new(40, 89),
                    new(50, 85),
                    new(60, 78),
                    new(70, 70),
                    new(80, 60),
                    new(90, 55),
                    new(100, 50)
                })
            },
            {
                EColorScheme.Orange,
                new KnownColor(new Range(19, 46), new Range[]
                {
                    new(20, 100),
                    new(30, 93),
                    new(40, 88),
                    new(50, 86),
                    new(60, 85),
                    new(70, 70),
                    new(100, 70)
                })
            },
            {
                EColorScheme.Yellow,
                new KnownColor(new Range(47, 62), new Range[]
                {
                    new(20, 100),
                    new(30, 93),
                    new(40, 88),
                    new(50, 86),
                    new(60, 85),
                    new(70, 70),
                    new(100, 70)
                })
            },
            {
                EColorScheme.Green,
                new KnownColor(new Range(63, 178), new Range[]
                {
                    new(30, 100),
                    new(40, 90),
                    new(50, 85),
                    new(60, 81),
                    new(70, 74),
                    new(80, 64),
                    new(90, 50),
                    new(100, 40)
                })
            },
            {
                EColorScheme.Blue,
                new KnownColor(new Range(179, 257), new Range[]
                {
                    new(20, 100),
                    new(30, 86),
                    new(40, 80),
                    new(50, 74),
                    new(60, 60),
                    new(70, 52),
                    new(80, 44),
                    new(90, 39),
                    new(100, 35)
                })
            },
            {
                EColorScheme.Purple,
                new KnownColor(new Range(258, 282), new Range[]
                {
                    new(20, 100),
                    new(30, 87),
                    new(40, 79),
                    new(50, 70),
                    new(60, 65),
                    new(70, 59),
                    new(80, 52),
                    new(90, 45),
                    new(100, 42)
                })
            },
            {
                EColorScheme.Pink,
                new KnownColor(new Range(283, 334), new Range[]
                {
                    new(20, 100),
                    new(30, 90),
                    new(40, 86),
                    new(60, 84),
                    new(80, 80),
                    new(90, 75),
                    new(100, 73)
                })
            }
        };
    }

    public KnownColor? GetColor(int hue)
    {
        if (hue is >= 334 and <= 360)
        {
            hue -= 360;
        }

        return _knownColors.Values.FirstOrDefault(color => color.Includes(hue));
    }

    public KnownColor? GetColor(EColorScheme? scheme)
    {
        return _knownColors.GetValueOrDefault(scheme ?? default);
    }

    public Range? GetSaturationRange(int hue)
    {
        return GetColor(hue)?.Saturation;
    }

    public int GetMinimumValue(int hue, int saturation)
    {
        var minimumValue = 0;

        var lowerBounds = GetColor(hue)?.LowerBounds;

        for (var i = 0; i < lowerBounds?.Length -1; i++)
        {
            var s1 = lowerBounds[i].Lower;
            var v1 = lowerBounds[i].Upper;
            
            var s2 = lowerBounds[i + 1].Lower;
            var v2 = lowerBounds[i + 1].Upper;

            if (saturation >= s1 && saturation <= s2)
            {
                var m = (v2 - v1) / (s2 - s1);
                var b = v1 - m * s1;

                minimumValue = m * saturation + b;
            }
        }

        return minimumValue;
    }
}