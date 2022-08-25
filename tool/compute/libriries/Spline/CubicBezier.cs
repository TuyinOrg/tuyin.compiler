using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using FLOAT = System.Single;
using VECTOR = compute.drawing.PointF;

namespace compute.spline
{
    /// <summary>
    /// Cubic Bezier curve in 2D consisting of 4 control points.
    /// </summary>
    struct CubicBezier : IEquatable<CubicBezier>
    {
        // Control points
        public readonly VECTOR p0;
        public readonly VECTOR p1;
        public readonly VECTOR p2;
        public readonly VECTOR p3;

        /// <summary>
        /// Creates a new cubic bezier using the given control points.
        /// </summary>
        public CubicBezier(VECTOR p0, VECTOR p1, VECTOR p2, VECTOR p3)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        public float GetArcLength()
        {
            float arcLength = 0.0f;
            ArcLengthUtil(p0, p1, p2, p3, 5, ref arcLength);
            return arcLength;
        }

        private void ArcLengthUtil(VECTOR A, VECTOR B, VECTOR C, VECTOR D, uint subdiv, ref float L)
        {
            if (subdiv > 0)
            {
                VECTOR a = A + (B - A) * 0.5f;
                VECTOR b = B + (C - B) * 0.5f;
                VECTOR c = C + (D - C) * 0.5f;
                VECTOR d = a + (b - a) * 0.5f;
                VECTOR e = b + (c - b) * 0.5f;
                VECTOR f = d + (e - d) * 0.5f;

                // left branch
                ArcLengthUtil(A, a, d, f, subdiv - 1, ref L);
                // right branch
                ArcLengthUtil(f, e, c, D, subdiv - 1, ref L);
            }
            else
            {
                float controlNetLength = (B - A).Magnitude() + (C - B).Magnitude() + (D - C).Magnitude();
                float chordLength = (D - A).Magnitude();
                L += (chordLength + controlNetLength) / 2.0f;
            }
        }

        /// <summary>
        /// Samples the bezier curve at the given t value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>Sampled point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VECTOR Sample(FLOAT t)
        {
            FLOAT ti = 1 - t;
            FLOAT t0 = ti * ti * ti;
            FLOAT t1 = 3 * ti * ti * t;
            FLOAT t2 = 3 * ti * t * t;
            FLOAT t3 = t * t * t;
            return (t0 * p0) + (t1 * p1) + (t2 * p2) + (t3 * p3);
        }

        /// <summary>
        /// Gets the first derivative of the curve at the given T value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>First derivative of curve at sampled point.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VECTOR Derivative(FLOAT t)
        {
            FLOAT ti = 1 - t;
            FLOAT tp0 = 3 * ti * ti;
            FLOAT tp1 = 6 * t * ti;
            FLOAT tp2 = 3 * t * t;
            return (tp0 * (p1 - p0)) + (tp1 * (p2 - p1)) + (tp2 * (p3 - p2));
        }

        /// <summary>
        /// Gets the tangent (normalized derivative) of the curve at a given T value.
        /// </summary>
        /// <param name="t">Time value at which to sample (should be between 0 and 1, though it won't fail if outside that range).</param>
        /// <returns>Direction the curve is going at that point.</returns>
        #if !UNITY
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        #endif
        public VECTOR Tangent(FLOAT t)
        {
            return VectorHelper.Normalize(Derivative(t));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CubicBezier: (<");
            sb.Append(VectorHelper.GetX(p0).ToString("#.###", CultureInfo.InvariantCulture));
            sb.Append(", ");
            sb.Append(VectorHelper.GetY(p0).ToString("#.###", CultureInfo.InvariantCulture));
            sb.Append("> <");
            sb.Append(VectorHelper.GetX(p1).ToString("#.###", CultureInfo.InvariantCulture));
            sb.Append(", ");
            sb.Append(VectorHelper.GetY(p1).ToString("#.###", CultureInfo.InvariantCulture));
            sb.Append("> <");
            sb.Append(VectorHelper.GetX(p2).ToString("#.###", CultureInfo.InvariantCulture));
            sb.Append(", ");
            sb.Append(VectorHelper.GetY(p2).ToString("#.###", CultureInfo.InvariantCulture));
            sb.Append("> <");
            sb.Append(VectorHelper.GetX(p3).ToString("#.###", CultureInfo.InvariantCulture));
            sb.Append(", ");
            sb.Append(VectorHelper.GetY(p3).ToString("#.###", CultureInfo.InvariantCulture));
            sb.Append(">)");
            return sb.ToString();
        }

        // Equality members -- pretty straightforward
        public static bool operator ==(CubicBezier left, CubicBezier right) { return left.Equals(right); }
        public static bool operator !=(CubicBezier left, CubicBezier right) { return !left.Equals(right); }
        public bool Equals(CubicBezier other) { return p0.Equals(other.p0) && p1.Equals(other.p1) && p2.Equals(other.p2) && p3.Equals(other.p3); }
        public override bool Equals(object obj) { return obj is CubicBezier && Equals((CubicBezier) obj); }
        public override int GetHashCode()
        {
            JenkinsHash hash = new JenkinsHash();
            hash.Mixin(VectorHelper.GetX(p0).GetHashCode());
            hash.Mixin(VectorHelper.GetY(p0).GetHashCode());
            hash.Mixin(VectorHelper.GetX(p1).GetHashCode());
            hash.Mixin(VectorHelper.GetY(p1).GetHashCode());
            hash.Mixin(VectorHelper.GetX(p2).GetHashCode());
            hash.Mixin(VectorHelper.GetY(p2).GetHashCode());
            hash.Mixin(VectorHelper.GetX(p3).GetHashCode());
            hash.Mixin(VectorHelper.GetY(p3).GetHashCode());
            return hash.GetValue();
        }

        /// <summary>
        /// Simple implementation of Jenkin's hashing algorithm.
        /// http://en.wikipedia.org/wiki/Jenkins_hash_function
        /// I forget where I got these magic numbers from; supposedly they're good.
        /// </summary>
        private struct JenkinsHash
        {
            private int _current;

            #if !UNITY
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            #endif
            public void Mixin(int hash)
            {
                unchecked
                {
                    int num = _current;
                    if(num == 0)
                        num = 0x7e53a269;
                    else
                        num *= -0x5aaaaad7;
                    num += hash;
                    num += (num << 10);
                    num ^= (num >> 6);
                    _current = num;
                }
            }

            #if !UNITY
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            #endif
            public int GetValue()
            {
                unchecked
                {
                    int num = _current;
                    num += (num << 3);
                    num ^= (num >> 11);
                    num += (num << 15);
                    return num;
                }
            }
        }

        public float[] GetRoots() 
        {
            return GetRoots(p0.Y, p1.Y, p2.Y, p3.Y);
        }

        private static float[] GetRoots(float pa, float pb, float pc, float pd)
        {
            const float _epsilon = 0.000001f;

            // https://pomax.github.io/bezierinfo/index.html#extremities

            // A helper function to filter for values in the [0,1] interval:
            bool Accept(double t) => 0f <= t && t <= 1f;

            // A real-cuberoots-only function:
            double CubeRoot(double v)
            {
                return (v < 0)
                    ? -Math.Pow(-v, 1.0 / 3.0)
                    : Math.Pow(v, 1.0 / 3.0);
            }

            bool IsFloatEquals(float a, float b, float epsilon = _epsilon) => Math.Abs(a - b) < epsilon;

            var a = (3 * pa - 6 * pb + 3 * pc);
            var b = (-3 * pa + 3 * pb);
            var c = pa;
            var d = (-pa + 3 * pb - 3 * pc + pd);

            // do a check to see whether we even need cubic solving:
            if (IsFloatEquals(d, 0))
            {
                // this is not a cubic curve.
                if (IsFloatEquals(a, 0))
                {
                    // in fact, this is not a quadratic curve either.
                    if (IsFloatEquals(b, 0))
                    {
                        // in fact in fact, there are no solutions.
                        return new float[0];
                    }
                    // linear solution
                    return new[] { -c / b }.Where(x => Accept(x)).ToArray();
                }
                // quadratic solution
                var q1 = Math.Sqrt(b * b - 4 * a * c);
                var n2a = 2 * a;
                var root01 = (q1 - b) / n2a;
                var root02 = (-b - q1) / n2a;
                return new[] { root01, root02 }.Where(x => Accept(x)).Select(x => (float)x).ToArray();
            }

            // at this point, we know we need a cubic solution.

            a /= d;
            b /= d;
            c /= d;

            var p = (3 * b - a * a) / 3;
            var p3 = p / 3;
            var q = (2 * a * a * a - 9 * a * b + 27 * c) / 27;
            var q2 = q / 2;
            var discriminant = q2 * q2 + p3 * p3 * p3;

            // and some variables we're going to use later on:
            double u1, v1, root1, root2, root3;

            // three possible real roots:
            if (discriminant < 0)
            {
                var mp3 = -p / 3;
                var mp33 = mp3 * mp3 * mp3;
                var r = Math.Sqrt(mp33);
                var t = -q / (2 * r);
                var cosphi = t < -1 ? -1 : t > 1 ? 1 : t;
                var phi = Math.Acos(cosphi);
                var crtr = CubeRoot(r);
                var t1 = 2 * crtr;
                root1 = t1 * Math.Cos(phi / 3) - a / 3;
                root2 = t1 * Math.Cos((phi + 2 * Math.PI) / 3) - a / 3;
                root3 = t1 * Math.Cos((phi + 4 * Math.PI) / 3) - a / 3;
                return new[] { root1, root2, root3 }.Where(x => Accept(x)).Select(x => (float)x).ToArray();
            }

            // three real roots, but two of them are equal:
            if (discriminant == 0)
            {
                u1 = q2 < 0 ? CubeRoot(-q2) : -CubeRoot(q2);
                root1 = 2 * u1 - a / 3;
                root2 = -u1 - a / 3;
                return new[] { root1, root2 }.Where(x => Accept(x)).Select(x => (float)x).ToArray();
            }

            // one real root, two complex roots
            var sd = Math.Sqrt(discriminant);
            u1 = CubeRoot(sd - q2);
            v1 = CubeRoot(sd + q2);
            root1 = u1 - v1 - a / 3;

            return new[] { root1 }.Where(x => Accept(x)).Select(x => (float)x).ToArray();
        }
    }
}