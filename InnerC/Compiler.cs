using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace InnerC
{
    public class Compiler
    {
        private string outputPath;
        public string OutputPath
        {
            get { return outputPath; }
            set { outputPath = value; }
        }

        public void Compile(string file)
        {
            byte[] bytes;

            using (Stream stream = File.Open(file, FileMode.Open))
            {
                bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);

            }

            char[] chars = Encoding.UTF8.GetChars(bytes);




            ParseResult r = new ParseResult();


            第一层_Parser.Parse(r, chars);


            List<语法错误> list语法错误 = new List<语法错误>();

            r.类型和语法检查(list语法错误);

            if (list语法错误.Count > 0)
            {
                StringBuilder sb = new StringBuilder("\r\n");

                foreach(语法错误 error in list语法错误)
                {
                    sb.Append("\r\n");
                    sb.Append(error.ToString());
                }

                throw new Exception(sb.ToString());
            }

            //  因为还没有全部完成，所以先注释掉，不然会报错 。


            //  r.类型和语法检查(list语法错误);


            /* **  类型和语法检查” 包含了以下内容 ： 

                检查上级作用域中是否已定义了同名的变量
                变量 参数 返回值 字段 的 类型 是否正确，比如 是否是 int float 等基础类型或结构体
                是否使用了 未定义 的 变量 参数 字段
                变量不能在声明前使用
                运算符两边的表达式的类型是否匹配
                Cast 是否合法
                函数返回值的类型和声明的返回类型是否一致
                是否使用了 未定义 的 函数 和 结构体

                数组声明 的 维度长度 只能是 常量 或者 常量表达式，如果用 常量 初始化数组，可以不用声明维度长度，但这好像只适用于 一维数组
                全局变量 初始化 只能用 常量 或者 常量表达式

                变量名 不能和 函数名 重名，对于 编译器 来说，重名也可以，加入 A 又是 函数名，又是变量名，则会识别为 变量，
                但是这样会导致 函数名调用 和 函数指针调用 容易混淆，
                那么，变量名 能不能 和 结构体 重名 ？
                在 C# 中，变量名 可以和 类名 重名，也可以和 方法名 重名，
                问题是 C# 中对函数指针采用了 委托 包装，好像更清楚一点？又或者不见得？
                把 A 方法 包装为一个 变量名 为 A 的 委托，调用 A ，这样岂不是也很容易混淆？
                如果 变量 A 的 委托 包装 的 方法 不是 A 方法，那更糟。
                但问题是 C# 中好像不太会出现这种状况，而 C 就很容易出现这种状况，真奇怪。
            

                因为 大部分 的 语法检查 都和 类型 有关，所以归到一起称为 “类型和语法检查”

            ** */



            // 检查函数内所有路径都有返回值(r);

            // 检查结构体不能循环包含(r);



            //  r.生成目标代码();


            r.还原_C_源代码(file + ".reverse.c");

        }

    }
}
