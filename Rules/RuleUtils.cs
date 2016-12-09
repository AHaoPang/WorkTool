using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    /// <summary>
    /// 为规则提供公用的工具方法
    /// </summary>
    public class RuleUtils
    {
        /// <summary>
        /// 获取代码的作用域
        /// </summary>
        /// <param name="pos">要判断的行位置</param>
        /// <param name="fileContent">文件中的所有行</param>
        /// <returns>元祖 Item1:最小行号 Item2:最大行号 Item2为0表示没有找到</returns>
        public static Tuple<int, int> GetCodeRange(int pos, List<string> fileContent)
        {
            //利用大括号的一一对应特质来找到代码的作用域
            int minNum = 0;
            int maxNum = 0;
            int bracket = 0;

            for (int i = pos + 1; i < fileContent.Count; i++)
            {
                if (fileContent[i].IndexOf("{") != -1)
                {
                    bracket++;
                    if (minNum == 0) minNum = i;
                }
                else if (i - (pos + 1) > 3 && bracket == 0) break;  //第一个左大括号的查找限制在4行以内
                if (fileContent[i].IndexOf("}") != -1)
                {
                    bracket--;
                    if (bracket == 0)
                    {
                        maxNum = i;
                        break;
                    }
                }
            }

            return new Tuple<int, int>(minNum, maxNum);
        }

        /// <summary>
        /// 获取当前位置前的注释行范围（在4行之内找，没找到就默认为没有注释）
        /// </summary>
        /// <param name="pos">当前的位置</param>
        /// <param name="fileContent">文件中的行列表</param>
        /// <returns>注释行范围 Item1:最小值 Item2:最大值</returns>
        public static Tuple<int, int> GetCommentRange(int pos, List<string> fileContent)
        {
            int minNum = 0;
            int maxNum = 0;

            int countLimt = 0;
            for (int i = pos - 1; i >= 0; i--)
            {
                if (fileContent[i].IndexOf("//") != -1)
                {
                    if (maxNum == 0) maxNum = i;
                    minNum = i;
                }
                else
                {
                    countLimt++;
                    if (maxNum != 0) break;
                    if (countLimt >= 4) break;
                }
            }

            return new Tuple<int, int>(minNum, maxNum);
        }

        /// <summary>
        /// 将多个行之间的字符串按照一定规则连接起来（去掉斜杠、空格、空白操作符,）
        /// </summary>
        /// <param name="startLinePos">起始行</param>
        /// <param name="endLinePos">终止行</param>
        /// <param name="fileContent">文件行列表</param>
        /// <returns>多个行连成一个字符串的结果</returns>
        private static string GetLinesStr(int startLinePos, int endLinePos, List<string> fileContent)
        {
            string strTemp = "";

            if (startLinePos == endLinePos) strTemp = fileContent[startLinePos].Replace("/", "").Replace(" ", "").Trim();
            else
            {
                for (int i = startLinePos; i <= endLinePos; i++)
                {
                    strTemp += fileContent[i].Replace("/", "").Replace(" ", "").Trim();
                }
            }
            return strTemp;
        }

        /// <summary>
        /// 判断指定标签对内部是否有内容
        /// </summary>
        /// <param name="startP">完整字符串的起始核查位置</param>
        /// <param name="checkStr">要核查的字符串</param>
        /// <param name="totalStr">完整的字符串</param>
        /// <returns>判断结果</returns>
        private static bool HasValue(int startP, string checkStr, string totalStr, out int endP)
        {
            endP = 0;
            int startPos = 0;
            int endPos = 0;
            string strT = "";

            int tempInt = totalStr.IndexOf(checkStr, startP);
            if (tempInt != -1)
            {
                startPos = tempInt + checkStr.Length;
                int tempInt2 = totalStr.IndexOf(checkStr, startPos);
                if (tempInt2 != -1)
                {
                    endPos = tempInt2 - 1;

                    if (startPos == endPos) return false;
                    else
                    {
                        endP = tempInt2 + checkStr.Length;
                        strT = totalStr.Substring(startPos, endPos + 1 - startPos);
                        if (string.IsNullOrEmpty(strT)) return false;
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 判断Param标签对内部是否有内容
        /// </summary>
        /// <param name="startP">完整字符串的起始核查位置</param>
        /// <param name="totalStr">完整的字符串</param>
        /// <returns>判断结果</returns>
        private static bool HasParamValue(int startP, string totalStr, out int endP)
        {
            string checkStr = "<param";
            endP = 0;
            int startPos = 0;
            int endPos = 0;
            string strT = "";

            int tempInt = totalStr.IndexOf(checkStr, startP);
            if (tempInt != -1)
            {
                int tempInt3 = totalStr.IndexOf(">", tempInt);
                startPos = tempInt3 + 1;
                int tempInt2 = totalStr.IndexOf(checkStr, startPos);
                if (tempInt2 != -1)
                {
                    endPos = tempInt2 - 1;

                    if (startPos == endPos) return false;
                    else
                    {
                        endP = tempInt2 + checkStr.Length;
                        strT = totalStr.Substring(startPos, endPos + 1 - startPos);
                        if (string.IsNullOrEmpty(strT)) return false;
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 判断Summary标签对内部是否有内容
        /// </summary>
        /// <param name="startLinePos">判断的起始行位置</param>
        /// <param name="endLinePos">判断的中止行位置</param>
        /// <param name="fileContent">文件的行内容列表</param>
        /// <returns>是否有内容</returns>
        public static bool HasSummaryValue(int startLinePos, int endLinePos, List<string> fileContent)
        {
            string strTemp = GetLinesStr(startLinePos, endLinePos, fileContent);

            //<summary>和<summary>之间的内容判断
            int outP = 0;
            int sP = 0;
            bool returnB = false;

            do
            {
                returnB = HasValue(sP, "<summary>", strTemp, out outP);
                if (returnB == false && outP != 0) break;
                sP = outP;
            } while (outP != 0);

            return returnB;
        }

        /// <summary>
        /// 判断param标签对内部是否有内容
        /// </summary>
        /// <param name="startLinePos">判断的起始行位置</param>
        /// <param name="endLinePos">判断的中止行位置</param>
        /// <param name="fileContent">文件的行内容列表</param>
        /// <returns>是否有内容</returns>
        public static bool HasParamValue(int startLinePos, int endLinePos, List<string> fileContent)
        {
            string strTemp = GetLinesStr(startLinePos, endLinePos, fileContent);

            //<param和<param>的内容判断
            int outP = 0;
            int sP = 0;
            bool returnB = false;

            do
            {
                returnB = HasParamValue(sP, strTemp, out outP);
                if (returnB == false && outP != 0) break;
                sP = outP;
            } while (outP != 0);

            return returnB;
        }

        /// <summary>
        /// 判断returns标签对内部是否有内容
        /// </summary>
        /// <param name="startLinePos">判断的起始行位置</param>
        /// <param name="endLinePos">判断的中止行位置</param>
        /// <param name="fileContent">文件的行内容列表</param>
        /// <returns>是否有内容</returns>
        public static bool HasReturnsValue(int startLinePos, int endLinePos, List<string> fileContent)
        {
            string strTemp = GetLinesStr(startLinePos, endLinePos, fileContent);

            //<returns>和<returns>的内容判断
            int outP = 0;
            int sP = 0;
            bool returnB = false;

            do
            {
                returnB = HasValue(sP, "<returns>", strTemp, out outP);
                if (returnB == false && outP != 0) break;
                sP = outP;
            } while (outP != 0);

            return returnB;
        }
    }
}
