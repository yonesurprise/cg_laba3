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
    public partial class Form1 : Form
    {
        private Task3 task3;
        private Task2 task2;
        private Task1 task1;

        public Form1()
        {
            InitializeComponent();
            task3 = new Task3(this);
            task3.Visible = false;
            task2 = new Task2();
            task2.Visible = false;
            task1 = new Task1();
            task1.Visible = false;
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            //task3.Visible = true;
            task3.Show();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            task1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            task2.Show();
        }
    }
}
