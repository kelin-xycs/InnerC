using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    class 变量 : 表达式
    {
        public string name;

        public 变量(string name)
        {
            this.name = name;
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append(this.name);
        }

        public override void 类型和语法检查()
        {
            throw new NotImplementedException("不需要调用 变量.类型和语法检查() 方法。变量 的 类型和语法检查 就是 检查 变量 是否已声明。使用到 变量 的 表达式 一定会进行相关的 类型检查，此时会调用 变量.Get_变量声明() 方法 来获得 变量声明，若 变量 未声明，则会在 变量.Get_变量声明() 方法 中 抛出异常 。");
        }

        public 变量声明和初始化 Get_变量声明()
        {
            变量声明和初始化 变量声明 = this.作用域.Get_变量声明_查找_当前作用域_上级作用域_全局变量(this.name);

            if (变量声明 == null)
                throw new Exception("找不到名为 \"" + this.name + "\" 的 变量/参数/全局变量 。");

            return 变量声明;
        }
    }
}
