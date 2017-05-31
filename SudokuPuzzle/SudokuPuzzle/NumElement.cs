using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuPuzzle
{
    public class NumElement
    {

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public NumElement() { 
        
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="numValue">元素值</param>
        /// <param name="numFlag">元素标记</param>
        public NumElement(int numValue, bool numFlag) {
            this.NumValue = numValue;
            this.NumFlag = numFlag;
        }

        /// <summary>
        /// 数值
        /// </summary>
        public int NumValue
        {
            get;
            set;
        }

        /// <summary>
        /// 数标记
        /// </summary>
        public bool NumFlag
        {
            get;
            set;
        }
    }
}
