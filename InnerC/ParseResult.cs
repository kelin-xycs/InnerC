using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using InnerC.C_Members;
using InnerC.C_Members.语句s;
using InnerC.C_Members.表达式s;

namespace InnerC
{
    class ParseResult
    {
        public Dictionary<string, 全局变量> dic全局变量 = new Dictionary<string, 全局变量>();

        public Dictionary<string, 结构体> dic结构体 = new Dictionary<string, 结构体>();

        public Dictionary<string, 函数> dic函数 = new Dictionary<string, 函数>();

        public void 命名检查()
        {
            foreach(结构体 结构体 in this.dic结构体.Values)
            {
                结构体.命名检查();
            }

            foreach(函数 函数 in this.dic函数.Values)
            {
                函数.命名检查();
            }
        }

        public void 类型和语法检查()
        {
            foreach (全局变量 全局变量 in this.dic全局变量.Values)
            {
                全局变量.类型和语法检查(); ;
            }

            foreach (结构体 结构体 in this.dic结构体.Values)
            {
                结构体.类型和语法检查();
            }

            foreach (函数 函数 in this.dic函数.Values)
            {
                函数.类型和语法检查();
            }
        }

        public void 生成目标代码()
        {
            foreach (全局变量 全局变量 in this.dic全局变量.Values)
            {
                全局变量.生成目标代码(); ;
            }

            foreach (结构体 结构体 in this.dic结构体.Values)
            {
                结构体.生成目标代码();
            }

            foreach (函数 函数 in this.dic函数.Values)
            {
                函数.生成目标代码();
            }
        }

        public void 还原_C_源代码(string 还原后的文件)
        {
            StringBuilder sb = new StringBuilder();


            foreach (全局变量 全局变量 in this.dic全局变量.Values)
            {
                全局变量.还原_C_源代码(sb);

                sb.Append(";\r\n");
            }

            sb.Append("\r\n");

            foreach (结构体 结构体 in this.dic结构体.Values)
            {
                结构体.还原_C_源代码(sb);
            }

            foreach (函数 函数 in this.dic函数.Values)
            {
                函数.还原_C_源代码(sb);
            }


            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());

            using (Stream stream = File.Create(还原后的文件))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

        }
    }
}
