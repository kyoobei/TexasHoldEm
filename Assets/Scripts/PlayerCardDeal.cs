using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardDeal : CardDeal
{
    public string PlayerName = string.Empty;
    public Hand CurrentHand => m_currentHand;
    public List<Card> WinningCard => m_winningCard;
    private Hand m_currentHand;
    private List<Card> m_winningCard = new List<Card>();
    private CardHand m_cardHand;
    private void Awake()
    {
        m_cardHand = new CardHand
        (
            new CardCheckingFactory(),
            new CardBuilder()
        );
    }
    public void CheckPlayerCard(List<Card> middleTableCards)
    {
        m_winningCard.Clear();
        m_currentHand = m_cardHand.GetHandType(m_currentCardsList, middleTableCards);
        m_winningCard = m_cardHand.BestCardsHand;
    }
}
