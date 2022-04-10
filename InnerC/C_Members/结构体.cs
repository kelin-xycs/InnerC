using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members.表达式s;

namespace InnerC.C_Members
{
    class 结构体 : I_C_Member
    {
        public string name;
        //private Dictionary<string, 字段声明> dic字段声明;
        public int 结构体名_iLeft;

        private 作用域 字段声明;

        private ParseResult r;

        //public 结构体(string name, Dictionary<string, 字段声明> dic字段声明, int 结构体名_iLeft)
        //{
        //    this.name = name;
        //    this.dic字段声明 = dic字段声明;
        //    this.结构体名_iLeft = 结构体名_iLeft;
        //}

        public 结构体(string name, 作用域 字段声明, int 结构体名_iLeft)
        {
            this.name = name;
            this.字段声明 = 字段声明;
            this.结构体名_iLeft = 结构体名_iLeft;
        }

        public void Set_作用域(作用域 全局变量)
        {
            this.字段声明.Set_作用域(全局变量);
        }

        //public void Set_全局成员(ParseResult r)
        //{
        //    this.r = r;
        //}

        public void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("struct ");

            sb.Append(this.name);
            sb.Append("\r\n");

            sb.Append("{\r\n");

            foreach (变量声明和初始化 字段 in this.字段声明.dic变量声明.Values)
            {
                字段.还原_C_源代码(sb);

                sb.Append(",\r\n");
            }

            if (this.字段声明.dic变量声明.Values.Count > 0)
            {
                sb.Remove(sb.Length - 3, 3);
            }

            sb.Append("\r\n");
            sb.Append("}\r\n\r\n");
        }

        public void 生成目标代码()
        {
            throw new NotImplementedException();
        }

        public void 类型和语法检查(List<语法错误> list语法错误)
        {
            this.字段声明.类型和语法检查(list语法错误);
            //foreach (变量声明和初始化 字段声明 in this.字段声明.dic变量声明.Values)
            ////foreach(字段声明 字段声明 in this.字段声明.dic变量声明.Values)
            //{
            //    字段声明.类型和语法检查(list语法错误);
            //}
        }

        //public void 命名检查()
        //{
        //    if (!Util.Check_是否_下划线字母数字_且以_下划线字母_开头(this.name))
        //        throw new Exception("无效的 结构体名 \"" + this.name + "\"，结构体名 应由 下划线字母数字 组成且以 下划线或字母 开头 。");

        //    if (Util.Check_是否关键字(this.name))
        //        throw new Exception("无效的 结构体名 \"" + this.name + "\"，结构体名 不能和 关键字 相同 。");

        //}
    }
}
