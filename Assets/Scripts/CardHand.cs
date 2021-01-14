using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CardHand
{    
    public List<Card> m_combineCards;
    private List<Card> winningCardList = new List<Card>();
    private List<Card> m_originalCards;
    private List<Card> m_otherCards;
    public /*Hand*/void GetHandType(List<Card> originalHand, List<Card> otherHand)
    {
        m_combineCards = new List<Card>();
        m_originalCards = new List<Card>(originalHand);
        m_otherCards = new List<Card>(otherHand);
        originalHand.ForEach(hand => m_combineCards.Add(hand));
        otherHand.ForEach(hand => m_combineCards.Add(hand));
        m_combineCards.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));

        if(IsStraight())
        {
            Debug.Log("is a straight");
            if(IsFlush())
            {
                Debug.Log("This is a straight flush");
                if(winningCardList[winningCardList.Count - 1].Value == (int)HighCard.Ace)
                {
                    Debug.Log("this is a royal flush");
                }
            }
        }
        if(IsFlush())
        {
            //this will display since its not yet a method that returns a Hand enum
            Debug.Log("this is just a flush");
        }
        // if(winningCardList.Count > 0)
        // {
        //     for(int i = 0; i < winningCardList.Count; i++)
        //     {
        //         Debug.Log("this is the winning: " + winningCardList[i].Suit + " " + winningCardList[i].Value);
        //     }
        // }
        // else
        // {
        //     Debug.Log("no winning list");
        // }
        
        // return Hand.HIGH_CARD;
    }
    public bool IsHandEqual(List<Card> cardA, List<Card> cardB)
    {
        return false;
    }
    public bool IsHandGreater(List<Card> cardA, List<Card> cardB)
    {
        return false;
    }
    private bool IsStraight()
    {
        //TODO: if there will be same number but different suit there will be a problem or bug
        List<Card> straightCard = new List<Card>();
        for(int i = 0; i < m_combineCards.Count; i++)
        {
            if(i != m_combineCards.Count - 1)
            {
                if((m_combineCards[i].Value + 1) == m_combineCards[i + 1].Value)
                {
                    if(straightCard.Count < 5)
                    {
                        if(!straightCard.Contains(m_combineCards[i]))
                        {
                            straightCard.Add(m_combineCards[i]);
                        }
                        if(!straightCard.Contains(m_combineCards[i + 1]))
                        {
                            straightCard.Add(m_combineCards[i + 1]); 
                        }                 
                    }
                }
                else if((m_combineCards[i].Value + 1) != m_combineCards[i + 1].Value)
                {
                    if(straightCard.Count < 5)
                    {
                        straightCard.Clear();
                    }
                }
            }
        }
        if(straightCard.Count >= 5)
        {
            if(straightCard.Contains(m_originalCards[0]) || 
                straightCard.Contains(m_originalCards[1]))
            {
                winningCardList = new List<Card>(straightCard);
                return true;
            }
        }
        return false;
    }
    private bool IsFlush()
    {
        if(winningCardList.Count > 0)
        {
            return winningCardList.GroupBy(card => card.Suit).Count() == 1;
        }
        else if(winningCardList.Count <= 0)
        {
            foreach(var groups in m_combineCards.GroupBy(card => card.Suit))
            {
                if(groups.Count() == 5 && (groups.Contains(m_originalCards[0]) ||
                    groups.Contains(m_originalCards[1])))
                {
                    winningCardList = new List<Card>(groups);
                    return true;
                }
            }
        }
        return false;
    }
}