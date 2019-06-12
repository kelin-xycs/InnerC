using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC
{
    class 第一层_代码块
    {
        public int iLeft;
        public int iRight;
        public 第一层_代码块_Type type;
        public StrSpan 大括号块;

        public 第一层_代码块(int iLeft, int iRight, 第一层_代码块_Type type, StrSpan 大括号块)
        {
            this.iLeft = iLeft;
            this.iRight = iRight;
            this.type = type;
            this.大括号块 = 大括号块;
        }
    }

    enum 第一层_代码块_Type
    {
        全局变量块,
        结构体或函数块
    }
}
