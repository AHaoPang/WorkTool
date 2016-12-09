using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddIFFormat: I_Rules
    {
        public string GetRuleName()
        {
            return "if判断格式加分";
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

            //在文件行中查找的关键字是：if\else if
            //他们的作用域都能找到的时候，就证明符合规范的加分条件

            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i].IndexOf("if (") != -1
                    ||
                    fileContent[i].IndexOf("else if (") != -1)
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
            return 0.2f;
        }
    }
}
