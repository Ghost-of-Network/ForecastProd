using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ForecastProd
{
    public partial class MainForm : Form
    {
        public string firstCell;
        public string lastCell;
        public string irrCell;
        private int numRowTable;
        private double[] actual;
        private double[] cumulativeProd;
        private double[] avgActual;
        private double[] lnWor;
        private double[] mined;
        private double incline;
        private double line;
        private double irr;
        private bool valueChange;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обрабатывает событие нажатия на кнопку "Рассчитать"
        /// </summary>
        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count - 1 >= 3)
            {
                if (inputData())
                {
                    avgActual = Сalc.avgProd(actual);
                    lnWor = Сalc.lnWor(avgActual);
                    mined = Сalc.mined(cumulativeProd, irr);

                    incline = Сalc.incline(lnWor, mined);
                    line = Сalc.line(lnWor, mined, incline);

                    outputData();

                    createGraphToolStripMenuItem.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Длинна массива данных должна быть не меньше трёх.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия на пункт меню "Загрузить из .xlsx"
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearForm();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы Excel(*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataSelectionForm DSF = new DataSelectionForm();
                DSF.Owner = this;
                DSF.ShowDialog();

                switch (DSF.DialogResult)
                {
                    case DialogResult.OK:
                        {
                            numRowTable = countNumberRow(firstCell, lastCell);

                            dataGridView.Rows.Clear();

                            ExcelIO readExcel = new ExcelIO(openFileDialog.FileName, 1);
                            irrTextBox.Text = readExcel.readCell(irrCell);
                            actual = readExcel.readRangeCell(firstCell, removeDigit(firstCell) + removeLetter(lastCell));
                            cumulativeProd = readExcel.readRangeCell(removeDigit(lastCell) + removeLetter(firstCell), lastCell);
                            readExcel.close(false);

                            dataGridView.Rows.Add(numRowTable);

                            for (int i = 0; i < numRowTable; i++)
                            {
                                dataGridView.Rows[i].Cells[0].Value = actual[i];
                                dataGridView.Rows[i].Cells[1].Value = cumulativeProd[i];
                            }
                            initializationArray();
                            saveData();
                        }
                        break;
                    case DialogResult.Abort:
                        MessageBox.Show("Неверный формат адреса ячейки.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    default:
                        return;
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Загружает данные из формы ввода в память
        /// </summary>
        private bool inputData()
        {
            numRowTable = dataGridView.Rows.Count - 1;
            initializationArray();

            try
            {
                irr = Convert.ToDouble(irrTextBox.Text);

                for (int i = 0; i < actual.Length; i++)
                {
                    actual[i] = Convert.ToDouble(dataGridView.Rows[i].Cells[0].Value);
                    cumulativeProd[i] = Convert.ToDouble(dataGridView.Rows[i].Cells[1].Value);
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Входные данные имеют неверный формат.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Выводит данные в форму
        /// </summary>
        private void outputData()
        {
            irrTextBox.Text = irr.ToString();
            for (int i = 0; i < actual.Length; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = actual[i];
                dataGridView.Rows[i].Cells[1].Value = cumulativeProd[i];
                dataGridView.Rows[i].Cells[2].Value = avgActual[i];
                dataGridView.Rows[i].Cells[3].Value = lnWor[i];
                dataGridView.Rows[i].Cells[4].Value = mined[i];
            }
            inclineValueTextBox.Text = incline.ToString();
            lineValueTextBox.Text = line.ToString();
        }

        /// <summary>
        /// Обрабатывает событие нажатия на кнопку "Форматировать данные"
        /// </summary>
        private void formatDataButton_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                formatDataButton.Visible = false;
                sourceDataButton.Visible = true;
                dataGridView.ReadOnly = true;
                formatData();
            }
        }

        /// <summary>
        /// Форматирует данные
        /// </summary>
        private void formatData()
        {
            irrTextBox.Text = Math.Round(irr).ToString();
            for (int i = 0; i < actual.Length; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = (Math.Round(actual[i], 2) * 100).ToString() + '%';
                dataGridView.Rows[i].Cells[1].Value = Math.Round(cumulativeProd[i]);
                dataGridView.Rows[i].Cells[2].Value = (Math.Round(avgActual[i], 2) * 100).ToString() + '%';
                dataGridView.Rows[i].Cells[3].Value = Math.Round(lnWor[i], 2);
                dataGridView.Rows[i].Cells[4].Value = (Math.Round(mined[i], 2) * 100).ToString() + '%';
            }
            inclineValueTextBox.Text = Math.Round(incline, 3).ToString();
            lineValueTextBox.Text = Math.Round(line, 3).ToString();
        }

        /// <summary>
        /// Обрабатывает события нажатия на кнопку "Исходные данные"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sourceDataButton_Click(object sender, EventArgs e)
        {
            sourceDataButton.Visible = false;
            formatDataButton.Visible = true;
            dataGridView.ReadOnly = false;

            outputData();
        }

        /// <summary>
        /// Обрабатывает события нажатия на пункт меню "Новый расчёт"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        /// <summary>
        /// Полностью очищает форму и готовит её для ввода данных
        /// </summary>
        private void clearForm()
        {
            dataGridView.Rows.Clear();
            irrTextBox.Clear();
            inclineValueTextBox.Clear();
            lineValueTextBox.Clear();
            sourceDataButton.Visible = false;
            formatDataButton.Visible = true;
            createGraphToolStripMenuItem.Visible = false;
            dataGridView.ReadOnly = false;
        }

        /// <summary>
        /// Инициализирует массивы для хранения данных
        /// </summary>
        private void initializationArray()
        {
            actual = new double[numRowTable];
            cumulativeProd = new double[numRowTable];
            avgActual = new double[numRowTable];
            lnWor = new double[numRowTable];
            mined = new double[numRowTable];
            incline = new double();
            line = new double();
            irr = new double();
        }

        /// <summary>
        /// Сохраняет посчитаные данные в память
        /// </summary>
        private bool saveData()
        {
            if (valueChange == true)
            {
                try
                {
                    irr = Convert.ToDouble(irrTextBox.Text);
                    for (int i = 0; i < actual.Length; i++)
                    {
                        actual[i] = Convert.ToDouble(dataGridView.Rows[i].Cells[0].Value);
                        cumulativeProd[i] = Convert.ToDouble(dataGridView.Rows[i].Cells[1].Value);
                        avgActual[i] = Convert.ToDouble(dataGridView.Rows[i].Cells[2].Value);
                        lnWor[i] = Convert.ToDouble(dataGridView.Rows[i].Cells[3].Value);
                        mined[i] = Convert.ToDouble(dataGridView.Rows[i].Cells[4].Value);
                    }
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Входные данные имеют неверный формат.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Нарисовать график"
        /// </summary>
        private void createGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveData();
            GraphForm GF = new GraphForm(mined, lnWor, Сalc.findApproximatedStraight(lnWor, mined));
            GF.Show();
        }

        /// <summary>
        /// Считает количество строк в диапазоне ячеек
        /// </summary>
        private int countNumberRow(string firstCell, string lastCell)
        {
            return Convert.ToInt32(removeLetter(lastCell)) - Convert.ToInt32(removeLetter(firstCell)) + 1; // вычисление количества строк в таблице
        }

        /// <summary>
        /// Удаляет из имени ячейки буквы
        /// </summary>
        private string removeLetter(string input)
        {
            string pattern = "[A-Za-z]";
            return Regex.Replace(input, pattern, String.Empty);
        }

        /// <summary>
        /// Удаляет из имени ячейки цмфры
        /// </summary>
        private string removeDigit(string input)
        {
            string pattern = "[0-9]";
            return Regex.Replace(input, pattern, String.Empty);
        }

        /// <summary>
        /// Нумерует строки в таблице
        /// </summary>
        private void dataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView.Rows[index].HeaderCell.Value = indexStr;
        }

        /// <summary>
        /// Обрабатывает событие нажатия на пункт меню "Сохранить в .xlsx"
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы Excel(*.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExcelIO writeExcel = new ExcelIO(saveFileDialog.FileName, 1);

                writeExcel.writeCell(2, 1, "НИЗ");
                writeExcel.writeCell(2, 2, irr);
                writeExcel.writeCell(2, 3, "тыс.т.");
                writeExcel.writeCell(3, 1, "ф-ия вида:  y = a * x + b ");
                writeExcel.writeCell(4, 1, "коэф-т a");
                writeExcel.writeCell(4, 2, incline);
                writeExcel.writeCell(4, 4, "коэф-т a");
                writeExcel.writeCell(4, 5, line);
                writeExcel.writeCell(5, 1, "Историч данные по %В (от 30% до макс).");
                writeExcel.writeCell(6, 1, "факт %В");
                writeExcel.writeCell(6, 2, "нак.доб");
                writeExcel.writeCell(6, 3, "%В_сред");
                writeExcel.writeCell(6, 4, "LN(WOR)");
                writeExcel.writeCell(6, 5, "отбНИЗ");
                writeExcel.writeColumn(7, 1, actual);
                writeExcel.writeColumn(7, 2, cumulativeProd);
                writeExcel.writeColumn(7, 3, avgActual);
                writeExcel.writeColumn(7, 4, lnWor);
                writeExcel.writeColumn(7, 5, mined);

                writeExcel.drawGraph(saveFileDialog.FileName, 1, firstCell, lastCell);

                writeExcel.close(true);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия на пункт меню "Выход"
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Обрабатывает событие изменения данных в таблице
        /// </summary>
        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            valueChange = true;
        }
    }
}
