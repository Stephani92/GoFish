using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoFish
{   
    class Game
    {
        public List<Player> players;
        private Dictionary<Values, Player> books;
        private Deck stock;
        private TextBox textBoxonForm;
        public Game(string PlayerName, IEnumerable<string> opponentNames, 
            TextBox textBox)
        {
            stock = new Deck();
            Random random = new Random();
            textBoxonForm = textBox;
            players = new List<Player>
            {
                new Player(PlayerName, random, textBoxonForm, stock.Deal())
            };
            foreach (string player in opponentNames)
            {
                players.Add(new Player(player, random, textBoxonForm, stock.Deal()));
            }

            books = new Dictionary<Values, Player>();
            
            Deal();
           // players[0].SortHand();

        }
        private void Deal()
        {
            stock.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                foreach (Player item in players)
                {
                    item.takeCard(stock.Deal());
                }
                foreach (Player item in players)
                {
                    PullOutBooks(item);
                }
            }
        }

        public bool PlayOneRound(int selectedPlayerCard)
        {
            Values cardToAskfor = players[0].deck.Peek(selectedPlayerCard).Value;
            for (int i = 0; i < players.Count; i++)
            {
                if (i == 0)
                {
                    players[0].AskForACard(players, 0, stock, cardToAskfor);
                }
                else
                {
                    players[i].AskForACard(players, i, stock);
                }
                if (PullOutBooks(players[i]))
                {
                    textBoxonForm.Text += players[i].Name + " drew a new hand" + Environment.NewLine;
                    int card = 1;

                    while (card<= 5 && stock.Count > 0)
                    {
                        players[i].takeCard(stock.Deal());
                        card++;
                    }
                }
                players[0].SortHand();
                if (stock.Count == 0)
                {
                    textBoxonForm.Text = "The stock is out of cards. Game Over!" + Environment.NewLine;
                    return true;
                }
            }
            return false;

        }
        public bool PullOutBooks(Player player)
        {
            IEnumerable<Values> bookPulled = player.PullOutBooks();
            foreach (Values item in bookPulled)
            {
                books.Add(item, player);
            }
            if (player.CardCount == 0)
            {
                return true;
            }
            return false;

        }
        public string DescribeBooks()
        {
            string booksRelatorio = "";
            foreach (Values key in books.Keys)
            {
                booksRelatorio += "O  "+books[(Values)key].Name + "Tem "+ books[(Values)key]+Environment.NewLine;
            }
            return booksRelatorio;
        }
        
        public string GetWinnerName()
        {
            Dictionary<string, int> winner = new Dictionary<string, int>();
            foreach (Values values in books.Keys)
            {
                string name = books[values].Name;
                if (winner.ContainsKey(name))
                {
                    winner[name]++;
                }
                else
                {
                    winner.Add(name, 1);
                }
            }
                
            int mostBook = 0;
            foreach (string Name in winner.Keys)
            {
                if (winner[Name] > mostBook)
                {
                    mostBook = winner[Name];
                }
            }
            bool tie = false;
            string winnerList = "";
            foreach (string item in winner.Keys)
            {
                if (winner[item] == mostBook)
                {
                    if (!String.IsNullOrEmpty(winnerList))
                    {
                        winnerList += " and ";
                        tie = true;
                    }
                    winnerList += item;
                }
            }
            winnerList += " with " + mostBook + " books ";
            if (tie)
            {
                return "A tie between " + winnerList;
            }
            else
            {
                return winnerList;
            }
            
        }
        public IEnumerable<string> GetPlayerCardNames()
        {
            return players[0].GetCardNames();
        }
        public string DescribePlayerHands()
        {
            string describe = "";
            for (int i = 0; i < players.Count; i++)
            {
                describe += players[i].Name + " has " + players[i].deck.Count;
                if (players[i].deck.Count == 1)
                {
                    describe += " card." + Environment.NewLine;
                }
                else
                {
                    describe += " cards" + Environment.NewLine;
                }
            }
            describe += "The stock has " + stock.cards.Count + " cards left.";
            return describe;
        }
    }
}
