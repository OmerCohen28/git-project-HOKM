using WindowsFormsApp4.Instances;

namespace WindowsFormsApp4
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private void AfterInitializeComponent() {
            this.card1 = new Card(7, "Spades");
            this.card2 = new Card(10, "Hearts");
            this.SuspendLayout();
            // 
            // card1
            // 
            this.card1.Location = new System.Drawing.Point(357, 294);
            this.card1.Name = "card1";
            this.card1.TabIndex = 0;
            this.card1.Click += new System.EventHandler(this.button1_Click);
            // 
            // card2
            // 
            this.card2.Location = new System.Drawing.Point(450, 294);
            this.card2.Name = "card2";
            this.card2.TabIndex = 1;
            this.card2.Click += new System.EventHandler(this.button1_Click);
            //
            // other cards
            //
            Card card3 = new Card(3, "Diamonds");
            Card card4 = new Card(11, "Spades");
            Card card5 = new Card(9, "Hearts");
            card4.Location = new System.Drawing.Point(10, 10);
            //
            // hand
            //
            this.hand = new Hand(new Card[] { this.card1, this.card2, card3, card4, card5 });
            this.hand.SetUp(400, 330, 8, 'x', System.Windows.Forms.AnchorStyles.Bottom);
            this.hand1 = new Hand(new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            this.hand1.SetUp(400, 70, 8, 'x', System.Windows.Forms.AnchorStyles.Top);
            Hand hand2 = new Hand(new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            hand2.SetUp(50, 200, 8, 'y', System.Windows.Forms.AnchorStyles.Left);
            Hand hand3 = new Hand(new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            hand3.SetUp(750, 200, 8, 'y', System.Windows.Forms.AnchorStyles.Right);
            // 
            // Form1
            // 
            this.hand.AddControls(this.Controls);
            this.hand1.AddControls(this.Controls);
            hand2.AddControls(this.Controls);
            hand3.AddControls(this.Controls);
            this.Controls.Add(card4);
            this.ResumeLayout(false);
        }

        private Card card1;
        private Card card2;
        private Hand hand;
        private Hand hand1;
    }
}