using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CardHand
{    
    public List<Card> m_combineCards; // <-- public for now need to be private later
    public List<Card> winningCardList = new List<Card>(); // <-- will be used for comparison to other cards.. public for now
    private List<Card> m_originalCards; // <-- original hole cards or cards in the hands of players
    private List<Card> m_otherCards; // <-- community cards or cards in the middle table
    private CardBuilder m_cardBuilder;
    private CardCheckingFactory m_cardCheckingFactory;
    public CardHand(CardCheckingFactory cardCheckingFactory, CardBuilder cardBuilder)
    {
        m_cardBuilder = cardBuilder;
        m_cardCheckingFactory = cardCheckingFactory;
    }
    public Hand GetHandType(List<Card> cardAtHand, List<Card> cardAtTable)
    {
        m_combineCards = m_cardBuilder.Build(cardAtHand, cardAtTable);
        Debug.Log("my hand: " + m_cardCheckingFactory.GetCardHand(m_combineCards).ToString());
        // winningCardList.Clear();
        // m_combineCards = new List<Card>();
        // m_originalCards = new List<Card>(originalHand);
        // m_otherCards = new List<Card>(otherHand);
        // List<Card> cardPairsByValue = new List<Card>();

        // originalHand.ForEach(hand => m_combineCards.Add(hand));
        // otherHand.ForEach(hand => m_combineCards.Add(hand));

        #region VERSION 1 WORKING
        // m_originalCards.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));
        // m_combineCards.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));
        // m_originalCards.Reverse();
        // m_combineCards.Reverse();

        // if(IsStraight())
        // {
        //     if(IsFlush())
        //     {
        //         if(winningCardList[0].Value == (int)HighCard.Ace)
        //         {
        //             return Hand.ROYAL_FLUSH;
        //         }
        //         return Hand.STRAIGHT_FLUSH;
        //     }
        //     return Hand.STRAIGHT;
        // }
        // if(IsFlush())
        // {
        //     return Hand.FLUSH;
        // }

        // if(HasPairs(ref cardPairsByValue))
        // {
        //     if(HasFullHouse(ref cardPairsByValue))
        //     {
        //         winningCardList = new List<Card>(cardPairsByValue);
        //         return Hand.FULL_HOUSE;
        //     }
        //     if(HasOnePair(ref cardPairsByValue))
        //     {
        //         winningCardList = new List<Card>(cardPairsByValue);
        //         return Hand.ONE_PAIR;
        //     } 
        //     if(HasThreeOfAKind(ref cardPairsByValue))
        //     {
        //         if(HasFullHouse(ref cardPairsByValue))
        //         {
        //             winningCardList = new List<Card>(cardPairsByValue);
        //             return Hand.FULL_HOUSE;
        //         }
        //         winningCardList = new List<Card>(cardPairsByValue);
        //         return Hand.THREE_OF_A_KIND;
        //     }
        //     if(HasTwoPair(ref cardPairsByValue))
        //     {
        //         winningCardList = new List<Card>(cardPairsByValue);
        //         return Hand.TWO_PAIR;
        //     }
        //     if(HasFourOfAKind(ref cardPairsByValue))
        //     {
        //         winningCardList = new List<Card>(cardPairsByValue);
        //         return Hand.FOUR_OF_A_KIND;
        //     }
        // }
        #endregion
        
        return Hand.HIGH_CARD;
    }

    #region VERSION 1 METHODS
    // private bool IsStraight()
    // {
    //     List<Card> straightCard = new List<Card>();
    //     for(int i = 0; i < m_combineCards.Count; i++)
    //     {
    //         if(i != m_combineCards.Count - 1)
    //         {
    //             if((m_combineCards[i].Value - 1) == m_combineCards[i + 1].Value)
    //             {
    //                 if(straightCard.Count < 5)
    //                 {
    //                     if(!straightCard.Contains(m_combineCards[i]))
    //                     {
    //                         straightCard.Add(m_combineCards[i]);
    //                     }
    //                     if(!straightCard.Contains(m_combineCards[i + 1]))
    //                     {
    //                         straightCard.Add(m_combineCards[i + 1]); 
    //                     }                 
    //                 }
    //             }
    //             else if((m_combineCards[i].Value - 1) != m_combineCards[i + 1].Value)
    //             {
    //                 if(straightCard.Count < 5)
    //                 {
    //                     straightCard.Clear();
    //                 }
    //             }
    //         }
    //     }
    //     Debug.Log("straight card count: " + straightCard.Count());
    //     if(straightCard.Count >= 5)
    //     {
    //         if(straightCard.Contains(m_originalCards[0]) || 
    //             straightCard.Contains(m_originalCards[1]))
    //         {
    //             winningCardList = new List<Card>(straightCard);
    //             return true;
    //         }
    //         else
    //         {
    //             return false;    
    //         }
    //     }
    //     return false;
    // }
    // private bool IsFlush()
    // {
    //     if(winningCardList.Count > 0)
    //     {
    //         return winningCardList.GroupBy(card => card.Suit).Count() == 1;
    //     }
    //     else if(winningCardList.Count <= 0)
    //     {
    //         List<Card> temporaryFlushCards = new List<Card>();
    //         foreach(var groups in m_combineCards.GroupBy(card => card.Suit))
    //         {
    //             if(groups.Count() >= 5 && (groups.Contains(m_originalCards[0]) ||
    //                 groups.Contains(m_originalCards[1])))
    //             {
    //                 temporaryFlushCards = new List<Card>(groups);
    //             }
    //         }
    //         if(temporaryFlushCards.Count <= 0)
    //         {
    //             return false;
    //         }
    //         else if(temporaryFlushCards.Count > 0)
    //         {
    //             RemoveCards(ref temporaryFlushCards, 5);
    //             temporaryFlushCards.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));
    //             temporaryFlushCards.Reverse();
    //             winningCardList = new List<Card>(temporaryFlushCards);
    //             return true;
    //         }
    //     }
    //     return false;
    // }
    // private bool HasPairs(ref List<Card> cardPairs)
    // {
    //     var groupedCardsByValue = m_combineCards.GroupBy(cards => cards.Value);
    //     foreach(var group in groupedCardsByValue)
    //     {
    //         //check if a specific group of value has more than one
    //         if(group.Count() >= 2)
    //         {
    //             List<Card> currentCardGroup = group.ToList();
    //             cardPairs.AddRange(currentCardGroup);
    //         }
    //     }
    //     Debug.Log("card pairs: " +  cardPairs.Count());
    //     //remove pairs that are more than 5
    //     if(cardPairs.Count() > 5)
    //     {
    //         RemoveCards(ref cardPairs, 4);
    //     }
    //     else if(cardPairs.Count() == 7)
    //     {
    //         RemoveCards(ref cardPairs, 5);
    //     }
    //     if(cardPairs.Count() <= 0)
    //     {
    //         return false;
    //     }
    //     else
    //     {
    //         return true;
    //     }
    // }
    // private bool HasOnePair(ref List<Card> currentCardPairs)
    // {
    //     if(currentCardPairs.Count == 2)
    //     {
    //         FillRemainingCardArea(ref currentCardPairs);
    //         return true;
    //     }
    //     return false;
    // }
    // private bool HasTwoPair(ref List<Card> currentCardPairs)
    // {
    //     if(currentCardPairs.Count == 4)
    //     {
    //         if(currentCardPairs.GroupBy(card => card.Value).Count() == 2)
    //         {
    //             FillRemainingCardArea(ref currentCardPairs);
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     return false;
    // }
    // private bool HasThreeOfAKind(ref List<Card> currentCardPairs)
    // {
    //     if(currentCardPairs.Count == 3)
    //     {
    //         FillRemainingCardArea(ref currentCardPairs);
    //         return true;
    //     }
    //     return false;
    // }
    // private bool HasFourOfAKind(ref List<Card> currentCardPairs)
    // {
    //     if(currentCardPairs.Count == 4)
    //     {
    //         if(currentCardPairs.GroupBy(card => card.Value).Count() == 1)
    //         {
    //             FillRemainingCardArea(ref currentCardPairs);
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     return false;
    // }
    // private bool HasFullHouse(ref List<Card> currentCardPairs)
    // {
    //     if(currentCardPairs.Count == 5)
    //     {
    //         if(currentCardPairs.Contains(m_originalCards[0]) 
    //             || currentCardPairs.Contains(m_originalCards[1]))
    //         {
    //             List<Card> temporaryFullHouse = new List<Card>(currentCardPairs);
    //             var sortedGroup = temporaryFullHouse.GroupBy(card => card.Value)
    //                 .OrderByDescending(card => card.Count());
    //             currentCardPairs.Clear();
    //             foreach(var group in sortedGroup)
    //             {
    //                 List<Card> currentCardGroup = group.ToList();
    //                 currentCardPairs.AddRange(currentCardGroup);
    //             }
    //             return true;
    //         }
    //         else
    //         {
    //             Debug.Log("here?");
    //             RemoveCards(ref currentCardPairs, 3);
    //             return false;
    //         }
    //     }
    //     return false;
    // }
    // private void RemoveCards(ref List<Card> currentCards, int removeOffSet)
    // {
    //     List<Card> temporaryHolder = new List<Card>(currentCards);
    //     for(int i = temporaryHolder.Count - 1; i >= removeOffSet; i--)
    //     {
    //         currentCards.RemoveAt(i);
    //     }
    // }
    // private void FillRemainingCardArea(ref List<Card> currentCardPairs)
    // {
    //     List<Card> filler = new List<Card>();
    //     int remainingToFill = 5 - currentCardPairs.Count();
    //     //for three of kind card if the holder hand has a pair
    //     if(remainingToFill == 2)
    //     {
    //         if(!currentCardPairs.Contains(m_originalCards[0]) &&
    //             !currentCardPairs.Contains(m_originalCards[1])){
    //             if(m_originalCards[0].Value == m_originalCards[1].Value)
    //             {
    //                 for(int i = 0; i < 2; i++)
    //                 {
    //                     filler.Add(m_originalCards[i]);
    //                 }
    //             }
    //         }
    //     }
    //     for(int i = 0; i < m_combineCards.Count(); i++)
    //     { 
    //         if(filler.Count() < remainingToFill)
    //         {
    //             if(!currentCardPairs.Contains(m_combineCards[i]))
    //             {
    //                 filler.Add(m_combineCards[i]);
    //             }
    //         }
    //         if(filler.Count() == remainingToFill - 1)
    //         {
    //             if(!currentCardPairs.Contains(m_originalCards[0]))
    //             {
    //                 if(!currentCardPairs.Contains(m_originalCards[1]))
    //                 {
    //                     filler.Add(m_originalCards[0]);
    //                 }
    //                 else
    //                 {
    //                     filler.Add(m_combineCards[i]);
    //                 }
    //             }
    //         }
    //         if(filler.Count() >= remainingToFill)
    //         {
    //             break;
    //         }
    //     }
    //     filler.Sort((handA, handB) => handA.Value.CompareTo(handB.Value));
    //     filler.Reverse();
    //     currentCardPairs.AddRange(filler);
    // }
    #endregion
}