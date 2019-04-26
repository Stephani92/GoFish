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
        private List<Player> players;
        private Dictionary<Values, Player> books;
        private Deck stock;
        private TextBox textBoxonForm;
        public Game(string PlayerName, IEnumerable<string> opponentNames, 
            TextBox textBox)
        {
            Random random = new Random();
            textBoxonForm = textBox;
            players = new List<Player>();
            players.Add(new Player(PlayerName, random, textBoxonForm));
            foreach (string player in opponentNames)
            {
                players.Add(new Player(player, random, textBoxonForm));
            }

            books = new Dictionary<Values, Player>();
            stock = new Deck();
            Deal();
           // players[0].SortHand();

        }
        private void Deal()
        {
            stock.Shuffle();            
            foreach (Player player in players)
            {
                for (int i = 0; i <=5; i++)
                {
                    player.deck.Add(stock.Deal());
                }
                player.PullOutBooks();
            }
        }

        public bool PlayOneRound(int selectedPlayerCard)
        {
            for (int i = 0; i <=3; i++)
            { Random random = new Random();
                players[i].AskForACard(players, i, stock, (Values)random.Next(1,14));
                if (PullOutBooks(players[i]))
                {
                    textBoxonForm.Text = players[i].Name + " Comprou cardas do stock.";
                    return false;
                }
            }
            CardBySort CardBySort = new CardBySort();

            players[0].deck.Sort(CardBySort);
            if (stock.cards.Count < 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public bool PullOutBooks(Player player)
        {
            if (player.deck.Count < 5 & stock.Count >= 3)
            {
                for (int i = 0; i <= 5; i++)
                {
                    player.deck.Add(stock.Deal());
                }

                return false;
            }
            if (player.deck.Count >= 5)
            {
                for (int i = 0; i <= 13; i++)
                {
                    if (player.deck.HasBook((Values)i))
                    {
                        books.Add((Values)i, player);
                    }
                }
                return true;
            }
            return false;

        }
        public string DescribeBooks()
        {

        }
        public string GetWinnerName()
        {

        }
    }
}
