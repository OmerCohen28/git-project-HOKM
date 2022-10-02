using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.CustomControlls
{
    public class Card : System.Windows.Forms.PictureBox
    {
        public Card()
        {
            this.Location = new System.Drawing.Point(348, 317);
            this.Name = "pictureBox1";
            this.Size = new System.Drawing.Size(80, 112);
            this.TabIndex = 0;
            this.TabStop = false;
        }
    }
}
