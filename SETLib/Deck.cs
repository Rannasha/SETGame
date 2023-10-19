using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETLib
{
    public class Deck : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Card> Cards;

        public int CardsInDeck
        {
            get
            { 
                return Cards.Count; 
            }
        }

        private int _setsFound;
        public int SetsFound
        {
            get
            {
                return _setsFound;
            }
            set
            {
                _setsFound = value;
                OnPropertyChanged(nameof(SetsFound));
            }

        }

        private Random rng;

        private void RemoveCard(Card c)
        {
            Cards.Remove(c);
            OnPropertyChanged(nameof(CardsInDeck));
        }

        private void AddCard(Card c) 
        { 
            Cards.Add(c);
            OnPropertyChanged(nameof(CardsInDeck));
        }

        public Card? SelectCard()
        {
            if (Cards.Count == 0)
            {
                return null;
            }
            int ix = rng.Next(Cards.Count);

            Card c = Cards[ix];
            RemoveCard(c);

            return c;
        }

        public void InitDeck()
        {
            Cards = new List<Card>();
            for (int i0 = 0; i0 < 3; i0++)
            {
                for (int i1 = 0; i1 < 3; i1++)
                {
                    for (int i2 = 0; i2 < 3; i2++)
                    {
                        for (int i3 = 0; i3 < 3; i3++)
                        {
                            AddCard(new Card(i0, i1, i2, i3));
                        }
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public Deck() 
        {
            Cards = new List<Card>();
            rng = new Random();
            InitDeck();
        }
    }
}
