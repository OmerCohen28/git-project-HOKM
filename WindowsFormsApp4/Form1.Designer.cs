using System;
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

        private void UpdateCards(Hand[] hands, Card[] cards) {
            Console.WriteLine("UPDATE");
            this.hands = hands;
            this.deck = cards;
            UpdateView();
        }

        private void AfterInitializeComponent() {
            this.SuspendLayout();

            //
            // hands
            //
            // Adding the cards to the hands
            this.hands[0] = new Hand(new Card[] { new Card(1, "Spades"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            this.hands[1] = new Hand(new Card[] { new Card(1, "Spades"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            this.hands[2] = new Hand(new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            this.hands[3] = new Hand(new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            this.deck = new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") };

            this.ResumeLayout(false);

            UpdateView();
        }

        private void UpdateView()
        {
            this.SuspendLayout();

            // Configuring the hand's display
            this.hands[0].SetUp(400, 330, 8, 'x', System.Windows.Forms.AnchorStyles.Bottom);
            this.hands[1].SetUp(400, 70, 8, 'x', System.Windows.Forms.AnchorStyles.Top);
            this.hands[2].SetUp(50, 200, 8, 'y', System.Windows.Forms.AnchorStyles.Left);
            this.hands[3].SetUp(750, 200, 8, 'y', System.Windows.Forms.AnchorStyles.Right);

            // Adding hands to the form
            this.hands[0].AddControls(this.Controls);
            this.hands[1].AddControls(this.Controls);
            this.hands[2].AddControls(this.Controls);
            this.hands[3].AddControls(this.Controls);
            foreach (Card c in deck)
            {
                this.Controls.Add(c);
            }

            this.ResumeLayout(false);
        }

        private Hand[] hands = new Hand[4];
        private Card[] deck = new Card[4];
    }
}