using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC
{
    class 表达式段
    {
        public int iLeft;
        public int iRight;
        public 表达式段_Type type;

        public 表达式段(int iLeft, int iRight, 表达式段_Type type)
        {
            this.iLeft = iLeft;
            this.iRight = iRight;
            this.type = type; 
        }
    }

    enum 表达式段_Type
    {
        普通段,
        单引号段,
        双引号段,
        小括号段,
        中括号段,
        运算符段
    }
}
