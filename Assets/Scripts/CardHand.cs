using System.Collections.Generic;
public class CardHand
{
    public List<Card> CombinedCards => m_combineCards;
    private List<Card> m_combineCards;
    public void CompareHands(List<Card> cardA, List<Card> cardB)
    {
        m_combineCards = new List<Card>();
        cardA.ForEach(hand => m_combineCards.Add(hand));
        cardB.ForEach(hand => m_combineCards.Add(hand));
        m_combineCards.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));
    }
    
}