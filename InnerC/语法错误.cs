using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnerC
{
    class 语法错误
    {
        private string toStr;

        public 语法错误(string desc, char[] chars, int charIndex)
        {

            int iLeft = charIndex - 9;
            int iRight = charIndex + 9;

            if (iLeft < 0)
            {
                iLeft = 0;

                iRight = 18;

                if (iRight >= chars.Length)
                    iRight = chars.Length - 1;
            }
            else
            { 
                if (iRight >= chars.Length)
                {
                    iRight = chars.Length - 1;

                    iLeft = charIndex - (18 - (iRight - charIndex));

                    if (iLeft < 0)
                        iLeft = 0;
                }
            }

            this.toStr = desc + " " + "在 第 " + charIndex + " 个 字符 \""
                    + chars[charIndex] + "\" 附近 ： \"" + new string(chars, iLeft, iRight - iLeft + 1) + "\" 。";

            //int i;
            //int j;

            //int iLeft = charIndex;
            //int iRight = charIndex;

            //while (true)
            //{
            //    i = iLeft - 1;
            //    j = iRight + 1;

            //    if (i < 0 && j >= chars.Length)
            //    {
            //        break;
            //    }

            //    if (i >= 0)
            //    {
            //        iLeft = i;
            //    }

            //    if (j < chars.Length)
            //    {
            //        iRight = j;
            //    }

            //    int l = iRight - iLeft + 1;

            //    if (l >= chars.Length || l >= 18)
            //    {
            //        this.toStr = desc + " " + "在 第 " + charIndex + " 个 字符 \""
            //            + chars[charIndex] + "\" 附近 ： \"" + new string(chars, iLeft, iRight - iLeft + 1) + "\" 。";

            //        break;
            //    }

            //}
        }

        public override string ToString()
        {
            return this.toStr;
        }
    }

    class 语法错误_Exception : Exception
    {

        private 语法错误 语法错误;

        public 语法错误_Exception(string desc, char[] chars, int charIndex)
        {
            this.语法错误 = new 语法错误(desc, chars, charIndex);
        }

        public override string Message
        {
            get
            {
                return this.语法错误.ToString();
            }
        }


    }
}
