using RSEM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuPuzzle
{
    public class NumHandler
    {
        #region 文件操作

        /// <summary>
        /// 按顺序获取ID编号组
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Dictionary<string, double> SetIDPair(int count)
        {
            Dictionary<string, double> IDPairDic = new Dictionary<string, double>();
            for (int i = 1; i <= count; i++)
            {
                for (int j = i + 1; j <= count; j++)
                {
                    IDPairDic.Add(i + "-" + j, 0);
                }
            }
            return IDPairDic;
        }

        /// <summary>
        /// 获取执行路径
        /// </summary>
        /// <returns></returns>
        public static string GetExecuteDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.ToString());
        }

        #region 读取InputFile目录文件

        /// <summary>
        /// 读文件返回矩阵
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<String> ReadFile()
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.ToString());
            string folderDir = Path.Combine(directory, "inputFile");
            string dataPath = Path.Combine(folderDir, "SU_DOKU_QU.txt");
            if (!File.Exists(dataPath))
            {
                MessageBox.Show("抱歉，DataFile目录中不存在文件SU_DOKU_QU.txt！");
                return null;
            }
            List<int[]> list = new List<int[]>();
            List<String> srcData = new List<String>();
            StringBuilder numBuider = new StringBuilder();
            try
            {
                using (StreamReader sr = new StreamReader(dataPath))
                {
                    int num = 0;
                    while (!sr.EndOfStream)
                    {
                        string lineData = sr.ReadLine();
                        if (String.IsNullOrEmpty(lineData)) { continue; }
                        //保证在遍历下一组时已添加前一组数据
                        if (num % 9 == 0 && num != 0)
                        {
                            srcData.Add(numBuider.ToString());
                            numBuider.Clear();
                            num = 0;
                        }
                        if (lineData.Contains("[") || lineData.Contains("]")) continue;
                        numBuider.Append(lineData);
                        numBuider.Append(",");
                        num++;
                    }
                    //添加最后一次读取的数据
                    srcData.Add(numBuider.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("系统异常,请检查资源文件是否正确！");
                return null;
            }
            return srcData;
        }
        #endregion

        #region 存储数据=============
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="matrix"></param>
        public static void SaveData(string resValue)
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.ToString());
            string folderDir = Path.Combine(directory, @"outputFile");
            string dataPath = Path.Combine(folderDir, "SU_DOKU_AN.txt");
            StringBuilder sbStr = new StringBuilder();
            try
            {
                using (StreamWriter sw = new StreamWriter(dataPath, true))
                {
                    sbStr.Append(resValue);
                    sw.Write(sbStr.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("系统异常，保存数据错误！");
            }
        }

        /// <summary>
        /// 保存临时数据
        /// </summary>
        /// <param name="resValue"></param>
        public static void SaveTempData(string resValue, string index)
        {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.ToString());
            string folderDir = Path.Combine(directory, @"outputFile");
            string dataPath = Path.Combine(folderDir, "TEMP_SU_DOKU_AN_" + index + ".txt");
            StringBuilder sbStr = new StringBuilder();
            try
            {
                using (StreamWriter sw = new StreamWriter(dataPath, true))
                {
                    sbStr.Append(resValue);
                    sw.Write(sbStr.ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("系统异常，保存数据错误！");
            }
        }
        #endregion

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void deleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location.ToString());
            string folderDir = Path.Combine(directory, @"outputFile");
            string dataPath = Path.Combine(folderDir, fileName);
            if (File.Exists(dataPath))
            {
                File.Delete(dataPath);
            }
        }
        #endregion


        #region 基本数据处理

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="srcData"></param>
        /// <returns></returns>
        public static bool checkData(string srcData)
        {
            if (string.IsNullOrEmpty(srcData)) {  return false; }
            String[] numDataArr = srcData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (numDataArr.Length != 81){ return false; }
            bool isOver = false;
            for (int i = 0; i < numDataArr.Length; i++)
            {
                bool checkNumRes = checkNumber(numDataArr[i]);
                if (!checkNumRes)
                {
                    isOver = true;
                    break;
                }
            }
            if (isOver) { return false; }
            return true;
        }

        /// <summary>
        /// 将string数组转化为int数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] StringToInt(string[] array)
        {
            if (array == null || array.Length == 0) return null;
            int[] dataArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                dataArray[i] = Convert.ToInt32(array[i]);
            }
            return dataArray;
        }

                /// <summary>
        /// 输出结果
        /// </summary>
        /// <returns></returns>
        public static String getResult(NumElement[][][][] sudokuData)
        {
            StringBuilder resBuider = new StringBuilder();
            for (int m = 0; m < 3; m++)
            {
                for (int i = 0; i < 3; i++)
                {
                    StringBuilder rowBuider = new StringBuilder();
                    for (int n = 0; n < 3; n++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            var cellValue = sudokuData[m][n][i][j].NumValue;
                            rowBuider.Append(cellValue).Append(",");
                        }
                    }
                    string tempRowVal = rowBuider.ToString().TrimEnd(',');
                    resBuider.Append(tempRowVal).AppendLine();
                }
            }
            return resBuider.ToString();
        }

        /// <summary>
        /// 校验数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool checkNumber(string str)
        {
            Regex reg = new Regex("^[0-9]$");
            Match ma = reg.Match(str);
            if (ma.Success)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }
        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowProcess(TextBox displayControl, string msg)
        {
            lock (typeof(NumHandler))
            {
                displayControl.AppendText("******** " + msg + "\r\n\r\n");
            }
        }
        #endregion

    }
}
