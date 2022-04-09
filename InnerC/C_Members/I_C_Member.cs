using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InnerC.C_Members.表达式s;

namespace InnerC.C_Members
{
    interface I_C_Member
    {
        void Set_作用域(作用域 作用域);

        void 类型和语法检查(List<语法错误> list语法错误);

        void 还原_C_源代码(StringBuilder sb);

        void 生成目标代码();
    }
}
