using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members.表达式s;

namespace InnerC.C_Members
{
    class 作用域 : I_C_Member
    {

        public Dictionary<string, 变量声明和初始化> dic变量声明 = new Dictionary<string, 变量声明和初始化>();

        private 作用域 父作用域;

        private ParseResult r;

        private 函数 所在函数;

        private 语句 所在的_while_for_语句;


        public 变量声明和初始化 Get_变量声明_查找_当前作用域_上级作用域_全局变量(string name)
        {
            变量声明和初始化 变量声明;

            if (this.dic变量声明.TryGetValue(name, out 变量声明))
            {
                return 变量声明;
            }

            if (this.父作用域 != null)
            {
                return this.父作用域.Get_变量声明_查找_当前作用域_上级作用域_全局变量(name);
            }

            if (this.r != null)
            {
                if (this.r.dic全局变量.TryGetValue(name, out 全局变量 全局变量))
                {
                    return 全局变量;
                }
            }

            return null;
        }

        public 变量声明和初始化 Get_变量声明_查找_上级作用域(string name)
        {
            if (this.父作用域 == null)
                return null;

            变量声明和初始化 变量声明;

            if (this.父作用域.dic变量声明.TryGetValue(name, out 变量声明))
            {
                return 变量声明;
            }

            return Get_变量声明_查找_上级作用域(name);
        }

        public void Set_父作用域(作用域 父作用域)
        {
            this.父作用域 = 父作用域;
        }

        public void Set_全局成员(ParseResult r)
        {
            this.r = r;
        }

        public ParseResult Get_全局成员()
        {
            if (this.r != null)
                return r;

            if (this.父作用域 != null)
                return this.父作用域.Get_全局成员();

            throw new InnerCException("找不到全局成员 。");
        }

        public void Set_所在函数(函数 函数)
        {
            this.所在函数 = 函数;
        }

        public void Set_所在的_while_for_语句(语句 语句)
        {
            this.所在的_while_for_语句 = 语句;
        }

        public virtual void 还原_C_源代码(StringBuilder sb)
        {
            throw new NotImplementedException();
        }

        public virtual void 生成目标代码()
        {
            throw new NotImplementedException();
        }

        public virtual void 类型和语法检查(List<语法错误> list语法错误)
        {
            foreach(变量声明和初始化 变量声明 in this.dic变量声明.Values)
            {
                变量声明.类型和语法检查(list语法错误);
            }
        }
    }
}
