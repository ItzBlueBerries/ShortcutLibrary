using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib
{
    public class External
    {
        public static byte[] StringToByte(string stringToParse)
        {
            stringToParse = stringToParse.Trim();
            byte[] stringByteArray = Array.ConvertAll(stringToParse.Split(','), byte.Parse);
            return stringByteArray;
        }

        public static float[] StringToFloat(string stringToParse)
        {
            stringToParse = stringToParse.Trim();
            float[] stringFloatArray = Array.ConvertAll(stringToParse.Split(','), float.Parse);
            return stringFloatArray;
        }
    }
}
