using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddCodeBeautiful : RuleBase
    {
        public override string GetRuleName()
        {
            return "编码风格优美加分";
        }

        protected override bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;

            //代码行数量在100~300之间的，被称为“风格优美”
            int lineCount = fileContent.Count;
            if (lineCount >= 100 && lineCount <= 300) return true;

            return false;
        }

        public override float GetRuleGrade()
        {
            return 0.05f;
        }
    }
}
