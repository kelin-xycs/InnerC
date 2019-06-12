using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.语句s;

namespace InnerC
{
    class if_语句_Parser
    {
        public static if_语句 Parse(char[] chars, int beginIndex, int endIndex, out int j)
        {
            j = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            //bool b = Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "if");

            //if (!b)
            //    return null;

            //if ()

            if_分句 if_分句 = Parse_if_分句(chars, span.iLeft, span.iRight, out j);

            if (if_分句 == null)
                return null;

            List<if_分句> if_分句_和_else_if_分句_List = new List<if_分句>();
            块作用域 最后结尾的_else_分句 = null;

            if_分句_和_else_if_分句_List.Add(if_分句);

            while (true)
            {
                //b = Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "if");

                //if (!b)
                //    break;

                //if_分句 if_分句 = Parse_if_分句(chars, span.iLeft, span.iRight, out j);

                //if_分句_和_else_if_分句_List.Add(if_分句);

                span = StrUtil.Trim(chars, j + 1, span.iRight, Parser._whiteSpaces);

                if (span.isEmpty)
                    break;

                bool b = Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "else");

                if (!b)
                    break;

                int else后的位置 = span.iLeft + 4;

                if (else后的位置 > span.iRight)
                    break;

                char c = chars[span.iLeft + 4];

                if (!StrUtil.IsOneOf(c, Parser._whiteSpaces))
                    break;

                //int p = StrUtil.FindForwardUntilNot(chars, span.iLeft + 4, span.iRight, Parser._whiteSpaces);

                //if (p == -1)
                //    break;

                int else结束位置 = span.iLeft + 3;

                span = StrUtil.Trim(chars, else后的位置, span.iRight, Parser._whiteSpaces);

                if (span.isEmpty)
                    throw new InnerCException("\"else\" 后缺少子句 。", chars, else结束位置);

       
                    
                if_分句 = Parse_if_分句(chars, span.iLeft, span.iRight, out j);

                if (if_分句 != null)
                {
                    if_分句_和_else_if_分句_List.Add(if_分句);

                    continue;
                }
                //b = Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "if");

                //if (b)
                //{

                //    if_分句 = Parse_if_分句(chars, span.iLeft, span.iRight, out j);

                //    if_分句_和_else_if_分句_List.Add(if_分句);

                //    continue;
                //}


                最后结尾的_else_分句 = Parse_子句(chars, span.iLeft, span.iRight, out j);

                //if (最后结尾的_else_分句.list语句.Count == 0)
                //    throw new InnerCException("无效的语句 。", chars, span.iLeft);

                break;

            }

            //if (1 == 1)
            //{

            //}
            //else
            //{

            //}

            return new if_语句(if_分句_和_else_if_分句_List, 最后结尾的_else_分句);
            
        }

        private static if_分句 Parse_if_分句(char[] chars, int beginIndex, int endIndex, out int j)
        {
            j = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            bool b = Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "if");

            if (!b)
                return null;

            int 左小括号位置 = StrUtil.FindForwardUntilNot(chars, span.iLeft + 2, span.iRight, Parser._whiteSpaces);

            if (左小括号位置 == -1)
                throw new InnerCException("未结束的 if 语句 。", chars, span.iLeft + 1);

            char c = chars[左小括号位置];

            if (c != '(')
                throw new InnerCException("if 语句 缺少条件判断 左小括号 。", chars, 左小括号位置);

            int 右小括号位置 = Parser.Find_小括号_右(chars, 左小括号位置 + 1, span.iRight);

            if (右小括号位置 == -1)
                throw new InnerCException("if 语句 缺少条件判断 右小括号 。", chars, 左小括号位置 + 1);


            StrSpan span判断 = StrUtil.Trim(chars, 左小括号位置 + 1, 右小括号位置 - 1, Parser._whiteSpaces);

            if (span判断.isEmpty)
                throw new InnerCException("if 语句 缺少判断表达式 。", chars, 左小括号位置);

            表达式 if_判断 = 表达式_Parser.Parse(chars, 左小括号位置 + 1, 右小括号位置 - 1);


            StrSpan span子句 = StrUtil.Trim(chars, 右小括号位置 + 1, span.iRight, Parser._whiteSpaces);

            if (span子句.isEmpty)
                throw new InnerCException("if 语句 缺少子句 。", chars, 右小括号位置 + 1);

            块作用域 子句 = Parse_子句(chars, span子句.iLeft, span子句.iRight, out j);

            //j = span子句.iRight;

            return new if_分句(if_判断, 子句);

        }

        private static 块作用域 Parse_子句(char[] chars, int beginIndex, int endIndex, out int j)
        {
            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                throw new InnerCException("缺少子句 。", chars, span.iLeft);

            char c = chars[span.iLeft];

            if (c == '{')
            {
                return Parse_子句_大括号块(chars, span.iLeft, span.iRight, out j);
            }
                
            return Parse_子句_单句(chars, span.iLeft, span.iRight, out j);
        }

        private static 块作用域 Parse_子句_大括号块(char[] chars, int 左大括号位置, int endIndex, out int j)
        {
            int 右大括号位置 = Parser.Find_大括号_右(chars, 左大括号位置 + 1, endIndex);

            if (右大括号位置 == -1)
                throw new InnerCException("缺少 右大括号 。", chars, 左大括号位置);

            j = 右大括号位置;

            return 语句_Parser.Parse_块作用域(chars, 左大括号位置 + 1, 右大括号位置 - 1);

        }

        private static 块作用域 Parse_子句_单句(char[] chars, int beginIndex, int endIndex, out int j)
        {
            
            return 语句_Parser.Parse_块作用域(chars, beginIndex, endIndex, ParseOptions.Parse_第一个语句, out j);

        }
    }

    
}
