[System.Serializable]
public class Card
{
   public Suit Suit => suit;
   public int Value => value;
   public Suit suit; //should be private
   public int value; //should be private
   public Card(Suit suit, int value)
   {
      this.suit = suit;
      this.value = value;
   }
}