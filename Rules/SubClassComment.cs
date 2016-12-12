using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckTools.Rules
{
    public class SubClassComment : I_Rules
    {
        public string GetRuleName()
        {
            return "类注释扣分";
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
            //默认是不扣分的
            bool returnB = false;
            //遍历文件中的每一行
            for (int i = 0; i < fileContent.Count; i++)
            {
                //如果包含class关键字，且当前行没有被注释，那么就开始判断
                if (fileContent[i].IndexOf(" class ") != -1)
                {
                    rowNum = i;

                    //检测类的注释
                    var rowsRange = RuleUtils.GetCommentRange(i, fileContent);
                    //处理无注释的情况
                    if (rowsRange.Item2 == 0) return true;
                    //处理有summary，但是没有内容的情况
                    if (!RuleUtils.HasSummaryValue(rowsRange.Item1, rowsRange.Item2, fileContent)) return true;
                }
            }

            return returnB;
        }

        public float GetRuleGrade()
        {
            return -1;
        }
    }
}
