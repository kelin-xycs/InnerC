using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    abstract class 二元表达式 : 表达式
    {

        protected 表达式 左边的表达式;
        protected 表达式 右边的表达式;

        protected 二元表达式(表达式 左边的表达式, 表达式 右边的表达式, char[] chars, int iLeft) : base(chars, iLeft)
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

        public override void 类型和语法检查(List<语法错误> list语法错误)
        {
            this.左边的表达式.类型和语法检查(list语法错误);

            this.右边的表达式.类型和语法检查(list语法错误);
        }

        public override abstract void 还原_C_源代码(StringBuilder sb);
    }
}
