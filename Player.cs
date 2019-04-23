using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GoFish
{
    class Player
    {
        private string name;
        private string Name { get { return Name; } }
        private Random random;
        private Deck deck;
        private TextBox textBox;

        public Player(string Name, TextBox textBox)
        {
            name = Name;
            this.random = new Random();
            this.textBox = textBox;
            List<Deck> deck = new List<Deck>();
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

        public Cards GetRandomValue(List<Cards> cards)
        {
            //Este metodo obtem um valor aleatorio - 
            //mas deve se uma q exista no trabalho
            int c;
            c = random.Next(cards.Count);
            return cards[c] ;

        }

        Deck Card = new Deck();
        public Deck DoYouHaneAny(Values value)
        {   
            // se vc tem a carta com um certo valor 
            //
                
            for (int i = 0; i < deck.Count; i++)
            {
                if (deck.cards[i].Value == value)
                {
                    Card.cards.Add(deck.cards[i]);
                }
            }            
            textBox.Text = Name + "has" + Card.cards.Count + " " + value;
            return Card;
            
        }
        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {

        }
        public void AskForACard2 (List<Player> players, int myIndex, 
            Deck stock, Values values)
        {
            textBox.Text = Name + " is needing of " + values+
                Environment.NewLine;
            List<Deck> cardsAsk = new List<Deck>();
            for (int i = 0; i < players.Count; i++)
            {
                
                if (players[i].deck.ContainValue(values))
                {
                    cardsAsk.Add(players[i].DoYouHaneAny(values));
                }
            }
            if (cardsAsk.Count > 0)
            {

            }
        }
    }
}
