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
            if (player.deck.Count >= 5 & stock.Count >= 3)
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
            string booksRelatorio = "";
            foreach (int key in books.Keys)
            {
                booksRelatorio += "O  "+books[(Values)key].Name + "Tem "+ books[(Values)key]+Environment.NewLine;
            }
            return booksRelatorio;
        }
        Dictionary< int, Player> tabela = new Dictionary<int, Player>();
        Dictionary<int, Player> tabFinal = new Dictionary<int, Player>();
        public NumberByHight resul = new NumberByHight();
        string stri;
        public string GetWinnerName()
        {
            foreach (Player player in players)
            {
                int NumberBook = 0;
                foreach (Player item in books.Values)
                {
                    if (player.Name == item.Name)
                    {
                        NumberBook++;
                    }

                }
                tabela.Add( NumberBook, player);
            }
            List<int> list = new List<int>();
            foreach (int item in tabela.Keys)
            {
                list.Add(item);
            }
            list.Sort(resul);
            
            foreach (int pontos in tabela.Keys)
            {
                foreach (int item in list)
                {
                    if (pontos == item)
                    {
                        tabFinal.Add(item, tabela[item]);
                    }
                }
            }
            foreach (int item in tabFinal.Keys)
            {
                stri += tabFinal[item].Name;
            }
            
            return stri;
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
