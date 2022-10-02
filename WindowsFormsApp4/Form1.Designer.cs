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
            this.SuspendLayout();

            //
            // other cards
            //
            Card card3 = new Card(3, "Diamonds");
            Card card4 = new Card(11, "Spades");
            Card card5 = new Card(9, "Hearts");
            //
            // hands
            //
            // Adding the cards to the hands
            this.bottom_hand = new Hand(new Card[] { card3, card4, card5 });
            this.top_hand = new Hand(new Card[] { new Card(1, "Spades"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            this.left_hand = new Hand(new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            this.right_hand = new Hand(new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            this.deck = new Hand(new Card[] { new Card(7, "Hearts"), new Card(9, "Diamonds"), new Card(1, "Clubs") });
            // Configuring the hand's display
            this.bottom_hand.SetUp(400, 330, 8, 'x', System.Windows.Forms.AnchorStyles.Bottom);
            this.top_hand.SetUp(400, 70, 8, 'x', System.Windows.Forms.AnchorStyles.Top);
            this.left_hand.SetUp(50, 200, 8, 'y', System.Windows.Forms.AnchorStyles.Left);
            this.right_hand.SetUp(750, 200, 8, 'y', System.Windows.Forms.AnchorStyles.Right);
            this.deck.SetUp(400, 200, -55, 'x');
            // 
            // Form1
            // 
            // Adding hands to the form
            this.bottom_hand.AddControls(this.Controls);
            this.top_hand.AddControls(this.Controls);
            this.left_hand.AddControls(this.Controls);
            this.right_hand.AddControls(this.Controls);
            this.deck.AddControls(this.Controls);

            this.ResumeLayout(false);
        }

        private Hand bottom_hand;
        private Hand top_hand;
        private Hand left_hand;
        private Hand right_hand;
        private Hand deck;
    }
}