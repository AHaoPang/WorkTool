using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class SubIFComment: I_Rules
    {
        public string GetRuleName()
        {
            return "判断注释扣分";
        }

        public bool RuleResule(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            try
            {
                return RealOpera(fileContent, out rowNum);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            //判断过程十分复杂，考虑以后改进和实现（有的加，有的不加，这个范围不好选择）

            return false;
        }

        public float GetRuleGrade()
        {
            return -1;
        }
    }
}
