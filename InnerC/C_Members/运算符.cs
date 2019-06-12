using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InnerC.C_Members
{
    class 运算符
    {
        public string op;
        //public bool isCast;
        //public bool is_指针取值;
        //public bool is_取地址;
        public 运算符_Type type;
        public int 优先级;
        public string castType;
        public int iLeft;
        public int iRight;

        public 运算符(string op, int 优先级, int iLeft, int iRight)
        {
            this.op = op;
            this.优先级 = 优先级;
            this.iLeft = iLeft;
            this.iRight = iRight;

            this.type = 运算符_Type.普通;

        }

        public 运算符(string op, 运算符_Type type, int 优先级, string castType, int iLeft, int iRight)
        {
            this.op = op;
            this.type = type;
            //this.isCast = isCast;
            //this.is_指针取值 = is_指针取值;
            //this.is_取地址 = is_取地址;
            this.优先级 = 优先级;
            this.castType = castType;
            this.iLeft = iLeft;
            this.iRight = iRight;
        }
    }

    enum 运算符_Type
    {
        普通,
        指针取值,
        取地址,
        Cast
    }
}
