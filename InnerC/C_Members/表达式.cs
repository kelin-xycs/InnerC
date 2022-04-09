using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members
{
    abstract class 表达式 : I_C_Member
    {
        protected 作用域 作用域;

        public char[] chars;
        public int iLeft;

        protected 表达式(char[] chars, int iLeft)
        {
            this.chars = chars;
            this.iLeft = iLeft;
        }

        public virtual void 生成目标代码()
        {
            throw new NotImplementedException();
        }

        public abstract void 还原_C_源代码(StringBuilder sb);

        public virtual void Set_作用域(作用域 作用域)
        {
            this.作用域 = 作用域;
        }

        public abstract void 类型和语法检查(List<语法错误> list语法错误);
        
    }
}
