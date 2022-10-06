using System;
using System.Windows.Forms;
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
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private void UpdateCards(Hand[] hands, Card[] cards) {
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
        }

        private void AfterInitializeComponent() {

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
                        deck[0].Location = new System.Drawing.Point((int)this.ClientSize.Width / 2, (int)this.ClientSize.Height / 2 - 10 - Card.size.Height / 2);
                        break;
                    case 1:
                        deck[1].Location = new System.Drawing.Point((int)this.ClientSize.Width / 2 + 10 + Card.size.Width / 2, (int)this.ClientSize.Height / 2);
                        break;
                    case 2:
                        deck[2].Location = new System.Drawing.Point((int)this.ClientSize.Width / 2, (int)this.ClientSize.Height / 2 + 10 + Card.size.Height / 2);
                        break;
                    case 3:
                        deck[3].Location = new System.Drawing.Point((int)this.ClientSize.Width / 2 - 10 - Card.size.Width / 2, (int)this.ClientSize.Height / 2);
                        break;
                }
            }
            
        }

        private Hand[] hands = new Hand[4];
        private Card[] deck = new Card[4];
    }
}