using System.Collections.Generic;
using UnityEngine;
public class CardHand
{    
     public List<Card> m_combineCards;
    private List<Card> winningCardList;
    private List<Card> m_originalCards;
    private List<Card> m_otherCards;
    // public void CurrentHand(List<Card> currentHand, List<Card> cardToCompare)
    // {
    //     for(int i = 0; i < currentHand.Count; i++)
    //     {
    //         //adding of each card at hand to the middle card area or something
    //         List<Card> temporaryCardList = new List<Card>();
    //         temporaryCardList.Add(currentHand[i]);
    //         cardToCompare.ForEach(cards => temporaryCardList.Add(cards));

            
    //     }
    // }
    public /*Hand*/void GetHandType(List<Card> originalHand, List<Card> otherHand)
    {
         m_combineCards = new List<Card>();
        m_originalCards = new List<Card>(originalHand);
        m_otherCards = new List<Card>(otherHand);
        originalHand.ForEach(hand => m_combineCards.Add(hand));
        otherHand.ForEach(hand => m_combineCards.Add(hand));
        m_combineCards.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));

        Debug.Log("Is straight: " + IsStraight());
        // if(IsStraight())
        // {
        //     return Hand.STRAIGHT;
        // }
        // // if(IsStraight())
        // // {
        // //     if(IsFlush())
        // //     {
        // //         return Hand.STRAIGHT_FLUSH;
        // //     }
        // //     return Hand.STRAIGHT;
        // // }
        // // if(IsFlush())
        // // {
        // //     return Hand.FLUSH;
        // // }

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
        List<Card> straightCard = new List<Card>();
        for(int i = 0; i < m_combineCards.Count; i++)
        {
            if(i != m_combineCards.Count - 1)
            {
                if((m_combineCards[i].Value + 1) == m_combineCards[i + 1].Value)
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
        return false;
    }
}