using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RSEM
{
    public class CustomMatrix
    {
        public int Row
        {
            get;
            set;
        }

        public int Column
        {
            get;
            set;
        }

        public double[,] Data
        {
            get;
            set;
        }
        public CustomMatrix()
        {

        }
        public CustomMatrix(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.Data = new Double[row, column];
        }
        public CustomMatrix(double[,] members)
        {
            this.Row = members.GetUpperBound(0) + 1;
            this.Column = members.GetUpperBound(1) + 1;
            this.Data = new double[this.Row, this.Column];
            Array.Copy(members, this.Data, this.Row * this.Column);
        }
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public double this[int r, int c]
        {
            get { return this.Data[r, c]; }
            set { this.Data[r, c] = value; }
        }


        #region 矩阵运算符重载
        /// <summary>
        /// 矩阵相加
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CustomMatrix operator +(CustomMatrix a, CustomMatrix b)
        {
            if (a == null || b == null) return null;
            if (a.Row != b.Row || a.Column != b.Column)
            {
                MessageBox.Show("注意！行数和列数不相等的矩阵不能相加...");
                return null;
            }
            CustomMatrix result = new CustomMatrix(a.Row, a.Column);
            for (int i = 0; i < a.Row; i++)
            {
                for (int j = 0; j < a.Column; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }
            return result;
        }
        /// <summary>
        /// 矩阵相减
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CustomMatrix operator -(CustomMatrix a, CustomMatrix b)
        {
            if (a == null || b == null) return null;
            return a + b * (-1);
        }
        /// <summary>
        /// 矩阵乘以实数
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static CustomMatrix operator *(CustomMatrix matrix, double factor)
        {
            if (matrix == null) return null;
            CustomMatrix result = new CustomMatrix(matrix.Row, matrix.Column);
            for (int i = 0; i < matrix.Row; i++)
                for (int j = 0; j < matrix.Column; j++)
                    matrix[i, j] = matrix[i, j] * factor;
            return matrix;
        }

        /// <summary>
        /// 实数乘以矩阵
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static CustomMatrix operator *(double factor, CustomMatrix matrix)
        {
            return matrix * factor;
        }

        /// <summary>
        /// 矩阵相乘
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CustomMatrix operator *(CustomMatrix a, CustomMatrix b)
        {
            if (a == null || b == null) return null;
            if (a.Column != b.Row)
            {
                MessageBox.Show("相乘矩阵维数不匹配。");
                return null;
            }
            CustomMatrix result = new CustomMatrix(a.Row, b.Column);
            for (int i = 0; i < a.Row; i++)
                for (int j = 0; j < b.Column; j++)
                    for (int k = 0; k < a.Column; k++)
                        result[i, j] += a[i, k] * b[k, j];
            return result;
        }
        #endregion
    }
}
