using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    class 字段 : 表达式
    {
        private 表达式 左边的表达式;
        private string 字段名;

        public 字段(表达式 左边的表达式, string 字段名)
        {
            this.左边的表达式 = 左边的表达式;
            this.字段名 = 字段名;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            this.左边的表达式.Set_作用域(作用域);
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {

            this.左边的表达式.还原_C_源代码(sb);

            sb.Append(".");

            sb.Append(this.字段名);

        }
    }

    class 指针字段 : 表达式
    {
        private 表达式 左边的表达式;
        private string 字段名;

        public 指针字段(表达式 左边的表达式, string 字段名)
        {
            this.左边的表达式 = 左边的表达式;
            this.字段名 = 字段名;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            this.左边的表达式.Set_作用域(作用域);
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {

            this.左边的表达式.还原_C_源代码(sb);

            sb.Append("->");

            sb.Append(this.字段名);

        }
    }

    class 数组元素 : 表达式
    {
        public 表达式 左边的表达式;
        public List<表达式> list下标;

        public 数组元素(表达式 左边的表达式, List<表达式> list下标)
        {
            this.左边的表达式 = 左边的表达式;
            this.list下标 = list下标;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            this.左边的表达式.Set_作用域(作用域);
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {

            this.左边的表达式.还原_C_源代码(sb);

            表达式 下标;

            for (int i=0; i<list下标.Count; i++)
            {
                sb.Append("[");

                下标 = list下标[i];

                if (下标 != null)
                    下标.还原_C_源代码(sb);

                sb.Append("]");
            }

        }
    }

    class 函数调用 : 表达式
    {
        private string 函数名;
        private List<表达式> list实参;

        public 函数调用(string 函数名, List<表达式> list实参)
        {
            this.函数名 = 函数名;
            this.list实参 = list实参;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            表达式 实参;

            for (int i=0; i<list实参.Count; i++)
            {
                实参 = list实参[i];

                实参.Set_作用域(作用域);
            }
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append(this.函数名);

            sb.Append("(");

            表达式 实参;

            for (int i=0; i<this.list实参.Count; i++)
            {
                实参 = list实参[i];

                实参.还原_C_源代码(sb);

                sb.Append(", ");
            }

            if (this.list实参.Count > 0)
                sb.Remove(sb.Length - 2, 2);

            sb.Append(")");
        }
    }

    class 函数指针调用 : 表达式
    {
        private 指针取值 指针取值;
        private List<表达式> list实参;

        //public 函数指针调用(指针取值 指针取值, List<表达式> list实参)
        //{
        //    this.指针取值 = 指针取值;
        //    this.list实参 = list实参;
        //}

        public 函数指针调用(指针取值 指针取值, List<表达式> list实参)
        {
            this.指针取值 = 指针取值;
            this.list实参 = list实参;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            表达式 实参;

            for (int i = 0; i < list实参.Count; i++)
            {
                实参 = list实参[i];

                实参.Set_作用域(作用域);
            }
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            this.指针取值.还原_C_源代码(sb);

            sb.Append("(");

            表达式 实参;

            for (int i = 0; i < this.list实参.Count; i++)
            {
                实参 = list实参[i];

                实参.还原_C_源代码(sb);

                sb.Append(", ");
            }

            if (this.list实参.Count > 0)
                sb.Remove(sb.Length - 2, 2);

            sb.Append(")");
        }
    }
}
