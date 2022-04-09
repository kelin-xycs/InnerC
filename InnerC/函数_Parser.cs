using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.语句s;
using InnerC.C_Members.表达式s;

namespace InnerC
{
    class 函数_Parser
    {
        public static 函数 Parse(char[] chars, 第一层_代码块 代码块)
        {
            //int i;

            //int beginIndex = 上一个大括号块 == null ? 0 : 上一个大括号块.iRight + 1;
            //int endIndex = 大括号块.iLeft - 1;

            //i = Parser.从右向左寻找_分号_单引号_双引号(chars, beginIndex, endIndex);

            //if (i != -1)
            //    beginIndex = i + 1;

            //StrSpan span = new StrSpan(beginIndex, 大括号块.iRight);

            //int args_beginIndex;
            //int args_endIndex;

            StrSpan span = StrUtil.Trim(chars, 代码块.iLeft, 代码块.iRight, Parser._whiteSpaces);

            int 右小括号位置 = StrUtil.FindBackwardUntilNot(chars, span.iLeft, 代码块.大括号块.iLeft - 1, Parser._whiteSpaces);

            if (右小括号位置 == -1)
                throw new 语法错误_Exception("函数定义错误： 函数头为空 。", chars, 代码块.大括号块.iLeft - 1);

            if (chars[右小括号位置] != ')')
                throw new 语法错误_Exception("函数定义错误： 函数头 缺少 右小括号 \"(\" 。", chars, 右小括号位置);

            //int 右小括号位置 = StrUtil.FindBackward(chars, 代码块.iLeft, 代码块.大括号块.iLeft - 1, ')');

            //if (右小括号位置 == -1)
            //    throw new InnerCException("函数定义错误： 函数头 缺少 右小括号 \"(\" 。", chars, beginIndex - 1);

            //args_endIndex = i - 1;
            //beginIndex = i - 1;

            int 左小括号位置 = StrUtil.FindBackward(chars, span.iLeft, 右小括号位置 - 1, '(');

            if (左小括号位置 == -1)
                throw new 语法错误_Exception("函数定义错误： 函数头 缺少 左小括号 \"(\" 。", chars, span.iLeft);

            int 函数名结束位置 = StrUtil.FindBackwardUntilNot(chars, span.iLeft, 左小括号位置 - 1, Parser._whiteSpaces);

            if (!Util.Check_是否_下划线字母数字(chars[函数名结束位置]))
                throw new 语法错误_Exception("函数定义错误： 缺少 函数名 。", chars, 函数名结束位置);

            int 函数名开始位置的前一个位置 = Parser.向左寻找_不是_下划线字母数字_的字符(chars, span.iLeft, 函数名结束位置);

            

            

            int 函数名开始位置;

            if (函数名开始位置的前一个位置 == -1)
                函数名开始位置 = span.iLeft;
            else
                函数名开始位置 = 函数名开始位置的前一个位置 + 1; ;

            char c = chars[函数名开始位置];

            string 函数名 = new string(chars, 函数名开始位置, 函数名结束位置 - 函数名开始位置 + 1);

            if (!(c == '_' || StrUtil.IsLetter(c)))
                throw new 语法错误_Exception("无效的 函数名 \"" + 函数名 + "\"，函数名 应由 下划线字母数字 组成且以 下划线或字母 开头 。", chars, 函数名开始位置);

            if (Util.Check_是否关键字(函数名))
                throw new 语法错误_Exception("无效的 函数名 \"" + 函数名 + "\"，函数名 不能和 关键字 相同 。", chars, 函数名开始位置);

            //变量声明 返回类型;

            //if (函数名开始位置 == span.iLeft)
            //{
            //    return Parse(chars, -1, -1, span.iLeft, 函数名结束位置, 左小括号位置, 右小括号位置, 代码块.大括号块.iLeft, 代码块.大括号块.iRight);
            //}


            //c = chars[函数名开始位置 - 1];

            //if (!StrUtil.IsOneOf(c, Parser._whiteSpaces和星号))
            //    throw new InnerCException("未结束的语句，可能缺少分号 \";\" 。", chars, 函数名开始位置 - 1);

            int 报错位置;

            类型 返回类型 = Parse_返回类型(chars, span.iLeft, 函数名开始位置 - 1, out 报错位置);

            if (报错位置 != -1)
                throw new 语法错误_Exception("未结束的语句，可能缺少分号 \";\" 。", chars, 报错位置);


            作用域 形参列表 = Parse_形参列表(chars, 左小括号位置, 右小括号位置);
            块作用域 函数体 = Parse_函数体(chars, 代码块.大括号块);

            

            return new 函数(返回类型, 函数名, 形参列表, 函数体, 函数名开始位置);

            //int 空白和星号开始的位置的前一个位置 = StrUtil.FindBackwardUntilNot(chars, span.iLeft, 函数名开始位置 - 1, Parser._whiteSpaces和星号);

            //int 空白和星号开始位置;

            //if (空白和星号开始的位置的前一个位置 == -1)
            //{
            //    K
            //}
                

            //args_beginIndex = i + 1;
            //beginIndex = i - 1;

            //StrSpan 函数名和返回值 = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            //if (函数名和返回值.isEmpty)
            //    throw new InnerCException("函数定义错误： 缺少 函数名 。", chars, beginIndex - 1);

            //StrSpan[] spans = Parser.从右向左寻找第一个空白字符以此Split将得到的两个字符串Trim返回(chars, 函数名和返回值.iLeft, 函数名和返回值.iRight);

            //StrSpan 参数列表 = new StrSpan(args_beginIndex, args_endIndex);
            //StrSpan 函数体 = StrUtil.Trim(chars, 大括号块.iLeft + 1, 大括号块.iRight - 1, Parser._whiteSpaces);

            //return new 函数块(span, spans[0], spans[1], 参数列表, 函数体);
        }

        private static 类型 Parse_返回类型(char[] chars, int beginIndex, int endIndex, out int 报错位置)
        {

            报错位置 = -1;

            类型 返回类型 = new 类型("void", 0, null, chars, endIndex);

            //if (beginIndex > endIndex)
            //{
            //    报错位置 = -1;
            //    return 返回类型;
            //}
            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
            {
                //报错位置 = -1;
                return 返回类型;
            }

            char c = chars[endIndex];

            if (!StrUtil.IsOneOf(c, Parser._whiteSpaces和星号))
            {
                报错位置 = endIndex;
                return 返回类型;
            }

            int 空白和星号开始位置的前一个位置 = StrUtil.FindBackwardUntilNot(chars, span.iLeft, span.iRight, Parser._whiteSpaces和星号);

            if (空白和星号开始位置的前一个位置 == -1)
            {
                报错位置 = span.iRight;
                return 返回类型;
            }

            c = chars[空白和星号开始位置的前一个位置];

            if (!Util.Check_是否_下划线字母数字(c))
            {
                报错位置 = span.iRight;
                return 返回类型;
            }

            int 类型开始位置的前一个位置 = Parser.向左寻找_不是_下划线字母数字_的字符(chars, span.iLeft, 空白和星号开始位置的前一个位置);

            int 类型开始位置;

            if (类型开始位置的前一个位置 == -1)
            {
                类型开始位置 = span.iLeft;
            }
            else
            {
                类型开始位置 = 类型开始位置的前一个位置 + 1;
            }

            c = chars[类型开始位置];

            if (!(c == '_' || StrUtil.IsLetter(c)))
            {
                报错位置 = span.iRight;
                return 返回类型;
            }

            string type = new string(chars, 类型开始位置, 空白和星号开始位置的前一个位置 - 类型开始位置 + 1);

            int 星号Count = 0;

            for (int i = 空白和星号开始位置的前一个位置 + 1; i<=span.iRight; i++)
            {
                c = chars[i];

                if (c == '*')
                    星号Count++;
            }

            返回类型 = new 类型(type, 星号Count, null, chars, 类型开始位置);

            if (span.iLeft < 类型开始位置)
            {
                报错位置 = 类型开始位置 - 1;
            }

            return 返回类型;
        }

        private static 作用域 Parse_形参列表(char[] chars, int 左小括号位置, int 右小括号位置)
        {
            作用域 形参列表 = new 作用域();

            StrSpan span = StrUtil.Trim(chars, 左小括号位置 + 1, 右小括号位置 - 1, Parser._whiteSpaces);

            if (span.isEmpty)
                return 形参列表;

            List<StrSpan> list = Parser.在_单引号双引号_以外_Split(chars, span.iLeft, span.iRight, ',');

            StrSpan span形参;

            for (int i = 0; i < list.Count; i++)
            {
                span形参 = list[i];

                形参 形参 = Parse_形参(chars, span形参);

                形参列表.Add_变量定义(形参, chars);
                //if (形参列表.dic变量声明.ContainsKey(形参.name))
                //    throw new 语法错误_Exception("参数名 \"" + 形参.name + "\" 重复 。", chars, 形参.变量名位置);

                //形参列表.dic变量声明.Add(形参.name, 形参);
            }

            return 形参列表;
        }

        private static 形参 Parse_形参(char[] chars, StrSpan span)
        {
            if (span.isEmpty)
                throw new 语法错误_Exception("缺少参数定义 。", chars, span.iLeft);

            //span = StrUtil.Trim(chars, span.iLeft, span.iRight, Parser._whiteSpaces);

            //if (span.isEmpty)
            //    throw new InnerCException("缺少参数定义 。", chars, span.iLeft);


            变量声明和初始化 变量声明 = 语句_Parser.Parse_变量声明(chars, span.iLeft, span.iRight);

            if (变量声明 == null)
                throw new 语法错误_Exception("错误的参数定义 。", chars, span.iLeft);

            return 变量声明.To_形参();
            
        }

        //private static 变量声明 Parse_形参(char[] chars, StrSpan span)
        //{
        //    if (span.isEmpty)
        //        throw new InnerCException("缺少参数定义 。", chars, span.iLeft);

        //    //span = StrUtil.Trim(chars, span.iLeft, span.iRight, Parser._whiteSpaces);

        //    //if (span.isEmpty)
        //    //    throw new InnerCException("缺少参数定义 。", chars, span.iLeft);

        //    char c = chars[span.iLeft];

        //    if (!(c == '_' || StrUtil.IsLetter(c)))
        //        throw new InnerCException("错误的参数定义 。", chars, span.iLeft);

        //    int 类型结束位置的下一个位置 = Parser.向右寻找_不是_下划线字母数字_的字符(chars, span.iLeft, span.iRight);

        //    if (类型结束位置的下一个位置 == -1)
        //        throw new InnerCException("错误的参数定义 。", chars, span.iLeft);

        //    int 空白和星号结束位置的下一个位置 = StrUtil.FindForwardUntilNot(chars, 类型结束位置的下一个位置, span.iRight, Parser._whiteSpaces和星号);

        //    if (空白和星号结束位置的下一个位置 == -1)
        //        throw new InnerCException("错误的参数定义 。", chars, span.iLeft);

        //    StrSpan span参数名 = StrUtil.Trim(chars, 空白和星号结束位置的下一个位置, span.iRight, Parser._whiteSpaces);

        //    if (span参数名.isEmpty)
        //        throw new InnerCException("错误的参数定义 。", chars, span.iLeft);

        //    if (!Util.Check_是否_下划线字母数字_且以_下划线字母_开头(chars, span参数名.iLeft, span参数名.iRight))
        //        throw new InnerCException("错误的参数定义 。", chars, span.iLeft);

        //    string type = new string(chars, span.iLeft, 类型结束位置的下一个位置 - span.iLeft);

        //    int 星号Count = 0;

        //    for (int i = 类型结束位置的下一个位置; i <= 空白和星号结束位置的下一个位置 - 1; i++)
        //    {
        //        c = chars[i];

        //        if (c == '*')
        //            星号Count++;
        //    }

        //    string 参数名 = new string(chars, 空白和星号结束位置的下一个位置, span.iRight - 空白和星号结束位置的下一个位置 + 1);

        //    return new 变量声明(new 类型(type, 星号Count, null), 参数名, -1);
        //}

        private static 块作用域 Parse_函数体(char[] chars, StrSpan 大括号块)
        {

            //StrSpan span = StrUtil.Trim(chars, 大括号块.iLeft + 1, 大括号块.iRight - 1, Parser._whiteSpaces);

            //if (span.isEmpty)
            //    return new 块作用域();

            //return C_Parser.Parse_块作用域(chars, span.iLeft, span.iRight);

            return 语句_Parser.Parse_块作用域(chars, 大括号块.iLeft + 1, 大括号块.iRight - 1);
        }
    }
}
