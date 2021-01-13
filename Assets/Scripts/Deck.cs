using System.Collections.Generic;
using System;
public class Deck 
{
    private readonly Random m_random = new Random();
    private List<Card> m_currentCardList = new List<Card>();
    private Queue<Card> m_currentCardQueue = new Queue<Card>();
    private int m_numberOfSuits = 0;
    private int m_numberOfCardsToDeal = 0;
    public Action OnFinishedShuffling;
    public void Build(int numberOfSuits = 4, int numberOfCardsPerSuit = 13)
    {
        m_numberOfSuits = numberOfSuits;
        m_numberOfCardsToDeal = numberOfCardsPerSuit;
        for(int i = 0; i < m_numberOfSuits; i++)
        {
            for(int j = 1; j <= m_numberOfCardsToDeal; j++)
            {
                m_currentCardList.Add(new Card((Suit)i, j));
            }
        }
    }
    public void Shuffle()
    {
        m_currentCardQueue.Clear();
        if(m_currentCardList.Count <= 0)
        {
            Build();
        }
        while(m_currentCardList.Count > 0)
        {
            int randIndex = m_random.Next(0,m_currentCardList.Count);
            m_currentCardQueue.Enqueue(m_currentCardList[randIndex]);
            m_currentCardList.RemoveAt(randIndex);
        }
        OnFinishedShuffling?.Invoke();
    }
    public Card GetCardFromDeck()
    {
        return (m_currentCardQueue.Count > 0) ? 
        m_currentCardQueue.Dequeue() : null;
    }
    public List<Card> GetCardsFromDeck(int cardCount)
    {
        List<Card> cardToReturn = new List<Card>();
        if(cardCount <= m_currentCardQueue.Count)
        {
            for(int i = 0; i < cardCount; i++)
            {
                Card card = m_currentCardQueue.Dequeue();
                cardToReturn.Add(card);
            }
        }
        else if(cardCount > m_currentCardQueue.Count)
        {
            for(int i = 0; i < m_currentCardQueue.Count; i++)
            {
                Card card = m_currentCardQueue.Dequeue();
                cardToReturn.Add(card);
            }
        }
        return cardToReturn;
    }
}
