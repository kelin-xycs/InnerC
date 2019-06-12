using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.表达式s
{
    class 常量 : 表达式
    {
        public 常量_Type type;
        public string value;

        public 常量(常量_Type type, string value, int 参考位置_iLeft)
        {
            this.type = type;
            this.value = value;
            this.参考位置_iLeft = 参考位置_iLeft;
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append(this.value);
        }
    }

    enum 常量_Type
    {
        _char,
        _String,
        _int,
        _float,
        中括号数组常量
    }
}
