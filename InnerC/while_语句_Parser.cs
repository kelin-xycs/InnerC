using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.语句s;

namespace InnerC
{
    class while_语句_Parser
    {
        public static while_语句 Parse(char[] chars, int beginIndex, int endIndex, out int 语句结束位置)
        {
            语句结束位置 = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            if (!Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "while"))
                return null;

            int 左小括号位置 = StrUtil.FindForwardUntilNot(chars, span.iLeft + 5, span.iRight, Parser._whiteSpaces);

            if (左小括号位置 == -1)
                throw new 语法错误_Exception("未结束的 while 语句 。", chars, span.iLeft + 1);

            char c = chars[左小括号位置];

            if (c != '(')
                throw new 语法错误_Exception("while 语句 缺少条件判断 左小括号 。", chars, 左小括号位置);

            int 右小括号位置 = Parser.Find_小括号_右(chars, 左小括号位置 + 1, span.iRight);

            if (右小括号位置 == -1)
                throw new 语法错误_Exception("while 语句 缺少条件判断 右小括号 。", chars, 左小括号位置 + 1);


            StrSpan span判断 = StrUtil.Trim(chars, 左小括号位置 + 1, 右小括号位置 - 1, Parser._whiteSpaces);

            if (span判断.isEmpty)
                throw new 语法错误_Exception("while 语句 缺少判断表达式 。", chars, 左小括号位置);

            表达式 while_条件 = 表达式_Parser.Parse(chars, 左小括号位置 + 1, 右小括号位置 - 1);


            StrSpan span子句 = StrUtil.Trim(chars, 右小括号位置 + 1, span.iRight, Parser._whiteSpaces);

            if (span子句.isEmpty)
                throw new 语法错误_Exception("while 语句 缺少子句 。", chars, 右小括号位置 + 1);

            块作用域 子句 = Parse_子句(chars, span子句.iLeft, span子句.iRight, out 语句结束位置);

            //j = span子句.iRight;

            return new while_语句(while_条件, 子句);

        }

        private static 块作用域 Parse_子句(char[] chars, int beginIndex, int endIndex, out int j)
        {
            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                throw new 语法错误_Exception("缺少子句 。", chars, span.iLeft);

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
                throw new 语法错误_Exception("缺少 右大括号 。", chars, 左大括号位置);

            j = 右大括号位置;

            return 语句_Parser.Parse_块作用域(chars, 左大括号位置 + 1, 右大括号位置 - 1);

        }

        private static 块作用域 Parse_子句_单句(char[] chars, int beginIndex, int endIndex, out int j)
        {

            return 语句_Parser.Parse_块作用域(chars, beginIndex, endIndex, ParseOptions.Parse_第一个语句, out j);

        }
    }
}
