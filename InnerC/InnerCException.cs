using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnerC
{
    class InnerCException : Exception
    {
        private string message;

        public InnerCException(string message, char[] chars, int charIndex)
        {
            int i;
            int j;

            int iLeft = charIndex;
            int iRight = charIndex;

            while(true)
            {
                i = iLeft - 1;
                j = iRight + 1;

                if (i < 0 && j >= chars.Length)
                {
                    break;
                }

                if (i >= 0)
                {
                    iLeft = i;
                }

                if (j < chars.Length)
                {
                    iRight = j;
                }

                int l = iRight - iLeft + 1;

                if (l >= chars.Length || l >= 18)
                {
                    this.message = message + " " + "在 第 " + charIndex + " 个 字符 \"" 
                        + chars[charIndex] + "\" 附近 ： \"" + new string(chars, iLeft, iRight - iLeft + 1) + "\" 。";

                    break;
                }

            }
        }

        public override string Message
        {
            get
            {
                return this.message;
            }
        }
    }
}
