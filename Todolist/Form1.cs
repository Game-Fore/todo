using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Todolist
{
    //класс для формы с базовыми функциями
    public partial class Form1 : Form
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private TextBox textBoxDescription;
        private Button buttonSaveDescription;
        private Label labelDeadline;
        private Button buttonSetDeadline;

        public Form1()
        {
            InitializeComponent();
            InitializeDescriptionPanel();

            tasks.Add(new TaskItem("Пример задачи 1", "Это описание первой задачи"));
            tasks.Add(new TaskItem("Пример задачи 2", ""));

            UpdateTasksList();
            checkedListBox1.KeyDown += checkedListBox1_KeyDown;
            textBox1.KeyPress += textBox1_KeyPress;
            checkedListBox1.MouseDown += checkedListBox1_MouseDown;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            textBoxDescription.TextChanged += textBoxDescription_TextChanged;
        }



        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                // Загружаем описание выбранной задачи
                TaskItem selectedTask = tasks[checkedListBox1.SelectedIndex];
                textBoxDescription.Text = selectedTask.Description;
                textBoxDescription.Enabled = true;
                buttonSaveDescription.Enabled = true;

                // Обновляем отображение дедлайна (если добавили)
                if (labelDeadline != null)
                {
                    if (selectedTask.Deadline.HasValue)
                    {
                        labelDeadline.Text = $"Дедлайн: {selectedTask.Deadline.Value:dd.MM.yyyy HH:mm}";
                        if (selectedTask.IsOverdue)
                        {
                            labelDeadline.ForeColor = Color.Red;
                            labelDeadline.Text += " ⛔️ ПРОСРОЧЕНО";
                        }
                        else
                        {
                            labelDeadline.ForeColor = Color.Green;
                        }
                    }
                    else
                    {
                        labelDeadline.Text = "Дедлайн: не установлен";
                        labelDeadline.ForeColor = Color.Gray;
                    }
                }
            }
            else
            {
                // Сбрасываем если ничего не выбрано
                textBoxDescription.Text = "";
                textBoxDescription.Enabled = false;
                buttonSaveDescription.Enabled = false;

                if (labelDeadline != null)
                {
                    labelDeadline.Text = "Дедлайн: не установлен";
                    labelDeadline.ForeColor = Color.Gray;
                }
            }
        }
        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                tasks[checkedListBox1.SelectedIndex].Description = textBoxDescription.Text;
            }
        }
        private void ButtonSaveDescription_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                tasks[checkedListBox1.SelectedIndex].Description = textBoxDescription.Text;
                MessageBox.Show("Описание сохранено!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewTasks();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        private void AddNewTasks()
        {
            string newTaskTitle = textBox1.Text.Trim();

            if (newTaskTitle.Length != 0)
            {
                TaskItem newTask = new TaskItem(newTaskTitle);
                tasks.Add(newTask);
                textBox1.Clear();
                UpdateTasksList();
            }
        }

        private void DeleteTask()
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                int selectedIndex = checkedListBox1.SelectedIndex;
                tasks.RemoveAt(selectedIndex);
                UpdateTasksList();

                textBoxDescription.Text = "";
                textBoxDescription.Enabled = false;
                buttonSaveDescription.Enabled = false;
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateTasksList()
        {
            checkedListBox1.DataSource = null;
            checkedListBox1.DataSource = tasks;
            for (int i = 0; i < tasks.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, tasks[i].IsCompleted);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AddNewTasks();
                e.Handled = true;
            }
        }

        private void checkedListBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteTask();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && checkedListBox1.SelectedIndex != -1)
            {
                EditTask(checkedListBox1.SelectedIndex);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.D && e.Control) // ctrl + d для дедлайна
            {
                if (checkedListBox1.SelectedIndex != -1)
                {
                    SetDeadline(checkedListBox1.SelectedIndex);
                    e.Handled = true;
                }
            }
        }

        private void checkedListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2 && e.Button == MouseButtons.Left)
            {
                int index = checkedListBox1.IndexFromPoint(e.Location);
                if (index != -1)
                {
                    EditTask(index);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                int index = checkedListBox1.IndexFromPoint(e.Location);
                if (index != -1)
                {
                    checkedListBox1.SelectedIndex = index;
                    var contextMenu = new ContextMenuStrip();
                    contextMenu.Items.Add("Установить дедлайн", null, (s, args) => SetDeadline(index));
                    contextMenu.Items.Add("Редактировать", null, (s, args) => EditTask(index));
                    contextMenu.Items.Add("Удалить", null, (s, args) => DeleteTask());
                    contextMenu.Show(checkedListBox1, e.Location);
                }
            }
        }

        private void EditTask(int index)
        {
            TaskItem currentTask = tasks[index];
            string editedTitle = Microsoft.VisualBasic.Interaction.InputBox(
                "Редактировать задачу:", "Редактирование", currentTask.Title);

            if (!string.IsNullOrEmpty(editedTitle))
            {
                tasks[index].Title = editedTitle;
                UpdateTasksList();
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index >= 0 && e.Index < tasks.Count)
            {
                tasks[e.Index].IsCompleted = (e.NewValue == CheckState.Checked);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SetDeadline(int taskIndex)
        {
            if (taskIndex < 0 || taskIndex >= tasks.Count) return;

            TaskItem task = tasks[taskIndex];

            using (var deadlineForm = new DeadlineForm(task.Deadline))
            {
                if (deadlineForm.ShowDialog() == DialogResult.OK)
                {
                    task.Deadline = deadlineForm.SelectedDeadline;
                    UpdateTasksList();
                }
            }
        }
        private void ButtonSetDeadline_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                SetDeadline(checkedListBox1.SelectedIndex);
            }
        }
        

        // Для проверки просроченных
        private void CheckOverdueTasks()
        {
            foreach (var task in tasks)
            {
                if (task.IsOverdue)
                {

                }
            }
        }
    }

    //класс для описания 
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }  // ← Правильное название

        public TaskItem(string title, string description = "")
        {
            Title = title;
            Description = description;
            IsCompleted = false;
            Deadline = null;
        }

        //для отображения в CheckedListBox
        public override string ToString()
        {
            if (Deadline.HasValue)  
            {
                string status = IsCompleted ? "🆒 " : "";
                string overdue = Deadline.Value < DateTime.Now ? "⛔️" : "";
                return $"{status}{overdue}{Title} (до {Deadline.Value:dd.MM.yyyy})";
            }
            else
            {
                string status = IsCompleted ? "🆒 " : "";
                return $"{status}{Title}";
            }
        }

        //свойство для чтения для проверки просроченности
        public bool IsOverdue => Deadline.HasValue && Deadline.Value < DateTime.Now && !IsCompleted;
    }

    //класс для дедлайна
    public partial class DeadlineForm : Form
    {
        public DateTime? SelectedDeadline { get; private set; }
        private DateTimePicker dateTimePicker;
        private CheckBox checkBoxSetDeadline;
        private Button buttonOK;
        private Button buttonCancel;

        public DeadlineForm(DateTime? currentDeadline)
        {
            InitializeComponent();
            SelectedDeadline = currentDeadline;

            if (currentDeadline.HasValue)
            {
                dateTimePicker.Value = currentDeadline.Value;
                checkBoxSetDeadline.Checked = true;
            }
        }
        private void InitializeComponent()
        {
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.checkBoxSetDeadline = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // dateTimePicker
            this.dateTimePicker.Location = new System.Drawing.Point(20, 40);
            this.dateTimePicker.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimePicker.ShowUpDown = true;

            // checkBoxSetDeadline
            this.checkBoxSetDeadline.Location = new System.Drawing.Point(20, 10);
            this.checkBoxSetDeadline.Size = new System.Drawing.Size(150, 24);
            this.checkBoxSetDeadline.Text = "Установить дедлайн";
            this.checkBoxSetDeadline.CheckedChanged += new System.EventHandler(this.checkBoxSetDeadline_CheckedChanged);

            // buttonOK
            this.buttonOK.Location = new System.Drawing.Point(20, 80);
            this.buttonOK.Size = new System.Drawing.Size(90, 25);
            this.buttonOK.Text = "OK";
            this.buttonOK.DialogResult = DialogResult.OK;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);

            // buttonCancel
            this.buttonCancel.Location = new System.Drawing.Point(120, 80);
            this.buttonCancel.Size = new System.Drawing.Size(90, 25);
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.DialogResult = DialogResult.Cancel;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            // DeadlineForm
            this.ClientSize = new System.Drawing.Size(240, 120);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.checkBoxSetDeadline);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Text = "Установить дедлайн";
            this.ResumeLayout(false);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (checkBoxSetDeadline.Checked)
            {
                SelectedDeadline = dateTimePicker.Value;
            }
            else
            {
                SelectedDeadline = null;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        private void checkBoxSetDeadline_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker.Enabled = checkBoxSetDeadline.Checked;
            if (!checkBoxSetDeadline.Checked)
            {
                SelectedDeadline = null;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}