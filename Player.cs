using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GoFish
{
    class Player
    {
        private string name;
        public string Name { get { return name; } }
        private Random random;
        public Deck deck;
        private TextBox textBox;
        public Dictionary<Values, int> Placas;

        public Player(string Name,Random random,TextBox textBox, Cards card)
        {
            name = Name;
            this.random = random;
            deck = new Deck(card);
            this.textBox = textBox;
            textBox.Text = Name + "Has just joined the game"
                + Environment.NewLine;
        }
        public IEnumerable<Values> PullOutBooks()
        {
            List<Values> books = new List<Values>();
            for (int i = 1; i <=13; i++)
            {
                Values value = (Values)i;
                int howMany = 0;
                for (int j = 0; j < deck.Count; j++)
                {
                    if (deck.Peek(j).Value == value)
                    {
                        howMany++;
                    }
                }
                if (howMany == 4)
                {
                    books.Add(value);
                    for (int j = deck.Count-1; j >=0 ; j++)
                    {
                        deck.Deal(j);
                    }
                }
            }
            return books;
        }

        public void PlacaPonto(Values value)
        {
            int NumberCards = 0;
            foreach (Cards card in deck.cards)
            {
                if (card.Value == value)
                {
                    NumberCards++;
                }
                Placas.Add(value, NumberCards);
            }            
        }

        public Cards GetRandomValue(List<Cards> cards)
        {
            //Este metodo obtem um valor aleatorio - 
            // q exista no trabalho
            int c;
            c = random.Next(cards.Count);
            return cards[c] ;

        }
        public void takeCard(Cards cards)
        {
            deck.Add(cards);
        }
       private Deck Card = new Deck();
        public Deck DoYouHaveAny(Values value)
        {   
            // se vc tem a carta com um certo valor 
            //
                
            for (int i = 0; i <= deck.cards.Count; i++)
            {
                if (deck.cards[i].Value == value)
                {
                    Card.cards.Add(deck.cards[i]);
                }
            }            
            textBox.Text = Name + "has" + Card.cards.Count + " " + value;
            return Card;
            
        }
        
        public void AskForACard( Deck stock)
        {
            //Carta aleatoria do stock

            deck.Add(stock.Deal((int)GetRandomValue(stock.cards).Value));
        }

        public bool AskForACard (List<Player> players, int myIndex, 
            Deck stock, Values values)
        {
            // value especifico 
            textBox.Text = Name + " is needing of " + values+
                Environment.NewLine;
            
            for (int i = 0; i < players.Count || i != myIndex; i++)
            {
                
                if (players[i].deck.ContainValue(values))
                {
                    for (int j  = 0; j <= players[i].DoYouHaveAny(values).Count; j++)
                    {
                        deck.Add(players[i].DoYouHaveAny(values).cards[j]);
                    }
                    return true;
                    
                }
            }
            return false;
        }
        public IEnumerable<string> GetCardNames() { return deck.GetCardNames(); }
        
    }
}
