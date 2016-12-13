using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddCodeBlock : RuleBase
    {
        public override string GetRuleName()
        {
            return "代码块加分";
        }

        protected override bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            //每100行代码，包含一对#region，就表明代码块是应该加分的

            //1.计算文件的总行数
            int fileLines = fileContent.Count;
            //2.计算#region的数量
            int regionNum = fileLines / 100;
            int realRegionNum = 0;

            //3.遍历每一行，得到#region的数量
            foreach (var line in fileContent)
            {
                if (line.Trim().StartsWith("#region")) realRegionNum++;
            }

            //4.比较理论数量和真实数量
            if (realRegionNum >= regionNum) return true;

            return false;
        }

        public override float GetRuleGrade()
        {
            return 0.1f;
        }
    }
}
