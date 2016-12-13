using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class SubIFComment: RuleBase
    {
        public override string GetRuleName()
        {
            return "判断注释扣分";
        }

        protected override bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            //判断过程十分复杂，考虑以后改进和实现（有的加，有的不加，这个范围不好选择）

            return false;
        }

        public override float GetRuleGrade()
        {
            return -1;
        }
    }
}
