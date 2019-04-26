using System;
using System.Collections.Generic;

namespace GoFish
{
    class Deck
    {
        public List<Cards> cards;
        private Random random = new Random();
        public int Count { get { return cards.Count; } }

        public Deck()
        {
            cards = new List<Cards>();
            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 1; value <= 13; value++)
                {
                    cards.Add(new Cards((Suits)suit, (Values)value));
                }
            }
        }
        public Deck(IEnumerable<Cards> initialCards)
        {
            cards = new List<Cards>(initialCards);
        }

        public IEnumerable<string> GetCardNames()
        {
            string[] NameCard = new string[cards.Count];
            for (int i = 0; i < cards.Count; i++)
            {
                NameCard[i] = cards[i].name;
            }
            
            return NameCard;
        }    
        
        public void Shuffle()
        {
            List<Cards> NewCards = new List<Cards>();
            while (cards.Count>0)
            {
                int CardToMove = random.Next(cards.Count);
                NewCards.Add(cards[CardToMove]);
                cards.RemoveAt(CardToMove);
            }
            cards = NewCards;
        }

        public void Add(Cards cardToAdd)
        {
            cards.Add(cardToAdd);
        }

        public Cards Deal(int index)
        {
            Cards CardsToDeal = cards[index];
            cards.RemoveAt(index);
            return CardsToDeal;
        }
        
        
        public void Sort(CardBySort cardBySort)
        {
            cards.Sort(new CardBySort());
        }

        //Update
        
        public Cards Peek(int cardNumber)
        {
            return cards[cardNumber];
        } 

        public Cards Deal()
        {
            return Deal(0);
        }

        public bool ContainValue(Values values)
        {
            foreach (Cards card in cards)
            {
                if (card.Value == values)
                {
                    return true;
                }
            }
            return false;
        }

        public Deck PullOutValue( Values value)
        {
            Deck deckToReturn = new Deck(new Cards[] { });
            for (int i = cards.Count - 1; i >= 0; i++)
            {
                if (cards[i].Value == value)
                {
                    deckToReturn.Add(Deal(i));
                }
            }
            return deckToReturn;
        }
        public bool HasBook(Values value)
        {
            int NumberCards = 0;
            foreach (Cards card in cards)
            {
                if (card.Value == value)
                {
                    NumberCards++;
                }
            }
            if (NumberCards == 4)
            {
                return true;
            }
            else
            {
                return false;               
            }
        }
    }
}

