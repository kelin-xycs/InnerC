using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members
{
    abstract class 语句 : I_C_Member
    {
        protected 作用域 作用域;

        public void 生成目标代码()
        {
            throw new NotImplementedException();
        }

        public abstract void 还原_C_源代码(StringBuilder sb);

        public virtual void Set_作用域(作用域 作用域)
        {
            this.作用域 = 作用域;
        }

        public virtual void 类型和语法检查(List<语法错误> list语法错误)
        {
            throw new NotImplementedException();
        }
    }
}
