using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckTools.Rules
{
    public class SubClassComment : I_Rules
    {
        public string GetRuleName()
        {
            return "类注释扣分";
        }

        public bool RuleResule(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            bool returnB = false;
            //遍历文件中的每一行
            for (int i = 0; i < fileContent.Count; i++)
            {
                //如果包含class关键字，且当前行没有被注释，那么就开始判断
                if (fileContent[i].IndexOf(" class ") != -1
                    &&
                    !fileContent[i].TrimStart(new char[] { ' ' }).StartsWith("//"))
                {
                    rowNum = i;
                    //检测当前行的前几行，是否包含关键字：</summary>和<summary>
                    int minNum = 0;
                    int maxNum = 0;

                    //遍历得到主要关键字的位置（不能超过12行）
                    for (int j = 1; j <= 8; j++)
                    {
                        if (i - j > 0)
                        {
                            if (fileContent[i - j].IndexOf("</summary>") != -1 && maxNum == 0) maxNum = i - j;
                            if (fileContent[i - j].IndexOf("<summary>") != -1 && minNum == 0) minNum = i - j;
                        }
                        else
                        {
                            break;
                        }
                    }

                    //找到了内容，那么就开始对内容做检验
                    if (minNum != 0 && maxNum != 0 && minNum < maxNum)
                    {
                        string temp = "";
                        for (int k = minNum; k <= maxNum; k++)
                        {
                            temp += fileContent[k];
                        }

                        temp = temp.Trim()
                            .Replace("</summary>", "")
                            .Replace("<summary>", "")
                            .Replace("/", "")
                            .Replace(" ", "");
                        if (string.IsNullOrEmpty(temp)) returnB = true;
                    }
                    else
                    {
                        returnB = true;
                    }
                }

                if (returnB) return true;
            }

            return returnB;
        }

        public float GetRuleGrade()
        {
            return -1;
        }
    }
}
