using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddForFormat : RuleBase
    {
        public override string GetRuleName()
        {
            return "循环格式加分";
        }

        protected override bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            //在文件行中查找的关键字是：for\foreach
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

        public override float GetRuleGrade()
        {
            return 0.1f;
        }
    }
}
