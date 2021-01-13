using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CardController : MonoBehaviour, IController
{
    private const int TOTAL_CARDS = 9;
    //private Action OnGet
    [SerializeField] private Pooler m_cardPooler = null;
    [SerializeField] private List<Card> m_currentCardsAcquired;
    //[SerializeField] private List<CardDeal> m_
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
        m_currentCardsAcquired.Clear();
        m_currentCardsAcquired = new List<Card>(m_deck.GetCardsFromDeck(TOTAL_CARDS));
        //UpdatePooledCards();
    }
    // private void UpdatePooledCards()
    // {
    //     for(int i = 0; i < TOTAL_CARDS; i++)
    //     {
    //         GameObject cardObject = m_cardPooler.GetClone();
    //         CardBehaviour behaviour = cardObject.GetComponent<CardBehaviour>();
    //         behaviour.ResetBehaviour();
    //         behaviour.UpdateDisplay(m_currentCardsAcquired[i]);
    //     }
    // }
}
