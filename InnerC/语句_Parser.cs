using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.语句s;
using InnerC.C_Members.表达式s;

namespace InnerC
{
    enum ParseOptions
    {
        Parse_第一个语句,
        Parse_全部语句
    }

    class 语句_Parser
    {

        public static 块作用域 Parse_块作用域(char[] chars, int beginIndex, int endIndex)
        {
            return Parse_块作用域(chars, beginIndex, endIndex, ParseOptions.Parse_全部语句, out int j);
        }

        public static 块作用域 Parse_块作用域(char[] chars, int beginIndex, int endIndex, ParseOptions option, out int j)
        {
            j = -1;

            块作用域 块 = new 块作用域();

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return 块;

            //int j;

            //List<语句> list语句 = new List<语句>();
            

            for (int i = span.iLeft; ; i = j + 1)
            {
                span = StrUtil.Trim(chars, i, span.iRight, Parser._whiteSpaces);

                if (span.isEmpty)
                    break;
                //if (Check_变量类型_KeyWord(chars, i, 代码段.iRight, "int"))
                //{
                //    语句 = Parse_变量声明和赋值_语句(chars, i, 代码段.iRight, "int", out j, list变量声明);
                //}
                //else if (Check_变量类型_KeyWord(chars, i, 代码段.iRight, "float"))
                //{
                //    语句 = Parse_变量声明和赋值_语句(chars, i, 代码段.iRight, "float", out j, list变量声明);
                //}
                //else if (Check_变量类型_KeyWord(chars, i, 代码段.iRight, "double"))
                //{
                //    语句 = Parse_变量声明和赋值_语句(chars, i, 代码段.iRight, "double", out j, list变量声明);
                //}
                //else if (Check_变量类型_KeyWord(chars, i, 代码段.iRight, "char"))
                //{
                //    语句 = Parse_变量声明和赋值_语句(chars, i, 代码段.iRight, "char", out j, list变量声明);
                //}
                //if (Check_if_while_KeyWord(chars, i, 代码段.iRight, "if"))
                //{
                //    语句 = Parse_if_语句(chars, i, 代码段.iRight, out j, list变量声明);
                //}
                //else if (Check_if_while_KeyWord(chars, i, 代码段.iRight, "while"))
                //{
                //    语句 = Parse_while_语句(chars, i, 代码段.iRight, out j, list变量声明);
                //}
                //else if (Check_return_KeyWord(chars, i, 代码段.iRight))
                //{
                //    语句 = Parse_return_语句(chars, i, 代码段.iRight, out j, list变量声明);
                //}
                //else if (Check_变量声明(chars, i, 代码段.iRight, out 变量类型))
                //{
                //    语句 = Parse_变量声明和赋值_语句(chars, i, 代码段.iRight, 变量类型, out j, list变量声明);
                //}
                //else
                //{
                //    语句 = Parse_表达式_语句(chars, i, 代码段.iRight, out j, list变量声明);
                //}

                if_语句 if_语句 = if_语句_Parser.Parse(chars, span.iLeft, span.iRight, out j);

                if (if_语句 != null)
                {
                    if_语句.Set_作用域(块);
                    
                    块.list语句.Add(if_语句);

                    continue;
                }

                while_语句 while_语句 = while_语句_Parser.Parse(chars, span.iLeft, span.iRight, out j);

                if (while_语句 != null)
                {
                    while_语句.Set_作用域(块);

                    块.list语句.Add(while_语句);

                    continue;
                }

                break_语句 break_语句 = Parse_break_语句(chars, span.iLeft, span.iRight, out j);

                if (break_语句 != null)
                {
                    break_语句.Set_作用域(块);

                    块.list语句.Add(break_语句);

                    continue;
                }

                return_语句 return_语句 = Parse_return_语句(chars, span.iLeft, span.iRight, out j);

                if (return_语句 != null)
                {
                    return_语句.Set_作用域(块);

                    块.list语句.Add(return_语句);

                    continue;
                }


                变量声明和初始化语句 变量声明语句 = Parse_变量声明和初始化_语句(chars, span.iLeft, span.iRight, out j);

                if (变量声明语句 != null)
                {
                    变量声明和初始化 变量声明 = 变量声明语句.变量声明;

                    if (块.dic变量声明.ContainsKey(变量声明.name))
                        throw new 语法错误_Exception("在当前作用域范围内已定义了名为 \"" + 变量声明.name + "\" 的变量 。", chars, 变量声明.变量名位置);

                    变量声明语句.Set_作用域(块);

                    块.dic变量声明.Add(变量声明.name, 变量声明);

                    块.list语句.Add(变量声明语句);

                    continue;
                }


                表达式语句 表达式语句 = Parse_表达式语句(chars, span.iLeft, span.iRight, out j);

                if (表达式语句 != null)
                {
                    表达式语句.Set_作用域(块);

                    块.list语句.Add(表达式语句);
                }
                


                if (option == ParseOptions.Parse_第一个语句)
                    break;


               
            }

            return 块;
        }

        private static 变量声明和初始化语句 Parse_变量声明和初始化_语句(char[] chars, int beginIndex, int endIndex, out int 语句结束位置)
        {

            语句结束位置 = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            语句结束位置 = Parser.Find_单引号对双引号对以外的内容(chars, span.iLeft, span.iRight, ';');

            if (语句结束位置 == -1)
                return null;

            变量声明和初始化 变量声明 = Parse_变量声明(chars, span.iLeft, 语句结束位置 - 1);

            if (变量声明 == null)
                return null;

            return new 变量声明和初始化语句(变量声明);
            //变量 变量 = 表达式 as 变量;

            //if (变量 != null)
            //{
            //    return new 变量声明语句(new string(chars, beginIndex, 类型结束的位置的下一个字符位置 - beginIndex), 变量.name, 星号Count, null);
            //}

            //赋值 赋值 = 表达式 as 赋值;

            //if (赋值 != null)
            //{
            //    变量 = 赋值.等号左边的表达式 as 变量;

            //    if (变量 != null)
            //    {
            //        赋值语句 = new 赋值语句(赋值);
            //        return new 变量声明语句(new string(chars, beginIndex, 类型结束的位置的下一个字符位置 - beginIndex), 变量.name, 星号Count, null);
            //    }

            //    数组元素 数组元素 = 赋值.等号左边的表达式 as 数组元素;

            //    if (数组元素 != null)
            //    {
            //        变量 = 数组元素.左边的表达式 as 变量;

            //        if (变量 != null)
            //        赋值语句 = new 赋值语句(new 赋值());
            //        return new 变量声明语句(new string(chars, beginIndex, 类型结束的位置的下一个字符位置 - beginIndex), 变量.name, 星号Count, null);
            //    }
            //}
        }

        public static 变量声明和初始化 Parse_变量声明(char[] chars, int beginIndex, int endIndex)
        {

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            char c = chars[span.iLeft];

            if (!(c == '_' || StrUtil.IsLetter(c)))
                return null;

            int 类型结束的位置的下一个字符位置 = Parser.向右寻找_不是_下划线字母数字_的字符(chars, span.iLeft + 1, span.iRight);

            if (类型结束的位置的下一个字符位置 == -1)
                return null;

            c = chars[类型结束的位置的下一个字符位置];

            if (!(StrUtil.IsOneOf(c, Parser._whiteSpaces)) || c == '*')
                return null;

            int 类型后不是空白和星号的位置 = StrUtil.FindForwardUntilNot(chars, 类型结束的位置的下一个字符位置, span.iRight, Parser._whiteSpaces和星号);

            if (类型后不是空白和星号的位置 == -1)
                return null;

            c = chars[类型后不是空白和星号的位置];

            if (!(c == '_' || StrUtil.IsLetter(c)))
                return null;

            int 星号Count = 0;

            for (int i = 类型结束的位置的下一个字符位置; i < 类型后不是空白和星号的位置; i++)
            {
                if (chars[i] == '*')
                    星号Count++;
            }

            表达式 表达式 = 表达式_Parser.Parse(chars, 类型后不是空白和星号的位置, span.iRight);

            int 类型字符串长度 = 类型结束的位置的下一个字符位置 - span.iLeft;

            变量声明和初始化 变量声明 = Parse_普通变量声明(chars, 表达式, span.iLeft, 类型字符串长度, 星号Count);

            if (变量声明 != null)
                return 变量声明;

            变量声明 = Parse_数组变量声明(chars, 表达式, span.iLeft, 类型字符串长度, 星号Count);

            if (变量声明 != null)
                return 变量声明;
            
            return null;
        }

        private static 变量声明和初始化 Parse_普通变量声明(char[] chars, 表达式 表达式, int p, int l, int 星号Count)
        {

            变量 变量 = 表达式 as 变量;

            if (变量 != null)
            {
                return new 变量声明和初始化(new 类型(new string(chars, p, l), 星号Count, null, chars, p), 变量.name, null, 变量.iLeft, chars, p);
            }

            赋值 赋值 = 表达式 as 赋值;

            if (赋值 != null)
            {
                变量 = 赋值.左边的表达式 as 变量;

                if (变量 != null)
                {
                    return new 变量声明和初始化(new 类型(new string(chars, p, l), 星号Count, null, chars, p), 变量.name, 赋值.右边的表达式, 变量.iLeft, chars, p);
                }
            }

            return null;
        }

        private static 变量声明和初始化 Parse_数组变量声明(char[] chars, 表达式 表达式, int p, int l, int 星号Count)
        {

            变量 变量;

            //List<常量> list_数组维度_Length;

            数组元素 数组元素 = 表达式 as 数组元素;
            

            if (数组元素 != null)
            {
                变量 = 数组元素.左边的表达式 as 变量;

                if (变量 != null)
                {
                    //list_数组维度_Length = Get_list_数组维度_Length(数组元素, chars);

                    return new 变量声明和初始化(new 类型(new string(chars, p, l), 星号Count, 数组元素.list下标, chars, p), 变量.name, null, 变量.iLeft, chars, p);
                }
            }

            赋值 赋值 = 表达式 as 赋值;

            if (赋值 != null)
            {
                数组元素 = 赋值.左边的表达式 as 数组元素;

                if (数组元素 != null)
                {
                    变量 = 数组元素.左边的表达式 as 变量;

                    if (变量 != null)
                    {
                        //list_数组维度_Length = Get_list_数组维度_Length(数组元素, chars);

                        //常量 常量 = 赋值.右边的表达式 as 常量;

                        //if (常量 == null)
                        //    throw new InnerCException("只能用常量对数组初始化 。", chars, p);

                        return new 变量声明和初始化(new 类型(new string(chars, p, l), 星号Count, 数组元素.list下标, chars, p), 变量.name, 赋值.右边的表达式, 变量.iLeft, chars, p);
                    }
                }
            }

            return null;
        }

        //private static List<常量> Get_list_数组维度_Length(数组元素 数组元素, char[] chars)
        //{
        //    List<常量> list_数组维度_Length = new List<常量>();

        //    for (int i = 0; i < 数组元素.list下标.Count; i++)
        //    {
        //        //表达式 维度长度 = 数组元素.list下标[i];

        //        //常量 维度Length = 维度长度 as 常量;

        //        //if (维度Length == null)
        //        //    throw new InnerCException("数组维度长度声明应是常量 。", chars, 维度长度.参考位置_iLeft);

        //        //list_数组维度_Length.Add(维度Length);
        //    }

        //    return list_数组维度_Length;
        //}

        //private static bool Check_变量类型_KeyWord(char[] chars, int beginIndex, int endIndex, string str)
        //{
        //    bool b = 开头一段_Equals(chars, beginIndex, endIndex, str);

        //    if (!b)
        //        return false;

        //    char c = chars[str.Length + 1];

        //    if (StrUtil.IsOneOf(c, Parser._whiteSpaces) || c == '*')
        //        return true;

        //    return false;
        //}

        //private static bool Check_变量声明(char[] chars, int beginIndex, int endIndex, out string type)
        //{

        //    char c = chars[beginIndex];

        //    if (!(StrUtil.isLetter(c) || c == '_'))
        //    {
        //        type = null;
        //        return false;
        //    }


        //    int 空白和星号第一次出现的位置 = -1;

        //    for (int i=beginIndex + 1; i<=endIndex; i++)
        //    {
        //        c = chars[i];

        //        if (StrUtil.IsOneOf(c, _whiteSpaces和星号))
        //        {
        //            空白和星号第一次出现的位置 = i;

        //            break;
        //        }

        //        if (!(StrUtil.isLetter(c) || c == '_'))
        //        {
        //            type = null;
        //            return false;
        //        }
        //    }

        //    if (空白和星号第一次出现的位置 == -1)
        //    {
        //        type = null;
        //        return false;
        //    }

        //    for (int i = 空白和星号第一次出现的位置 + 1; i<=endIndex; i++)
        //    {
        //        c = chars[i];

        //        if (StrUtil.IsOneOf(c, _whiteSpaces和星号))
        //        {
        //            continue;
        //        }

        //        if (StrUtil.isLetter(c) || c == '_')
        //        {
        //            type = new string(chars, beginIndex, 空白和星号第一次出现的位置 - beginIndex);
        //            return true;
        //        }
        //        else
        //        {
        //            type = null;
        //            return false;
        //        }
        //    }

        //    type = null;
        //    return false;

        //}

        //private static bool 开头一段_Equals(char[] chars, int beginIndex, int endIndex, string str)
        //{
        //    if (endIndex - beginIndex >= str.Length)
        //        return false;

        //    int j;

        //    for (int i=0; i<str.Length; i++)
        //    {
        //        j = beginIndex + i;

        //        if (chars[j] != str[i])
        //            return false;
        //    }

        //    return true;
        //}

        //private static 语句 Parse_变量声明和赋值_语句(char[] chars, int beginIndex, int endIndex, string type, out int j, List<变量声明语句> list变量声明)
        //{
        //    int i = StrUtil.FindForwardUntilNot(chars, beginIndex + type.Length + 1, endIndex, _whiteSpaces和星号);

        //    if (i == -1)
        //        throw new InnerCException("未正确结束的 变量声明 语句 。", chars, beginIndex);

        //    StrSpan span = StrUtil.Trim(chars, beginIndex + type.Length, i - 1, Parser._whiteSpaces);

        //    int 星号Count = 0;

        //    for (int p = span.iLeft; p<= span.iRight; p++)
        //    {
        //        星号Count++;
        //    }

        //    j = Parser.Find_单引号对双引号对以外的内容(chars, i, endIndex, ';');

        //    if (j == -1)    
        //        throw new InnerCException("未正确结束的 变量声明 语句 。", chars, i);

        //    int q = Parser.Find_单引号对双引号对以外的内容(chars, i, j - 1, '[');

        //    if (q == -1)
        //    {
        //        return Parse_普通变量声明(chars, i, j - 1, type, 星号Count, list变量声明);
        //    }
        //    else
        //    {
        //        return Parse_数组声明(chars, i, j - 1, q, type, 星号Count, list变量声明);
        //    }




        //}

        //private static 语句 Parse_普通变量声明(char[] chars, int beginIndex, int endIndex, string type, int 星号Count, List<变量声明语句>list变量声明)
        //{
        //    StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

        //    if (span.isEmpty)
        //        throw new InnerCException("未结束的变量声明语句 。", chars, beginIndex);

        //    表达式 express = C_Parser_3.Parse_表达式(chars, span.iLeft, span.iRight);

        //    变量 变量 = express as 变量;

        //    if (变量 != null)
        //    {
        //        list变量声明.Add(new 变量声明语句(type, 变量.name, 星号Count, null));
        //        return null;
        //    }

        //    赋值 赋值 = express as 赋值;

        //    if (赋值 != null)
        //    {
        //        list变量声明.Add(new 变量声明语句(type, 赋值.变量.name));
        //        return new 赋值语句(赋值);
        //    }

        //    throw new InnerCException("变量声明语句 的 类型 后应是 变量名 或 变量初始化 。", chars, span.iLeft);


        //}

        //private static 语句 Parse_数组声明(char[] chars, int beginIndex, int endIndex, int 第一个中括号出现位置, string type, int 星号Count, List<变量声明语句> list变量声明)
        //{
        //    StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

        //    if (span.isEmpty)
        //        throw new InnerCException("未结束的数组声明语句 。", chars, beginIndex);

        //    表达式 express = C_Parser_3.Parse_表达式(chars, span.iLeft, span.iRight);

        //    常量_变量_字段_指针字段_数组元素_函数调用_函数指针调用_调用链 express2 = express as 常量_变量_字段_指针字段_数组元素_函数调用_函数指针调用_调用链;

        //    变量声明语句 数组声明;

        //    if (express2 != null)
        //    {
        //        数组声明 = Get_数组声明(type, express2, 星号Count, chars, 第一个中括号出现位置);
        //        //list变量声明.Add(new 变量声明语句(type, 变量.name, 星号Count, null));
        //        list变量声明.Add(数组声明);
        //        return null;
        //    }

        //    赋值 赋值 = express as 赋值;

        //    if (赋值 == null)
        //        throw new InnerCException("无效的数组声明语句 。", chars, 第一个中括号出现位置);

        //    express2 = 赋值.等号左边的表达式 as 常量_变量_字段_指针字段_数组元素_函数调用_函数指针调用_调用链;

        //    if (express2 == null)
        //        throw new InnerCException("无效的数组声明语句 。", chars, 第一个中括号出现位置);

        //    数组声明 = Get_数组声明(type, express2, 星号Count, chars, 第一个中括号出现位置);

        //    list变量声明.Add(数组声明);

        //    return new 赋值语句(new 赋值(new 变量(数组声明.name), 赋值.值));

        //}

        //private static 变量声明语句 Get_数组声明(string type, 常量_变量_字段_指针字段_数组元素_函数调用_函数指针调用_调用链 express, int 星号Count, char[] chars, int 第一个中括号出现位置)
        //{
        //    if (express.list.Count < 2)
        //        throw new InnerCException("无效的数组声明语句 。", chars, 第一个中括号出现位置);

        //    变量 变量 = express.list[0] as 变量;

        //    if (变量 == null)
        //        throw new InnerCException("无效的数组声明语句 。", chars, 第一个中括号出现位置);

        //    数组元素 数组元素 = express.list[1] as 数组元素;

        //    if (数组元素 == null)
        //        throw new InnerCException("无效的数组声明语句 。", chars, 第一个中括号出现位置);

        //    List<常量> list_维度_Length = new List<常量>();

        //    for (int i = 0; i < 数组元素.listIndex.Count; i++)
        //    {
        //        常量 常量 = 数组元素.listIndex[i] as 常量;

        //        if (常量 == null)
        //             throw new InnerCException("数组声明 长度 应是 常量 。", chars, 第一个中括号出现位置);

        //        if (常量.type != "int")
        //            throw new InnerCException("数组声明 长度 应是 正整数 常量 。", chars, 第一个中括号出现位置);

        //        list_维度_Length.Add(常量);
        //    }

        //    return new 变量声明语句(type, 变量.name, 星号Count, list_维度_Length);
        //}

        public static 表达式语句 Parse_表达式语句(char[] chars, int beginIndex, int endIndex, out int 语句结束位置)
        {
            语句结束位置 = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            语句结束位置 = Parser.Find_单引号对双引号对以外的内容(chars, beginIndex, endIndex, ';');

            if (语句结束位置 == -1)
                throw new 语法错误_Exception("未结束的语句，缺少分号 \";\" 。", chars, span.iRight);

            span = StrUtil.Trim(chars, span.iLeft, span.iRight, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            表达式 表达式 = 表达式_Parser.Parse(chars, span.iLeft, 语句结束位置 - 1);

            return new 表达式语句(表达式);
        }

        public static return_语句 Parse_return_语句(char[] chars, int beginIndex, int endIndex, out int 语句结束位置)
        {
            语句结束位置 = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            if (!Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "return"))
                return null;

            int return后的位置 = span.iLeft + 6;

            if (return后的位置 > span.iRight)
                throw new 语法错误_Exception("未结束的 return 语句 。", chars, span.iLeft);

            char c = chars[return后的位置];

            if (c == '_' || StrUtil.IsLetter(c) || StrUtil.IsNumber(c))
                return null;

            语句结束位置 = Parser.Find_单引号对双引号对以外的内容(chars, return后的位置, span.iRight, ';');

            if (语句结束位置 == -1)
                throw new 语法错误_Exception("未结束的 return 语句，缺少分号 \";\" 。", chars, span.iLeft);

            StrSpan span返回值 = StrUtil.Trim(chars, return后的位置, 语句结束位置 - 1, Parser._whiteSpaces);

            表达式 返回值;

            if (span返回值.isEmpty)
            {
                返回值 = null;
            }
            else
            {
                返回值 = 表达式_Parser.Parse(chars, span返回值.iLeft, span返回值.iRight);
            }

            return new return_语句(返回值);
        }

        public static break_语句 Parse_break_语句(char[] chars, int beginIndex, int endIndex, out int 语句结束位置)
        {
            语句结束位置 = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            if (!Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "break"))
                return null;

            int break后的位置 = span.iLeft + 5;

            if (break后的位置 > span.iRight)
                throw new 语法错误_Exception("未结束的 break 语句 。", chars, span.iLeft);

            char c = chars[break后的位置];

            if (c == '_' || StrUtil.IsLetter(c) || StrUtil.IsNumber(c))
                return null;

            int break后不是空白的位置 = StrUtil.FindForwardUntilNot(chars, break后的位置, span.iRight, Parser._whiteSpaces);

            if (break后不是空白的位置 == -1)
                throw new 语法错误_Exception("未结束的 break 语句，缺少分号 \";\" 。", chars, span.iLeft);

            c = chars[break后不是空白的位置];

            if (c != ';')
                throw new 语法错误_Exception("无效的 break 语句，无效的字符 \"" + c + "\" 。", chars, break后不是空白的位置);

            语句结束位置 = break后不是空白的位置;

            return new break_语句();
        }

        public static continue_语句 Parse_continue_语句(char[] chars, int beginIndex, int endIndex, out int 语句结束位置)
        {
            语句结束位置 = -1;

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            if (!Parser.开头一段_Equals(chars, span.iLeft, span.iRight, "continue"))
                return null;

            int break后的位置 = span.iLeft + 5;

            if (break后的位置 > span.iRight)
                throw new 语法错误_Exception("未结束的 continue 语句 。", chars, span.iLeft);

            char c = chars[break后的位置];

            if (c == '_' || StrUtil.IsLetter(c) || StrUtil.IsNumber(c))
                return null;

            int break后不是空白的位置 = StrUtil.FindForwardUntilNot(chars, break后的位置, span.iRight, Parser._whiteSpaces);

            if (break后不是空白的位置 == -1)
                throw new 语法错误_Exception("未结束的 continue 语句，缺少分号 \";\" 。", chars, span.iLeft);

            c = chars[break后不是空白的位置];

            if (c != ';')
                throw new 语法错误_Exception("无效的 continue 语句，无效的字符 \"" + c + "\" 。", chars, break后不是空白的位置);

            语句结束位置 = break后不是空白的位置;

            return new continue_语句();
        }
    }
}