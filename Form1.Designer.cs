namespace CheckTools
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.rdoFirst = new System.Windows.Forms.RadioButton();
            this.rdoSecond = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 87);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(608, 103);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始审核";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // rdoFirst
            // 
            this.rdoFirst.AutoSize = true;
            this.rdoFirst.Location = new System.Drawing.Point(38, 36);
            this.rdoFirst.Name = "rdoFirst";
            this.rdoFirst.Size = new System.Drawing.Size(186, 20);
            this.rdoFirst.TabIndex = 1;
            this.rdoFirst.TabStop = true;
            this.rdoFirst.Text = "项目发布当天提交审核";
            this.rdoFirst.UseVisualStyleBackColor = true;
            // 
            // rdoSecond
            // 
            this.rdoSecond.AutoSize = true;
            this.rdoSecond.Location = new System.Drawing.Point(340, 36);
            this.rdoSecond.Name = "rdoSecond";
            this.rdoSecond.Size = new System.Drawing.Size(186, 20);
            this.rdoSecond.TabIndex = 2;
            this.rdoSecond.TabStop = true;
            this.rdoSecond.Text = "项目发布次日提交审核";
            this.rdoSecond.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 202);
            this.Controls.Add(this.rdoSecond);
            this.Controls.Add(this.rdoFirst);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RadioButton rdoFirst;
        private System.Windows.Forms.RadioButton rdoSecond;
    }
}

