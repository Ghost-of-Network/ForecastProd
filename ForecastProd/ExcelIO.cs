using System;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Text;

namespace ForecastProd
{
    class ExcelIO
    {
        Excel.Application ObjWorkExcel;
        Excel.Workbook ObjWorkBook;
        Excel.Workbooks ObjWorkBooks;
        Excel.Worksheet ObjWorkSheet;
        Excel.Sheets ObjSheets;

        private static Excel.Range excelRange;

        /// <summary>
        /// Конструктор, открывает выбранный файл
        /// </summary>
        public ExcelIO(string filename, int numSheets)
        {
            StringBuilder s = new StringBuilder();
            ObjWorkExcel = new Excel.Application();
            ObjWorkBook = ObjWorkExcel.Workbooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //открыть файл
            ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[numSheets];
        }

        /// <summary>
        /// Читает ячейку
        /// </summary>
        public string readCell(string nameCell)
        {
            return ObjWorkSheet.get_Range(nameCell, nameCell).Value2.ToString();
        }
        /// <summary>
        /// Читает диапазон ячеек
        /// </summary>
        public double[] readRangeCell(string firstCell, string lastCell)
        {
            int numRowTable = countNumberRow(firstCell, lastCell);
            double[] arrayDataCell = new double[numRowTable];
            excelRange = ObjWorkSheet.get_Range(lastCell, lastCell);

            for (int i = 0; i < numRowTable; i++)
            {
                arrayDataCell[i] = (double)ObjWorkSheet.Cells[excelRange.Row - numRowTable + 1 + i, excelRange.Column].Value2;
            }
            return arrayDataCell;
        }

        /// <summary>
        /// Считает количество строк в диапазоне ячеек
        /// </summary>
        private int countNumberRow(string firstCell, string lastCell)
        {
            string pattern = "[A-Za-z]";
            return Convert.ToInt32(Regex.Replace(lastCell, pattern, String.Empty)) - Convert.ToInt32(Regex.Replace(firstCell, pattern, String.Empty)) + 1; // вычисление количества строк в таблице
        }

        /// <summary>
        /// Записывает данные в одну ячейку
        /// </summary>
        public void writeCell(int row, int col, double data)
        {
            ObjWorkSheet.Cells[row, col].Value2 = data;
        }

        /// <summary>
        /// Записывает данные в одну ячейку
        /// </summary>
        public void writeCell(int row, int col, string data)
        {
            ObjWorkSheet.Cells[row, col].Value2 = data;
        }

        /// <summary>
        /// Записывает данные в диапазон ячеек
        /// </summary>
        public void writeRange(string filename, int numSheets, string firstCell, string lastCell, string data)
        {
            excelRange = ObjWorkSheet.get_Range(firstCell, lastCell);
            excelRange.Merge(Type.Missing);
            excelRange.HorizontalAlignment = Excel.Constants.xlCenter;
            excelRange.VerticalAlignment = Excel.Constants.xlCenter;
            excelRange.Font.Bold = true;
            excelRange.Value = data;
        }

        /// <summary>
        /// Записывает столбец данных
        /// </summary>
        public void writeColumn(int row, int col, double[] data)
        {
            for (int i = row; i < data.Length + row; i++)
            {
                ObjWorkSheet.Cells[i, col].Value2 = data[i - row];
                ObjWorkSheet.Cells[i, col].HorizontalAlignment = Excel.Constants.xlCenter;
            }
        }

        /// <summary>
        /// Рисует график
        public void drawGraph(string filename, int numSheets, string firstCell, string lastCell/*, double[] approxY*/)
        {
            ObjWorkBooks = ObjWorkExcel.Workbooks;
            ObjSheets = ObjWorkBook.Worksheets;

            Excel.ChartObjects chartsobjrcts = (Excel.ChartObjects)ObjWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject chartsobjrct = chartsobjrcts.Add(1, 360, 400, 250);

            Excel.Chart chart = chartsobjrct.Chart;
            Excel.SeriesCollection seriesCollection = (Excel.SeriesCollection)chart.SeriesCollection(Type.Missing);
            Excel.Series series = seriesCollection.NewSeries();

            Excel.Trendlines trendlines = (Excel.Trendlines)series.Trendlines(System.Type.Missing);
            Excel.Trendline newTrendline = trendlines.Add(Excel.XlTrendlineType.xlPower, 2, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, true, Type.Missing);
            newTrendline.Select();

            chart.Legend.Delete();

            series.XValues = ObjWorkSheet.get_Range("E7", "E26");
            series.Values = ObjWorkSheet.get_Range("D7", "D26");
            chart.ChartType = Excel.XlChartType.xlXYScatter;
        }

        /// <summary>
        /// Заакрывает файл
        /// </summary>
        public void close(bool b)
        {
            ObjWorkBook.Close(b, Type.Missing, Type.Missing);
            ObjWorkExcel.Quit();
        }
    }
}
