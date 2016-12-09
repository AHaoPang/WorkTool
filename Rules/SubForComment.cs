using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class SubForComment : I_Rules
    {
        public string GetRuleName()
        {
            return "循环注释扣分";
        }

        public bool RuleResule(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            try
            {
                return RealOpera(fileContent, out rowNum);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            //定位到关键字的位置
            //定位到作用域的位置
            //在 关键字-1~作用域的起始位置，找关键字：// /*
            //找到false 找不到true

            for (int i = 0; i < fileContent.Count; i++)
            {
                rowNum = i;
                if (fileContent[i].IndexOf("for (") != -1
                    ||
                    fileContent[i].IndexOf("foreach (") != -1
                    ||
                    fileContent[i].IndexOf("while (") != -1)
                {
                    int minNum = 0;
                    int maxNum = 0;
                    minNum = i - 1 >= 0 ? i - 1 : 0;

                    var tupleTemp = RuleUtils.GetCodeRange(i, fileContent);
                    maxNum = tupleTemp.Item2 != 0 ? tupleTemp.Item1 : i;

                    if (minNum == 0 || maxNum == 0) return false;

                    string checkStr = "";
                    for (int j = minNum; j <= maxNum; j++)
                    {
                        checkStr += fileContent[j];
                    }
                    if (checkStr.IndexOf("//") != -1
                        ||
                        checkStr.IndexOf("/*") != -1)
                    {
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public float GetRuleGrade()
        {
            return -1;
        }
    }
}
