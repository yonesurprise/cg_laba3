using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Task3 : Form
    {
        private Form1 main;
        int gr1x, gr1y;
        int gr2x, gr2y;
        int gr3x, gr3y;
        Color gr1 = Color.White, gr2 = Color.Blue, gr3 = Color.Red;
        private Graphics g;

        enum ClickStatus
        {
            First,
            Second,
            Third,
        }

        ClickStatus status = ClickStatus.First;
        private Bitmap bit;

        (double, double) interpolation(double x1, double y1, double x2, double y2)
        {
            double h;
            if (y2 - y1 != 0)
                h = (x2 - x1) / (y2 - y1);
            else
                h = 0;
            (double, double) ur = (h, -y1 * h + x1);
            return ur;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            g.Clear(Color.White);
            if (status == ClickStatus.First)
            {
                gr1x = e.X; gr1y = e.Y;
                status = ClickStatus.Second;
                label1.Text = "1";

                return;
            }
            if (status == ClickStatus.Second)
            {
                gr2x = e.X; gr2y = e.Y;
                status = ClickStatus.Third;
                label1.Text = "2";
                return;
            }
            if (status == ClickStatus.Third)
            {
                gr3x = e.X; gr3y = e.Y;
                status = ClickStatus.First;
                label1.Text = "3";
            }

            if (gr1x >= pictureBox1.Width || gr1x < 0 || gr2x >= pictureBox1.Width || gr2x < 0 || gr3x >= pictureBox1.Width || gr3x < 0 ||
                gr1y >= pictureBox1.Height || gr1y < 0 || gr2y >= pictureBox1.Height || gr2y < 0 || gr3y >= pictureBox1.Height || gr3y < 0)
            {
                MessageBox.Show("Неверные координаты");
                return;
            }

            g.Clear(Color.White);
            List<(int, int, Color)> sort = new List<(int, int, Color)>();
            sort.Add((gr1y, gr1x, gr1)); sort.Add((gr2y, gr2x, gr2)); sort.Add((gr3y, gr3x, gr3));
            sort.Sort();
            Console.WriteLine(sort[0] + " " + sort[1] + " " + sort[2]);
            gr1x = sort[0].Item2; gr1y = sort[0].Item1; gr1 = sort[0].Item3;
            gr3x = sort[1].Item2; gr3y = sort[1].Item1; gr3 = sort[1].Item3;
            gr2x = sort[2].Item2; gr2y = sort[2].Item1; gr2 = sort[2].Item3;
            //уравнения сторон
            double h1 = (gr2x - gr1x) / (double)(gr2y - gr1y);
            (double, double) ur1_2 = interpolation(gr1x, gr1y, gr2x, gr2y);
            double h2 = (gr3x - gr1x) / (double)(gr3y - gr1y);
            (double, double) ur1_3 = interpolation(gr1x, gr1y, gr3x, gr3y);
            double h3 = (gr3x - gr2x) / (double)(gr3y - gr2y);
            (double, double) ur2_3 = interpolation(gr2x, gr2y, gr3x, gr3y);
            //уравнения цветов стороны 1-2
            if (gr2x - gr1x != 0)
                h1 = (gr2.R - gr1.R) / (double)(gr2x - gr1x);
            else
                h1 = 0;
            (double, double) r1 = (h1, -gr1x * h1 + gr1.R);
            if (gr2x - gr1x != 0)
                h2 = (gr2.G - gr1.G) / (double)(gr2x - gr1x);
            else
                h2 = 0;
            (double, double) g1 = (h2, -gr1x * h2 + gr1.G);
            if (gr2x - gr1x != 0)
                h3 = (gr2.B - gr1.B) / (double)(gr2x - gr1x);
            else
                h3 = 0;
            (double, double) b1 = (h3, -gr1x * h3 + gr1.B);
            //уравнения цветов стороны 1-3
            if (gr3x - gr1x != 0)
                h1 = (gr3.R - gr1.R) / (double)(gr3x - gr1x);
            else
                h1 = 0;
            (double, double) r2 = (h1, -gr1x * h1 + gr1.R);
            if (gr3x - gr1x != 0)
                h2 = (gr3.G - gr1.G) / (double)(gr3x - gr1x);
            else
                h2 = 0;
            (double, double) g2 = (h2, -gr1x * h2 + gr1.G);
            if (gr3x - gr1x != 0)
                h3 = (gr3.B - gr1.B) / (double)(gr3x - gr1x);
            else
                h3 = 0;
            (double, double) b2 = (h3, -gr1x * h3 + gr1.B);
            //уравнения цветов стороны 2-3
            if (gr3x - gr2x != 0)
                h1 = (gr3.R - gr2.R) / (double)(gr3x - gr2x);
            else
                h1 = 0;
            (double, double) r3 = (h1, -gr2x * h1 + gr2.R);
            if (gr3x - gr2x != 0)
                h2 = (gr3.G - gr2.G) / (double)(gr3x - gr2x);
            else
                h2 = 0;
            (double, double) g3 = (h2, -gr2x * h2 + gr2.G);
            if (gr3x - gr1x != 0)
                h3 = (gr3.B - gr2.B) / (double)(gr3x - gr2x);
            else
                h3 = 0;
            (double, double) b3 = (h3, -gr2x * h3 + gr2.B);
            //Console.WriteLine(r1 + " " + g1 + " " + b1);            
            for (int i = gr1y + 1; i <= gr3y; i++)
            {
                double x1 = ur1_2.Item1 * i + ur1_2.Item2, x2 = (ur1_3.Item1 * i + ur1_3.Item2);
                Color tekcl = Color.FromArgb((int)(r1.Item1 * x1 + r1.Item2), (int)(g1.Item1 * x1 + g1.Item2),
                    (int)(b1.Item1 * x1 + b1.Item2));
                Color tekc2 = Color.FromArgb((int)(r2.Item1 * x2 + r2.Item2), (int)(g2.Item1 * x2 + g2.Item2),
                    (int)(b2.Item1 * x2 + b2.Item2));
                Color tekc = Color.FromArgb((tekcl.R + tekc2.R) / 2, (tekcl.G + tekc2.G) / 2, (tekcl.B + tekc2.B) / 2);
                Pen tekp = new Pen(tekc, 1);
                //Console.WriteLine(i + " " + (float)(ur1_2.Item1 * i + ur1_2.Item2) + " " + (float)(ur1_3.Item1 * i + ur1_3.Item2));
                (double, double) urR = interpolation(tekcl.R, x1, tekc2.R, x2);
                (double, double) urG = interpolation(tekcl.G, x1, tekc2.G, x2);
                (double, double) urB = interpolation(tekcl.B, x1, tekc2.B, x2);
                for (int j = (int)x1; j <= x2; j++)
                {
                    bit.SetPixel(j, i, Color.FromArgb((int)(urR.Item1 * j + urR.Item2), (int)(urG.Item1 * j + urG.Item2),
                        (int)(urB.Item1 * j + urB.Item2)));

                }
                //g.DrawLine(tekp, (float)(x1), i, (float)(x2), i);
            }
            for (int i = gr3y + 1; i < gr2y; i++)
            {
                double x1 = ur1_2.Item1 * i + ur1_2.Item2, x2 = (ur2_3.Item1 * i + ur2_3.Item2);
                Color tekcl = Color.FromArgb((int)(r1.Item1 * x1 + r1.Item2), (int)(g1.Item1 * x1 + g1.Item2),
                    (int)(b1.Item1 * x1 + b1.Item2));
                Color tekc3 = Color.FromArgb((int)(r3.Item1 * x2 + r3.Item2), (int)(g3.Item1 * x2 + g3.Item2),
                    (int)(b3.Item1 * x2 + b3.Item2));
                Color tekc = Color.FromArgb((tekcl.R + tekc3.R) / 2, (tekcl.G + tekc3.G) / 2, (tekcl.B + tekc3.B) / 2);
                Pen tekp = new Pen(tekc, 1);
                (double, double) urR = interpolation(tekcl.R, x1, tekc3.R, x2);
                (double, double) urG = interpolation(tekcl.G, x1, tekc3.G, x2);
                (double, double) urB = interpolation(tekcl.B, x1, tekc3.B, x2);
                for (int j = (int)x1; j <= x2; j++)
                {
                    bit.SetPixel(j, i, Color.FromArgb((int)(urR.Item1 * j + urR.Item2), (int)(urG.Item1 * j + urG.Item2),
                        (int)(urB.Item1 * j + urB.Item2)));
                }
                //g.DrawLine(tekp, (float)(x1), i, (float)(x2), i);
            }
            pictureBox1.Invalidate();

        }

        public Task3(Form1 form1)
        {
            main = form1;
            InitializeComponent();
            bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bit;
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            ((Bitmap)pictureBox1.Image).SetPixel(gr1x, gr1y, gr1);
            ((Bitmap)pictureBox1.Image).SetPixel(gr2x, gr2y, gr2);
            ((Bitmap)pictureBox1.Image).SetPixel(gr3x, gr3y, gr3);
            pictureBox1.Invalidate();
            button1.BackColor = gr1;
            button2.BackColor = gr2;
            button3.BackColor = gr3;
        }


        private void task3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                gr1 = colorDialog1.Color;
                ((Bitmap)pictureBox1.Image).SetPixel(gr1x, gr1y, gr1);
                pictureBox1.Invalidate();
                button1.BackColor = colorDialog1.Color;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                gr2 = colorDialog1.Color;
                ((Bitmap)pictureBox1.Image).SetPixel(gr2x, gr2y, gr2);
                pictureBox1.Invalidate();
                button2.BackColor = colorDialog1.Color;
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                gr3 = colorDialog1.Color;
                ((Bitmap)pictureBox1.Image).SetPixel(gr3x, gr3y, gr3);
                pictureBox1.Invalidate();
                button3.BackColor = colorDialog1.Color;
            }
        }
    }
}
