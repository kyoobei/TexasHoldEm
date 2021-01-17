using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CardHand
{   
    public List<Card> BestCardsHand => m_currentBestCardHand; 
    private List<Card> m_currentBestCardHand;
    private CardBuilder m_cardBuilder;
    private CardCheckingFactory m_cardCheckingFactory;
    public CardHand(CardCheckingFactory cardCheckingFactory, CardBuilder cardBuilder)
    {
        m_cardBuilder = cardBuilder;
        m_cardCheckingFactory = cardCheckingFactory;
    }
    public Hand GetHandType(List<Card> cardAtHand, List<Card> cardAtTable)
    {
        m_currentBestCardHand = m_cardBuilder.Build(cardAtHand, cardAtTable);
        return m_cardCheckingFactory.GetCardHand(m_currentBestCardHand);
    }
}