﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.语句s;
using InnerC.C_Members.表达式s;

namespace InnerC
{
    class 结构体_Parser
    {
        public static 结构体 Parse(char[] chars, 第一层_代码块 代码块)
        {
            StrSpan span = StrUtil.Trim(chars, 代码块.iLeft, 代码块.iRight, Parser._whiteSpaces);

            int struct关键字位置;

            int j = 代码块.大括号块.iLeft - 1;

            while(true)
            {
                struct关键字位置 = StrUtil.FindBackward(chars, span.iLeft, j, "struct");

                if (struct关键字位置 == -1)
                    return null;

                if (StrUtil.IsOneOf(chars[struct关键字位置 + 6], Parser._whiteSpaces))
                    break;

                j = struct关键字位置 - 1;
            }

            StrSpan span结构体名 = StrUtil.Trim(chars, struct关键字位置 + 6, 代码块.大括号块.iLeft - 1, Parser._whiteSpaces);

            Dictionary<string, 字段声明> dic字段声明 = Parse_字段声明(chars, 代码块.大括号块);

            string 结构体名 = new string(chars, span结构体名.iLeft, span结构体名.iRight - span结构体名.iLeft + 1);

            return new 结构体(结构体名, dic字段声明, span结构体名.iLeft);
        }

        private static Dictionary<string, 字段声明> Parse_字段声明(char[] chars, StrSpan 大括号块)
        {

            Dictionary<string, 字段声明> dic字段声明 = new Dictionary<string, 字段声明>();
            

            StrSpan span = StrUtil.Trim(chars, 大括号块.iLeft + 1, 大括号块.iRight - 1, Parser._whiteSpaces);

            if (span.isEmpty)
                return dic字段声明;

            List<StrSpan> list = Parser.在_单引号双引号_以外_Split(chars, span.iLeft, span.iRight, ',');

            StrSpan span字段;

            for (int i = 0; i < list.Count; i++)
            {
                span字段 = list[i];

                字段声明 字段 = Parse_字段(chars, span字段);

                if (dic字段声明.ContainsKey(字段.name))
                    throw new InnerCException("字段名 \"" + 字段.name + "\" 重复 。", chars, 字段.变量位置_iLeft);

                dic字段声明.Add(字段.name, 字段);

            }

            return dic字段声明;
        }

        private static 字段声明 Parse_字段(char[] chars, StrSpan span)
        {
            if (span.isEmpty)
                throw new InnerCException("缺少字段定义 。", chars, span.iLeft);

            //span = StrUtil.Trim(chars, span.iLeft, span.iRight, Parser._whiteSpaces);

            //if (span.isEmpty)
            //    throw new InnerCException("缺少字段定义 。", chars, span.iLeft);

            变量声明和初始化 变量声明 = 语句_Parser.Parse_变量声明(chars, span.iLeft, span.iRight);

            if (变量声明 == null)
                throw new InnerCException("错误的字段定义 。", chars, span.iLeft);

            return 变量声明.To_字段声明();
        }
    }
}
