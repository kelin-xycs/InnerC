using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.语句s
{
    class if_语句 : 语句
    {

        private List<if_分句> if_分句_和_else_if_分句_List;
        private 块作用域 最后结尾的_else_分句;

        public if_语句(List<if_分句> if_分句_和_else_if_分句_List, 块作用域 最后结尾的_else_分句)
        {
            this.if_分句_和_else_if_分句_List = if_分句_和_else_if_分句_List;
            this.最后结尾的_else_分句 = 最后结尾的_else_分句;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            if_分句 if_分句;

            for (int i = 0; i < this.if_分句_和_else_if_分句_List.Count; i++)
            {
                if_分句 = this.if_分句_和_else_if_分句_List[i];

                if_分句.Set_作用域(作用域);
            }

            if (this.最后结尾的_else_分句 != null)
            {
                this.最后结尾的_else_分句.Set_父作用域(作用域);
            }
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            if_分句 if_分句;

            for (int i = 0; i<this.if_分句_和_else_if_分句_List.Count; i++)
            {
                if_分句 = this.if_分句_和_else_if_分句_List[i];

                if_分句.还原_C_源代码(sb);

                sb.Append("else ");
            }

            if (this.最后结尾的_else_分句 == null)
            {
                sb.Remove(sb.Length - 5, 5);

                return;
            }
            
            sb.Append("\r\n{\r\n");

            this.最后结尾的_else_分句.还原_C_源代码(sb);

            sb.Append("}\r\n");
            
        }

    }

    class if_分句 : 语句
    {
        private 表达式 if_判断;
        private 块作用域 子句;

        public if_分句(表达式 if_判断, 块作用域 子句)
        {
            this.if_判断 = if_判断;
            this.子句 = 子句;
        }

        public override void Set_作用域(作用域 作用域)
        {
            base.Set_作用域(作用域);

            this.if_判断.Set_作用域(作用域);

            this.子句.Set_父作用域(作用域);
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("if (");

            this.if_判断.还原_C_源代码(sb);

            sb.Append(")\r\n");

            sb.Append("{\r\n");

            this.子句.还原_C_源代码(sb);

            sb.Append("}\r\n");
        }
    }
}
