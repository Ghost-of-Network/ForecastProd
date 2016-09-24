using System;
using System.Windows.Forms;

namespace ForecastProd
{
    public partial class GraphForm : Form
    {
        public GraphForm(double[] xArray, double[] yArray, double[] approxY)
        {
            InitializeComponent();

            chart.Legends[0].Enabled = false;

            drawGraph(xArray, yArray, 0);
            drawGraph(xArray, approxY, 1);

            stepGraph(xArray, yArray);
        }

        /// <summary>
        /// Рисует график
        /// </summary>
        private void drawGraph(double[] xArray, double[] yArray, int numGrah)
        {
            for (int i = 0; i < xArray.Length; i++)
                chart.Series[numGrah].Points.AddXY(Math.Round(xArray[i], 4), Math.Round(yArray[i], 4));
        }

        /// <summary>
        /// Вычисляет шаг сетки графика
        /// </summary>
        public void stepGraph(double[] xArray, double[] yArray)
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chartArea1.AxisX.Interval = Math.Round(Сalc.avg(yArray), 3) / 100;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.Interval = Math.Round(Сalc.avg(yArray), 3) / 7;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(chartArea1);
        }
    }
}
