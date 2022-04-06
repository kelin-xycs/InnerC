using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.语句s;

using InnerC.C_Members.表达式s;

namespace InnerC
{
    class for_语句_Parser
    {
        public static for_语句 Parse(char[] chars, int beginIndex, int endIndex, out int 语句结束位置)
        {
            语句结束位置 = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            if (!Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "for"))
                return null;

            int 左小括号位置 = StrUtil.FindForwardUntilNot(chars, span.iLeft + 3, span.iRight, Parser._whiteSpaces);

            if (左小括号位置 == -1)
                throw new 语法错误_Exception("未结束的 for 语句 。", chars, span.iLeft + 1);

            char c = chars[左小括号位置];

            if (c != '(')
                throw new 语法错误_Exception("for 语句 缺少 左小括号 。", chars, 左小括号位置);

            int 右小括号位置 = Parser.Find_小括号_右(chars, 左小括号位置 + 1, span.iRight);

            if (右小括号位置 == -1)
                throw new 语法错误_Exception("for 语句 缺少 右小括号 。", chars, 左小括号位置 + 1);


            //StrSpan span判断 = StrUtil.Trim(chars, 左小括号位置 + 1, 右小括号位置 - 1, Parser._whiteSpaces);

            //if (span判断.isEmpty)
            //    throw new 语法错误_Exception("for 语句 缺少表达式 。", chars, 左小括号位置);

            //表达式 for_表达式 = 表达式_Parser.Parse(chars, 左小括号位置 + 1, 右小括号位置 - 1);
            表达式[] 小括号表达式s = Parse_小括号(chars, 左小括号位置, 右小括号位置);


            StrSpan span子句 = StrUtil.Trim(chars, 右小括号位置 + 1, span.iRight, Parser._whiteSpaces);

            if (span子句.isEmpty)
                throw new 语法错误_Exception("for 语句 缺少子句 。", chars, 右小括号位置 + 1);

            块作用域 子句 = Parse_子句(chars, span子句.iLeft, span子句.iRight, out 语句结束位置);

            //j = span子句.iRight;

            return new for_语句(小括号表达式s, 子句);

        }

        private static 表达式[] Parse_小括号(char[] chars, int 左小括号位置, int 右小括号位置)
        {
            //作用域 形参列表 = new 作用域();

            表达式[] 小括号表达式s = new 表达式[3];

            StrSpan span = StrUtil.Trim(chars, 左小括号位置 + 1, 右小括号位置 - 1, Parser._whiteSpaces);

            if (span.isEmpty)
                throw new 语法错误_Exception("for() 小括号内应是两个分号 。", chars, 左小括号位置);
            //return exppressArr;

            List<StrSpan> list = Parser.在_单引号双引号_以外_Split(chars, span.iLeft, span.iRight, ';');

            if (list.Count != 3)
                throw new 语法错误_Exception("for() 小括号内应是两个分号 。", chars, 左小括号位置);

            StrSpan spanExpress;

            for (int i = 0; i < list.Count; i++)
            {
                spanExpress = list[i];

                变量声明和初始化 变量声明 = 语句_Parser.Parse_变量声明(chars, spanExpress.iLeft, spanExpress.iRight);

                if  (变量声明 != null)
                {
                    小括号表达式s[i] = 变量声明;
                    continue;
                }

                表达式 表达式 = 表达式_Parser.Parse(chars, spanExpress.iLeft, spanExpress.iRight);

                小括号表达式s[i] = 表达式;
                
            }

            return 小括号表达式s;
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
