using System.Collections.Generic;
using UnityEngine;
public class CardController : MonoBehaviour, IController
{
    private const int TOTAL_CARDS = 9;
    [SerializeField] private List<CardDeal> m_cardDealersList = new List<CardDeal>();
    [SerializeField] private CardDeal m_middleTable = null;
    // private List<Card> m_currentCardsAcquired = new List<Card>();
    private Deck m_deck = new Deck();
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
        // m_currentCardsAcquired.Clear();
        // m_currentCardsAcquired = new List<Card>(m_deck.GetCardsFromDeck(TOTAL_CARDS));
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
