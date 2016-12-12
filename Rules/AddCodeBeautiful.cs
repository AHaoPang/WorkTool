using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class AddCodeBeautiful : I_Rules
    {
        public string GetRuleName()
        {
            return "编码风格优美加分";
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
            //代码行数量在100~300之间的，被称为“风格优美”
            int lineCount = fileContent.Count;
            if (lineCount >= 100 && lineCount <= 300) return true;

            return false;
        }

        public float GetRuleGrade()
        {
            return 0.05f;
        }
    }
}
