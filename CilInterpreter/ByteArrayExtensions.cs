using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter
{
    static class ByteArrayExtensions
    {
        public static int Ref(this byte[] bytes)
        {
            if (bytes.Length != 4)
                throw new Exception("INTERNAL ERROR: INVALID REF CONVERT");
            else
                return BitConverter.ToInt32(bytes, 0);
        }

        public static int I4(this byte[] bytes)
        {
            if (bytes.Length != 4)
                throw new Exception("INTERNAL ERROR: INVALID I4 CONVERT");
            else
                return BitConverter.ToInt32(bytes, 0);
        }
    }
}
