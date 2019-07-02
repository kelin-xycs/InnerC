using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnerC
{
    class Parser
    {

        public readonly static char[] _whiteSpaces = { ' ', '\r', '\n', '\t' };

        public readonly static char[] _wrapperList = { '\'', '[', '(' };

        public readonly static char[] _whiteSpaces和星号;

        public readonly static char[] _左小括号左中括号单引号双引号 = { '(', '[', '\'', '"' };


        static Parser()
        {
            _whiteSpaces和星号 = new char[Parser._whiteSpaces.Length + 1];

            Parser._whiteSpaces.CopyTo(_whiteSpaces和星号, 0);

            _whiteSpaces和星号[_whiteSpaces和星号.Length - 1] = '*';
        }

        public static int Find_单引号对双引号对以外的内容(char[] chars, int beginIndex, int endIndex, char c1)
        {
            for (int i = beginIndex; i <= endIndex; i++)
            {
                char c = chars[i];

                if (c == '"')
                {
                    int 右双引号Index = Find_双引号_右(chars, i, endIndex);

                    if (右双引号Index == -1)
                        return -1;

                    i = 右双引号Index;

                    continue;
                }

                if (c == '\'')
                {
                    int 右单引号Index = Find_单引号_右(chars, i, endIndex);

                    if (右单引号Index == -1)
                        return -1;

                    i = 右单引号Index;

                    continue;
                }

                if (c == c1)
                {
                    return i;
                }
            }

            return -1;
        }

        //public static int Find_单引号对双引号对以外的内容(char[] chars, int beginIndex, int endIndex, char c1, char c2)
        //{
        //    for (int i = beginIndex; i <= endIndex; i++)
        //    {
        //        if (chars[i] == '"')
        //        {
        //            int 右双引号Index = Find_双引号_右(chars, i, endIndex);

        //            if (右双引号Index == -1)
        //                return -1;

        //            i = 右双引号Index;

        //            continue;
        //        }

        //        if (chars[i] == '\'')
        //        {
        //            int 右单引号Index = Find_单引号_右(chars, i, endIndex);

        //            if (右单引号Index == -1)
        //                return -1;

        //            i = 右单引号Index;

        //            continue;
        //        }

        //        if (chars[i] == c1 || chars[i] == c2)
        //        {
        //            return i;
        //        }
        //    }

        //    return -1;
        //}

        public static int 从右向左寻找_分号_单引号_双引号(char[] chars, int beginIndex, int endIndex)
        {

            char c;

            for (int i = endIndex; i >= beginIndex; i--)
            {
                c = chars[i];

                if (c== '\'' || c == '"' || c == ';')
                {
                    return i;
                }
            }

            return -1;

        }

        public static StrSpan[] 从右向左寻找第一个空白字符以此Split将得到的两个字符串Trim返回(char[] chars, int beginIndex, int endIndex)
        {
            int i = StrUtil.FindBackward(chars, beginIndex, endIndex, Parser._whiteSpaces);

            StrSpan 返回值类型;
            StrSpan 函数名;
            

            if (i == -1)
            {
                //返回值类型 = StrSpan.Empty;
                返回值类型 = new StrSpan(beginIndex, beginIndex, true);
                函数名 = new StrSpan(beginIndex, endIndex);
            }
            else
            {
                返回值类型 = StrUtil.Trim(chars, beginIndex, i - 1, Parser._whiteSpaces);
                函数名 = new StrSpan(i + 1, endIndex);
            }


            StrSpan[] spans = new StrSpan[2];

            spans[0] = 返回值类型;
            spans[1] = 函数名;

            return spans;
        }

        public static int Find_大括号_左(char[] chars, int beginIndex, int endIndex)
        {

            //int 右双引号Index;

            for (int i = beginIndex; i <= endIndex; i++)
            {
                if (chars[i] == '"')
                {
                    int 右双引号Index = Find_双引号_右(chars, i, endIndex);

                    if (右双引号Index == -1)
                        return -1;

                    i = 右双引号Index;

                    continue;
                }

                if (chars[i] == '\'')
                {
                    int 右单引号Index = Find_单引号_右(chars, i, endIndex);

                    if (右单引号Index == -1)
                        return -1;

                    i = 右单引号Index;

                    continue;
                }

                if (chars[i] == '{')
                {
                    return i;
                }
            }

            return -1;

        }

        public static int Find_大括号_右(char[] chars, int beginIndex, int endIndex)
        {
            int counter = 1;

            //int 右双引号Index;

            for (int i = beginIndex; i <= endIndex; i++)
            {
                if (chars[i] == '"')
                {
                    int 右双引号Index = Find_双引号_右(chars, i, endIndex);

                    if (右双引号Index == -1)
                        return -1;

                    i = 右双引号Index;

                    continue;
                }

                if (chars[i] == '\'')
                {
                    int 右单引号Index = Find_单引号_右(chars, i, endIndex);

                    if (右单引号Index == -1)
                        return -1;

                    i = 右单引号Index;

                    continue;
                }

                if (chars[i] == '}')
                {
                    counter--;

                    if (counter == 0)
                        return i;

                    continue;
                }

                if (chars[i] == '{')
                {
                    counter++;
                }

            }

            return -1;
        }

        public static int Find_小括号_右(char[] chars, int beginIndex, int endIndex)
        {
            int counter = 1;

            //int 右双引号Index;

            for (int i = beginIndex; i <= endIndex; i++)
            {
                if (chars[i] == '"')
                {
                    int 右双引号Index = Find_双引号_右(chars, i, endIndex);

                    if (右双引号Index == -1)
                        return -1;

                    i = 右双引号Index;

                    continue;
                }

                if (chars[i] == '\'')
                {
                    int 右单引号Index = Find_单引号_右(chars, i, endIndex);

                    if (右单引号Index == -1)
                        return -1;

                    i = 右单引号Index;

                    continue;
                }

                if (chars[i] == ')')
                {
                    counter--;

                    if (counter == 0)
                        return i;

                    continue;
                }

                if (chars[i] == '(')
                {
                    counter++;
                }

            }

            return -1;
        }
        public static int Find_中括号_右(char[] chars, int beginIndex, int endIndex)
        {
            int counter = 1;

            //int 右双引号Index;

            for (int i = beginIndex; i <= endIndex; i++)
            {
                if (chars[i] == '"')
                {
                    int 右双引号Index = Find_双引号_右(chars, i, endIndex);

                    if (右双引号Index == -1)
                        return -1;

                    i = 右双引号Index;

                    continue;
                }

                if (chars[i] == '\'')
                {
                    int 右单引号Index = Find_单引号_右(chars, i, endIndex);

                    if (右单引号Index == -1)
                        return -1;

                    i = 右单引号Index;

                    continue;
                }

                if (chars[i] == ']')
                {
                    counter--;

                    if (counter == 0)
                        return i;

                    continue;
                }

                if (chars[i] == '[')
                {
                    counter++;
                }

            }

            return -1;
        }

        private static int Find_双引号_右(char[] chars, int beginIndex, int endIndex)
        {
            for (int i = beginIndex; i <= endIndex; i++)
            {
                if (chars[i] == '"')
                {
                    if (i > 0 && chars[i - 1] != '\\')
                        return i;
                }
                    //return i;
            }

            return -1;
        }

        private static int Find_单引号_右(char[] chars, int beginIndex, int endIndex)
        {
            for (int i = beginIndex; i <= endIndex; i++)
            {
                if (chars[i] == '\'')
                {
                    if (i > 0 && chars[i - 1] != '\\')
                        return i;
                }
                //return i;
            }

            return -1;
        }


        public static List<表达式段> 按_小括号中括号单引号双引号_分段(char[] chars, int beginIndex, int endIndex)
        {
            char c;

            List<表达式段> list = new List<表达式段>();

            int j;

            for (int i = beginIndex; i <= endIndex; )
            {
                c = chars[i];

                if (c == '(')
                {
                    j = Parser.Find_小括号_右(chars, i + 1, endIndex);

                    if (j == -1)
                        throw new 语法错误_Exception("未结束的小括号，缺少右小括号 。", chars, i);

                    list.Add(new 表达式段(i, j, 表达式段_Type.小括号段));

                    i = j + 1;

                    continue;
                }

                if (c == '[')
                {
                    j = Parser.Find_中括号_右(chars, i + 1, endIndex);

                    if (j == -1)
                        throw new 语法错误_Exception("未结束的中括号，缺少右中括号 。", chars, i);

                    list.Add(new 表达式段(i, j, 表达式段_Type.中括号段));

                    i = j + 1;

                    continue;
                }

                if (c == '\'')
                {
                    j = Parser.Find_单引号_右(chars, i + 1, endIndex);

                    if (j == -1)
                        throw new 语法错误_Exception("未结束的单括号，缺少右单引号 。", chars, i);

                    list.Add(new 表达式段(i, j, 表达式段_Type.单引号段));

                    i = j + 1;

                    continue;
                }

                if (c == '"')
                {
                    j = Parser.Find_双引号_右(chars, i + 1, endIndex);

                    if (j == -1)
                        throw new 语法错误_Exception("未结束的单括号，缺少右双引号 。", chars, i);

                    list.Add(new 表达式段(i, j, 表达式段_Type.双引号段));

                    i = j + 1;

                    continue;
                }


                j = StrUtil.FindForward(chars, i + 1, endIndex, _左小括号左中括号单引号双引号);

                if (j == -1)
                {
                    list.Add(new 表达式段(i, endIndex, 表达式段_Type.普通段));
                    break;
                }

                list.Add(new 表达式段(i, j - 1, 表达式段_Type.普通段));

                i = j;
            }

            return list;
        }

        public static void 把普通段分为运算符段和普通段(char[] chars, 表达式段 表达式段, List<表达式段> listNew)
        {
            char c1, c2;

            int p = 表达式段.iLeft;

            for (int i = 表达式段.iLeft; i <= 表达式段.iRight; i++)
            {

                c1 = chars[i];

                if (i < 表达式段.iRight)
                {

                    c2 = chars[i + 1];


                    if (c1 == '-' && c2 == '>')
                    {
                        i = i + 2;
                        p = 2;
                        continue;
                    }

                    if (c1 == '=' && c2 == '=' || c1 == '>' && c2 == '=' || c1 == '<' && c2 == '='
                        || c1 == '!' && c2 == '=' || c1 == '&' && c2 == '&' || c1 == '|' && c2 == '|')
                    {

                        if (p <= i - 1)
                        {
                            listNew.Add(new 表达式段(p, i - 1, 表达式段_Type.普通段));
                        }

                        listNew.Add(new 表达式段(i, i + 1, 表达式段_Type.运算符段));

                        i = i + 2;

                        p = i;

                        continue;
                    }
                }


                if (c1 == '+' || c1 == '-' || c1 == '*' || c1 == '/' || c1 == '>' || c1 == '<' || c1 == '!'
                    || c1 == '=' || c1 == '&')
                {
                    if (p <= i - 1)
                    {
                        listNew.Add(new 表达式段(p, i - 1, 表达式段_Type.普通段));
                    }

                    listNew.Add(new 表达式段(i, i, 表达式段_Type.运算符段));

                    i = i + 1;

                    p = i;
                }

            }

            if (p <= 表达式段.iRight)
            {
                listNew.Add(new 表达式段(p, 表达式段.iRight, 表达式段_Type.普通段));
            }
                
        }

        public static List<表达式段> 去掉空白段(char[] chars, List<表达式段> list)
        {
            List<表达式段> listNew = new List<表达式段>();

            StrSpan span;

            表达式段 表达式段;

            for (int i = 0; i < list.Count; i++)
            {
                表达式段 = list[i];

                span = StrUtil.Trim(chars, 表达式段.iLeft, 表达式段.iRight, Parser._whiteSpaces);

                if (span.isEmpty)
                    continue;

                listNew.Add(表达式段);
            }

            return listNew;
        }

        public static int 向左寻找_不是_下划线字母数字_的字符(char[] chars, int beginIndex, int endIndex)
        {
            char c;

            for (int i = endIndex; i>=beginIndex; i--)
            {
                c = chars[i];

                if (!Util.Check_是否_下划线字母数字(c))
                    return i;
            }

            return -1;
        }

        public static int 向右寻找_不是_下划线字母数字_的字符(char[] chars, int beginIndex, int endIndex)
        {
            char c;

            for (int i = beginIndex; i <= endIndex; i++)
            {
                c = chars[i];

                if (!Util.Check_是否_下划线字母数字(c))
                    return i;
            }

            return -1;
        }

        public static List<StrSpan> 在_单引号双引号_以外_Split(char[] chars, int beginIndex, int endIndex, char c)
        {

            List<StrSpan> list = new List<StrSpan>();

            int iLeft = beginIndex;

            for ( ; iLeft <= endIndex ; )
            {
                int q = Find_单引号对双引号对以外的内容(chars, iLeft, endIndex, c);

                if (q == -1)
                    break;

                list.Add(StrUtil.Trim(chars, iLeft, q - 1, Parser._whiteSpaces));

                iLeft = q + 1;

            }

            list.Add(StrUtil.Trim(chars, iLeft, endIndex, Parser._whiteSpaces));

            return list;
        }

        public static bool 开头一段_Equals(char[] chars, int beginIndex, int endIndex, string str)
        {
            if (endIndex - beginIndex + 1 < str.Length)
                return false;

            int j;

            for (int i = 0; i < str.Length; i++)
            {
                j = beginIndex + i;

                if (chars[j] != str[i])
                    return false;
            }

            return true;
        }


    }

}
