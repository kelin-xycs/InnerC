﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    class Cast : 一元表达式
    {
        private string castType;
        //private 表达式 右边的表达式;

        //public Cast(string castType, 表达式 右边的表达式, char[] chars, int iLeft) : base(chars, iLeft)
        //{
        //    this.castType = castType;
        //    this.右边的表达式 = 右边的表达式;
        //}

        public Cast(string castType, 表达式 子表达式, char[] chars, int iLeft) : base(子表达式, chars, iLeft)
        {
            this.castType = castType;
        }

        //public override void Set_作用域(作用域 作用域)
        //{
        //    base.Set_作用域(作用域);

        //    this.右边的表达式.Set_作用域(作用域);
        //}

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("(");

            sb.Append("(");

            sb.Append(this.castType);

            sb.Append(")");

            //this.右边的表达式.还原_C_源代码(sb);
            this.子表达式.还原_C_源代码(sb);

            sb.Append(")");
        }
    }
}
