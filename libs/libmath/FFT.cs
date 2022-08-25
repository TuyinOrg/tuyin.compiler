#define UsingNumerics

using System;
using System.Collections.Generic;

#if UsingNumerics
using System.Numerics;
#endif

namespace libmath
{
#if !UsingNumerics
    /// <summary>
    /// Defining Structure for Complex Data type  N=R+Ii
    /// </summary>
    struct Complex
    {
        public double Real, Imaginary;

        public Complex(double x, double y)
        {
            Real = x;
            Imaginary = y;
        }
        public float Magnitude()
        {
            return ((float)Math.Sqrt(Real * Real + Imaginary * Imaginary));
        }

        public float Phase()
        {
            return ((float)Math.Atan(Imaginary / Real));
        }
    }
#endif

    static class FFT
    {
        public static void ToFunction(int[,] data)
        {
            var space = ConvertSpace(ConvertHash(data), default);


        }

        /// <summary>
        /// 将散列数据转变为空间数据
        /// </summary>
        private static SpaceComplex[] ConvertSpace(Complex[,] fourier, Complex invaild)
        {
            var width = fourier.GetLength(0);
            var height = fourier.GetLength(1);
            var scs = new List<SpaceComplex>();
            for (var i = 0; i <= width - 1; i++)
                for (var j = 0; j <= height - 1; j++)
                {
                    var f = fourier[i, j];
                    if (!f.Equals(invaild))
                    {
                        scs.Add(new SpaceComplex(f, i, j));
                    }
                }

            return scs.ToArray();
        }

        /// <summary>
        /// 转换散列数据到Complex
        /// </summary>
        private static Complex[,] ConvertHash(int[,] data)
        {
            var width = data.GetLength(0);
            var height = data.GetLength(1);
            var fourier = new Complex[width, height];

            //Copy Image Data to the Complex Array
            for (var i = 0; i <= width - 1; i++)
                for (var j = 0; j <= height - 1; j++)
                    fourier[i, j] = new Complex(data[i, j], 0);

            return FFT2D(fourier, width, height, 1);
        }

        /*-------------------------------------------------------------------------
           Perform a 2D FFT inplace given a complex 2D array
           The direction dir, 1 for forward, -1 for reverse
           The size of the array (nx,ny)
           Return false if there are memory problems or
           the dimensions are not powers of 2
        */
        private static Complex[,] FFT2D(Complex[,] c, int nx, int ny, int dir)
        {
            int i, j;
            int m;//Power of 2 for current number of points
            double[] real;
            double[] imag;
            Complex[,] output;//=new COMPLEX [nx,ny];
            output = c; // Copying Array
            // Transform the Rows 
            real = new double[nx];
            imag = new double[nx];

            for (j = 0; j < ny; j++)
            {
                for (i = 0; i < nx; i++)
                {
                    real[i] = c[i, j].Real;
                    imag[i] = c[i, j].Imaginary;
                }
                // Calling 1D FFT Function for Rows
                m = (int)Math.Log(nx, 2);//Finding power of 2 for current number of points e.g. for nx=512 m=9
                FFT1D(dir, m, ref real, ref imag);

                for (i = 0; i < nx; i++)
                {
                    output[i, j] = new Complex(real[i], imag[i]);
                }
            }
            // Transform the columns  
            real = new double[ny];
            imag = new double[ny];

            for (i = 0; i < nx; i++)
            {
                for (j = 0; j < ny; j++)
                {
                    //real[j] = c[i,j].real;
                    //imag[j] = c[i,j].imag;
                    real[j] = output[i, j].Real;
                    imag[j] = output[i, j].Imaginary;
                }
                // Calling 1D FFT Function for Columns
                m = (int)Math.Log(ny, 2);//Finding power of 2 for current number of points e.g. for nx=512 m=9
                FFT1D(dir, m, ref real, ref imag);
                for (j = 0; j < ny; j++)
                {
                    output[i, j] = new Complex(real[i], imag[i]);
                }
            }

            // return(true);
            return output;
        }

        /*-------------------------------------------------------------------------
            This computes an in-place complex-to-complex FFT
            x and y are the real and imaginary arrays of 2^m points.
            dir = 1 gives forward transform
            dir = -1 gives reverse transform
            Formula: forward
                     N-1
                      ---
                    1 \         - j k 2 pi n / N
            X(K) = --- > x(n) e                  = Forward transform
                    N /                            n=0..N-1
                      ---
                     n=0
            Formula: reverse
                     N-1
                     ---
                     \          j k 2 pi n / N
            X(n) =    > x(k) e                  = Inverse transform
                     /                             k=0..N-1
                     ---
                     k=0
            */
        private static void FFT1D(int dir, int m, ref double[] x, ref double[] y)
        {
            long nn, i, i1, j, k, i2, l, l1, l2;
            double c1, c2, tx, ty, t1, t2, u1, u2, z;
            /* Calculate the number of points */
            nn = 1;
            for (i = 0; i < m; i++)
                nn *= 2;
            /* Do the bit reversal */
            i2 = nn >> 1;
            j = 0;
            for (i = 0; i < nn - 1; i++)
            {
                if (i < j)
                {
                    tx = x[i];
                    ty = y[i];
                    x[i] = x[j];
                    y[i] = y[j];
                    x[j] = tx;
                    y[j] = ty;
                }
                k = i2;
                while (k <= j)
                {
                    j -= k;
                    k >>= 1;
                }
                j += k;
            }
            /* Compute the FFT */
            c1 = -1.0;
            c2 = 0.0;
            l2 = 1;
            for (l = 0; l < m; l++)
            {
                l1 = l2;
                l2 <<= 1;
                u1 = 1.0;
                u2 = 0.0;
                for (j = 0; j < l1; j++)
                {
                    for (i = j; i < nn; i += l2)
                    {
                        i1 = i + l1;
                        t1 = u1 * x[i1] - u2 * y[i1];
                        t2 = u1 * y[i1] + u2 * x[i1];
                        x[i1] = x[i] - t1;
                        y[i1] = y[i] - t2;
                        x[i] += t1;
                        y[i] += t2;
                    }
                    z = u1 * c1 - u2 * c2;
                    u2 = u1 * c2 + u2 * c1;
                    u1 = z;
                }
                c2 = Math.Sqrt((1.0 - c1) / 2.0);
                if (dir == 1)
                    c2 = -c2;
                c1 = Math.Sqrt((1.0 + c1) / 2.0);
            }
            /* Scaling for forward transform */
            if (dir == 1)
            {
                for (i = 0; i < nn; i++)
                {
                    x[i] /= nn;
                    y[i] /= nn;

                }
            }

            //  return(true) ;
            return;
        }

        struct SpaceComplex
        {
            public Complex Complex { get; }

            public int X { get; }

            public int Y { get; }

            public SpaceComplex(Complex complex, int x, int y)
            {
                Complex = complex;
                X = x;
                Y = y;
            }
        }
    }
}
