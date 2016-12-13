using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class SubForComment : RuleBase
    {
        public override string GetRuleName()
        {
            return "循环注释扣分";
        }

        protected override bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            //定位到关键字的位置
            //定位到作用域的位置
            //在 关键字-1~作用域的起始位置，找关键字：// /*
            //找到false 找不到true
            for (int i = 0; i < fileContent.Count; i++)
            {
                rowNum = i;
                if (fileContent[i].Trim().StartsWith("for (") ||
                    fileContent[i].Trim().StartsWith("foreach (") ||
                    fileContent[i].Trim().StartsWith("while ("))
                {
                    int minNum, maxNum;

                    minNum = i - 1 >= 0 ? i - 1 : 0;
                    var tupleTemp = RuleUtils.GetCodeRange(i, fileContent);
                    maxNum = tupleTemp.Item2 != 0 ? tupleTemp.Item1 : i;

                    if (minNum == 0 || maxNum == 0) return false;

                    string checkStr = "";
                    for (int j = minNum; j <= maxNum; j++)
                    {
                        checkStr += fileContent[j];
                    }
                    if (checkStr.IndexOf("//") == -1 &&
                        checkStr.IndexOf("/*") == -1)
                        return true;
                }
            }

            return false;
        }

        public override float GetRuleGrade()
        {
            return -1;
        }
    }
}
