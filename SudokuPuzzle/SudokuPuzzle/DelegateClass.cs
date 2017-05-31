using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuPuzzle
{

    /// <summary>
    /// 计算数独委托
    /// </summary>
    /// <param name="paramInfo"></param>
    /// <returns></returns>
    public delegate ParamInfo DelegateCalcSudoku(ParamInfo paramInfo);
}
