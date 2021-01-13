[System.Serializable]
public class Card
{
   public Suit Suit => suit;
   public int Value => value;
   public Suit suit;
   public int value;
   public Card(Suit suit, int value)
   {
      this.suit = suit;
      this.value = value;
   }
}