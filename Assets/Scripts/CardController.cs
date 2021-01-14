using System.Collections.Generic;
using UnityEngine;
public class CardController : MonoBehaviour, IController
{
    [SerializeField] private List<CardDeal> m_cardDealersList = new List<CardDeal>();
    [SerializeField] private CardDeal m_middleTable = null;
    private Deck m_deck = new Deck();
    private CardHand m_cardHand = new CardHand();

    private void Start()
    {
        m_deck.OnFinishedShuffling += FinishedShuffling;
    }
    private void OnDisable()
    {
        m_deck.OnFinishedShuffling -= FinishedShuffling;
    }
    public void StartController()
    {
        m_deck.Shuffle();
    }
    private void FinishedShuffling()
    {
        UpdateCardDealers();
    }
    private void UpdateCardDealers()
    {
        //for players
        for(int i = 0; i < m_cardDealersList.Count; i++)
        {
            m_cardDealersList[i].SetCardValue
                (m_deck.GetCardsFromDeck(m_cardDealersList[i].NumberOfCardsToDeal));
        }
        //for the middle table
        m_middleTable.SetCardValue(m_deck.GetCardsFromDeck(m_middleTable.NumberOfCardsToDeal));
    }
}
