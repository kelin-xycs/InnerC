using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.语句s;
using InnerC.C_Members.表达式s;

namespace InnerC
{
    class 第一层_Parser
    {
        public static void Parse(ParseResult r, char[] chars)
        {


            List<第一层_代码块> list = 划分_全局变量块_结构体或函数块(chars);

            //List<StrSpan> list大括号块 = Get_大括号块_List(chars);
            第一层_代码块 代码块;


            for (int i = 0; i<list.Count; i++)
            {
                代码块 = list[i];

                Parse(r, chars, 代码块);
            }

            //List<结构体块> list结构体块 = new List<结构体块>();
            //List<函数块> list函数块 = new List<函数块>();

            //List<StrSpan> list结构体块和函数块 = new List<StrSpan>();

            //StrSpan 上一个大括号块 = null;

            //for (int i = 0; i < list大括号块.Count; i++)
            //{
            //    StrSpan 大括号块 = list大括号块[i];

            //    if (i > 0)
            //        上一个大括号块 = list大括号块[i - 1];

            //    结构体块 结构体块 = Parse_结构体块(chars, 大括号块, 上一个大括号块);

            //    if (结构体块 != null)
            //    {
            //        list结构体块.Add(结构体块);
            //        list结构体块和函数块.Add(结构体块.span);
            //        continue;
            //    }

            //    函数块 函数块 = Parse_函数块(chars, 大括号块, 上一个大括号块);

            //    list函数块.Add(函数块);
            //    list结构体块和函数块.Add(函数块.span);
            //}

            //List<全局变量块> list全局变量块 = Get_结构体和函数以外的块(chars, list结构体块和函数块);
        }

        private static void Parse(ParseResult r, char[] chars, 第一层_代码块 代码块)
        {

            if (代码块.type == 第一层_代码块_Type.全局变量块)
            {
                Parse_全局变量(r, chars, 代码块);
                return;
            }
                

            结构体 结构体 = 结构体_Parser.Parse(chars, 代码块);

            if (结构体 != null)
            {
                if (r.dic结构体.ContainsKey(结构体.name))
                    throw new 语法错误_Exception("结构体名 \"" + 结构体.name + "\" 重复 。", chars, 结构体.结构体名_iLeft);

                //结构体.Set_全局成员(r);
                结构体.Set_作用域(r.全局变量);

                r.dic结构体.Add(结构体.name, 结构体);

                return;
            }


            函数 函数 = 函数_Parser.Parse(chars, 代码块);

            if (r.dic函数.ContainsKey(函数.name))
                throw new 语法错误_Exception("函数名 \"" + 函数.name + "\" 重复 。", chars, 函数.函数名_iLeft);

            //函数.Set_全局成员(r);
            函数.Set_作用域(r.全局变量);

            r.dic函数.Add(函数.name, 函数);
            
        }

        private static List<第一层_代码块> 划分_全局变量块_结构体或函数块(char[] chars)
        {
            List<第一层_代码块> list = new List<第一层_代码块>();

            List<StrSpan> list大括号块 = Get_大括号块_List(chars);

            //第一层_代码块 块;

            StrSpan 大括号块;
            StrSpan span;

            int p = 0, q;

            for (int i = 0; i< list大括号块.Count; i++)
            {
                大括号块 = list大括号块[i];

                q = Parser.从右向左寻找_分号_单引号_双引号(chars, p, 大括号块.iLeft);

                if (q == -1)
                {
                    list.Add(new 第一层_代码块(p, 大括号块.iRight, 第一层_代码块_Type.结构体或函数块, 大括号块));
                }
                else
                {
                    span = StrUtil.Trim(chars, p, q, Parser._whiteSpaces);

                    if (!span.isEmpty)
                    {
                        list.Add(new 第一层_代码块(span.iLeft, span.iRight, 第一层_代码块_Type.全局变量块, null));
                    }

                    list.Add(new 第一层_代码块(q + 1, 大括号块.iRight, 第一层_代码块_Type.结构体或函数块, 大括号块));
                }

                p = 大括号块.iRight + 1;
            }

            span = StrUtil.Trim(chars, p, chars.Length - 1, Parser._whiteSpaces);

            if (!span.isEmpty)
            {
                list.Add(new 第一层_代码块(span.iLeft, span.iRight, 第一层_代码块_Type.全局变量块, null));
            }

            return list;
        }

        private static List<StrSpan> Get_大括号块_List(char[] chars)
        {
            int beginIndex = 0;
            int endIndex = chars.Length - 1;

            List<StrSpan> list = new List<StrSpan>();

            while (true)
            {
                int i = Parser.Find_大括号_左(chars, beginIndex, endIndex);

                if (i == -1)
                    break;

                int j = Parser.Find_大括号_右(chars, i + 1, endIndex);

                if (j == -1)
                    throw new 语法错误_Exception("未找到与左大括号成对的右大括号 。", chars, i);

                list.Add(new StrSpan(i, j));

                beginIndex = j + 1;

                if (beginIndex > endIndex)
                    break;
            }

            return list;

        }

        private static void Parse_全局变量(ParseResult r, char[] chars, 第一层_代码块 代码块)
        {

            //块作用域 块 = 语句_Parser.Parse_块作用域(chars, 代码块.iLeft, 代码块.iRight);

            List<StrSpan> list = Parser.在_单引号双引号_以外_Split(chars, 代码块.iLeft, 代码块.iRight, ';');

            StrSpan span;

            for (int i = 0; i<list.Count; i++)
            {
                span = list[i];

                if (span.isEmpty)
                    continue;

                if (i == list.Count - 1)
                    throw new 语法错误_Exception("未结束的语句，缺少分号 \";\" 。", chars, span.iRight);


                变量声明和初始化 变量声明 = 语句_Parser.Parse_变量声明(chars, span.iLeft, span.iRight);

                if (变量声明 == null)
                    throw new 语法错误_Exception("无效的表达式 。", chars, span.iLeft);

                r.全局变量.Add_变量定义(变量声明, chars);
                //if (r.dic全局变量.ContainsKey(变量声明.name))
                //if (r.全局变量.dic变量声明.ContainsKey(变量声明.name))
                //    throw new 语法错误_Exception("已定义了名为 \"" + 变量声明.name + "\" 的 全局变量 。", chars, 变量声明.变量名位置);

                //r.全局变量.dic变量声明.Add(变量声明.name, 变量声明.To_全局变量());
                //r.dic全局变量.Add(变量声明.name, 变量声明.To_全局变量());

            }
        }
        //private static 结构体块 Parse_结构体块(char[] chars, StrSpan 大括号块, StrSpan 上一个大括号块)
        //{
        //    int i;

        //    int beginIndex = 上一个大括号块 == null ? 0 : 上一个大括号块.iRight + 1;
        //    int endIndex = 大括号块.iLeft - 1;

        //    i = Parser.从右向左寻找_分号_或者_双引号(chars, beginIndex, endIndex);

        //    if (i != -1)
        //        beginIndex = i + 1;

        //    StrSpan span = new StrSpan(beginIndex, 大括号块.iRight);

        //    while (true)
        //    {
        //        i = StrUtil.FindBackward(chars, beginIndex, endIndex, "struct");

        //        if (i == -1)
        //            break;

        //        char c1 = chars[i - 1];
        //        char c2 = chars[i + 6];

        //        if (
        //            (i == span.iLeft
        //              || i > span.iLeft && StrUtil.IsOneOf(chars[i - 1], Parser._whiteSpaces))
        //            &&
        //            (i + 5 == span.iRight
        //              || i + 5 < span.iRight && StrUtil.IsOneOf(chars[i + 6], Parser._whiteSpaces))
        //              )
        //        {
        //            break;
        //        }

        //        beginIndex = i - 1;
        //    }

        //    if (i == -1)
        //        return null;

        //    StrSpan 结构体名字 = StrUtil.Trim(chars, i + 6, 大括号块.iLeft - 1, Parser._whiteSpaces);

        //    if (结构体名字.isEmpty)
        //        throw new InnerCException("结构体定义缺少结构体名字 。", chars, i + 5);

        //    return new 结构体块(span, 结构体名字, 大括号块);

        //}

        //private static 函数块 Parse_函数块(char[] chars, StrSpan 大括号块, StrSpan 上一个大括号块)
        //{

            
        //}

        //private static List<全局变量块> Get_结构体和函数以外的块(char[] chars, List<StrSpan> list结构体块和函数块)
        //{
        //    List<全局变量块> list = new List<全局变量块>();

        //    StrSpan span;

        //    if (list结构体块和函数块.Count == 0)
        //    {
        //        span = StrUtil.Trim(chars, 0, chars.Length - 1, Parser._whiteSpaces);

        //        if (!span.isEmpty)
        //            list.Add(new 全局变量块(span));

        //        return list;
        //    }

        //    for (int i = 0; i <= list结构体块和函数块.Count; i++)
        //    {

        //        int beginIndex = 0;
        //        int endIndex;

        //        if (i > 0)
        //            beginIndex = list结构体块和函数块[i - 1].iRight + 1;

        //        if (i == list结构体块和函数块.Count)
        //            endIndex = chars.Length - 1;
        //        else
        //            endIndex = list结构体块和函数块[i].iLeft - 1;

        //        span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

        //        if (span.isEmpty)
        //            continue;

        //        list.Add(new 全局变量块(span));
        //    }

        //    return list;
        //}

        //private static void Parse_全局变量(ParseResult r, List<全局变量块> list全局变量块)
        //{
        //    for (int i = 0; i < list全局变量块.Count; i++)
        //    {
        //        全局变量块 全局变量块 = list全局变量块[i];

        //        全局变量块.Parse(r);
        //    }
        //}
    }
}
