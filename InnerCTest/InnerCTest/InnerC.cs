using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace InnerCTest
{
    class InnerC
    {
        [DllImport("InnerC", CallingConvention = CallingConvention.Cdecl)]
        extern public static void CallFromDll(string msg);
    }
}
