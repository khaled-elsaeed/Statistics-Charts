using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace Statistics_Charts
{
    public partial class Boxplot : Form
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
        public Boxplot()
        {
            InitializeComponent();
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }


        private void Boxplot_Load(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            int i, j, temp;
            List<string> array = new List<string>();
            List<int> arrayInt = new List<int>();

            array.AddRange(textBox2.Text.Split(',').Select(txt => txt.Trim()).ToArray());
            arrayInt = array.Select(s => int.Parse(s)).ToList(); //Converting string array to int array

            //for (i = 1; i < array.Count(); i++)
            //{
            //    j = i;

            //    while (j > 0 && arrayInt[j - 1] > arrayInt[j])
            //    {
            //        temp = arrayInt[j];
            //        arrayInt[j] = arrayInt[j - 1];
            //        arrayInt[j - 1] = temp;
            //        j--;
            //    }

            //}


            //for (i = 0; i < array.Count(); i++)
            //{
            //    listBox1.Items.Add(arrayInt[i]);
            //}
            int x = listBox1.Items.Count;

            double c1 = ((x + 1.0) * 25.0 * 1.0) / 100.0;
            double c2 = ((x + 1.0) * 25.0 * 2.0) / 100.0;
            double c3 = ((x + 1.0) * 25.0 * 3.0) / 100.0;

            double q1 = ((double)arrayInt[(int)Math.Floor(c1) - 1] + (double)arrayInt[(int)Math.Floor(c1)]) / 2;
            double q2 = ((double)arrayInt[(int)Math.Floor(c2) - 1] + (double)arrayInt[(int)Math.Floor(c2)]) / 2;
            double q3 = ((double)arrayInt[(int)Math.Floor(c3) - 1] + (double)arrayInt[(int)Math.Floor(c3)]) / 2;
            double iqr = q3 - q2;
            double upperwhisker = iqr * 1.5;
            double lowerwhisker = iqr * 1.5;
            //double z = 0;
            //List<double> itt = new List<double>();
            //foreach (double item in listBox1.Items)
            //{
            //    if((item > upperwhisker) || (item < lowerwhisker))
            //    {

            //        listBox1.Items.Add(item);
            //    }
            //}

            double z = 0;
            chart1.Series["Series1"].Points.AddXY(z, lowerwhisker, q1, q2, q3, upperwhisker);


            label3.Text = q1.ToString();
            label4.Text = q2.ToString();
            label5.Text = q3.ToString();
            label6.Text = iqr.ToString();
            z++;
            textBox2.Text = null;
            listBox1.Items.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i, j, temp;
            List<string> array = new List<string>();
            List<int> arrayInt = new List<int>();

            array.AddRange(textBox2.Text.Split(',').Select(txt => txt.Trim()).ToArray());
            arrayInt = array.Select(s => int.Parse(s)).ToList(); //Converting string array to int array

            for (i = 1; i < array.Count(); i++)
            {
                j = i;

                while (j > 0 && arrayInt[j - 1] > arrayInt[j])
                {
                    temp = arrayInt[j];
                    arrayInt[j] = arrayInt[j - 1];
                    arrayInt[j - 1] = temp;
                    j--;
                }

            }


            for (i = 0; i < array.Count(); i++)
            {
                listBox1.Items.Add(arrayInt[i]);
            }
        }
    }
}
