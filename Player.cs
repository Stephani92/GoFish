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

        public Player(string Name,Random random,TextBox textBox, Cards stock)
        {
            name = Name;
            this.random = random;
            this.deck = new Deck(stock);
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

        
        public Values GetRandomValue()
        {
            //Este metodo obtem um valor aleatorio - 
            // q exista no trabalho
            Values randomCard = deck.Deal(random.Next(deck.Count)).Value;
            return randomCard;

        }
        public void takeCard(Cards cards)
        {
            deck.Add(cards);
        }
        public Deck DoYouHaveAny(Values value)
        {
            // se vc tem a carta com um certo valor 
            //

            Deck Card = deck.PullOutValue(value);          
            textBox.Text = Name + " has " + Card.cards.Count + " " + value;
            return Card;
            
        }
        public int CardCount { get
            {
                return deck.Count;
            } }
        
        public void AskForACard(List<Player> players, int myIndex,
            Deck stock)
        {
            //Carta aleatoria do stock
            Values randomValue = GetRandomValue();
            
        }

        public void AskForACard (List<Player> players, int myIndex, 
            Deck stock, Values values)
        {
            // value especifico 
            textBox.Text = Name + " is needing of " + values+
                Environment.NewLine;
            int cardsGiven = 0;
            for (int i = 0; i >= players.Count ; i++)
            {
                if (i != myIndex)
                {
                    Player player = players[i];
                    Deck CarddsGiven = player.DoYouHaveAny(values);
                    cardsGiven += deck.Count;
                    while (CarddsGiven.Count > 0)
                    {
                        this.deck.Add(CarddsGiven.Deal());
                    }
                }
            }
            if (cardsGiven ==0)
            {
                textBox.Text += Name + " must draw the stock." + Environment.NewLine;
                deck.Add(stock.Deal());
            }
        }
        public IEnumerable<string> GetCardNames() { return deck.GetCardNames(); }

        public void SortHand()
        {
            deck.Sort(new CardBySort());
        }

        public Cards Peek(int cardPeek)
        {
            return deck.Peek(cardPeek);
        }
    }
}
