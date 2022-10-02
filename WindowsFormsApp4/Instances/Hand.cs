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
            this.cards = cards;
        }

        public void SetUp(int x, int y, int space, char option, 
            System.Windows.Forms.AnchorStyles anchor = System.Windows.Forms.AnchorStyles.None) {
            if (cards.Length == 0) return;
            int height = Card.size.Height;
            int width = Card.size.Width;
            if (option == 'x')
            {
                int totalWidth = width * cards.Length + space * (cards.Length - 1);
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].Location = new System.Drawing.Point(x - (totalWidth / 2) + i * (width + space), y - height/2);
                }
            } else if (option == 'y')
            {
                int totalHeight = height * cards.Length + space * (cards.Length - 1);
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i].Location = new System.Drawing.Point(x - width/2, y - (totalHeight / 2) + i * (height + space));
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
    }
}
