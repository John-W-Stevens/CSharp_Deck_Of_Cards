using System;
using System.Linq;
using System.Collections.Generic;

namespace DeckOfCards
{

    // Create a class called "Card"  
        // a. Give the Card class a property "stringVal" which will hold the value of the card ex. (Ace, 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King). This "val" should be a string.
        // b. Give the Card a property "suit" which will hold the suit of the card(Clubs, Spades, Hearts, Diamonds).
        // c. Give the Card a property "val" which will hold the numerical value of the card 1-13 as integers

    public class Card
    {
        private string stringVal;
        private string suit;
        private int val;

        public string StringVal
        {
            get {return this.stringVal;}
            set {stringVal = value;}
        }

        public string Suit
        {
            get {return this.suit;}
            set {this.suit = value;}
        }

        public int Val
        {
            get {return this.val; }
            set {this.val = value;}
        }
    }

    // Next, create a class called "Deck"
        // a. Give the Deck class a property called "cards" which is a list of Card objects.
        // b. When initializing the deck, make sure that it has a list of 52 unique cards as its "cards" property.
        // c. Give the Deck a deal method that selects the "top-most" card, removes it from the list of cards, and returns the Card.
        // d. Give the Deck a reset method that resets the cards property to contain the original 52 cards.
        // e. Give the Deck a shuffle method that randomly reorders the deck's cards

    public class Deck
    {
        // Three arrays used to build deck
        private string[] CardStringValues = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
        private string[] Suits = { "Clubs", "Spades", "Hearts", "Diamonds" };
        private int[] CardIntValuesArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        // HashMap associating stringValues with intValues
        private Dictionary<string, int> CardIntValues = new Dictionary<string, int>();

        // List object stores Deck of cards
        public List<Card> cards = new List<Card>();

        // Constructor (generates hashMap and builds a new deck
        public Deck()
        {
            for (int i=0; i<CardStringValues.Length; i++)
            {
                CardIntValues[CardStringValues[i]] = CardIntValuesArray[i];
            }
            BuildDeck();
        }

        // Private sub-routine generates all 52 playing cards:
        private void BuildDeck()
        {
            foreach (string suit in Suits)
            {
                foreach (string val in CardStringValues)
                {
                    Card playingCard = new Card();
                    playingCard.Suit = suit;
                    playingCard.StringVal = val;
                    playingCard.Val = CardIntValues[val];
                    cards.Add(playingCard);
                }
            }
        }

        public Card Deal()
        {
            Card card = cards[cards.Count - 1];

            this.cards.RemoveAt(cards.Count - 1);
            return card;
        }

        public void Reset()
        {
            this.cards = new List<Card>();
            this.BuildDeck();
        }

        public void Shuffle()
        {
            // Fisher-Yates Shuffle
            Random rand = new Random();
            int n = this.cards.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                Card Temp = cards[k];
                cards[k] = cards[n];
                cards[n] = Temp;
            }
        }
    }

    // Finally, create a class called "Player"
        // a. Give the Player class a name property.
        // b. Give the Player a hand property that is a List of type Card.
        // c. Give the Player a draw method of which draws a card from a deck, adds it to the player's hand and returns the Card.
        // d. Note this method will require reference to a deck object
        // e. Give the Player a discard method which discards the Card at the specified index from the player's hand and returns this Card or null if the index does not exist.

    public class Player
    {
        private string Name;
        public string name
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        private List<Card> Hand;

        public List<Card> hand
        {
            get { return this.Hand; }
            set { this.Hand = value; }
        }

        // constructor
        public Player(int handSize, Deck deck)
        {
            List<Card> newHand = new List<Card>();

            for (int i=0; i<handSize; i++)
            {
                newHand.Add(deck.Deal());
            }
            this.Hand = newHand;
        }

        public Card Draw(Deck deck)
        {
            Card NewCard = deck.Deal();
            this.hand.Add(NewCard);
            return NewCard;
        }

        public Card Discard(int idx)
        {
            if (this.hand.ElementAtOrDefault(idx) != null)
            {
                Card card = this.hand[idx];
                this.hand.RemoveAt(idx);
                return card;
            }
            else
            {
                Console.WriteLine("Returning null...");
                return null;
            }
        }

    }





    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate a new Deck object
            Deck NewDeck = new Deck();

            // Verify decklength == 52
            Console.WriteLine(NewDeck.cards.Count);

            // Verify deck contains the correct cards:
            foreach (Card playingCard in NewDeck.cards)
            {
                Console.WriteLine($"{playingCard.StringVal} of {playingCard.Suit}, which has a numerical value of {playingCard.Val}");
            }
            // Test the length of the deck after multiple deals:
            NewDeck.Deal();
            NewDeck.Deal();
            NewDeck.Deal();
            NewDeck.Deal();
            Console.WriteLine(NewDeck.cards.Count);

            // Test Deck.Reset() -> Should produce a new deck of 52 cards
            NewDeck.Reset();
            Console.WriteLine(NewDeck.cards.Count);
            foreach (Card playingCard in NewDeck.cards)
            {
                Console.WriteLine($"{playingCard.StringVal} of {playingCard.Suit}, which has a numerical value of {playingCard.Val}");
            }

            // Test Deck.Shuffle() method
            Console.WriteLine("Shuffling deck...");
            NewDeck.Shuffle();
            foreach (Card playingCard in NewDeck.cards)
            {
                Console.WriteLine($"{playingCard.StringVal} of {playingCard.Suit}, which has a numerical value of {playingCard.Val}");
            }

            // Instantiate a Player Object
            Player PlayerOne = new Player(5, NewDeck);

            // Verify player has a hand of 5 playing cards
            Console.WriteLine(PlayerOne.hand.Count);

            // Test player.discard() method:
            Console.WriteLine();
            Console.WriteLine($"Discarding the card at index 3...");
            Console.WriteLine();

            PlayerOne.Discard(3);

            foreach (Card PC in PlayerOne.hand)
            {
                Console.WriteLine($"{PC.StringVal} of {PC.Suit}");
            }

            // Test that player.discard method returns null if index doesn't exist
            Console.WriteLine(PlayerOne.Discard(12));

        }
    }
}
