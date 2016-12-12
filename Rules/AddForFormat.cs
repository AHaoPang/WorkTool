using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddForFormat : I_Rules
    {
        public string GetRuleName()
        {
            return "循环格式加分";
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
            //在文件行中查找的关键字是：for\foreach\while
            //他们的作用域都能找到的时候，就证明符合规范的加分条件
            bool returnB = false;

            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i].Trim().StartsWith("for (") ||
                    fileContent[i].Trim().StartsWith("foreach ("))
                {
                    var tupleTemp = RuleUtils.GetCodeRange(i, fileContent);
                    if (tupleTemp.Item2 == 0) return false;
                    returnB = true;
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
