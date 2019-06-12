using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnerC
{
    class StrUtil
    {
        public static int FindForward(char[] chars, int beginIndex, int endIndex, char c)
        {
            for (int i = beginIndex; i <= endIndex; i++)
            {
                if (chars[i] == c)
                    return i;
            }

            return -1;
        }

        public static int FindBackward(char[] chars, int beginIndex, int endIndex, char c)
        {
            for (int i = endIndex; i >= beginIndex; i--)
            {
                if (chars[i] == c)
                    return i;
            }

            return -1;
        }

        public static int FindForward(char[] str, int beginIndex, int endIndex, string value)
        {
            char[] v = value.ToCharArray();

            //for (int i = beginIndex; i < (str.Length - (v.Length - 1)); i++)
            for (int i = beginIndex; i <= (endIndex - v.Length + 1); i++)
            {
                for (int j = 0; j < v.Length; j++)
                {
                    if (str[i + j] == v[j])
                    {
                        if (j == v.Length - 1)
                        {
                            return i;
                            //index = i;
                            //break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                //if (index != -1)
                //    break;
            }

            return -1;

        }
        //public static int FindForward(char[] str, int beginIndex, string value)
        //{
        //    return FindForward(str, beginIndex, str.Length - 1, value);
        //}

        //public static int FindBackward(char[] str, int beginIndex, string value)
        //{
        //    return FindBackward(str, beginIndex, 0, value);
        //}

        public static int FindBackward(char[] str, int beginIndex, int endIndex, string value)
        {
            //int index;
            char[] v = value.ToCharArray();

            //int minEndIndex = v.Length - 1;

            //endIndex = endIndex >= minEndIndex ? endIndex : minEndIndex;

            for (int i = endIndex - v.Length + 1; i >= beginIndex; i--)
            {
                //index = i - (v.Length - 1);

                for (int j = 0; j < v.Length; j++)
                {
          
                    //if (str[index + j] == v[j])
                    if (str[i + j] == v[j])
                    {
                        if (j == v.Length - 1)
                        {
                            return i;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }

            return -1;


        }

        //public static int FindForward(char[] str, int beginIndex, char[] charList)
        //{
        //    return FindForward(str, beginIndex, str.Length - 1, charList);
        //}
        public static int FindForward(char[] str, int beginIndex, int endIndex, char[] charList)
        {
            for (int i = beginIndex; i <= endIndex; i++)
            {
                for (int j = 0; j < charList.Length; j++)
                {
                    if (str[i] == charList[j])
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        //public static int FindBackward(char[] str, int beginIndex, char[] charList)
        //{
        //    return FindBackward(str, beginIndex, 0, charList);
        //}
        public static int FindBackward(char[] str, int beginIndex, int endIndex, char[] charList)
        {
            for (int i = endIndex; i >= beginIndex; i--)
            {
                for (int j = 0; j < charList.Length; j++)
                {
                    if (str[i] == charList[j])
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        //public static int FindForwardUtilNot(char[] str, int beginIndex, string value)
        //{
        //    return FindForwardUtilNot(str, beginIndex, value.ToCharArray());
        //}

        //public static int FindForwardUntilNot(char[] str, int beginIndex, char[] charList)
        //{
        //    return FindForwardUntilNot(str, beginIndex, str.Length - 1, charList);
        //}
        public static int FindForwardUntilNot(char[] str, int beginIndex, int endIndex,   char[] charList)
        {

            for (int i = beginIndex; i <= endIndex; i++)
            {
                for (int j = 0; j < charList.Length; j++)
                {
                    if (str[i] == charList[j])
                        break;

                    if (j == charList.Length - 1)
                        return i;
                }


            }

            return -1;
        }

        //public static int FindBackwardUntilNot(char[] str, int beginIndex, char[] charList)
        //{
        //    return FindBackwardUntilNot(str, beginIndex, 0, charList);
        //}
        public static int FindBackwardUntilNot(char[] str, int beginIndex, int endIndex, char[] charList)
        {

            for (int i = endIndex; i >= beginIndex; i--)
            {

                for (int j = 0; j < charList.Length; j++)
                {
                    if (str[i] == charList[j])
                        break;

                    if (j == charList.Length - 1)
                        return i;
                }

            }

            return -1;
        }

        public static bool IsOneOf(char c, char[] charList)
        {
            for (int i = 0; i < charList.Length; i++)
            {
                if (c == charList[i])
                    return true;
            }

            return false;
        }

        public static List<StrSpan> Split(char[] str, int beginIndex, int endIndex, char c)
        {
            List<StrSpan> tokenList = new List<StrSpan>();

            StrSpan span;

            int iLeft = beginIndex;
            //int iRight;

            for(int i = iLeft; i <= endIndex; i++)
            {

                if (str[i] == c)
                {
                    if (i == iLeft)
                    {
                        span = new StrSpan(i, i);
                        span.isEmpty = true;
                    }
                    else
                    {
                        span = new StrSpan(iLeft, i - 1);
                    }

                    tokenList.Add(span);

                    iLeft = i + 1;
                }

            }
            
            

            if (iLeft > endIndex)
            {
                span = new StrSpan(endIndex, endIndex);
                span.isEmpty = true;
            }
            else
            {
                span = new StrSpan(iLeft, endIndex);
            }

            tokenList.Add(span);
            
            return tokenList;
        }

        public static StrSpan Trim(char[] str, int beginIndex, int endIndex, char[] charList)
        {

            //if (beginIndex > endIndex)
            //    throw new Exception("StrUtil.Trim() ： beginIndex 应小于等于 endIndex 。");

            if (beginIndex > endIndex)
            {
                //return StrSpan.Empty;
                return new StrSpan(beginIndex, endIndex, true);
            }

            //StrSpan span;
            
            int i = FindForwardUntilNot(str, beginIndex, endIndex, charList);

            if (i == -1)
            {
                //return StrSpan.Empty;
                //span = new StrSpan(beginIndex, endIndex);
                //span.isEmpty = true;

                //return span;
                return new StrSpan(beginIndex, endIndex, true);
            }
                

            if (i == endIndex)
            {
                //span = new StrSpan(i, i);

                //return span;

                return new StrSpan(i, i);
            }
                

            int j = FindBackwardUntilNot(str, i, endIndex, charList);

            
            return new StrSpan(i, j);


        }

        public static bool IsNumber(char c)
        {
            byte b = (byte)c;

            return b >= 48 && b <= 57;
        }

        public static bool IsLetter(char c)
        {
            byte b = (byte)c;

            return b >= 65 && b <= 90 || b >= 97 && b <= 122;
        }
    }

    class StrSpan
    {
        public int iLeft;
        public int iRight;
        public bool isEmpty = false;

        public StrSpan(int iLeft, int iRight)
        {
            this.iLeft = iLeft;
            this.iRight = iRight;
        }

        public StrSpan(int iLeft, int iRight, bool isEmpty)
        {
            this.iLeft = iLeft;
            this.iRight = iRight;
            this.isEmpty = isEmpty;
        }

        //public static StrSpan Empty;

        //static StrSpan()
        //{
        //    Empty = new StrSpan(-1, -1);

        //    Empty.isEmpty = true;
        //}
    }
}
