using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Statistics_Charts
{
    public partial class Pie_Form : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessge(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftftreact, //x-coordinate of upper-left corner
            int nTopReact, //y-coordinate of upper-left corner
            int nRightReact, //x-coordinate of lower-right corner
            int nBottomReact,// y-coordinate of lower-right corner
            int nWidthEllipse, //height of ellipse 
            int nHeightEllipse //width of ellipse
            );
        DataTable table = new DataTable("table");
        int index;
        public Pie_Form()
        {
            InitializeComponent();
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // user clicked yes

                Mainform frm1 = new Mainform();
                frm1.Show();
                this.Close();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessge(this.Handle, 0x112, 0xf012, 0);
        }

        private void Pie_Form_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Slice Name", Type.GetType("System.String"));
            table.Columns.Add("Slice Count", Type.GetType("System.Decimal"));
            dataGridView2.DataSource = table;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                table.Rows.Add(textBox2.Text, textBox3.Text.ToString());
                //textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Name = "Series1";
                Console.WriteLine(dataGridView2.Rows.Count);
                Console.WriteLine("Slice Name" + dataGridView2.Rows[0].Cells[0].Value);
                Console.WriteLine("Slice Count" + dataGridView2.Rows[0].Cells[1].Value);
                string x = "0";
                double y = 0;
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    x = (dataGridView2.Rows[i].Cells[0].Value.ToString());
                    y = double.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
                    chart1.Series[0].Points.AddXY(x, y);
                               
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter The Missing Fields ", "Error");
            }
            if (dataGridView2.CurrentCell != null)
            {
                chart1.Visible = true;

                Decimal[] columnData = (from DataGridViewRow row in dataGridView2.Rows
                                        where row.Cells[1].FormattedValue.ToString() != string.Empty
                                        select Convert.ToDecimal(row.Cells[1].FormattedValue)).ToArray();

                // Sum Value
                sumtxtbox.Text = columnData.Sum().ToString();
                // Max Value
                maxtxtbox.Text = columnData.Max().ToString();
                // Min Value
                mintxtbox.Text = columnData.Min().ToString();
                // Average Value
                avgtxtbox.Text = columnData.Average().ToString();

                groupBox1.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell != null)
            {
                index = dataGridView2.CurrentCell.RowIndex;
                dataGridView2.Rows.RemoveAt(index);
                //textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Name = "Series1";
                Console.WriteLine(dataGridView2.Rows.Count);
                Console.WriteLine("X" + dataGridView2.Rows[0].Cells[0].Value);
                Console.WriteLine("Y" + dataGridView2.Rows[0].Cells[1].Value);
                string x = "0";
                double y = 0;
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    x = (dataGridView2.Rows[i].Cells[0].Value.ToString());
                    y = double.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
                    chart1.Series[0].Points.AddXY(x, y);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form pieform = new Pie_Form();
            pieform.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
         
        }
    }
}
     

