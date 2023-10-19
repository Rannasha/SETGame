using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETLib
{
    public enum SelectionChangeResult { CardSelected, CardUnselected, TooManyCardsSelected, ValidSet, InvalidSet };

    public class Board
    {
        public Card[] Cards;
        bool[] SelectedCards;

        private int NumSelected()
        {
            int cnt = 0;
            for (int i = 0; i < 12; i++)
            {
                if (SelectedCards[i])
                {
                    cnt++;
                }
            }
            return cnt;
        }

        private List<Card> CardsSelected()
        {
            List<Card> res = new List<Card>();
            for (int i = 0; i < Cards.Count(); i++)
            {
                if (SelectedCards[i])
                {
                    res.Add(Cards[i]);
                }
            }

            return res;
        }

        public Card ReplaceCard(Card c0, Deck d)
        {
            int ix = 0;
            bool found = false;
            while (!found && ix < 12)
            {
                if (Cards[ix] == c0)
                {
                    found = true;
                    Cards[ix] = d.SelectCard();
                    SelectedCards[ix] = false;
                    break;
                }
                ix++;
            }

            return Cards[ix];
        }

        public bool ReplaceSet(Deck d)
        {
            if (d.CardsInDeck < 3)
            {
                return false;
            }

            List<Card> selC = CardsSelected();
            Card last = null;
            foreach (Card c in selC)
            {                
                last = ReplaceCard(c, d);
            }

            while (!HasSet() && d.CardsInDeck > 0)
            {
                last = ReplaceCard(last, d);
            }

            if (!HasSet() && d.CardsInDeck == 0) // Game ends
            {
                return false;
            }
            return true;
        }

        public bool HasSet()
        {
            for (int i0 = 0; i0 < Cards.Length - 2; i0++)
            {
                for (int i1 = i0 + 1; i1 < Cards.Length - 1; i1++)
                {
                    for (int i2 = i1 + 1; i2 < Cards.Length; i2++)
                    {
                        if (Utils.IsSet(Cards[i0], Cards[i1], Cards[i2]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CardSelected(int row, int col)
        {
            int ix = 4 * row + col;

            return SelectedCards[ix];
        }

        public SelectionChangeResult SelectCard(int row, int col)
        {
            int ix = 4 * row + col;

            if (NumSelected() >= 3 && !SelectedCards[ix])
            {
                return SelectionChangeResult.TooManyCardsSelected;
            }
            SelectedCards[ix] = !SelectedCards[ix];

            if (NumSelected() == 3)
            {
                // Check for SET
                List<Card> selection = CardsSelected();
                if (Utils.IsSet(selection[0], selection[1], selection[2])) // SET
                {
                    return SelectionChangeResult.ValidSet;
                }
                else // No SET
                {
                    return SelectionChangeResult.InvalidSet;
                }
            }
            else
            {
                if (SelectedCards[ix])
                {
                    return SelectionChangeResult.CardSelected;
                }
                else
                {
                    return SelectionChangeResult.CardUnselected;
                }
            }
        }

        public void Init(Deck deck)
        {
            for (int i = 0; i < 12; i++)
            {
                Cards[i] = deck.SelectCard();
                SelectedCards[i] = false;
            }
        }

        public Board(Deck deck) : this()
        {
            Init(deck);
        }

        public Board() 
        { 
            Cards = new Card[12];
            SelectedCards = new bool[12];
        }
    }
}
