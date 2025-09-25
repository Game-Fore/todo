using System.Windows.Forms;

namespace Todolist
{
    public partial class Form1 : Form
    {
        private List<string> tasks = new List<string>();
        public Form1()
        {
            InitializeComponent();


            UpdateTasksList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewTasks();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteTasks();
        }

        private void AddNewTasks()
        {
            string newtasks = textBox2.Text.Trim();

            if (newtasks.Length != 0)
            {
                tasks.Add(newtasks);

                textBox2.Clear();
                UpdateTasksList();
            }
        }

        private void DeleteTasks()
        {
            if (listBox1.SelectedIndex != -1)
            {
                int SelectedIndex = listBox1.SelectedIndex;
                tasks.RemoveAt(SelectedIndex);
                UpdateTasksList();
            }
        }

        private void UpdateTasksList()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = tasks;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
