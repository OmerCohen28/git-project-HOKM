using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.Instances
{
    public class Hand
    {
        private Card[] cards;
        private Control.ControlCollection controls;
        public Hand(Card[] cards) {
            int count = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null) count++;
            }
            Card[] newC = new Card[count];
            int j = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null) { newC[j] = cards[i]; j++; }
            }
            this.cards = newC;
        }

        public void SetUp(int x, int y, int space, char option, 
            System.Windows.Forms.AnchorStyles anchor = System.Windows.Forms.AnchorStyles.None) {
            if (cards.Length == 0) return;
            int height = Card.size.Height;
            int width = Card.size.Width;
            if (option == 'x')
            {
                int totalWidth = width * cards.Length + space * (cards.Length - 1);
                if (totalWidth > Form1.instance.ClientSize.Width)
                {
                    space = space + (Form1.instance.ClientSize.Width - totalWidth) / (cards.Length - 1);
                }
                totalWidth = Math.Min(totalWidth, Form1.instance.ClientSize.Width);
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].Location = new System.Drawing.Point(x -(totalWidth / 2) + i * (width + space), y - height/2);
                }
            } else if (option == 'y')
            {
                int totalHeight = height * cards.Length + space * (cards.Length - 1);
                if (totalHeight > Form1.instance.ClientSize.Height)
                {
                    space = space + (Form1.instance.ClientSize.Height - totalHeight) / (cards.Length - 1);
                }
                totalHeight = Math.Min(totalHeight, Form1.instance.ClientSize.Height);
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].Location = new System.Drawing.Point(x - width/2, y -(totalHeight / 2) + i * (height + space));
                }
            }
            for (int i = 0; i < cards.Length; i++) cards[i].Anchor = anchor;
        }

        public Card[] Cards { get => cards; set => cards = value; }
        public void AddCard(Card c) {
            List<Card> Cards = new List<Card>(cards);
            Cards.Append(c);
            this.cards = Cards.ToArray();
        }

        public void AddControls(Control.ControlCollection controls)
        {
            this.controls = controls;
            for (int i = 0; i < cards.Length; i++) controls.Add(cards[i]);
        }

        public void RemoveControls()
        {
            for (int i = 0; i < cards.Length; i++) this.controls.Remove(cards[i]);
        }
    }
}
