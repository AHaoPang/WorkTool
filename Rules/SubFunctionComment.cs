﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheckTools.Rules
{
    public class SubFunctionComment : RuleBase
    {
        public override string GetRuleName()
        {
            return "方法注释扣分";
        }

        protected override bool RealOpera(List<string> fileContent, out int rowNum)
        {
            rowNum = 0;
            //定位到方法的位置，排除 Page_Load ProcessRequest
            //注意：所有的内容判断都是可选的
            //<summary>和</summary>的内容判断
            //<param和</param>的内容判断
            //<returns>和</returns>的内容判断

            for (int i = 0; i < fileContent.Count; i++)
            {
                rowNum = i;
                if (Regex.IsMatch(fileContent[i], @"(public|protected|private|static).*\(") &&
                    fileContent[i].IndexOf("Page_Load") == -1 &&
                    fileContent[i].IndexOf("ProcessRequest") == -1 &&
                    fileContent[i].IndexOf("=") == -1)
                {
                    var tupleTemp = RuleUtils.GetCommentRange(i, fileContent);
                    if (tupleTemp.Item2 == 0) return true;
                    if (!RuleUtils.HasSummaryValue(tupleTemp.Item1, tupleTemp.Item2, fileContent)) return true;
                    if (!RuleUtils.HasReturnsValue(tupleTemp.Item1, tupleTemp.Item2, fileContent)) return true;
                    if (!RuleUtils.HasParamValue(tupleTemp.Item1, tupleTemp.Item2, fileContent)) return true;
                }
            }

            return false;
        }

        public override float GetRuleGrade()
        {
            return -1;
        }
    }
}
