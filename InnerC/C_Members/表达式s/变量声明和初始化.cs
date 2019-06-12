using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    class 变量声明和初始化 : 表达式
    {

        private 类型 类型;
        public string name;
        private 表达式 初始值;

        public int 变量位置_iLeft;

        public 变量声明和初始化(类型 类型, string name, 表达式 初始值, int 变量位置_iLeft)
        {

            this.类型 = 类型;
            this.name = name;
            this.初始值 = 初始值;

            this.变量位置_iLeft = 变量位置_iLeft;
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append(this.类型.type);

            sb.Append(" ");

            for (int i=0; i<this.类型.ptrLevel; i++)
            {
                sb.Append("*");
            }

            if (this.类型.ptrLevel > 0)
                sb.Append(" ");

            sb.Append(this.name);

            if (this.类型.list_数组维度_Length != null)
            {
                表达式 维度_Length;

                for (int i = 0; i < this.类型.list_数组维度_Length.Count; i++)
                {
                    维度_Length = this.类型.list_数组维度_Length[i];

                    sb.Append("[");

                    if (维度_Length != null)
                    {
                        维度_Length.还原_C_源代码(sb);
                    }

                    sb.Append("]");
                }
            }

            if (初始值 == null)
                return;

            sb.Append(" = ");

            this.初始值.还原_C_源代码(sb);
        }

        public override void 类型和语法检查()
        {

            this.类型.类型和语法检查();

            if (this.初始值 != null)
                this.初始值.类型和语法检查();
            
        }

        public 全局变量 To_全局变量()
        {
            return new 全局变量(this.类型, this.name, this.初始值, this.变量位置_iLeft);
        }

        public 字段声明 To_字段声明()
        {
            return new 字段声明(this.类型, this.name, this.初始值, this.变量位置_iLeft);
        }

        public 形参 To_形参()
        {
            return new 形参(this.类型, this.name, this.初始值, this.变量位置_iLeft);
        }
    }

    class 全局变量 : 变量声明和初始化
    {
        public 全局变量(类型 类型, string name, 表达式 初始值, int 变量位置_iLeft)
            : base(类型, name, 初始值, 变量位置_iLeft)
        {

        }

        public override void 类型和语法检查()
        {
            //  检查 this.类型 是否有效
            //  检查 this.初始值 是否是 常量 或 常量表达式
        }
    }

    class 字段声明 : 变量声明和初始化
    {
        public 字段声明(类型 类型, string name, 表达式 初始值, int 变量位置_iLeft)
            : base(类型, name, 初始值, 变量位置_iLeft)
        {

        }

        public override void 类型和语法检查()
        {
            //  检查 this.类型 是否有效
            //  检查 this.初始值 是否是 常量 或 常量表达式
        }
    }

    class 形参 : 变量声明和初始化
    {
        public 形参(类型 类型, string name, 表达式 初始值, int 变量位置_iLeft)
            : base(类型, name, 初始值, 变量位置_iLeft)
        {

        }

        public override void 类型和语法检查()
        {
            //  检查 this.类型 是否有效
        }
    }
}
