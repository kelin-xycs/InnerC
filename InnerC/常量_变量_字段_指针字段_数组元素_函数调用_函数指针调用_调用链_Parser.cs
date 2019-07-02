using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.表达式s;

namespace InnerC
{
    class 常量_变量_字段_指针字段_数组元素_函数调用_函数指针调用_调用链_Parser
    {
        public static 表达式 Parse(char[] chars, int beginIndex, int endIndex)
        {

            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                throw new 语法错误_Exception("无效的表达式 。 缺少内容 。", chars, beginIndex);


            List<表达式段> list = Parser.按_小括号中括号单引号双引号_分段(chars, span.iLeft, span.iRight);
            list = Parser.去掉空白段(chars, list);


            表达式 express = Parse_变量(chars, list);

            if (express != null)
                return express;

            express = Parse_常量(chars, list);

            if (express != null)
                return express;

            express = Parse_小括号表达式(chars, list);

            if (express != null)
                return express;

            express = Parse_字段(chars, list);

            if (express != null)
                return express;

            express = Parse_指针字段(chars, list);

            if (express != null)
                return express;

            express = Parse_数组元素(chars, list);

            if (express != null)
                return express;

            express = Parse_函数调用(chars, list);

            if (express != null)
                return express;

            //express = Parse_函数指针调用(chars, list);

            //if (express != null)
            //    return express;

            throw new 语法错误_Exception("无效的表达式 。", chars, span.iLeft);
        }

        private static 表达式 Parse_变量(char[] chars, List<表达式段> list)
        {
            if (list.Count > 1)
                return null;

            表达式段 段 = list[0];

            if (段.type != 表达式段_Type.普通段)
                return null;

            if (!Util.Check_是否_下划线字母数字_且以_下划线字母_开头(chars, 段.iLeft, 段.iRight))
                return null;

            string name = new string(chars, 段.iLeft, 段.iRight - 段.iLeft + 1);

            变量 变量 = new 变量(name, chars, 段.iLeft);

            //变量.参考位置_iLeft = 段.iLeft;

            return 变量;
        }

        private static 表达式 Parse_常量(char[] chars, List<表达式段> list)
        {
            if (list.Count > 1)
                return null;

            表达式段 段 = list[0];

            if (段.type == 表达式段_Type.单引号段)
                return new 常量(常量_Type._char, new string(chars, 段.iLeft, 段.iRight - 段.iLeft + 1), chars, 段.iLeft);

            if (段.type == 表达式段_Type.双引号段)
                return new 常量(常量_Type._String, new string(chars, 段.iLeft, 段.iRight - 段.iLeft + 1), chars, 段.iLeft);

            if (段.type == 表达式段_Type.中括号段)
                return new 常量(常量_Type.中括号数组常量, new string(chars, 段.iLeft, 段.iRight - 段.iLeft + 1), chars, 段.iLeft);

            if (段.type != 表达式段_Type.普通段)
                return null;

            StrSpan span = StrUtil.Trim(chars, 段.iLeft, 段.iRight, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            if (Util.Check_int(chars, span.iLeft, span.iRight))
                return new 常量(常量_Type._int, new string(chars, span.iLeft, span.iRight - span.iLeft + 1), chars, span.iLeft);

            if (Util.Check_float(chars, span.iLeft, span.iRight))
                return new 常量(常量_Type._float, new string(chars, span.iLeft, span.iRight - span.iLeft + 1), chars, span.iLeft);

            return null;
        }

        private static 表达式 Parse_小括号表达式(char[] chars, List<表达式段> list)
        {
            if (list.Count > 1)
                return null;

            表达式段 段 = list[0];

            if (段.type != 表达式段_Type.小括号段)
                return null;

            return 表达式_Parser.Parse(chars, 段.iLeft + 1, 段.iRight - 1);
        }

        private static 表达式 Parse_字段(char[] chars, List<表达式段> list)
        {

            表达式段 段 = list[list.Count - 1];

            if (段.type != 表达式段_Type.普通段)
                return null;

            string 点或箭头;

            int 点或箭头的位置 = 寻找最右边的点或箭头的位置(chars, 段.iLeft, 段.iRight, out 点或箭头);

            if (点或箭头的位置 == -1)
                return null;

            if (点或箭头 != ".")
                return null;

            StrSpan 右边的部分 = StrUtil.Trim(chars, 点或箭头的位置 + 1, 段.iRight, Parser._whiteSpaces);

            if (右边的部分.isEmpty)
                throw new 语法错误_Exception("\".\" 后面缺少 字段名 。", chars, 点或箭头的位置);

            StrSpan 左边的部分 = StrUtil.Trim(chars, list[0].iLeft, 点或箭头的位置 - 1, Parser._whiteSpaces);

            if (左边的部分.isEmpty)
                throw new 语法错误_Exception("\".\" 前面缺少 变量 或者 表达式 。", chars, 点或箭头的位置);

            表达式 express = Parse(chars, 左边的部分.iLeft, 左边的部分.iRight);

            //if (!(express is 变量 || express is 指针取值 || express is 数组元素))
            //    throw new InnerCException("\".\" 前面应该是 变量 或者 指针取值 表达式 。", chars, 左边的部分.iRight);

            if (!Util.Check_是否_下划线字母数字_且以_下划线字母_开头(chars, 右边的部分.iLeft, 右边的部分.iRight))
                throw new 语法错误_Exception("无效的字段名 。", chars, 右边的部分.iLeft);

            return new 字段(express, new string(chars, 右边的部分.iLeft, 右边的部分.iRight - 右边的部分.iLeft + 1), chars, 右边的部分.iLeft);

            //变量 变量 = express as 变量;

            //if (变量 != null)
            //    return 字段(变量, null, new string(chars, 右边的部分.iLeft, 右边的部分.iRight - 右边的部分.iLeft + 1));

            //指针取值 指针取值 = express as 指针取值;

            //if (指针取值 != null)
            //    return 字段(null, 指针取值, new string(chars, 右边的部分.iLeft, 右边的部分.iRight - 右边的部分.iLeft + 1));

            //throw new InnerCException("\".\" 前面应该是 变量 或者 指针取值 表达式 。", chars, 左边的部分.iRight);
        }

        private static 表达式 Parse_指针字段(char[] chars, List<表达式段> list)
        {
            表达式段 段 = list[list.Count - 1];

            if (段.type != 表达式段_Type.普通段)
                return null;

            string 点或箭头;

            int 点或箭头的位置 = 寻找最右边的点或箭头的位置(chars, 段.iLeft, 段.iRight, out 点或箭头);

            if (点或箭头的位置 == -1)
                return null;

            if (点或箭头 != "->")
                return null;

            StrSpan 右边的部分 = StrUtil.Trim(chars, 点或箭头的位置 + 2, 段.iRight, Parser._whiteSpaces);

            if (右边的部分.isEmpty)
                throw new 语法错误_Exception("\".\" 后面缺少 字段名 。", chars, 点或箭头的位置);

            StrSpan 左边的部分 = StrUtil.Trim(chars, 段.iLeft, 点或箭头的位置 - 1, Parser._whiteSpaces);

            if (左边的部分.isEmpty)
                throw new 语法错误_Exception("\".\" 前面缺少 变量 或者 表达式 。", chars, 点或箭头的位置);

            表达式 express = Parse(chars, 左边的部分.iLeft, 左边的部分.iRight);

            //if (!(express is 变量 || express is 指针取值))
            //    throw new InnerCException("\".\" 前面应该是 变量 或者 指针取值 表达式 。", chars, 左边的部分.iRight);

            if (!Util.Check_是否_下划线字母数字_且以_下划线字母_开头(chars, 右边的部分.iLeft, 右边的部分.iRight))
                throw new 语法错误_Exception("无效的字段名 。", chars, 右边的部分.iLeft);

            return new 指针字段(express, new string(chars, 右边的部分.iLeft, 右边的部分.iRight - 右边的部分.iLeft + 1), chars, 点或箭头的位置);
        }

        private static 表达式 Parse_数组元素(char[] chars, List<表达式段> list)
        {
            表达式段 段;

            List<表达式段> list右边连续的中括号段 = new List<表达式段>();

            int p = -1;

            for (int i= list.Count - 1; i>=0; i--)
            {
                段 = list[i];

                if (段.type != 表达式段_Type.中括号段)
                    break;

                p = i;
                //list右边连续的中括号段.Add(段);
            }

            if (p != -1)
            {
                for (int i = p; i < list.Count; i++)
                {
                    段 = list[i];

                    list右边连续的中括号段.Add(段);
                }
            }
            
            if (list右边连续的中括号段.Count == 0)
                return null;

            表达式 express;

            List<表达式> list下标 = new List<表达式>();

            for (int i = 0; i<list右边连续的中括号段.Count; i++)
            {
                段 = list右边连续的中括号段[i];

                StrSpan span下标 = StrUtil.Trim(chars, 段.iLeft + 1, 段.iRight - 1, Parser._whiteSpaces);

                if (span下标.isEmpty)
                {
                    list下标.Add(null);
                    continue;
                }

                express = 表达式_Parser.Parse(chars, span下标.iLeft, span下标.iRight);

                //express.参考位置_iLeft = 段.iLeft;

                list下标.Add(express);
            }

            StrSpan 左边的部分 = StrUtil.Trim(chars, list[0].iLeft, list右边连续的中括号段[0].iLeft - 1, Parser._whiteSpaces);

            if (左边的部分.isEmpty)
                throw new 语法错误_Exception("\"[ ]\" 前面缺少 数组变量 或者 可以返回一个数组指针的表达式 。", chars, list[0].iLeft);

            表达式 左边的表达式 = Parse(chars, 左边的部分.iLeft, 左边的部分.iRight);

            return new 数组元素(左边的表达式, list下标, chars, 左边的部分.iLeft);
        }

        private static 表达式 Parse_函数调用(char[] chars, List<表达式段> list)
        {

            if (list.Count < 2)
                return null;

            表达式段 段 = list[list.Count - 1];

            if (段.type != 表达式段_Type.小括号段)
                return null;

            表达式 左边的表达式 = Parse(chars, list[0].iLeft, 段.iLeft - 1);

            变量 函数名 = 左边的表达式 as 变量;

            if (函数名 != null)
            {
                List<表达式> list实参 = Parse_实参(chars, 段);

                return new 函数调用(函数名.name, list实参, chars, 段.iLeft);
            }

            指针取值 指针取值 = 左边的表达式 as 指针取值;

            if (指针取值 != null)
            {
                List<表达式> list实参 = Parse_实参(chars, 段);

                return new 函数指针调用(指针取值, list实参, chars, 段.iLeft);
            }

            throw new 语法错误_Exception("函数调用 左边应该是 函数名 或者 指针取值 表达式 如 (* p) 。", chars, 段.iLeft);
            //if (指针取值 == null)
            //    return null;


            //return new 函数调用(左边的表达式, list实参);
        }

        //private static 表达式 Parse_函数指针调用(char[] chars, List<表达式段> list)
        //{
        //    if (list.Count < 2)
        //        return null;

        //    表达式段 段 = list[list.Count - 1];

        //    if (段.type != 表达式段_Type.小括号段)
        //        return null;

        //    表达式 express = Parse(chars, list[0].iLeft, 段.iLeft - 1);

        //    //指针取值 指针取值 = express as 指针取值;

        //    //if (指针取值 == null)
        //    //    return null;

        //    List<表达式> list实参 = Parse_实参(chars, 段);
            
        //    //return new 函数指针调用(指针取值, list实参);
        //    return new 函数指针调用(express, list实参);
        //}

        private static List<表达式> Parse_实参(char[] chars, 表达式段 段)
        {

            List<表达式> list实参 = new List<表达式>();

            List<StrSpan> list = 在_小括号中括号单引号双引号_之外用逗号_Split(chars, 段.iLeft + 1, 段.iRight - 1);

            if (list.Count == 1 && list[0].isEmpty)
                return list实参;

            StrSpan span;

            for (int i = 0; i<list.Count; i++)
            {
                span = list[i];

                if (span.isEmpty)
                    throw new 语法错误_Exception("缺少参数 。", chars, span.iLeft);
            }

            for (int i = 0; i<list.Count; i++)
            {
                span = list[i];

                list实参.Add(表达式_Parser.Parse(chars, span.iLeft, span.iRight));
            }

            return list实参;

        }

        private static List<StrSpan> 在_小括号中括号单引号双引号_之外用逗号_Split(char[] chars, int beginIndex, int endIndex)
        {
            List<StrSpan> list = new List<StrSpan>();

            //StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            //if (span.isEmpty)
            //{
            //    list.Add(span);
            //    return list;
            //}


            List<表达式段> list段 = Parser.按_小括号中括号单引号双引号_分段(chars, beginIndex, endIndex);
            list段 = Parser.去掉空白段(chars, list段);


            int p = beginIndex, q;

            表达式段 段;

            for (int i = 0; i < list段.Count; i++)
            {
                段 = list段[i];

                if (段.type != 表达式段_Type.普通段)
                    continue;

                p = 段.iLeft;

                while (true)
                {
                    q = StrUtil.FindForward(chars, p, 段.iRight, ',');

                    if (q == -1)
                        break;

                    list.Add(StrUtil.Trim(chars, p, q - 1, Parser._whiteSpaces));

                    p = q + 1;
                }

            }

            //if (list.Count == 0)
            //{
            //    list.Add(span);
            //    return list;
            //}

            list.Add(StrUtil.Trim(chars, p, endIndex, Parser._whiteSpaces));

            return list;
        }


        public static int 寻找最右边的点或箭头的位置(char[] chars, int beginIndex, int endIndex, out string 点或箭头)
        {
            char c;

            for (int i = endIndex; i>=beginIndex; i--)
            {
                c = chars[i];

                if (c == '.')
                {
                    点或箭头 = ".";
                    return i;
                }
                    

                int i减1 = i - 1;

                if (c == '>' && i减1 >= beginIndex && chars[i减1] == '-')
                {
                    点或箭头 = "->";
                    return i减1;
                }
                    
            }

            点或箭头 = null;
            return -1;
        }
    }
}
