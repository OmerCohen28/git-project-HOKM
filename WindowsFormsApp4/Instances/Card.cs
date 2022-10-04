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
        public static System.Drawing.Size size = new System.Drawing.Size(75, 100);

        public Card(int rank, string type) : base() {
            this.rank = rank;
            this.type = type;
            base.Size = size;
            base.UseVisualStyleBackColor = true;
            base.Anchor = System.Windows.Forms.AnchorStyles.None;
            base.Click += new System.EventHandler(Form1.button1_Click);
            UpdateImage();
        }

        public int Rank {
            get => rank;
            set { rank = value; base.ResetText(); UpdateImage(); }
        }
        public string Type {
            get => type;
            set { type = value; base.ResetText(); UpdateImage(); }
        }
        
        public void UpdateImage()
        {
            switch (this.type)
            {
                case "Clubs":
                    switch (this.rank) {
                        case 1:
                            this.Image = Properties.Resources.row_1_column_1;
                            break;
                        case 2:
                            this.Image = Properties.Resources.row_1_column_2;
                            break;
                        case 3:
                            this.Image = Properties.Resources.row_1_column_3;
                            break;
                        case 4:
                            this.Image = Properties.Resources.row_1_column_4;
                            break;
                        case 5:
                            this.Image = Properties.Resources.row_1_column_5;
                            break;
                        case 6:
                            this.Image = Properties.Resources.row_1_column_6;
                            break;
                        case 7:
                            this.Image = Properties.Resources.row_1_column_7;
                            break;
                        case 8:
                            this.Image = Properties.Resources.row_1_column_8;
                            break;
                        case 9:
                            this.Image = Properties.Resources.row_1_column_9;
                            break;
                        case 10:
                            this.Image = Properties.Resources.row_1_column_10;
                            break;
                        case 11:
                            this.Image = Properties.Resources.row_1_column_11;
                            break;
                        case 12:
                            this.Image = Properties.Resources.row_1_column_12;
                            break;
                        case 13:
                            this.Image = Properties.Resources.row_1_column_13;
                            break;
                        default:
                            break;
                    }
                    break;
                case "Spades":
                    switch (this.rank)
                    {
                        case 1:
                            this.Image = Properties.Resources.row_2_column_1;
                            break;
                        case 2:
                            this.Image = Properties.Resources.row_2_column_2;
                            break;
                        case 3:
                            this.Image = Properties.Resources.row_2_column_3;
                            break;
                        case 4:
                            this.Image = Properties.Resources.row_2_column_4;
                            break;
                        case 5:
                            this.Image = Properties.Resources.row_2_column_5;
                            break;
                        case 6:
                            this.Image = Properties.Resources.row_2_column_6;
                            break;
                        case 7:
                            this.Image = Properties.Resources.row_2_column_7;
                            break;
                        case 8:
                            this.Image = Properties.Resources.row_2_column_8;
                            break;
                        case 9:
                            this.Image = Properties.Resources.row_2_column_9;
                            break;
                        case 10:
                            this.Image = Properties.Resources.row_2_column_10;
                            break;
                        case 11:
                            this.Image = Properties.Resources.row_2_column_11;
                            break;
                        case 12:
                            this.Image = Properties.Resources.row_2_column_12;
                            break;
                        case 13:
                            this.Image = Properties.Resources.row_2_column_13;
                            break;
                        default:
                            break;
                    }
                    break;
                case "Hearts":
                    switch (this.rank)
                    {
                        case 1:
                            this.Image = Properties.Resources.row_3_column_1;
                            break;
                        case 2:
                            this.Image = Properties.Resources.row_3_column_2;
                            break;
                        case 3:
                            this.Image = Properties.Resources.row_3_column_3;
                            break;
                        case 4:
                            this.Image = Properties.Resources.row_3_column_4;
                            break;
                        case 5:
                            this.Image = Properties.Resources.row_3_column_5;
                            break;
                        case 6:
                            this.Image = Properties.Resources.row_3_column_6;
                            break;
                        case 7:
                            this.Image = Properties.Resources.row_3_column_7;
                            break;
                        case 8:
                            this.Image = Properties.Resources.row_3_column_8;
                            break;
                        case 9:
                            this.Image = Properties.Resources.row_3_column_9;
                            break;
                        case 10:
                            this.Image = Properties.Resources.row_3_column_10;
                            break;
                        case 11:
                            this.Image = Properties.Resources.row_3_column_11;
                            break;
                        case 12:
                            this.Image = Properties.Resources.row_3_column_12;
                            break;
                        case 13:
                            this.Image = Properties.Resources.row_3_column_13;
                            break;
                        default:
                            break;
                    }
                    break;
                case "Diamonds":
                    switch (this.rank)
                    {
                        case 1:
                            this.Image = Properties.Resources.row_4_column_1;
                            break;
                        case 2:
                            this.Image = Properties.Resources.row_4_column_2;
                            break;
                        case 3:
                            this.Image = Properties.Resources.row_4_column_3;
                            break;
                        case 4:
                            this.Image = Properties.Resources.row_4_column_4;
                            break;
                        case 5:
                            this.Image = Properties.Resources.row_4_column_5;
                            break;
                        case 6:
                            this.Image = Properties.Resources.row_4_column_6;
                            break;
                        case 7:
                            this.Image = Properties.Resources.row_4_column_7;
                            break;
                        case 8:
                            this.Image = Properties.Resources.row_4_column_8;
                            break;
                        case 9:
                            this.Image = Properties.Resources.row_4_column_9;
                            break;
                        case 10:
                            this.Image = Properties.Resources.row_4_column_10;
                            break;
                        case 11:
                            this.Image = Properties.Resources.row_4_column_11;
                            break;
                        case 12:
                            this.Image = Properties.Resources.row_4_column_12;
                            break;
                        case 13:
                            this.Image = Properties.Resources.row_4_column_13;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public override string Text { get => this.Image != null? "" : rank.ToString() + type; }

    }
}
