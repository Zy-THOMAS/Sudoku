using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuPuzzle
{
    public class NumCell
    {
        /// <summary>
        /// 不带参数构造函数
        /// </summary>
        public NumCell()
        {
            this.IsHasValue = true;
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="value">单元格值</param>
        public NumCell(int value) {
            this.Value = value;
            this.IsHasValue = true;
        }

        /// <summary>
        /// 当前单元格位置
        /// </summary>
        public int[] CurCell
        {
            get;
            set;
        }

        /// <summary>
        /// 当前单元格可能的值
        /// </summary>
        public List<int> PropableValues
        {
            get;
            set;
        }

        /// <summary>
        /// 当前单元格是否存在可能值
        /// </summary>
        public bool IsHasValue
        {
            get;
            set;
        }

        /// <summary>
        /// 当前单元格的值
        /// </summary>
        public int Value
        {
            get;
            set;
        }
    }
}
