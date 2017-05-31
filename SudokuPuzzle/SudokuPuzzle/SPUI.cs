using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuPuzzle
{
    public partial class SPUI : Form
    {

        private List<String> srcData = new List<string>();

        public SPUI()
        {
            InitializeComponent();
        }

        #region 信息输出处理函数
        /// <summary>
        /// 处理过程中值的输出
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public void ShowProcess(string property, string value)
        {
            lock (this)
            {
                this.txtShowInfo.AppendText("******** " + property + "：\r\n");
                this.txtShowInfo.AppendText("\r\n");
                this.txtShowInfo.AppendText(value + "\r\n");
            }

        }

        public void ShowProcess(string msg)
        {
            lock (this)
            {
                this.txtShowInfo.AppendText("******** " + msg + "\r\n\r\n");
            }
        }
        #endregion

        /// <summary>
        /// 忽略线程安全模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SPUI_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.btnCalc.Enabled = false;
        }

        /// <summary>
        /// 读取资源文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            ShowProcess("数据初始化开始...");
            btnCalc.Enabled = false;
            List<String> resData = NumHandler.ReadFile();
            if (resData != null && resData.Count > 0)
            {
                ShowProcess("数据初始化结束，请点击【START】按钮开始数独计算。");
                btnCalc.Enabled = true;
                srcData = resData;
            }
            else
            {
                btnReadFile.Enabled = true;
            }
        }

        /// <summary>
        /// 初始化结果集
        /// </summary>
        /// <param name="resValueHt"></param>
        /// <param name="srcData"></param>
        private void initResValueHt(Hashtable resValueHt, List<String> srcData)
        {
            int num = 0;
            for (int i = 0; i < srcData.Count; i++)
            {
                if (string.IsNullOrEmpty(srcData[i])) { continue; }
                num++;
                resValueHt.Add(num, new StringBuilder());
            }
        }

        /// <summary>
        /// 开始计算数独
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_Click(object sender, EventArgs e)
        {
            this.labMsg.Text = "正在计算中...";
            this.txtCompleteNum.Text = "0";
            this.labMsg.Visible = true;
            this.btnCalc.Enabled = false;
            this.btnReadFile.Enabled = false;
            if (srcData == null || srcData.Count == 0)
            {
                ShowProcess("获取初始化数据失败！");
                return;
            }
            Hashtable resValueHt = new Hashtable();
            initResValueHt(resValueHt, srcData);
            int sudokuNum = 0;
            for (int i = 0; i < srcData.Count; i++)
            {
                if (string.IsNullOrEmpty(srcData[i])) { continue; }
                sudokuNum++;
                ParamInfo paramInfo = new ParamInfo();
                paramInfo.ResValue = resValueHt;
                paramInfo.StartTime = System.DateTime.Now;
                paramInfo.SudokuNum = sudokuNum;
                paramInfo.DisplayControl = this.txtShowInfo;
                bool checkRes = NumHandler.checkData(srcData[i]);
                if (!checkRes)
                {
                    string resValue = "[" + sudokuNum + ", -1]";
                    StringBuilder resValueBuilder = (StringBuilder)paramInfo.ResValue[paramInfo.SudokuNum];
                    resValueBuilder.Append(resValue).AppendLine();
                    paramInfo.ResValue[paramInfo.SudokuNum] = resValueBuilder;
                    DateTime eTime = System.DateTime.Now;
                    double costTime = eTime.Subtract(paramInfo.StartTime).TotalMilliseconds;
                    ShowProcess("当前已完成第【" + sudokuNum + "】组数独的计算,耗时：" + costTime + "毫秒");
                    int curCompleteNum = Convert.ToInt32(this.txtCompleteNum.Text);
                    curCompleteNum++;
                    this.txtCompleteNum.Text = curCompleteNum.ToString();
                    continue;
                }
                string[] numArr = srcData[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                ShowProcess("开始计算第【" + sudokuNum + "】组数独值");
                SudokuService sudokuService = new SudokuService();
                sudokuService.initSudokuData(NumHandler.StringToInt(numArr));
                //使用委托实现多线程解决
                DelegateCalcSudoku callMethod = new DelegateCalcSudoku(sudokuService.calcSudoduData);
                if (callMethod == null)
                {
                    MessageBox.Show("系统异常，启动线程传入方法为空！");
                    return;
                }
                callMethod.BeginInvoke(paramInfo, asyResult =>
                {
                    ParamInfo res = callMethod.EndInvoke(asyResult);
                    updateResultState(res, sudokuNum);
                }, null);
            }
        }

        /// <summary>
        /// 更新结果状态
        /// </summary>
        /// <param name="res"></param>
        /// <param name="sudokuNum"></param>
        private void updateResultState(ParamInfo res, int sudokuNum)
        {
            if (res != null)
            {
                int curCompleteNum = Convert.ToInt32(this.txtCompleteNum.Text);
                curCompleteNum++;
                DateTime eTime = System.DateTime.Now;
                double costTime = eTime.Subtract(res.StartTime).TotalMilliseconds;
                ShowProcess("当前已完成第【" + res.SudokuNum + "】组数独的所有解计算，耗时：" + costTime + "毫秒");
                if (curCompleteNum == sudokuNum)
                {
                    this.labMsg.Text = "***已完成所有数独的求解***";
                    this.btnCalc.Enabled = true;
                    this.btnReadFile.Enabled = true;
                    Hashtable resValueht = res.ResValue;
                    String[] resValueArr = new String[sudokuNum];
                    NumHandler.deleteFile("SU_DOKU_AN.txt");
                    for (int j = 0; j < sudokuNum; j++)
                    {
                        StringBuilder tempBuilder = (StringBuilder)resValueht[(j + 1)];
                        NumHandler.SaveData(tempBuilder.ToString());
                    }
                }
                else
                {
                    this.txtCompleteNum.Text = curCompleteNum.ToString();
                }
            }
        }

        /// <summary>
        /// 清空信息面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtShowInfo.Text = "";
        }
    }
}
