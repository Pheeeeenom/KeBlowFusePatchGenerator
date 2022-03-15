using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeBlowFusePatchGenerator.Classes
{
    public static class Extensions
    {
        public static bool GetBit(this byte src, int bitNumber)
        {
            return (src & (1 << bitNumber)) != 0;
        }
    }
}