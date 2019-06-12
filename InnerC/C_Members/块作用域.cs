using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members.语句s;
using InnerC.C_Members.表达式s;

namespace InnerC.C_Members
{
    class 块作用域 : 作用域
    {

        public List<语句> list语句 = new List<语句>();


        public override void 还原_C_源代码(StringBuilder sb)
        {
            //foreach (变量声明和初始化 变量声明 in this.dic变量声明.Values)
            //{
            //    变量声明.还原_C_源代码(sb);

            //    sb.Append(";\r\n");
            //}

            语句 语句;

            for (int i=0; i<this.list语句.Count; i++)
            {
                语句 = list语句[i];

                语句.还原_C_源代码(sb);
            }

        }

        public override void 类型和语法检查()
        {
            for(int i=0; i<this.list语句.Count; i++)
            {
                语句 语句 = this.list语句[i];

                语句.类型和语法检查();
            }
        }
    }
}
