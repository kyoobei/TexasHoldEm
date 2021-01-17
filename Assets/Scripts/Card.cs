[System.Serializable]
public class Card
{
   public Suit Suit => suit;
   public int Value => value;
   private Suit suit; 
   private int value; 
   public Card(Suit suit, int value)
   {
      this.suit = suit;
      this.value = value;
   }
}