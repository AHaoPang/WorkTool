using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckTools.Rules;

namespace CheckTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //窗体展示的时候，要加载所有的规则
            ruleList = new List<I_Rules>()
            {
                new SubClassComment(),
                new SubForComment(),
                new SubFunctionComment(),

                new AddCodeBlock(),
                new AddCodeBeautiful(),
                new AddSwitchFormat(),
                new AddForFormat(),
                new AddIFFormat()
            };
        }

        /// <summary>
        /// 所有审核规则的列表
        /// </summary>
        private List<I_Rules> ruleList;

        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //1.让使用者选择文件夹
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowDialog();

            if (string.IsNullOrEmpty(folderBrowser.SelectedPath)) return;

            string projectFolderPos = folderBrowser.SelectedPath;
            outputfile = projectFolderPos + ".txt";
            absolutePath = Path.GetDirectoryName(projectFolderPos);

            OutPutFileWrite("**********************************************");
            OutPutFileWrite("项目名称：" + Path.GetFileName(projectFolderPos));

            //2.从文件夹中遍历所有的C#文件，得到的是文件名
            List<string> filesArray = Directory.GetFiles(projectFolderPos, "*.cs", SearchOption.AllDirectories).ToList();

            //3.以文件为单元，开始做代码审核
            float projectTotalGrade = 0;
            foreach (var filePath in filesArray)
            {
                projectTotalGrade += ProcessContent(filePath);
            }

            OutPutFileWrite("************************************");
            OutPutFileWrite("项目总得分：" + projectTotalGrade);

            MessageBox.Show("操作完成");

        }

        /// <summary>
        /// 记录运行程序时，文件夹在本地的路径
        /// </summary>
        private string absolutePath;

        /// <summary>
        /// 以文件为单位，做每个规则的审核
        /// </summary>
        private float ProcessContent(string filePath)
        {
            List<string> fileContent = File.ReadAllLines(filePath).ToList();
            //记录项目文件的总得分
            float fileContentGrade = 0;
            OutPutFileWrite("------------------------------------------------");
            OutPutFileWrite("文件名：" + filePath.Replace(absolutePath, ""));

            int rowNum = 0;
            //1.用当前拿到的内容,去执行每一个规则
            foreach (var rule in ruleList)
            {
                //若满足规则所指条件，那么就追加记录规则名，以及分数
                if (rule.RuleResule(fileContent, out rowNum))
                {
                    if (rowNum != 0) OutPutFileWrite(string.Format("规则：{0} 分数：{1} 位置：{2}", rule.GetRuleName(), rule.GetRuleGrade(), rowNum + 1));
                    else OutPutFileWrite(string.Format("规则：{0} 分数：{1}", rule.GetRuleName(), rule.GetRuleGrade()));

                    fileContentGrade += rule.GetRuleGrade();
                }
            }

            OutPutFileWrite("文件总得分：" + fileContentGrade);
            return fileContentGrade;
        }

        #region 输出结果相关
        /// <summary>
        /// 输出文件的全名称
        /// </summary>
        private string outputfile;

        /// <summary>
        /// 向输出文件追加内容
        /// </summary>
        /// <param name="text">要向文件中追加的内容</param>
        private void OutPutFileWrite(string text)
        {
            File.AppendAllText(outputfile, text + Environment.NewLine);
        }
        #endregion
    }
}
