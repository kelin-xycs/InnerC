using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members.表达式s;

namespace InnerC.C_Members.语句s
{
    class 变量声明和初始化语句 : 语句
    {
        public 变量声明和初始化 变量声明;

        public 变量声明和初始化语句(变量声明和初始化 变量声明)
        {
            this.变量声明 = 变量声明;
        }

        public override void Set_作用域(作用域 作用域)
        {
            throw new NotImplementedException();
            //base.Set_作用域(作用域);

            //this.变量声明.Set_作用域(作用域);
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            this.变量声明.还原_C_源代码(sb);

            sb.Append(";\r\n");
        }

        //public override void 类型和语法检查(List<语法错误> list语法错误)
        //{
        //    //  作用域.类型和语法检查() 会对 变量声明和初始化 检查，就不需要这里重复检查了

        //    //this.变量声明.类型和语法检查(list语法错误);
        //}

        public override void 类型和语法检查(List<语法错误> list语法错误)
        {
            this.变量声明.类型和语法检查(list语法错误);
        }
    }
}
