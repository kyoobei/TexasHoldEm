using System.Collections.Generic;
using UnityEngine;
using System;
public class CardController : MonoBehaviour, IController
{
    public Action<List<string>,List<Hand>> OnAcquirePlayerHands;
    public Action<string> OnAcquireWinner;

    [SerializeField] private List<PlayerCardDeal> m_cardDealersList = new List<PlayerCardDeal>();
    [SerializeField] private CardDeal m_middleTable = null;
    private Deck m_deck = new Deck();
    private CardHand m_cardHand;
    private void Start()
    {
        CardHand cardHand = new CardHand
        (
            new CardCheckingFactory(),
            new CardBuilder()
        );
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
        //for the middle table
        m_middleTable.SetCardValue(m_deck.GetCardsFromDeck(m_middleTable.NumberOfCardsToDeal));
        //for players
        for(int i = 0; i < m_cardDealersList.Count; i++)
        {
            m_cardDealersList[i].SetCardValue
                (m_deck.GetCardsFromDeck(m_cardDealersList[i].NumberOfCardsToDeal));
            m_cardDealersList[i].CheckPlayerCard(m_middleTable.CurrentCards);
        }
        CheckForWinners();
    }
    private void CheckForWinners()
    {
        List<string> playerNames = new List<string>();
        List<Hand> playerHands = new List<Hand>();
        for(int i = 0; i < m_cardDealersList.Count; i++)
        {
            playerNames.Add(m_cardDealersList[i].PlayerName);
            playerHands.Add(m_cardDealersList[i].CurrentHand);
        }
        OnAcquirePlayerHands?.Invoke(playerNames, playerHands);
        
        if((int)m_cardDealersList[0].CurrentHand == 
            (int)m_cardDealersList[1].CurrentHand)
        {
            bool isCalled = false;
            for(int i = 0; i < m_cardDealersList[0].WinningCard.Count; i++)
            {
                if(m_cardDealersList[0].WinningCard[i].Value < 
                    m_cardDealersList[1].WinningCard[i].Value)
                {
                    isCalled = true;
                    OnAcquireWinner?.Invoke
                    (
                        string.Format($"{m_cardDealersList[1].PlayerName} wins!")
                    );
                    break;
                }
                else if(m_cardDealersList[0].WinningCard[i].Value > 
                    m_cardDealersList[1].WinningCard[i].Value)
                {
                    isCalled = true;
                    OnAcquireWinner?.Invoke
                    (
                        string.Format($"{m_cardDealersList[0].PlayerName} wins!")
                    );
                    break;
                }
                else if(m_cardDealersList[0].WinningCard[i].Value == 
                    m_cardDealersList[1].WinningCard[i].Value)
                {
                    continue;
                }
            }
            if(!isCalled)
            {
                OnAcquireWinner?.Invoke
                (
                    string.Format($"Draw!")
                );
            }
        }
        else if((int)m_cardDealersList[0].CurrentHand < 
            (int)m_cardDealersList[1].CurrentHand)
        {
            OnAcquireWinner?.Invoke
            (
                string.Format($"{m_cardDealersList[1].PlayerName} wins!")
            );
        }
        else
        {
            OnAcquireWinner?.Invoke
            (
                string.Format($"{m_cardDealersList[0].PlayerName} wins!")
            );
        }
    }
}
