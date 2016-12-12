using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddSwitchFormat : I_Rules
    {
        public string GetRuleName()
        {
            return "switch语句加分";
        }

        public bool RuleResule(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            try
            {
                return RealOpera(fileContent);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool RealOpera(List<string> fileContent)
        {
            bool returnB = false;
            //遍历每一行，找到包含switch关键字的行，就开始判断
            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i].Trim().StartsWith("switch"))
                {
                    var temp = RuleUtils.GetCodeRange(i, fileContent);
                    //1.没找到switch的作用域，那么就说明不符合要求
                    if (temp.Item2 == 0) return false;

                    //2.在作用域内部寻找关键字：default:
                    string tempStr = "";
                    for (int j = temp.Item1; j < temp.Item2; j++)
                    {
                        tempStr += fileContent[j];
                    }
                    if (tempStr.IndexOf("default:") != -1) returnB = true;
                }
            }

            return returnB;
        }

        public float GetRuleGrade()
        {
            return 0.1f;
        }
    }
}
