using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace snake
{
    public partial class Form1 : Form
    {
        public Form1(bool t)
        {
            InitializeComponent();
            if (t == true)
                this.label1.Text = "Congratulations, you won ! ! !"+ "\n" +"Now what do you want to do?";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.reset = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
