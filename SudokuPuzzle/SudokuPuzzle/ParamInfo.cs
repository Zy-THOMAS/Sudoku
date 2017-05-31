using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuPuzzle
{
    public class ParamInfo
    {

        public ParamInfo()
        {

        }

        public ParamInfo(int sudokuNum, DateTime startTime)
        {
            this.SudokuNum = sudokuNum;
            this.StartTime = startTime;
        }

        /// <summary>
        /// 数据编号
        /// </summary>
        public int SudokuNum
        {
            get;
            set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 显示控件
        /// </summary>
        public TextBox DisplayControl
        {
            get;
            set;
        }

        public Hashtable ResValue
        {
            get;
            set;
        }
    }
}
