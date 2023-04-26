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
    public partial class Dotsplot : Form
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

        public Dotsplot()
        {
            InitializeComponent();
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Dotsplot_Load(object sender, EventArgs e)
        {
            table.Columns.Add("X", Type.GetType("System.String"));
            table.Columns.Add("Y", Type.GetType("System.Decimal"));
            dataGridView1.DataSource = table;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessge(this.Handle, 0x112, 0xf012, 0);
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

        private void label2_Click(object sender, EventArgs e)
        {

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
                Console.WriteLine(dataGridView1.Rows.Count);
                Console.WriteLine("X" + dataGridView1.Rows[0].Cells[0].Value);
                Console.WriteLine("Y" + dataGridView1.Rows[0].Cells[1].Value);
                string x = "0";
                double y = 0;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    x = (dataGridView1.Rows[i].Cells[0].Value.ToString());
                    y = double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    chart1.Series[0].Points.AddXY(x, y);

                }

                chart1.Visible = true;



                Decimal[] columnData = (from DataGridViewRow row in dataGridView1.Rows
                                        where row.Cells[1].FormattedValue.ToString() != string.Empty
                                        select Convert.ToDecimal(row.Cells[1].FormattedValue)).ToArray();

                // Sum Value
                sumtxtbox.Text = columnData.Sum().ToString();
                // Max Value
                maxtxtbox.Text = columnData.Max().ToString();
                // Min Value
                mintxtbox.Text = columnData.Min().ToString();
                // Average Value
                avgtxtbox.Text = (Math.Round(columnData.Average() * 1000) / 1000).ToString();

                groupBox1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter The Missing Fields ", "Error");
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form newfrm = new Dotsplot();
            this.Hide();
            newfrm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                index = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(index);
                //textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Name = "Series1";
                Console.WriteLine(dataGridView1.Rows.Count);
                Console.WriteLine("X" + dataGridView1.Rows[0].Cells[0].Value);
                Console.WriteLine("Y" + dataGridView1.Rows[0].Cells[1].Value);
                string x = "0";
                double y = 0;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    x = (dataGridView1.Rows[i].Cells[0].Value.ToString());
                    y = double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    chart1.Series[0].Points.AddXY(x, y);
                    chart1.Visible = true;
                    //chart1.Series[textBox1.Text].Name = textBox1.Text;

                    Decimal[] columnData = (from DataGridViewRow row in dataGridView1.Rows
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
        }
    }
}
