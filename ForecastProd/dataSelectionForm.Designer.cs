namespace ForecastProd
{
    partial class DataSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.firstCellTextBox = new System.Windows.Forms.TextBox();
            this.lastCellTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.irrTextBox = new System.Windows.Forms.TextBox();
            this.instructionsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите диапазон ячеек с данными";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(67, 88);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // firstCellTextBox
            // 
            this.firstCellTextBox.Location = new System.Drawing.Point(56, 29);
            this.firstCellTextBox.Name = "firstCellTextBox";
            this.firstCellTextBox.Size = new System.Drawing.Size(40, 20);
            this.firstCellTextBox.TabIndex = 2;
            this.firstCellTextBox.Text = "B7";
            this.instructionsToolTip.SetToolTip(this.firstCellTextBox, "Левая верхняя ячейка с данными");
            // 
            // lastCellTextBox
            // 
            this.lastCellTextBox.Location = new System.Drawing.Point(118, 29);
            this.lastCellTextBox.Name = "lastCellTextBox";
            this.lastCellTextBox.Size = new System.Drawing.Size(40, 20);
            this.lastCellTextBox.TabIndex = 3;
            this.lastCellTextBox.Text = "C26";
            this.instructionsToolTip.SetToolTip(this.lastCellTextBox, "Правая нижняя ячейка с данными");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(102, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "НИЗ:";
            this.instructionsToolTip.SetToolTip(this.label3, "Начальные извлекаемые запасы");
            // 
            // irrTextBox
            // 
            this.irrTextBox.Location = new System.Drawing.Point(92, 55);
            this.irrTextBox.Name = "irrTextBox";
            this.irrTextBox.Size = new System.Drawing.Size(66, 20);
            this.irrTextBox.TabIndex = 6;
            this.irrTextBox.Text = "C2";
            this.instructionsToolTip.SetToolTip(this.irrTextBox, "Ячейка с НИЗ");
            // 
            // instructionsToolTip
            // 
            this.instructionsToolTip.AutoPopDelay = 5000;
            this.instructionsToolTip.InitialDelay = 250;
            this.instructionsToolTip.ReshowDelay = 100;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(161, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "?";
            this.instructionsToolTip.SetToolTip(this.label4, "Необходимо указать угловые ячейки с данными.\r\nКоличество строк с данными в докуме" +
        "нте должно быть не менее 3,\r\nа количество столбцов равное 2.");
            // 
            // DataSelectionForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 118);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.irrTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lastCellTextBox);
            this.Controls.Add(this.firstCellTextBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DataSelectionForm";
            this.Text = "Данные";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox firstCellTextBox;
        private System.Windows.Forms.TextBox lastCellTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox irrTextBox;
        private System.Windows.Forms.ToolTip instructionsToolTip;
        private System.Windows.Forms.Label label4;
    }
}