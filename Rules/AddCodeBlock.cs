using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddCodeBlock: I_Rules
    {
        public string GetRuleName()
        {
            return "代码块加分";
        }

        public bool RuleResule(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            try
            {
                return OperaReal(fileContent);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool OperaReal(List<string> fileContent)
        {
            //每100行代码，包含一对#region，就表明代码块是应该加分的

            //1.计算文件的总行数
            int fileLines = fileContent.Count;
            //2.计算#region的数量
            int regionNum = fileLines/100;
            int realRegionNum = 0;

            //3.遍历每一行，得到#region的数量
            foreach (var line in fileContent)
            {
                if (line.IndexOf("#region") != -1) realRegionNum++;
            }

            //4.比较理论数量和真是数量
            if (realRegionNum > regionNum) return true;

            return false;
        }

        public float GetRuleGrade()
        {
            return 0.1f;
        }
    }
}
