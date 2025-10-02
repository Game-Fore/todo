using System.Windows.Forms;

namespace Todolist
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            checkedListBox1 = new CheckedListBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(820, 405);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "Добавить\r\n";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(820, 459);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 2;
            button2.Text = "Удалить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 461);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(802, 27);
            textBox1.TabIndex = 4;
            // 
            // checkedListBox2
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(12, 12);
            checkedListBox1.Name = "checkedListBox2";
            checkedListBox1.Size = new Size(802, 422);
            checkedListBox1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 742);
            Controls.Add(checkedListBox1);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }


        private void InitializeDescriptionPanel()
        {
            // панель для описания
            Panel panelDescription = new Panel();
            panelDescription.Dock = DockStyle.Right;
            panelDescription.Width = 315;
            panelDescription.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelDescription);

            // Label для описания
            Label labelDesc = new Label();
            labelDesc.Text = "Описание задачи:";
            labelDesc.Location = new Point(10, 10);
            labelDesc.AutoSize = true;
            panelDescription.Controls.Add(labelDesc);

            // TextBox для описания
            textBoxDescription = new TextBox();
            textBoxDescription.Multiline = true;
            textBoxDescription.ScrollBars = ScrollBars.Vertical;
            textBoxDescription.Location = new Point(10, 35);
            textBoxDescription.Size = new Size(290, 365);
            textBoxDescription.Enabled = false;
            panelDescription.Controls.Add(textBoxDescription);

            // Кнопка сохранения
            buttonSaveDescription = new Button();
            buttonSaveDescription.Text = "Сохранить описание";
            buttonSaveDescription.Location = new Point(10, 195);
            buttonSaveDescription.Size = new Size(220, 25);
            buttonSaveDescription.Enabled = false;
            buttonSaveDescription.Click += ButtonSaveDescription_Click;
            panelDescription.Controls.Add(buttonSaveDescription);

            // Label для дедлайна
            labelDeadline = new Label();
            labelDeadline.Text = "Дедлайн: не установлен";
            labelDeadline.Location = new Point(10, 230);
            labelDeadline.AutoSize = true;
            labelDeadline.ForeColor = Color.Gray;
            panelDescription.Controls.Add(labelDeadline);

            // Кнопка установки дедлайна
            buttonSetDeadline = new Button();
            buttonSetDeadline.Text = "Установить дедлайн";
            buttonSetDeadline.Location = new Point(10, 255);
            buttonSetDeadline.Size = new Size(380, 25);
            buttonSetDeadline.Enabled = false;
            buttonSetDeadline.Click += ButtonSetDeadline_Click;
            panelDescription.Controls.Add(buttonSetDeadline);
        }
        

        #endregion
        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private CheckedListBox checkedListBox1;
        
    }
}