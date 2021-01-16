using System.Collections.Generic;
using UnityEngine;
public class CardController : MonoBehaviour, IController
{
    [SerializeField] private List<CardDeal> m_cardDealersList = new List<CardDeal>();
    [SerializeField] private CardDeal m_middleTable = null;
    private Deck m_deck = new Deck();
    private CardCheckingFactory cardCheckingFactory = new CardCheckingFactory();
    //for testing
    [SerializeField] private List<Card> testerOwnHand = new List<Card>();
    [SerializeField] private List<Card> testerMiddleTable = new List<Card>();
    [SerializeField] private List<Card> testerCombine;
    [SerializeField] private List<Card> testerWinningHand;
    [SerializeField] private Hand testerHand;

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
    [ContextMenu("Test update")]
    private void UpdateCardDealers()
    {
        
        CardHand cardHand = new CardHand(cardCheckingFactory);
        testerHand = cardHand.GetHandType(testerOwnHand, testerMiddleTable);
        testerCombine = new List<Card>(cardHand.m_combineCards);
        testerWinningHand = new List<Card>(cardHand.winningCardList);
        
        //this one is working need to remove comment later
        // //for players
        // for(int i = 0; i < m_cardDealersList.Count; i++)
        // {
        //     m_cardDealersList[i].SetCardValue
        //         (m_deck.GetCardsFromDeck(m_cardDealersList[i].NumberOfCardsToDeal));
        // }
        // //for the middle table
        // m_middleTable.SetCardValue(m_deck.GetCardsFromDeck(m_middleTable.NumberOfCardsToDeal));
    }
}
