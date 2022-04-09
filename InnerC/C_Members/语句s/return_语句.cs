using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.语句s
{
    class return_语句 : 语句
    {
        private 表达式 返回值;

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("return ");

            if (this.返回值 != null)
                返回值.还原_C_源代码(sb);

            sb.Append(";\r\n");
        }

        public return_语句(表达式 返回值)
        {
            this.返回值 = 返回值;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            if (this.返回值 != null)
                this.返回值.Set_作用域(作用域);
        }

        public override void 类型和语法检查(List<语法错误> list语法错误)
        {
            if (this.返回值 != null)
                this.返回值.类型和语法检查(list语法错误);
        }
    }
}
