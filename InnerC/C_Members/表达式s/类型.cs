﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    class 类型 : 表达式
    {
        public string type;

        //private bool isPtr;
        public int ptrLevel;
        //private bool isArray;
        public List<表达式> list_数组维度_Length;

        public 类型(string type, int ptrLevel, List<表达式> list_数组维度_Length, char[] chars, int iLeft) : base(chars, iLeft)
        {
            this.type = type;

            //if (ptrLevel > 0)
            //{
            //    this.isPtr = true;
            this.ptrLevel = ptrLevel;
            //}

            //if (list_数组维度_Length != null)
            //{
            //    this.isArray = true;
            this.list_数组维度_Length = list_数组维度_Length;
            //}
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            //foreach(表达式 维度_Length in this.list_数组维度_Length)
            //{
            //    维度_Length.Set_作用域(作用域);
            //}
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append(this.type);

            if (this.ptrLevel > 0)
                sb.Append(" ");

            for (int i = 0; i < this.ptrLevel; i++)
            {
                sb.Append("*");
            } 
            
            if (this.list_数组维度_Length == null)
                return;

            sb.Append(" ");

            表达式 维度_Length;

            for (int i = 0; i < this.list_数组维度_Length.Count; i++)
            {
                维度_Length = this.list_数组维度_Length[i];

                sb.Append("[");

                if (维度_Length != null)
                {
                    维度_Length.还原_C_源代码(sb);
                }
                    
                sb.Append("]");
            }
        }

        public override void 类型和语法检查(List<语法错误> list语法错误)
        {

            if (Util.Check_是否关键字(this.type))
                return;

            ParseResult r = this.作用域.Get_全局成员();

            if (r.dic结构体.ContainsKey(this.type))
                return;

            list语法错误.Add(new 语法错误("无效的类型 \"" + this.type + "\"，既不是 基础类型，又不是 结构体 。", this.chars, this.iLeft));
            //throw new Exception("无效的类型 \"" + this.type + "\"，既不是 基础类型，又不是 结构体 。");

        }
    }
}
