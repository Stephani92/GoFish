using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoFish
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        Game game;
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("please enter your name", "Can't start the game yet");
                return;
            }
            game = new Game(textBox2.Text, new List<string> { "Joe", "Bob" }, textBox1);
            button1.Enabled = false;
            textBox2.Enabled = false;
            btnAskForaCard.Enabled = true;
            UpdateForm();
            
        }
        private void UpdateForm()
        {
            listHand.Items.Clear();
            foreach (String cardName in game.GetPlayerCardNames())
            {
                listHand.Items.Add(cardName);
            }
            textBox3.Text = game.DescribeBooks();
            textBox1.Text += game.DescribePlayerHands();
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private void btnAskForaCard_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (listHand.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select a card");
                return;
            }
            if (game.PlayOneRound(listHand.SelectedIndex))
            {
                textBox1.Text += " the winner is .. " + game.GetWinnerName();
                textBox3.Text = game.DescribeBooks();
                btnAskForaCard.Enabled = false;
                for (int i = 1; i <= game.players.Count; i++)
                {
                    Values cardPeek = game.players[i].GetRandomValue();
                    game.PlayOneRound((int)cardPeek);

                }
            }
            else
            {
                
                textBox3.Text = game.DescribeBooks();
                UpdateForm();
            }
            
        }
    }
}

