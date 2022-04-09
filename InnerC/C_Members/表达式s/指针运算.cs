using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    class 指针取值 : 一元表达式
    {
        //表达式 指针表达式;

        //public 指针取值(表达式 指针表达式, char[] chars, int iLeft) : base(chars, iLeft)
        //{
        //    this.指针表达式 = 指针表达式;
        //}

        public 指针取值(表达式 子表达式, char[] chars, int iLeft) : base(子表达式, chars, iLeft)
        {

        }


        //public override void Set_作用域(作用域 作用域)
        //{
        //    base.Set_作用域(作用域);

        //    this.指针表达式.Set_作用域(作用域);
        //}

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("(");

            sb.Append(" * ");

            //this.指针表达式.还原_C_源代码(sb);
            this.子表达式.还原_C_源代码(sb);

            sb.Append(")");
        }
    }

    class 取地址 : 一元表达式
    {
        //表达式 变量;

        //public 取地址(表达式 变量, char[] chars, int iLeft) : base(chars, iLeft)
        //{
        //    this.变量 = 变量;
        //}

        public 取地址(表达式 子表达式, char[] chars, int iLeft) : base(子表达式, chars, iLeft)
        {
            
        }

        //public override void Set_作用域(作用域 作用域)
        //{
        //    base.Set_作用域(作用域);

        //    this.变量.Set_作用域(作用域);
        //}

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("(");

            sb.Append(" & ");

            //this.变量.还原_C_源代码(sb);
            this.子表达式.还原_C_源代码(sb);

            sb.Append(")");
        }
    }
}
