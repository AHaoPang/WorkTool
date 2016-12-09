using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTools
{
    /// <summary>
    /// 定义审核规则类的接口
    /// </summary>
    interface I_Rules
    {
        /// <summary>
        /// 得到规则名
        /// </summary>
        /// <returns>规则名</returns>
        string GetRuleName();

        /// <summary>
        /// 得到规则结果
        /// </summary>
        /// <param name="fileContent">要审核的文件内容</param>
        /// <returns></returns>
        bool RuleResule(List<string> fileContent, out int rowNum);

        /// <summary>
        /// 得到规则的分值
        /// </summary>
        /// <returns>规则分值</returns>
        float GetRuleGrade();
    }
}
