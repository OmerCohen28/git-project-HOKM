using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4.Instances
{
    public class Card : System.Windows.Forms.Button
    {
        private int rank;
        private string type;
        public static System.Drawing.Size size = new System.Drawing.Size(60, 90);

        public Card(int rank, string type) : base() {
            this.rank = rank;
            this.type = type;
            base.Size = size;
            base.UseVisualStyleBackColor = true;
            base.Anchor = System.Windows.Forms.AnchorStyles.None;
        }

        public int Rank {
            get => rank;
            set { rank = value; base.ResetText(); }
        }
        public string Type {
            get => type;
            set { type = value; base.ResetText(); }
        }
        public override string Text { get => rank.ToString() + type; }
    }
}
