using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC
{
    class Util
    {
        public static bool Check_是否_下划线字母数字_且以_下划线字母_开头(char[] chars, int beginIndex, int endIndex)
        {

            char c = chars[beginIndex];

            if (c != '_' && !StrUtil.IsLetter(c))
                return false;

            for (int i = beginIndex + 1; i <= endIndex; i++)
            {
                c = chars[i];

                if (c != '_' && !StrUtil.IsLetter(c) && !StrUtil.IsNumber(c))
                    return false;
            }

            return true;
        }

        public static bool Check_是否_下划线字母数字_且以_下划线字母_开头(string str)
        {
            char c = str[0];

            if (c != '_' && !StrUtil.IsLetter(c))
                return false;

            for (int i = 1; i < str.Length; i++)
            {
                c = str[i];

                if (c != '_' && !StrUtil.IsLetter(c) && !StrUtil.IsNumber(c))
                    return false;
            }

            return true;
        }

        public static bool Check_是否_下划线字母数字(char c)
        {
            if (c == '_' || StrUtil.IsLetter(c) || StrUtil.IsNumber(c))
                return true;

            return false;
        }

        public static bool Check_int(char[] chars, int beginIndex, int endIndex)
        {
            char c;

            for (int i = beginIndex; i<=endIndex; i++)
            {
                c = chars[i];

                if (!StrUtil.IsNumber(c))
                    return false;
            }

            return true;
        }

        public static bool Check_float(char[] chars, int beginIndex, int endIndex)
        {
            char c;

            for (int i = beginIndex; i <= endIndex; i++)
            {
                c = chars[i];

                if (!(StrUtil.IsNumber(c) || c == '.'))
                    return false;
            }

            c = chars[beginIndex];

            if (c == '.')
                return false;

            c = chars[endIndex];

            if (c == '.')
                return false;

            return true;
        }

        public static bool Check_是否关键字(string type)
        {
            if (type == "int" || type == "short" || type == "long" || type == "float" || type == "double" || type == "char"
                || type == "void" || type == "if" || type == "where" || type == "for" || type == "return" || type == "break" || type == "continue")
                return true;

            return false;
        }
    }
}
