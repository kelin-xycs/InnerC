using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.语句s
{
    class 表达式语句 : 语句
    {
        public 表达式 表达式;

        public 表达式语句(表达式 表达式)
        {
            this.表达式 = 表达式;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            this.表达式.Set_作用域(作用域);
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            this.表达式.还原_C_源代码(sb);

            sb.Append(";\r\n");
        }

        public override void 类型和语法检查(List<语法错误> list语法错误)
        {
            this.表达式.类型和语法检查(list语法错误);
        }
    }
}
