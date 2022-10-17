using System;
using System.Windows.Forms;
using WindowsFormsApp4.Instances;

namespace WindowsFormsApp4
{
    partial class Form1
    {
        public static Form1 instance; 

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Waiting For Server...";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(354, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 82);
            this.label2.TabIndex = 1;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void UpdateCards(Hand[] hands, Card[] cards, string[] scores) {
            Invoke(new Action<int>((int _) => { this.SuspendLayout(); }), 0);
            Console.WriteLine("UPDATE");
            Hand[] hs = this.hands;
            Card[] cs = this.deck;
            this.hands = hands;
            this.deck = cards;
            foreach (Hand hand in hs)
            {
                if (hand != null) Invoke(new Action<int>((int _) => { hand.RemoveControls(); }), 0);
            }
            foreach (Card card in cs)
            {
                if (card != null) Invoke(new Action<int>((int _) => { this.Controls.Remove(card); }), 0);
            }
            UpdateView(true);
            Invoke(new Action<int>((int _) => { this.label1.Text = string.Format("Team 1: {0}\nTeam 2: {1}", scores[0], scores[1]); }), 0);
        }

        private void AfterInitializeComponent() {
            instance = this;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
            true);
        }

        private void UpdateView(bool inv = false)
        {
            if (!inv) this.SuspendLayout();

            // Configuring the hand's display
            this.hands[2].SetUp((int)this.ClientSize.Width / 2, this.ClientSize.Height - 70, 8, 'x', System.Windows.Forms.AnchorStyles.Bottom);
            this.hands[0].SetUp((int)this.ClientSize.Width / 2, 70, 8, 'x', System.Windows.Forms.AnchorStyles.Top);
            this.hands[3].SetUp(50, (int)this.ClientSize.Height / 2, 8, 'y', System.Windows.Forms.AnchorStyles.Left);
            this.hands[1].SetUp(this.ClientSize.Width - 50, (int)this.ClientSize.Height / 2, 8, 'y', System.Windows.Forms.AnchorStyles.Right);

            // Adding hands to the form
            if (inv)
            {
                Invoke(new Action<System.Windows.Forms.Control.ControlCollection>(UpdateLocations), this.Controls);
            }
            else
            {
                UpdateLocations(this.Controls);
            }


            if (inv) Invoke(new Action<int>((int _) => { this.ResumeLayout(false); }), 0);
            else this.ResumeLayout(false);
        }

        private void UpdateLocations(System.Windows.Forms.Control.ControlCollection controls) {
            hands[0].AddControls(controls);
            hands[1].AddControls(controls);
            hands[2].AddControls(controls);
            hands[3].AddControls(controls);
            for (int i = 0; i < deck.Length; i++)
            {
                if (deck[i] == null) continue;
                controls.Add(deck[i]);
                switch (i) {
                    case 0:
                        deck[0].Location = new System.Drawing.Point((int)((this.ClientSize.Width - Card.size.Width) / 2), (int)this.ClientSize.Height / 2 - 10 - Card.size.Height);
                        break;
                    case 1:
                        deck[1].Location = new System.Drawing.Point((int)this.ClientSize.Width / 2 + 10, (int)((this.ClientSize.Height - Card.size.Height) / 2));
                        break;
                    case 2:
                        deck[2].Location = new System.Drawing.Point((int)((this.ClientSize.Width - Card.size.Width) / 2), (int)this.ClientSize.Height / 2 + 10);
                        break;
                    case 3:
                        deck[3].Location = new System.Drawing.Point((int)this.ClientSize.Width / 2 - 10 - Card.size.Width, (int)((this.ClientSize.Height - Card.size.Height) / 2));
                        break;
                }
            }
            
        }

        private void DisplayWinner()
        {
            if (label1.Text.Contains("\n"))
            {
                string[] s = label1.Text.Split('\n');
                Invoke(new Action<int>((int _) => {
                    label2.Text = int.Parse(s[0][8].ToString()) > int.Parse(s[1][8].ToString())? "Team 1 WINS" : "Team 2 WINS" ;
                    label1.Text = "";
                }), 0);
            }
        }

        private Hand[] hands = new Hand[4];
        private Card[] deck = new Card[4];
        private Label label1;
        private Label label2;
    }
}