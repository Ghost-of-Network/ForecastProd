using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ForecastProd
{
    public partial class DataSelectionForm : Form
    {
        public DataSelectionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Ok"
        /// Если данные введены корректно – отправляет их в родительскую форму
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+[0-9]+");
            Match firstMatch = regex.Match(firstCellTextBox.Text);
            Match lastMatch = regex.Match(lastCellTextBox.Text);
            Match irrMatch = regex.Match(irrTextBox.Text);

            if (firstMatch.Success && lastMatch.Success && irrMatch.Success)
            {
                MainForm main = this.Owner as MainForm;
                if (main != null)
                {
                    main.firstCell = firstCellTextBox.Text;
                    main.lastCell = lastCellTextBox.Text;
                    main.irrCell = irrTextBox.Text;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
            }
            this.Close();
        }
    }
}
