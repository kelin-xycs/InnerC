using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members.表达式s;

namespace InnerC.C_Members
{
    class 函数 : I_C_Member
    {

        private 类型 返回类型;

        public string name;

        private 作用域 形参列表;

        private 块作用域 函数体;

        public int 函数名_iLeft;

        private ParseResult r;


        public 函数(类型 返回类型, string name, 作用域 形参列表, 块作用域 函数体, int 函数名_iLeft)
        {
            this.返回类型 = 返回类型;
            this.name = name;
            this.形参列表 = 形参列表;
            this.函数体 = 函数体;
            this.函数名_iLeft = 函数名_iLeft;

            函数体.Set_父作用域(形参列表);
            形参列表.Set_所在函数(this);
        }

        public void Set_全局成员(ParseResult r)
        {
            this.形参列表.Set_全局成员(r);
        }

        public void 还原_C_源代码(StringBuilder sb)
        {
            this.返回类型.还原_C_源代码(sb);

            sb.Append(" ");
            sb.Append(this.name);
            sb.Append("(");

            foreach (变量声明和初始化 形参 in this.形参列表.dic变量声明.Values)
            {
                形参.还原_C_源代码(sb);

                sb.Append(", ");
            }

            if (this.形参列表.dic变量声明.Values.Count > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            sb.Append(")\r\n");
            sb.Append("{\r\n");

            this.函数体.还原_C_源代码(sb);

            sb.Append("}\r\n\r\n");

        }

        public void 生成目标代码()
        {
            throw new NotImplementedException();
        }

        public void 类型和语法检查(List<语法错误> list语法错误)
        {
            this.返回类型.类型和语法检查(list语法错误);
            this.形参列表.类型和语法检查(list语法错误);
            this.函数体.类型和语法检查(list语法错误);
        }

        //public void 命名检查()
        //{
        //    if (!Util.Check_是否_下划线字母数字_且以_下划线字母_开头(this.name))
        //        throw new Exception("无效的 函数名 \"" + this.name + "\"，函数名 应由 下划线字母数字 组成且以 下划线或字母 开头 。");

        //    if (Util.Check_是否关键字(this.name))
        //        throw new Exception("无效的 函数名 \"" + this.name + "\"，函数名 不能和 关键字 相同 。");

        //}
    }
}
