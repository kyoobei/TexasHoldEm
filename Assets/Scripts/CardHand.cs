using System.Collections.Generic;
public class CardHand
{
    private List<Card> m_cardA;
    private List<Card> m_cardB;
    public CardHand(List<Card> cardA, List<Card> cardB)
    {
        m_cardA = new List<Card>(cardA);
        m_cardB = new List<Card>(cardB);
    }
    
}