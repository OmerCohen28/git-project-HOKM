using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4.Instances;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AfterInitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            Card card = ((Card)sender);
            card.Rank = rand.Next(1, 13);
        }
    }
}
