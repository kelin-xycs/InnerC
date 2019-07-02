using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    

    class 赋值 : 表达式
    {
        public 表达式 左边的表达式;
        public 表达式 右边的表达式;

        public 赋值(表达式 左边的表达式, 表达式 右边的表达式, char[] chars, int iLeft) : base(chars, iLeft)
        {
            this.左边的表达式 = 左边的表达式;
            this.右边的表达式 = 右边的表达式;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            this.左边的表达式.Set_作用域(作用域);

            this.右边的表达式.Set_作用域(作用域);
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("(");

            this.左边的表达式.还原_C_源代码(sb);

            sb.Append(" = ");

            this.右边的表达式.还原_C_源代码(sb);

            sb.Append(")");
        }
    }
}
