namespace SudokuPuzzle
{
    partial class SPUI
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCalc = new System.Windows.Forms.Button();
            this.txtShowInfo = new System.Windows.Forms.TextBox();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.labMsg = new System.Windows.Forms.Label();
            this.txtCompleteNum = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(93, 12);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(75, 23);
            this.btnCalc.TabIndex = 0;
            this.btnCalc.Text = "START";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // txtShowInfo
            // 
            this.txtShowInfo.Location = new System.Drawing.Point(12, 41);
            this.txtShowInfo.Multiline = true;
            this.txtShowInfo.Name = "txtShowInfo";
            this.txtShowInfo.ReadOnly = true;
            this.txtShowInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShowInfo.Size = new System.Drawing.Size(486, 336);
            this.txtShowInfo.TabIndex = 1;
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(12, 12);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(75, 23);
            this.btnReadFile.TabIndex = 2;
            this.btnReadFile.Text = "INITDATA";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // labMsg
            // 
            this.labMsg.AutoSize = true;
            this.labMsg.Location = new System.Drawing.Point(15, 386);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(41, 12);
            this.labMsg.TabIndex = 4;
            this.labMsg.Text = "label1";
            this.labMsg.Visible = false;
            // 
            // txtCompleteNum
            // 
            this.txtCompleteNum.Location = new System.Drawing.Point(423, 383);
            this.txtCompleteNum.Name = "txtCompleteNum";
            this.txtCompleteNum.Size = new System.Drawing.Size(75, 21);
            this.txtCompleteNum.TabIndex = 5;
            this.txtCompleteNum.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(423, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // SPUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 404);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtCompleteNum);
            this.Controls.Add(this.labMsg);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.txtShowInfo);
            this.Controls.Add(this.btnCalc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SPUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUDOKU";
            this.Load += new System.EventHandler(this.SPUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.TextBox txtShowInfo;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Label labMsg;
        private System.Windows.Forms.TextBox txtCompleteNum;
        private System.Windows.Forms.Button btnClear;
    }
}

