﻿using System;
using System.Drawing;

namespace SwatchBuckler
{
    // Swatch Buckler palette library
    // Copyright © 2014-2015 Mechanika Design.
    // http://www.mechanikadesign.com/swatchbuckler

    // Licensed under the MIT License. See swatchbuckler-license.txt for the full text.

    // If you use this code in your application, acknowledgment is kindly appreciated

    /// <summary>
    /// Provides access to color comparision operations.
    /// </summary>
    public static class ColorComparer
    {
        #region Constants

        private const double _redLuminance = 0.212655;

        private const double _greenLuminance = 0.715158;

        private const double _blueLuminance = 0.072187;

        #endregion

        #region Public Class Members

        /// <summary>
        /// Ccompares two colors by brightness and returns an indication of their relative sort order.
        /// </summary>
        /// <param name="x">A color to compare to y.</param>
        /// <param name="y">A color to compare to x.</param>
        public static int Brightness(Color x, Color y)
        {
            int v1;
            int v2;
            int result;

            v1 = GetBrightness(x);
            v2 = GetBrightness(y);

            if (v1 < v2)
            {
                result = -1;
            }
            else if (v1 > v2)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Ccompares two colors by hue and returns an indication of their relative sort order.
        /// </summary>
        /// <param name="x">A color to compare to y.</param>
        /// <param name="y">A color to compare to x.</param>
        public static int Hue(Color x, Color y)
        {
            float v1;
            float v2;
            int result;

            v1 = x.GetHue();
            v2 = y.GetHue();

            if (v1 < v2)
            {
                result = -1;
            }
            else if (v1 > v2)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Ccompares two colors by value and returns an indication of their relative sort order.
        /// </summary>
        /// <param name="x">A color to compare to y.</param>
        /// <param name="y">A color to compare to x.</param>
        public static int Value(Color x, Color y)
        {
            int v1;
            int v2;
            int result;

            v1 = x.R << 16 | x.G << 8 | x.B;
            v2 = y.R << 16 | y.G << 8 | y.B;

            if (v1 > v2)
            {
                result = -1;
            }
            else if (v1 < v2)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        #endregion

        #region Private Class Members

        private static int GetBrightness(Color color)
        {
            //http://stackoverflow.com/a/13558570/148962

            // GRAY VALUE ("brightness")

            return GetGamma(_redLuminance * GetInverseGamma(color.R) + _greenLuminance * GetInverseGamma(color.G) + _blueLuminance * GetInverseGamma(color.B));
        }

        private static int GetGamma(double v)
        {
            // sRGB "gamma" function (approx 2.2)

            if (v <= 0.0031308)
            {
                v *= 12.92;
            }
            else
            {
                v = 1.055 * Math.Pow(v, 1.0 / 2.4) - 0.055;
            }

            return (int)(v * 255 + .5);
        }

        private static double GetInverseGamma(int ic)
        {
            double result;

            // Inverse of sRGB "gamma" function. (approx 2.2)

            double c = ic / 255.0;
            if (c <= 0.04045)
            {
                result = c / 12.92;
            }
            else
            {
                result = Math.Pow(((c + 0.055) / (1.055)), 2.4);
            }

            return result;
        }

        #endregion
    }
}