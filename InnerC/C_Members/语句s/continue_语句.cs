﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members.语句s
{
    class continue_语句 : 语句
    {
        public continue_语句()
        {
            
        }

        public override void 还原_C_源代码(StringBuilder sb)
        {
            sb.Append("continue;\r\n");
        }
    }
}
