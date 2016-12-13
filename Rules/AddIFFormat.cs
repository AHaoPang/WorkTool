using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddIFFormat : RuleBase
    {
        public override string GetRuleName()
        {
            return "if判断格式加分";
        }

        protected override bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            bool returnB = false;

            //在文件行中查找的关键字是：if\else if
            //他们的作用域都能找到的时候，就证明符合规范的加分条件

            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i].Trim().StartsWith("if (") ||
                    fileContent[i].Trim().StartsWith("else if ("))
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
            return 0.2f;
        }
    }
}
