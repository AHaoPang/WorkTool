using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools
{
    /// <summary>
    /// 所有规则类的基类
    /// </summary>
    public abstract class RuleBase : I_Rules
    {
        public abstract string GetRuleName();

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

        protected abstract bool RealOpera(List<string> fileContent, out int rowNum);

        public abstract float GetRuleGrade();
    }
}
