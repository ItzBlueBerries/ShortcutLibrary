using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib.Extras
{
    /// <summary>
    /// External / Extra methods for <see cref="ShortcutLib"/>.
    /// </summary>
    public static class External
    {
        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="byte"/> <see cref="Array"/>, useful for converting to RGB values. Separated by commas.
        /// </summary>
        /// <param name="str">The <see cref="string"/> to be converted to a <see cref="byte"/> <see cref="Array"/>.</param>
        /// <returns><see cref="byte[]"/></returns>
        public static byte[] StringToByte(string str)
        {
            str = str.Trim();
            byte[] array = Array.ConvertAll(str.Split(','), byte.Parse);
            return array;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="float"/> <see cref="Array"/>. Separated by commas.
        /// </summary>
        /// <param name="str">The <see cref="string"/> to be converted to a <see cref="float"/> <see cref="Array"/>.</param>
        /// <returns><see cref="float[]"/></returns>
        public static float[] StringToFloat(string str)
        {
            str = str.Trim();
            float[] array = Array.ConvertAll(str.Split(','), float.Parse);
            return array;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="int"/> <see cref="Array"/>. Separated by commas.
        /// </summary>
        /// <param name="str">The <see cref="string"/> to be converted to a <see cref="int"/> <see cref="Array"/>.</param>
        /// <returns><see cref="int[]"/></returns>
        public static int[] StringToInt(string str)
        {
            str = str.Trim();
            int[] array = Array.ConvertAll(str.Split(','), int.Parse);
            return array;
        }
    }
}
