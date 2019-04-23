namespace GoFish
{
    class Cards
    {
        public Suits Suit;
        public Values Value;
        public string name { get { return Suit.ToString() +" Of "+Value.ToString(); } }
        public Cards(Suits Sui, Values value)
        {
            this.Suit = Sui;
            this.Value = value;
        }
    }
}
