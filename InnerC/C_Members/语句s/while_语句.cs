using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.语句s
{
    class while_语句 : 语句
    {
        private 表达式 while_条件;
        private 块作用域 子句;

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("while ( ");

            this.while_条件.还原_C_源代码(sb);

            sb.Append(" )\r\n{\r\n");

            this.子句.还原_C_源代码(sb);

            sb.Append("}\r\n");
        }

        public while_语句(表达式 while_条件, 块作用域 子句)
        {
            this.while_条件 = while_条件;

            this.子句 = 子句;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            this.while_条件.Set_作用域(作用域);

            this.子句.Set_父作用域(作用域);

            作用域.Set_所在的_while_for_语句(this);
        }
    }
}
