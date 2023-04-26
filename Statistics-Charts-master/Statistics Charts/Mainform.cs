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
    public partial class Mainform : Form
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
        public Mainform()
        {
            InitializeComponent();
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BarForm frm = new BarForm();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            BarForm frm = new BarForm();
            frm.Show();
            this.Hide();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessge(this.Handle, 0x112, 0xf012, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pie_Form Pie= new Pie_Form();
            this.Hide();
            Pie.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form pyrmplot = new Pyramidchart();
            pyrmplot.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form dotspl = new Dotsplot();
            dotspl.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form frm = new Boxplot();
            frm.Show();
            this.Hide();
        }
    }
}
