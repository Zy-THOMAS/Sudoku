using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuPuzzle
{
    public class SudokuService
    {
        /// <summary>
        /// 四维数独数组
        /// </summary>
        private NumElement[][][][] sudokuData = new NumElement[3][][][];

        //记录每个单元格的可能数字
        private List<NumCell> cellValContainer = new List<NumCell>();

        //记录所有单元格的可能数字
        private List<NumCell> allCellValContainer = new List<NumCell>();

        public NumElement[][][][] SudokuData
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化数独四维数组
        /// </summary>
        /// <param name="srcData">数独初始值</param>
        /// <returns>初始化后的数独四维数组</returns>
        public void initSudokuData(int[] srcData)
        {
            for (int m = 0; m < 3; m++)
            {
                sudokuData[m] = new NumElement[3][][];
                for (int n = 0; n < 3; n++)
                {
                    sudokuData[m][n] = new NumElement[3][];
                    for (int i = 0; i < 3; i++)
                    {
                        sudokuData[m][n][i] = new NumElement[3];
                        for (int j = 0; j < 3; j++)
                        {
                            sudokuData[m][n][i][j] = new NumElement(0, false);
                        }
                    }
                }
            }
            int index = 0;
            for (int m = 0; m < 3; m++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            int curVal = srcData[index++];
                            sudokuData[m][n][i][j].NumValue = curVal;
                            if (curVal > 0)
                            {
                                sudokuData[m][n][i][j].NumFlag = true;
                            }
                        }
                    }
                }
            }
            //获取所有单元格可能的值
            RecordAllCellValues();
        }

        /// <summary>
        /// 计算数独空白单元格值
        /// </summary>
        public ParamInfo calcSudoduData(ParamInfo paramInfo)
        {
            string msg = "第【" + paramInfo.SudokuNum + "】组数独计算结果为：\r\n\r\n";
            string resValue = null;
            int resSort = 1;
            try
            {
                for (int m = 0; m < 3; m++)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (!sudokuData[m][n][i][j].NumFlag)
                                {
                                    int[] curPosition = new int[4] { m, n, i, j };
                                    NumCell cellVal = GetCellVal(curPosition);
                                    if (!cellVal.IsHasValue)
                                    {
                                        m = cellVal.CurCell[0];
                                        n = cellVal.CurCell[1];
                                        i = cellVal.CurCell[2];
                                        j = cellVal.CurCell[3];
                                        ClearAfter(cellVal.CurCell);
                                    }
                                    sudokuData[m][n][i][j].NumValue = cellVal.Value;
                                    NumCell lastEmptyCell = allCellValContainer[allCellValContainer.Count - 1];
                                    if (ComparePosition(curPosition, lastEmptyCell.CurCell) && cellValContainer.Count > 0)
                                    {
                                        bool checjRes = checkSudokuResult(sudokuData, paramInfo);
                                        if (!checjRes)
                                        {
                                            throw new Exception();
                                        }
                                        resValue = NumHandler.getResult(sudokuData);
                                        msg = "[" + paramInfo.SudokuNum + ", " + resSort + "]\r\n";
                                        StringBuilder resValueBuilder = (StringBuilder)paramInfo.ResValue[paramInfo.SudokuNum];
                                        resValueBuilder.Append(msg).Append(resValue);
                                        paramInfo.ResValue[paramInfo.SudokuNum] = resValueBuilder;
                                        //NumHandler.SaveTempData(msg + resValue, paramInfo.SudokuNum.ToString());
                                        NumHandler.ShowProcess(paramInfo.DisplayControl, "已完成第【" + paramInfo.SudokuNum + "】组数独解[" + resSort + "]");
                                        NumCell lastCellVals = cellValContainer[cellValContainer.Count - 1];
                                        m = lastCellVals.CurCell[0];
                                        n = lastCellVals.CurCell[1];
                                        i = lastCellVals.CurCell[2];
                                        j = lastCellVals.CurCell[3];
                                        resSort++;
                                    }
                                }
                            }
                        }
                    }
                }
                resValue = NumHandler.getResult(sudokuData);
            }
            catch (Exception)
            {
                resValue = "[" + paramInfo.SudokuNum + ", 0]";
                //NumHandler.SaveTempData(resValue, paramInfo.SudokuNum.ToString());
                StringBuilder resValueBuilder = (StringBuilder)paramInfo.ResValue[paramInfo.SudokuNum];
                resValueBuilder.Append(resValue).AppendLine();
                paramInfo.ResValue[paramInfo.SudokuNum] = resValueBuilder;
            }
            return paramInfo;
        }

        /// <summary>
        /// 检测结果集
        /// </summary>
        /// <param name="sudokuData"></param>
        /// <param name="paramInfo"></param>
        /// <returns></returns>
        private bool checkSudokuResult(NumElement[][][][] sudokuData, ParamInfo paramInfo)
        {
            if (sudokuData == null) { return false; }
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (sudokuData[m][n][i][j].NumValue == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void showResult(int index, int resSort, string resValue, TextBox displayControl)
        {
            string msg = "[" + index + ", " + resSort + "]\r\n\r\n";
            NumHandler.ShowProcess(displayControl, msg + resValue);
        }

        /// <summary>
        /// 判断两个位置是否相等
        /// </summary>
        /// <param name="srcPosition"></param>
        /// <param name="desPosition"></param>
        /// <returns></returns>
        private bool ComparePosition(int[] srcPosition, int[] desPosition)
        {
            bool isCompare = true;
            if (srcPosition == null || srcPosition.Length == 0 || desPosition == null || desPosition.Length == 0 || srcPosition.Length != desPosition.Length)
            {
                return false;
            }
            for (int i = 0; i < srcPosition.Length; i++)
            {
                if (srcPosition[i] != desPosition[i])
                {
                    isCompare = false;
                    break;
                }
            }
            return isCompare;
        }

       /// <summary>
       /// 记录所有单元格的可能的值
       /// </summary>
       /// <param name="curPosition"></param>
        public void RecordAllCellValues()
        {
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (!sudokuData[m][n][i][j].NumFlag)
                            {
                                int[] curPosition = new int[4] { m, n, i, j };
                                List<int> curCellVals = GetCellProableValue(curPosition);
                                NumCell cellVal = new NumCell(0);
                                cellVal.PropableValues = curCellVals;
                                cellVal.CurCell = curPosition;
                                allCellValContainer.Add(cellVal);
                            }
                        }
                    }
                }
            }
        }

        //获取当前单元格的可能数字
        public NumCell GetCellVal(int[] cellData)
        {
            List<int> nums = GetCellProableValue(cellData);
            if (nums.Count == 0)
            {
                if (cellValContainer.Count == 0) { return new NumCell(0); }
                NumCell cellVal = cellValContainer[cellValContainer.Count - 1];
                cellVal.IsHasValue = false;
                cellVal.Value = cellVal.PropableValues[0];
                cellVal.PropableValues.Remove(cellVal.Value);
                if (cellVal.PropableValues.Count == 0) { cellValContainer.Remove(cellVal); }
                return cellVal;
            }
            else
            {
                NumCell cellVal = new NumCell();
                cellVal.CurCell = cellData;
                cellVal.Value = nums[0];
                nums.Remove(cellVal.Value);
                cellVal.PropableValues = nums;
                if (nums.Count > 0) { cellValContainer.Add(cellVal); }
                return cellVal;
            }
        }

        /// <summary>
        /// 获取当前位置可能的值
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <returns></returns>
        private List<int> GetCellProableValue(int[] cellPosition) {
            List<int> nums = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                nums.Add(i);
            }
            //保证每一宫内不含重复数字
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //当前宫内
                    int eleVal = sudokuData[cellPosition[0]][cellPosition[1]][i][j].NumValue;
                    if (nums.Contains(eleVal))
                    {
                        nums.Remove(eleVal);
                    }
                }
            }
            //保证横向不含重复数字
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int eleVal = sudokuData[i][cellPosition[1]][j][cellPosition[3]].NumValue;
                    if (nums.Contains(eleVal))
                    {
                        nums.Remove(eleVal);
                    }
                }
            }
            //保证纵向不含重复数字
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int eleVal = sudokuData[cellPosition[0]][i][cellPosition[2]][j].NumValue;
                    if (nums.Contains(eleVal))
                    {
                        nums.Remove(eleVal);
                    }
                }
            }
            return nums;
        }

        //复原指定单元格之后所有初始值为0的单元格
        private void ClearAfter(int[] pos)
        {
            int min = pos[0] * 1000 + pos[1] * 100 + pos[2] * 10 + pos[3];
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (m * 1000 + n * 100 + i * 10 + j > min)
                            {
                                if (sudokuData[m][n][i][j].NumFlag == false)
                                {
                                    sudokuData[m][n][i][j].NumValue = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
