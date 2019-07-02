using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members;
using InnerC.C_Members.表达式s;

namespace InnerC
{
    class 表达式_Parser
    {
        private readonly static char[] _左小括号左中括号单引号双引号 = { '(', '[', '\'', '"' };


        public static 表达式 Parse(char[] chars, int beginIndex, int endIndex)
        {
            StrSpan span = StrUtil.Trim(chars, beginIndex, endIndex, Parser._whiteSpaces);

            if (span.isEmpty)
                throw new 语法错误_Exception("无效的表达式 。 缺少内容 。", chars, beginIndex);


            //List<表达式段> list = 按_小括号中括号双引号单引号_分段(chars, span.iLeft, span.iRight);

            运算符 op = Get_优先级最小的左右结合运算符和右结合运算符(chars, span.iLeft, span.iRight);

            if (op == null)
            {
                return 常量_变量_字段_指针字段_数组元素_函数调用_函数指针调用_调用链_Parser.Parse(chars, span.iLeft, span.iRight);
            }

            表达式 op_左边的表达式 = null;
            表达式 op_右边的表达式;

            if (op.op == "!" || op.type == 运算符_Type.Cast || op.type == 运算符_Type.指针取值 || op.type == 运算符_Type.取地址)
            {
                StrSpan 左边的内容 = StrUtil.Trim(chars, span.iLeft, op.iLeft - 1, Parser._whiteSpaces);

                if (!左边的内容.isEmpty)
                    throw new 语法错误_Exception("无效的内容，缺少运算符 。", chars, 左边的内容.iRight);

                op_右边的表达式 = Parse(chars, op.iRight + 1, endIndex);
            }
            else
            {
                op_左边的表达式 = Parse(chars, span.iLeft, op.iLeft - 1);
                op_右边的表达式 = Parse(chars, op.iRight + 1, endIndex);
            }


            if (op.type == 运算符_Type.Cast)
            {
                return new Cast(op.castType, op_右边的表达式, chars, op.iLeft);
            }
            if (op.type == 运算符_Type.指针取值)
            {
                return new 指针取值(op_右边的表达式, chars, op.iLeft);
            }
            if (op.type == 运算符_Type.取地址)
            {
                return new 取地址(op_右边的表达式, chars, op.iLeft);
            }

            if (op.op == "+")
            {
                return new 加(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "-")
            {
                return new 减(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "*")
            {
                return new 乘(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "/")
            {
                return new 除(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "==")
            {
                return new 等于(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == ">")
            {
                return new 大于(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "<")
            {
                return new 小于(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == ">=")
            {
                return new 大于等于(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "<=")
            {
                return new 小于等于(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "!=")
            {
                return new 不等于(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "&&")
            {
                return new 与(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "||")
            {
                return new 或(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "!")
            {
                return new 非(op_右边的表达式, chars, op.iLeft);
            }
            else if (op.op == "=")
            {
                return new 赋值(op_左边的表达式, op_右边的表达式, chars, op.iLeft);
            }

            throw new 语法错误_Exception("无效的运算符 \"" + op.op + "\" 。", chars, op.iLeft);
        }

        private static 运算符 Get_优先级最小的左右结合运算符和右结合运算符(char[] chars, int beginIndex, int endIndex)
        {
            List<表达式段> list = Parser.按_小括号中括号单引号双引号_分段(chars, beginIndex, endIndex);
            list = 继续把普通段分为运算符段和普通段(chars, list);
            list = Parser.去掉空白段(chars, list);

            表达式段 表达式段;
            运算符 op1 = null, op2;

            for (int i = 0; i < list.Count; i++)
            {
                表达式段 = list[i];

                if (表达式段.type == 表达式段_Type.小括号段)
                {
                    op2 = Check_IsCast(chars, 表达式段);
                }
                else if (表达式段.type == 表达式段_Type.运算符段)
                {
                    op2 = Get_运算符(chars, list, i);
                }
                else
                {
                    op2 = null;
                }


                if (op2 == null)
                    continue;

                if (op1 == null || op2.优先级 < op1.优先级)
                {
                    op1 = op2;
                }

            }

            return op1;
        }

        private static 运算符 Check_IsCast(char[] chars, 表达式段 段)
        {
            StrSpan span = StrUtil.Trim(chars, 段.iLeft + 1, 段.iRight - 1, Parser._whiteSpaces);

            if (span.isEmpty)
                return null;

            if (!Util.Check_是否_下划线字母数字_且以_下划线字母_开头(chars, span.iLeft, span.iRight))
                return null;

            string op = new string(chars, 段.iLeft, 段.iRight - 段.iLeft + 1);
            string castType = new string(chars, span.iLeft, span.iRight - span.iLeft + 1);

            return new 运算符(op, 运算符_Type.Cast, 6, castType, 段.iLeft, 段.iRight);

        }


        private static 运算符 Get_运算符(char[] chars, List<表达式段> list, int i)
        {
            表达式段 段 = list[i];

            string op = new string(chars, 段.iLeft, 段.iRight - 段.iLeft + 1);

            if (op == "*")
            {
                if (i == 0 || list[i - 1].type == 表达式段_Type.运算符段)
                    return new 运算符(op, 运算符_Type.指针取值, 6, null, 段.iLeft, 段.iRight);
            }

            if (op == "&")
            {
                if (i == 0 || list[i - 1].type == 表达式段_Type.运算符段)
                    return new 运算符(op, 运算符_Type.取地址, 6, null, 段.iLeft, 段.iRight);
            }

            if (op == "==" || op == ">=" || op == "<=" || op == "!=" || op == ">" || op == "<")
            {
                return new 运算符(op, 3, 段.iLeft, 段.iRight);
            }
            if (op == "&&")
            {
                return new 运算符(op, 2, 段.iLeft, 段.iRight);
            }
            if (op == "||")
            {
                return new 运算符(op, 1, 段.iLeft, 段.iRight);
            }
            if (op == "!")
            {
                return new 运算符(op, 6, 段.iLeft, 段.iRight);
            }
            if (op == "*" || op == "/")
            {
                return new 运算符(op, 5, 段.iLeft, 段.iRight);
            }
            if (op == "+" || op == "-")
            {
                return new 运算符(op, 4, 段.iLeft, 段.iRight);
            }
            
            
            
            if (op == "=")
            {
                return new 运算符(op, 0, 段.iLeft, 段.iRight);
            }
            
            throw new 语法错误_Exception("未知的运算符 ： \"" + new string(chars, 段.iLeft, 段.iRight - 段.iLeft + 1) + "\" 。", chars, 段.iLeft);
        }

        

        private static List<表达式段> 继续把普通段分为运算符段和普通段(char[] chars, List<表达式段> list)
        {
            List<表达式段> listNew = new List<表达式段>();

            表达式段 表达式段;

            for (int i = 0; i < list.Count; i++)
            {
                表达式段 = list[i];

                

                if (表达式段.type != 表达式段_Type.普通段)
                {
                    listNew.Add(表达式段);
                    continue;
                }
                    

                Parser.把普通段分为运算符段和普通段(chars, 表达式段, listNew);
            }

            return listNew;
        }

        

        
    }
}
