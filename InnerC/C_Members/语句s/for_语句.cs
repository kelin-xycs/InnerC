using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.语句s
{
    class for_语句 : 语句
    {
        private 表达式[] 小括号表达式s;
        private 块作用域 子句;

        public for_语句(表达式[] 小括号表达式s, 块作用域 子句)
        {
            this.小括号表达式s = 小括号表达式s;

            this.子句 = 子句;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            作用域 小括号作用域 = new 作用域();

            for (int i = 0; i < this.小括号表达式s.Length; i++)
            {
                表达式 express = this.小括号表达式s[i];

                if (express != null)
                    express.Set_作用域(小括号作用域);

            }

            小括号作用域.Set_父作用域(作用域);
            this.子句.Set_父作用域(小括号作用域);

        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("for ( ");

            for (int i = 0; i < this.小括号表达式s.Length; i++)
            {
                表达式 express = this.小括号表达式s[i];

                if (express != null)
                    express.还原_C_源代码(sb);

                if (i < 2)
                    sb.Append(";");
            }
            //this.for_表达式.还原_C_源代码(sb);

            sb.Append(" )\r\n{\r\n");

            this.子句.还原_C_源代码(sb);

            sb.Append("}\r\n");
        }
    }
}
